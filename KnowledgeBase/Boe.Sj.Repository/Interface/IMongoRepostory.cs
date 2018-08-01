﻿using BoeSj.KnowledgeBase.Domain.Model.Mongo;
using BoeSj.KnowledgeBase.Infrastructure.Mongo;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Interface
{
    public interface IMongoRepository
    {
        #region query

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = new CancellationToken());

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(CancellationToken cancellationToken = new CancellationToken());

        Task<IMongoQueryable<TEntity>> QueryAsync<TEntity>();

        #endregion query

        #region Insert

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Insert<TEntity>(TEntity entity);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<TEntity> InsertAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        ///     插入文档
        /// </summary>
        /// <param name="entities"></param>
        int Insert<TEntity>(IEnumerable<TEntity> entities);

        /// <summary>
        ///     异步插入文档
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<int> InsertAsync<TEntity>(IEnumerable<TEntity> entities,
            CancellationToken cancellationToken = default(CancellationToken));

        #endregion Insert

        #region Update

        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        long Update<TEntity>(TEntity entity) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class, IMongoEntity<string>;

        Task<long> UpdateAsync<TEntity>(string Id, object entity,
CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IMongoEntity<string>;

        Task<long> UpdateIncAsync<TEntity>(string Id, object IncInfo,
    CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IMongoEntity<string>;
        Task<long> UpdateAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, object entity,
    CancellationToken cancellationToken = new CancellationToken()) where TEntity : class, IMongoEntity<string>;
        /// <summary>
        ///     更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <returns></returns>
        long Update<TEntity>(IEnumerable<TEntity> entitys) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     异步更新文档
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> UpdateAsync<TEntity>(IEnumerable<TEntity> entitys,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class, IMongoEntity<string>;

        #endregion Update

        #region Delete

        /// <summary>
        ///     根据主键ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        long Delete<TEntity>(string id) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     异步根据ID删除文档
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(string id,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <returns></returns>
        long Delete<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <returns></returns>
        long Delete<TEntity>(ISpecification<TEntity> specification) where TEntity : class, IMongoEntity<string>;

        /// <summary>
        ///     异步删除
        /// </summary>
        /// <param name="specification">组合查询条件</param>
        /// <param name="cancellationToken">异步取消凭据</param>
        /// <returns></returns>
        Task<long> DeleteAsync<TEntity>(ISpecification<TEntity> specification,
            CancellationToken cancellationToken = default(CancellationToken)) where TEntity : class, IMongoEntity<string>;

        #endregion Delete

        Task<ObjectId> UploadFileAsync(string filePath, string fileName, GridFSUploadOptions uploadOptions = null);

        Task<ObjectId> UploadFileAsync(Stream sourceStream, string fileName, GridFSUploadOptions uploadOptions = null);

        Task<GridFSFileInfo> DownloadFileAsync(ObjectId id, GridFSDownloadOptions options = null);

        Task<GridFSFileInfo> GetFileInfoAsync(string id);

        Task<List<GridFSFileInfo>> FindfileByPostId(string postId, bool flag = true);

        Task DeleteFileAsync(string id);
        Task DeleteFileByPostGuidAsync(string postguid);

        Task<Tuple<string, MemoryStream>> DownloadFileAsync(string id, string bucketName = "");

        Task<TEntity> InsertOrUpdateAsync<TEntity>(TEntity entity,
            CancellationToken cancellationToken = new CancellationToken()) where TEntity : BaseModel;

        Task<List<MrDocument>> GetTags<TEntity>();
    }
}