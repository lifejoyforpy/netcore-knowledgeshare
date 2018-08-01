﻿using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.DTO.OutPut;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Domain.Model.Search;
using Microsoft.AspNetCore.Mvc;
using SSOManagerLib;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Web.Areas.KnowledgeFrontend.Controllers
{

    [NoAthorize]
    [Area("KnowledgeFrontend")]
    public class KnowledegeShareController : Controller
    {
        ISearchApp _searchApp;
        IFieldShareApp _fieldShareApp;
        public KnowledegeShareController(ISearchApp searchApp, IFieldShareApp fieldShareApp) {

            _searchApp = searchApp;
            _fieldShareApp = fieldShareApp;
        }
        public IActionResult Details()
        {
            return View();
        }

        //列表页

        public IActionResult KnowledegeShareList(string query)
        {
            //Response<List<post>> response = new Response<List<post>>();
            //response=await _searchApp.SearchPostbyField(new SearchInput { Query = query });
            ViewBag.data = query;
            return View();
        }

        public async Task<IActionResult> Search(SearchInput input)
        {

            Response<List<post>> response = new Response<List<post>>();
            response = await _searchApp.SearchPostbyField(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> Suggest(SearchInput input)
        {

            Response<IEnumerable<SearchOutput>> response = new Response<IEnumerable<SearchOutput>>();
            response = await _searchApp.IndexSuggestion(input);
            return new JsonResult(response);

        }

        // hot suggest
        public async Task<IActionResult> HotSuggest(SearchInput input)
        {

            Response<List<post>> response = new Response<List<post>>();
            response = await _searchApp.HotSuggest(input);
            return new JsonResult(response);

        }

        public  IActionResult  KnowledegeShareDetail(string id){

            //Response<KnowledegeShareDetail> response = await _fieldShareApp.GetKnowledgeShareDetail(id);
            ViewData["id"] = id;
            return View();
        }
        public async Task<IActionResult> getDetails(string id)
        {

            Response<KnowledegeShareDetail> response = await _fieldShareApp.GetKnowledgeShareDetail(id);
            return new JsonResult(response);
        }

        ////浏览数增加
        //public async Task<IActionResult> updatePost(KnowledegeInput input) {
          
        //    Response<Post> response=await _fieldShareApp.updatePost(input);
        //    return new JsonResult(response);
        //}

        public async Task<IActionResult> addLike(KnowledegeInput input)
        {

            Response<Post> response = await _fieldShareApp.addLike(input);
            return new JsonResult(response);
        }

        public async Task<IActionResult> addView(KnowledegeInput input)
        {

            Response<Post> response = await _fieldShareApp.addView(input);
            return new JsonResult(response);
        }
        //知识问答
        public IActionResult KnowledgeQADetail(string id) {

            ViewData["id"] = id;
            return View();
        }
        //用户前五文章
        public IActionResult UserHotPost() {


            return new JsonResult("");
        }

        public IActionResult ReadMore(int type)
        {
            ViewBag.Type = type;
            return View();
        }

    }
}