using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.DTO.OutPut;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Web.Areas.KonwledgeBackend.Controllers
{
    [Area("KonwledgeBackend")]
    public class BackendManageController : Controller
    {
        private IFieldShareApp _app;

        public BackendManageController(IFieldShareApp app)
        {
            _app = app;
        }

        //详情

        public IActionResult Details(string Id)
        {
            //Id = "a9f5e372-eb48-4f26-b1f8-bdae87520822";
            ViewData["Id"] = Id;
            return View();
        }

        //[Produces("application/json")]
        public async Task<IActionResult> GetKnowledgeShareDetail(string Id)
        {
            Response<KnowledegeShareDetail> response = new Response<KnowledegeShareDetail>();

            response = await _app.GetKnowledgeShareDetail(Id);

            return new JsonResult(response);
        }
        //获取附件
        public async Task<IActionResult> GetAtthchement(string id)
        {

          Response<List<FileInfo>> response= await _app.FileInfoLoad(id);
          return new JsonResult(response);
        }
        public async Task<IActionResult> Publish(string content)
        {
            //
            //try
            //{
            FileShare fs = new FileShare
            {
                Guid = Guid.NewGuid().ToString(),
                JobNum = "admin",
                CreateTime = DateTime.Now,
                Author = "admin",
                Title = "SQL规范"
            };
            //    Post post = new Post
            //    {
            //        Title = "知识分享",
            //        Tag = "mes ,it",
            //        Category = "mes",
            //        Content = content,
            //        CreateTime = DateTime.Now,
            //        PostGuid = fs.Guid,
            //        Type = 0
            //    };
            //    var result = _mongo.InsertAsync<Post>(post);
            //    GridFSUploadOptions options = new GridFSUploadOptions
            //    {
            //        Metadata = new BsonDocument
            //   {
            //       {
            //           "PostId",fs.Guid
            //       }
            //   }
            //    };
            //    var id = await _mongo.UploadFileAsync(@"E:\KnowledgeBase\KnowledgeBase\wwwroot\file\mes.pptx", "test.pptx", options);
            await _app.ExcutePost(fs);
            //}
            //catch (Exception ex)
            //{
            //}
            return new JsonResult(null);
        }

        //列表

        public IActionResult List()
        {
            return View();
        }

        public IActionResult GetMyKnowledgeShareList(KnowledegeInput input)
        {
            Response<List<KnowledegeShareList>> response = new Response<List<KnowledegeShareList>>();
            response = _app.GetMyKnowledgeShareList(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> DelMyKnowledgeShare(string id) {

            var response = await _app.DelMyKnowledgeShare(id);
            return new JsonResult(response);
        }


    }
}