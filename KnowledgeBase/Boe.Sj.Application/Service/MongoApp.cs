using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Repository.Interface;
using MongoDB.Bson;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Application.Service
{
    public class MongoApp : IMongoApp
    {
        private IMongoRepository _repository;

        public MongoApp(IMongoRepository repository)
        {
            _repository = repository;
        }

        public async Task<GridFSFileInfo> DownloadFileAsync(ObjectId id, GridFSDownloadOptions options = null)
        {
            return await _repository.DownloadFileAsync(id, options);
        }

        public async Task<TEntity> InsertAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = new CancellationToken())
        {
            return await _repository.InsertAsync<TEntity>(entity, cancellationToken);
        }

        public async Task<IEnumerable<TEntity>> QueryAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _repository.QueryAsync<TEntity>(predicate, cancellationToken);
        }

        public async Task<ObjectId> UploadFileAsync(string filePath, string fileName, GridFSUploadOptions uploadOptions = null)
        {
            return await _repository.UploadFileAsync(filePath, fileName, uploadOptions);
        }
    }
}