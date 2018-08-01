﻿using BoeSj.KnowledgeBase.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BoeSj.KnowledgeBase.Web.Controllers
{
    public class QAController : Controller
    {
        private readonly IRepository<adax> _a;

        public QAController(IRepository<adax> a)
        {
            _a = a;
        }

        public IActionResult QADetail()
        {
            return View();
        }

        [Produces("application/json")]
        public object asa()
        {
            return new { flag = 1, data = new { aa = 1, bb = "dad" } };
        }
    }

    public class adax
    {
        public string id { get; set; }
    }
}