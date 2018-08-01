﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using Microsoft.AspNetCore.Mvc;

namespace BoeSj.KnowledgeBase.Web.Areas.KonwledgeBackend.Controllers
{
    [Area("KonwledgeBackend")]
    public class CategoryController : Controller
    {
        private ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult CategoryIndex()
        {
            return View();
        }

        public async Task<List<CategoryTree>> GetCategories(string id="", string flag="")
        {
            var response = await _categoryService.GetCategoriesBy(id, flag);
            return response;
        }

        public async Task<Response<CategoryTree2>> GetCategories2()
        {
            return await _categoryService.GetCategories2();
        }

        public async Task<Response<List<CategoryDto>>> GetCategoriesForSelect2()
        {
            return await _categoryService.GetCategoriesForSelect2();
        }

        public async Task<Response<bool>> RemoveCategory(string id)
        {
            var response = await _categoryService.RemoveCategory(id);
            return response;
        }

        public async Task<Response<string>> CreateCategory(string parentid, string name)
        {
            return await _categoryService.CreateCategory(parentid, name);
        }

        public async Task<Response<bool>> RenameCategory(string id, string name)
        {
            return await _categoryService.RenameCategory(id, name);
        }

        public async Task<Response<bool>> UpdateSettings(CategoryDto dto)
        {
            dto.CreatedBy = "";
            dto.CreatedByJobNo = "";
            dto.CreatedTime = DateTime.Now;
            return await _categoryService.UpdateSettings(dto);
        }
    }
}