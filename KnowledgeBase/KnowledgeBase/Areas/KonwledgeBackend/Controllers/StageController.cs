﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace BoeSj.KnowledgeBase.Web.Areas.KonwledgeBackend.Controllers
{
    [Area("KonwledgeBackend")]
    public class StageController : Controller
    {
        private readonly IQAPublishApp _IQAPublishApp;
        public StageController(IQAPublishApp IQAPublishApp)
        {
            _IQAPublishApp = IQAPublishApp;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult QA()
        {
            return View();
        }
        public async Task<Response<object>> GetMyQAList(int pageIndex, int pageSize = 5)
        {
            var qalist = await _IQAPublishApp.GetMyQAList("xxx", pageIndex, pageSize);
            return new Response<object> { Status = 1, Msg = "", Data = qalist };
        }
        
        public async Task<Response<object>> GetMyDraftList(int pageIndex, int pageSize=5)
        {
            var qalist = await _IQAPublishApp.GetMyDraftList("xxx", pageIndex, pageSize);
            return new Response<object> { Status = 1, Msg = "", Data = qalist };
        }
        public async Task<Response<object>> GetMyNeedReplys(int pageIndex, int pageSize = 5)
        {

            var qalist = await _IQAPublishApp.GetReplyQAList("部门二", pageIndex, pageSize);
            return new Response<object> { Status = 1, Msg = "", Data = qalist };
        }
        public async Task<Response<object>> DeleteQA(string guid)
        {
            _IQAPublishApp.Delete(guid);
            return new Response<object> { Status = 1, Msg = "", Data = "OK" };
        }
        public async Task<Response<object>> SearchByCategory(int type, string category, int pageIndex=1, int pageSize=10)
        {

            var list = await _IQAPublishApp.SearchByCategory(type, category, pageIndex, pageSize);
            return new Response<object> { Status = 1, Msg = "", Data = list };
        }
        public async Task<object> SearchByCategory1(int type, string category, int pageIndex = 1, int pageSize = 10)
        {

            var list = await _IQAPublishApp.SearchByCategory(type, category, pageIndex, pageSize);
            return list;
        }
    }
}