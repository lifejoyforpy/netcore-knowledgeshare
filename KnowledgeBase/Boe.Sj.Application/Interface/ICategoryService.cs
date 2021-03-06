﻿using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Interface
{
    public interface ICategoryService
    {
        Task<Response<CategoryTree2>> GetCategories2();

        Task<List<CategoryTree>> GetCategoriesBy(string id, string flag);

        Task<Response<List<CategoryDto>>> GetCategoriesForSelect2();

        Task<Response<bool>> RemoveCategory(string id);

        Task<Response<string>> CreateCategory(string parentid, string name);

        Task<Response<bool>> RenameCategory(string id, string name);

        Task<Response<bool>> UpdateSettings(CategoryDto dto);
    }
}
