using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Interface
{
    public interface IMongoApp
    {
        Task<GridFSFileInfo> DownloadFileAsync(ObjectId id, GridFSDownloadOptions options = null);

        Task<ObjectId> UploadFileAsync(string filePath, string fileName, GridFSUploadOptions uploadOptions = null);

        Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new CancellationToken());

        Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = new CancellationToken());
    }
}