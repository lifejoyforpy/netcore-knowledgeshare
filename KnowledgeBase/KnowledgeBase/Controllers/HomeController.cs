using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using BoeSj.KnowledgeBase.Domain.Model.Search;
using BoeSj.KnowledgeBase.Repository.Interface;
using KnowledgeBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSOManagerLib;
using SSOManagerLib.UserService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace KnowledgeBase.Controllers
{

    [NoAthorize]
    public class HomeController : Controller
    {
        private SSOHelper _ssoHelper;
        IBackgroundTaskQueue _queue;
        ILogger<HomeController> _logger;
        private ISearchApp _search;
        private IUserServices _userServices;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sSOHelper"></param>
        /// <param name="queue"></param>
        /// <param name="logger"></param>
        /// <param name="search"></param>
        /// <param name="userServices">用户服务</param>
        public HomeController(SSOHelper sSOHelper, IBackgroundTaskQueue queue, ILogger<HomeController> logger, ISearchApp search, IUserServices userServices)
        {
            _ssoHelper = sSOHelper;
            _queue = queue;
            _logger = logger;
            _search = search;
            _userServices = userServices;
        }
      
        public IActionResult Index()
        {
            //_queue.QueueBackgroundWorkItem(async token =>
            //{
            //    var guid = Guid.NewGuid().ToString();

            //    for (int delayLoop = 0; delayLoop < 3; delayLoop++)
            //    {
            //        _logger.LogInformation(
            //            $"Queued Background Task {guid} is running. {delayLoop}/3");
            //        await Task.Delay(TimeSpan.FromSeconds(5), token);
            //    }

            //    _logger.LogInformation(
            //        $"Queued Background Task {guid} is complete. 3/3");
            //});

            return View();
        }

      
        public  IActionResult Search(SearchInput searchInput) {
            Response<string> response = new Response<string>();
            if (!string.IsNullOrEmpty(searchInput.Query))
            {
                response.Data = $"KnowledgeFrontend/KnowledegeShare/KnowledegeShareList?query={ searchInput.Query}";
               //RedirectToAction("KnowledegeShareList", "KnowledegeShare", new { area = "KnowledgeFrontend" ,query= searchInput.Query });
                return new JsonResult(response);
            }
            response.Data = "Home/Index";
            return  new JsonResult(response);
        }

        public async Task<Response<List<string>>> GetTopTags()
        {
            return await _search.TopTags();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Forbidden()
        {
            return View();
        }

        
        public async Task<IActionResult> Logout(string loginurl)
        {
            var token = HttpContext.Session.GetString("token");
            await _ssoHelper.RemoveToekn(token);
            HttpContext.Session.Clear();
            return Redirect(loginurl);
        }
        public async Task<IActionResult> getUserinfo(string userno)
        {
            var user = _userServices.GetUserInfo(userno);
            return new JsonResult(new { status = 1, data = user });
        }

        public IActionResult getDepartList()
        {

            var departlist = _userServices.GetDepartList();
            return new JsonResult(new { status = 1, data = departlist });
        }
        public IActionResult getFactoryList()
        {
            var factorylist = _userServices.GetFactoryList();
            return new JsonResult(new { status = 1, data = factorylist });
        }
    }
}