﻿using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Domain.Model.Search;
using BoeSj.KnowledgeBase.Repository.Interface;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Service
{
    public class SearchApp : ISearchApp
    {
        protected ISearchRepository _search;
        private IMongoRepository _mongoRepository;
        private ICacheRepository _cacheRepository;

        public SearchApp(ISearchRepository search, IMongoRepository mongoRepository, ICacheRepository cacheRepository)
        {
            _mongoRepository = mongoRepository;
            _search = search;
            _cacheRepository = cacheRepository;
        }

        public async Task<Response<List<post>>> SearchPost(DTO.Input.SearchInput search)
        {
            Response<List<post>> response = new Response<List<post>>();
            try
            {
                // post /knowledgebase/post/_search query=search.search
                Func<SearchDescriptor<post>, ISearchRequest> searchRequest = s=>s.
                    Index("knowledgebase").
                    Type(typeof(post).Name.ToLower()).
                    Query(q=>q.QueryString(qs=>qs.Query(search.Query)));

                Tuple<long, List<post>> result = await _search.Search<post>(searchRequest);
                response.Data = result.Item2;
                response.PageIndex = search.PageIndex;
                response.PageSize = search.PageSize;
                response.TotalCount = (int)result.Item1;
            }

            catch (Exception ex)
            {
                response.Status = 0;
                response.Msg = ex.Message.ToString();

            }
            return response;
        }
        // by title or category or 
        //score 
        public async Task<Response<List<post>>> SearchPostbyField(DTO.Input.SearchInput search)
        {
            Response<List<post>> response = new Response<List<post>>();
            try
            {
              

                // post /knowledgebase/post/_search query=search.search
                Func<SearchDescriptor<post>, ISearchRequest> request = s => s.Index("knowledgebase").Type(typeof(post).Name.ToLower()).
                Size(search.PageSize).From((search.PageIndex-1)*search.PageSize).
                    Query(q => q.Match(m=>m.Field(p=>p.Locked).Query(search.Locked))&&
                    //Match(m => m
                    //            .Field(p => p.Category)
                    //            .Boost(1000)
                    //            .Query(search.Query)
                    //        ) || q
                           /* .*/
                           q.FunctionScore(fs => fs
                            .MaxBoost(50)
                            .Functions(ff => ff
                                .FieldValueFactor(fvf => fvf
                                    .Field(p => p.Views)
                                    .Factor(0.0001)
                                 )
                             ).
                    Query(query =>
                      query.MultiMatch(m =>
                         m.Fields(f => f.Fields(p => p.Content,p=>p.Title,p=>p.Tag)).
                         Query(search.Query)))));

                Tuple<long, List<post>> result = await _search.Search<post>(request);
                response.Data = result.Item2;
                response.PageIndex = search.PageIndex;
                response.PageSize = search.PageSize;
                response.TotalCount = (int)result.Item1;
            }

            catch (Exception ex)
            {
                response.Status = 0;
                response.Msg = ex.Message.ToString();

            }
            return response;

        }



        public async Task<Response<IEnumerable<SearchOutput>>> IndexSuggestion(DTO.Input.SearchInput search)
        {
            Response<IEnumerable<SearchOutput>> response = new Response<IEnumerable<SearchOutput>>();
            try
            {

                //new List<string>("".Split('.')) { "" };
                 List<string> sbudiler = new List<string>();
                Func<SearchDescriptor<post>, ISearchRequest> request = req => req.Index<post>().Query(q => q.Match(m => m.Field(fd => fd.Locked).Query(search.Locked))).
                    Source(sf => sf.Includes(f => f.Field(ff => ff.Tag).Field(ff => ff.Views))).
                        Suggest(su => su.Completion("tag-suggestions", c => c.Prefix(search.Query).Field(p => p.Tag)));
                var result = await _search.SearchSuggest(request);
                var suggestions = result.Suggest["tag-suggestions"].FirstOrDefault().
                   Options.Select(suggest => new SearchOutput
                   {
                       suggesttag = suggest.Text
                   });
                response.Data = suggestions;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Msg = ex.Message.ToString();

            }
            return response;
        }



        public async Task<Response<List<post>>> HotSuggest (DTO.Input.SearchInput search) {

            Response<List<post>> response = new Response<List<post>>();
            try {
                Func<SearchDescriptor<post>, ISearchRequest> request = s => s.Index("knowledgebase").Type(typeof(post).Name.ToLower()).
                Size(10).Query(q => q.Match(m => m.Field(f => f.Type).Query(search.Type)) && q.Match(m => m.Field(f => f.Locked).Query(search.Locked))
                && q.Match(m => m.Field(f => f.State).Query(search.State)))
                .Source(sou=> sou.Includes(f => f.Field(ff => ff.Type).Field(ff => ff.Locked).Field(ff => ff.State).Field(ff => ff.Title).Field(ff => ff.Views).Field(ff=>ff.CreateTime).Field(ff=>ff.Likes).Field(ff=>ff.Category).Field(ff=>ff.PostGuid))).
                    Sort(sort=>{
                        if (search.Sort == (int)SearchSort.Views)
                            return sort.Descending(p => p.Views);
                        if (search.Sort == (int)SearchSort.Recent)
                            return sort.Descending(p => p.CreateTime);
                        return sort.Descending(SortSpecialField.Score);

                    });
                Tuple<long, List<post>> result = await _search.Search<post>(request);
                response.Data = result.Item2;
            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Msg = ex.Message.ToString();
            }
            return response;
        }

        public async Task<Response<List<string>>> TopTags()
        {
            var response = new Response<List<string>>();

            try
            {
                var result = await _mongoRepository.GetTags<Post>();
                var tops = result.OrderByDescending(o => o.value).Take(50).Select(s => s.Id).ToList();

                if (!_cacheRepository.Exists("kb_tags") || _cacheRepository.Get("kb_tags") == null)
                {
                    _cacheRepository.Set("kb_tags", tops, new TimeSpan(4, 0, 0));
                }
                response.Data = _cacheRepository.Get<List<string>>("kb_tags");

            }
            catch (Exception ex)
            {
                response.Status = 0;
                response.Msg = ex.Message.ToString();
            }

            return response;
        }

    }
}
