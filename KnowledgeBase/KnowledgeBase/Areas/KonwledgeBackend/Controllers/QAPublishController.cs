﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BoeSj.KnowledgeBase.Web.Areas.KonwledgeBackend.Controllers
{
    [Area("KonwledgeBackend")]
    public class QAPublishController : Controller
    {
        private readonly IQAPublishApp _IQAPublishApp;
        public QAPublishController(IQAPublishApp IQAPublishApp)
        {
            _IQAPublishApp = IQAPublishApp;
        }
        public IActionResult QADetail(string postGuid="")
        {
            
            ViewBag.tempGuid = postGuid==""? Guid.NewGuid().ToString(): postGuid;
            ViewBag.postGuid = postGuid;
            return View();
        }
        public IActionResult QAPage(string postGuid = "")
        {

            ViewBag.tempGuid = postGuid == "" ? Guid.NewGuid().ToString() : postGuid;
            ViewBag.postGuid = postGuid;
            return View();
        }
        public async Task<Response<object>> SaveQuestion(QAPublishInput input)
        {
            var postGuid = await _IQAPublishApp.QASave(input);
            return new Response<object> {Status=1, Msg="插入成功",Data =new { postGuid } };
        }
        public async Task<Response<object>> Publish(QAPublishInput input)
        {
            await _IQAPublishApp.Publish(input);
            return new Response<object> { Status = 1, Msg = "发布成功" };
        }
        public async Task<Response<object>> GetQAInfo(string postGuid)
        {
            var qaInfo = await _IQAPublishApp.GetQAInfo(postGuid);
            return new Response<object> { Status = 1, Msg = "", Data = qaInfo };
        }
        public async Task<Response<object>> Reply(QAPublishInput input)
        {
            await _IQAPublishApp.Reply(input);
            await _IQAPublishApp.SetState(input.Guid, 2);
            return new Response<object> { Status = 1, Msg = "", Data = "OK" };
        }
        public async Task<Response<object>> GetReplys(string postGuid)
        {
            var replys = await _IQAPublishApp.GetReplys(postGuid);
            return new Response<object> { Status = 1, Msg = "", Data = replys };
        }
        public async Task<Response<object>> Like(string replyid,int type)
        {
            await _IQAPublishApp.Like(replyid, type);
            return new Response<object> { Status = 1, Msg = "", Data = "OK" };
        }
        public async Task<Response<string>> SetBest(string replyid, string postguid)
        {
            return await _IQAPublishApp.SetBest(replyid, postguid);
        }
        public async Task<Response<object>> Back( string postguid, string reason)
        {
            await _IQAPublishApp.Back(postguid, reason);
            return new Response<object> { Status = 1, Msg = "", Data = "OK" };
        }


    }
}