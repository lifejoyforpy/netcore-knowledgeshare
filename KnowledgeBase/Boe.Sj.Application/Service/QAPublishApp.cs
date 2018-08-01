using AutoMapper;
using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Domain.Model.Search;
using BoeSj.KnowledgeBase.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Service
{
    public class QAPublishApp : IQAPublishApp
    {
        private readonly IRepository<QuestionsPublish> _qpRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly IMongoRepository _mongo;

        public QAPublishApp(IRepository<QuestionsPublish> qpRepository, IRepository<Category> categoryRepository, IMapper mapper, IMongoRepository mongo)
        {
            _qpRepository = qpRepository;
            _mapper = mapper;
            _mongo = mongo;
            _categoryRepository = categoryRepository;
        }

        public async Task<QAInfoOutput> GetQAInfo(string guid)
        {
            var entity = _qpRepository.Get(o => o.Guid == guid).First();
            var mongoContent =  _mongo.QueryAsync<Post>(o => o.PostGuid == guid && o.Type == 1).Result.ToList();
            var files = await _mongo.FindfileByPostId(guid);
            var qaInfo = _mapper.Map<QAInfoOutput>(entity);
            qaInfo.Content = mongoContent.Count()>0 ?mongoContent.First().Content :"";
            qaInfo.Files = files.Select(o =>
            {
                return new FileInfo
                {
                    FileId = o.Id.ToString(),
                    FileName = o.Filename
                };
            }).ToList();
            if(qaInfo.State==4)
            {
                qaInfo.BackInfo = _mongo.QueryAsync<Log>(o => o.PostGuid == guid && o.Type == 2).
                    Result.OrderBy(o=>o.CreateTime).
                    Select(a=>new BackInfo { Author = a.Author, Content=a.Content, Department=a.Department, JobNum=a.JobNum })
                    .First();
            }
            return qaInfo;
        }

        /// <summary>
        /// QA问题发布
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Publish(QAPublishInput input)
        {
            var entity = _qpRepository.Get(o => o.Guid == input.Guid).First();
            entity.QuestionCategory = input.QuestionCategory;
            entity.BelongDepartment = input.BelongDepartment;
            entity.PostTime = DateTime.Now;
            await _qpRepository.UpdateAsync(entity);
            await _mongo.UpdateAsync<Post>(o => o.PostGuid == input.Guid, new { Category = input.QuestionCategory });
            await this.SetState(input.Guid, 1);
            _qpRepository.SaveChanges();
        }

        /// <summary>
        /// 保存问题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<string> QASave(QAPublishInput input)
        {
            var postGuid = "";
            if (string.IsNullOrEmpty(input.Guid))
            {
                var entity = new QuestionsPublish();
                entity.Title = input.Title;
                entity.CreateTime = DateTime.Now;
                entity.State = 0;
                entity.Guid = input.TempGuid;
                entity.JobNum = "xxx";
                entity.Author = "xxxname";
                postGuid = entity.Guid;
                await _qpRepository.InsertAsync(entity);
                Post post = new Post
                {
                    Tag=new List<string> { "问答"},
                    Title = entity.Title,
                    Content = input.Content,
                    CreateTime = DateTime.Now,
                    PostGuid = postGuid,
                    Locked =2,
                    Type = 1
                };
                var result = _mongo.InsertAsync<Post>(post);
            }
            else//修改
            {
                var entity = _qpRepository.Get(o => o.Guid == input.Guid).First();
                postGuid = entity.Guid;
                entity.Title = input.Title;
                await _qpRepository.UpdateAsync(entity);
                var mongoContent = _mongo.QueryAsync<Post>(o => o.PostGuid == postGuid && o.Type == 1).Result.ToList();
                if(mongoContent.Count()>0)
                {
                    mongoContent.First().Content = input.Content;
                    await _mongo.UpdateAsync<Post>(mongoContent.First());
                }
                else
                {
                    Post post = new Post
                    {
                        Tag = new List<string> { "问答" },
                        Title = entity.Title,
                        Content = input.Content,
                        CreateTime = DateTime.Now,
                        PostGuid = postGuid,
                        Locked = 2,
                        Type = 1
                    };
                    var result = _mongo.InsertAsync<Post>(post);
                }
            }
            _qpRepository.SaveChanges();
            return postGuid;
        }

        /// <summary>
        /// QA问题回复
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task Reply(QAPublishInput input)
        {
            Reply reply = new Reply
            {
                Content = input.Content,
                CreateTime = DateTime.Now,
                PostGuid = input.Guid,
                JobNum = "10038243",
                Author = "杨帆",
                Attachment= input.Attachment
            };
            var result = await _mongo.InsertAsync<Reply>(reply);
            await this.SetState(input.Guid, 2,o=>o.State<3);
            _qpRepository.SaveChanges();
        }

        public async Task<List<Reply>> GetReplys(string guid)
        {
            var mongoReplys = await _mongo.QueryAsync<Reply>();
            return mongoReplys.Where(o => o.PostGuid == guid && o.DataFlag == 1).OrderByDescending(o => o.IsBest).ThenBy(o => o.CreateTime).ToList();
        }

        public async Task Like(string guid, int type)
        {
            if (type == 1)
            {
                await _mongo.UpdateIncAsync<Reply>(guid, new { Likes = 1 });
            }
            else
            {
                await _mongo.UpdateIncAsync<Reply>(guid, new { Likes = -1 });
            }
            //throw new NotImplementedException();
        }

        public async Task<Response<string>> SetBest(string guid, string postguid)
        {
            if (_mongo.QueryAsync<Reply>(o => o.PostGuid == postguid && o.IsBest == 1 && o.DataFlag == 1).Result.Count() >= 1)
            {
                return new Response<string> { Status = 1, Msg = "", Data = "不能设置多个最佳回答" };
            }
            await _mongo.UpdateAsync<Reply>(guid, new { IsBest = 1 });
            await this.SetState(postguid, 3);
            return new Response<string> { Status = 1, Msg = "", Data = "恭喜设置成功" };
        }

        public async Task SetState(string guid, int state, Func<QuestionsPublish, bool> func =null)
        {
            var entity = _qpRepository.Get(o => o.Guid == guid).First();
            
            if(func==null || func(entity))
            {
                entity.State = state;
                await _qpRepository.UpdateAsync(entity);
                await _mongo.UpdateAsync<Post>(o => o.PostGuid== guid, new { State = state });
            }
            _qpRepository.SaveChanges();
        }

        public async Task<SearchResult<QAInfoOutput>> GetMyQAList(string jobNum, int pageIndex, int pageSize)
        {
            var count = _qpRepository.Get(o => o.JobNum == jobNum && o.State >= 1 && o.Flag == 1).Count();
            var entitys = await _qpRepository.Get(o => o.JobNum== jobNum && o.State>=1 && o.Flag==1).Skip((pageIndex-1)* pageSize).Take(pageSize).ToAsyncEnumerable().ToList();
            var qaInfos = _mapper.Map<List<QAInfoOutput>>(entitys);
            return new SearchResult<QAInfoOutput> { Results= qaInfos,Page= pageIndex , Total= count };
        }
        public async Task<SearchResult<QAInfoOutput>> GetMyDraftList(string jobNum,int pageIndex, int pageSize)
        {
            var count = _qpRepository.Get(o => o.JobNum == jobNum && o.State == 0 && o.Flag == 1).Count();
            var entitys = await _qpRepository.Get(o => o.JobNum == jobNum && o.State ==0 && o.Flag == 1).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToAsyncEnumerable().ToList();
            var qaInfos = _mapper.Map<List<QAInfoOutput>>(entitys);
            return new SearchResult<QAInfoOutput> { Results = qaInfos, Page = pageIndex, Total = count };
        }
        public async Task<SearchResult<QAInfoOutput>> GetReplyQAList(string department, int pageIndex, int pageSize)
        {
            var categroys =_categoryRepository.Get(o => o.Departments == department).Select(o=>o.Guid).ToList();
            var count = _qpRepository.Get(o => o.BelongDepartment == department && o.State >= 1 && o.Flag == 1).Count();
            var entitys = await _qpRepository.Get(o => categroys.Contains(o.QuestionCategory) && o.State>=1 && o.Flag == 1).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToAsyncEnumerable().ToList();
            var qaInfos = _mapper.Map<List<QAInfoOutput>>(entitys);
            return new SearchResult<QAInfoOutput> { Results = qaInfos, Page = pageIndex, Total = count };
        }

        public async Task<SearchResult<SearchOutput>> SearchByCategory(int type,string category, int pageIndex, int pageSize)
        {
            int state = type == 0 ? 1 : 3;
            var count = _mongo.QueryAsync<Post>(o => o.Type == type && o.Locked == 2 && o.Category == category).Result.Count();
            var query = await _mongo.QueryAsync<Post>();
            var reslut = query.Where(o => o.Type == type && o.Locked == 2 && o.Category == category &&o.State== state).Skip((pageIndex - 1) * pageSize).Take(pageSize)
                .ToList()
                .Select(a => new SearchOutput
                {
                    PostGuid =a.PostGuid,
                    CreatTime = a.CreateTime,
                    Likes = a.Likes,
                    suggesttag = string.Join(',', a.Tag),
                    Title = a.Title,
                    Views = a.Views
                }).ToList();
            return new SearchResult<SearchOutput> { Results = reslut, Page = pageIndex, Total = count };
        }

        public async Task Delete(string guid)
        {
            var qa = _qpRepository.Get(o => o.Guid == guid).First();
            _qpRepository.Delete(qa);
            await _mongo.DeleteAsync<Reply>(o=>o.PostGuid == guid);
            await _mongo.DeleteFileByPostGuidAsync(guid);
            _qpRepository.SaveChanges();
        }
        public async Task Back(string guid,string reason)
        {
            await this.SetState(guid, 4);
            Log log = new Log
            {
                Author = "xxx",
                JobNum ="xxx",               
                Content = reason,
                CreateTime = DateTime.Now,
                PostGuid = guid,
                Type = 2
            };
            var result = _mongo.InsertAsync(log);
        }
    }
}