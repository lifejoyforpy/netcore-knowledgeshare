using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using Aspose.Words;
using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Infrastructure.Api;
using BoeSj.KnowledgeBase.Infrastructure.FileHelper;
using BoeSj.KnowledgeBase.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;

namespace BoeSj.KnowledgeBase.Web.Areas.KonwledgeBackend.Controllers
{
    [Area("KonwledgeBackend")]
    public class KnowLedgePublishController : Controller
    {
        private IFieldShareApp _app;

        private IMongoRepository _mongoRepository;
        public KnowLedgePublishController(IFieldShareApp app, IMongoRepository mongoRepository)
        {
            _app = app;
            _mongoRepository = mongoRepository;
        }

        public IActionResult Publish(string id="")
        {
            ViewBag.Id = id;
            return View();
        }

        public async Task<IActionResult> UploadFiles(IFormFile file, string guid)
        {
            var filextention = Path.GetExtension(file.FileName);
            //if (filextention != ".xls" && filextention != ".xlsx")

            //验证文件格式
            using (var stream = file.OpenReadStream())
            {
                var objid = await _mongoRepository.UploadFileAsync(stream, file.FileName,new GridFSUploadOptions { Metadata = new BsonDocument
                { { "PostId", guid}} });
                if (objid!=null)
                {
                    ApiDataHelper.GetHtmlId(objid.ToString());
                    return Json(new Response<string> { Data = objid.ToString(), Msg = "上传成功", Status = 1 });
                }
                return Json(new Response<string> { Data = "", Msg = "上传失败", Status = 0 });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="postguid"></param>
        /// <param name="type">1:QA 2:Reply</param>
        /// <returns></returns>
        public async Task<IActionResult> QAUploadFiles(IFormFile file, string postguid,int type=1)
        {
            if (file == null || file.Length == 0)
                return Json(new Response<bool> { Data = false, Msg = "未选择文件", Status = 0 });

            var filextention = Path.GetExtension(file.FileName);
            //if (filextention != ".xls" && filextention != ".xlsx")
            //    return Json(new Response<bool> { Data = false, Msg = "请选择Excel文件", Status = 0 });

            //验证文件格式
            GridFSUploadOptions options = new GridFSUploadOptions
            {
                Metadata = new BsonDocument
               {
                   {
                       "PostId",postguid

                   },
                   {
                        "Type",type
                   }
               }
            };
            using (var stream = file.OpenReadStream())
            {
                var id = await _mongoRepository.UploadFileAsync(stream, file.FileName, options);
                ApiDataHelper.GetHtmlId(id.ToString());
                return Json(new Response<object> { Status = 1, Msg = "", Data = new {  id,Name= file.FileName } });
            }
        }

        public async Task DeleteFile(string fileId)
        {
            await _mongoRepository.DeleteFileAsync(fileId);
        }

        public async Task<IActionResult> DownloadFile(string fileId)
        {
            var memory = new MemoryStream();

            var result = await _mongoRepository.DownloadFileAsync(fileId);
            memory = result.Item2;
            memory.Position = 0;
            return File(memory, result.Item1.GetContentType(), Path.GetFileName(result.Item1));
        }

        public async Task<IActionResult> MyKnowledgePublish(KnowledegeInput input,string original="")
        {
            Response<string> response = new Response<string>();
            response = await _app.MyKnowledgeSharePublish(input,original);
            return new JsonResult(response);
        }

        public async Task<Response<List<BoeSj.KnowledgeBase.Application.DTO.Output.FileInfo>>> FileInfoLoad(string postId)
        {
            var response = new Response<List<BoeSj.KnowledgeBase.Application.DTO.Output.FileInfo>>();
            response = await _app.FileInfoLoad(postId);
            return response;
        }

        /// <summary>
        /// get content by file id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> GetKnownHtml(string id)
        {
            var fileInfo = await _mongoRepository.GetFileInfoAsync(id);
            if (FileHelper.FileCollections.ContainsKey(Path.GetExtension(fileInfo.Filename).ToLowerInvariant()))
            {
                switch (FileHelper.FileCollections[Path.GetExtension(fileInfo.Filename).ToLowerInvariant()])
                {
                    case 0://无须转且可直接预览
                        //return await DownloadFile(id);
                        return new FileContentResult(await GetByte(id), FileHelper.GetContentType(fileInfo.Filename));
                        //return Content(await GetText(id), FileHelper.GetContentType(fileInfo.Filename));
                    case 1://必须转
                        if (fileInfo.Metadata.Contains("HtmlId") && fileInfo.Metadata["HtmlId"]!="")
                        {
                            string text = await GetText(fileInfo.Metadata["HtmlId"].ToString(),"Htmls");
                            return Content(text, "text/html", Encoding.UTF8);
                        }

                        var htmlid = await ApiDataHelper.GetHtmlId(id);
                        var hz = 0;
                        while (htmlid == "" && hz <2)
                        {
                            htmlid = await ApiDataHelper.GetHtmlId(id);
                            hz++;
                        }
                        if (htmlid == "")//again
                        {
                            return Content("文件转换多次未成功，请查看文档及转换接口", "text/html", Encoding.UTF8);
                        }
                        var stext = await GetText(htmlid, "Htmls");
                        return Content(stext, "text/html", Encoding.UTF8);

                    default://todo
                        break;
                }

            }

            return Content("附件格式无法识别...", "text/html", Encoding.UTF8);
        }

        private async Task<string> GetText(string htmlid, string bucket="")
        {
            var memory = new MemoryStream();
            var result = await _mongoRepository.DownloadFileAsync(htmlid, bucket);
            memory = result.Item2;
            memory.Position = 0;
            StreamReader reader = new StreamReader(memory);
            return reader.ReadToEnd();
        }

        private async Task<byte[]> GetByte(string id)
        {
            var memory = new MemoryStream();
            var result = await _mongoRepository.DownloadFileAsync(id);
            memory = result.Item2;
            memory.Position = 0;
            //StreamReader reader = new StreamReader(memory);
            return memory.ToArray();
        }
    }
}