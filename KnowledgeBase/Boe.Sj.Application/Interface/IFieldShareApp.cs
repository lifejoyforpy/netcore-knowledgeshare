﻿using BoeSj.KnowledgeBase.Application.DTO.Input;
using BoeSj.KnowledgeBase.Application.DTO.Output;
using BoeSj.KnowledgeBase.Application.DTO.OutPut;
using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using BoeSj.KnowledgeBase.Domain.Model.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Interface
{
    public interface IFieldShareApp
    {
        Task<Response<KnowledegeShareDetail>> GetKnowledgeShareDetail(string id);

        Task ExcutePost(FileShare TEntity, int id = 0);

        Response<List<KnowledegeShareList>> GetMyKnowledgeShareList(KnowledegeInput input);

        //知识分享发布

        Task<Response<string>> MyKnowledgeSharePublish(KnowledegeInput input, string original);

        Task<Response<List<FileInfo>>> FileInfoLoad(string postId);

        Task<Response<Post>> addView(KnowledegeInput input);
        Task<Response<Post>> addLike(KnowledegeInput input);
        Task<Response<bool>> DelMyKnowledgeShare(string id);
    }
}