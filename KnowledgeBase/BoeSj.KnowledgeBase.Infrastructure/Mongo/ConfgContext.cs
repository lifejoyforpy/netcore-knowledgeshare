using Microsoft.Extensions.Options;

namespace BoeSj.KnowledgeBase.Infrastructure.Mongo
{
    public class ConfgContext<TEntity> where TEntity : class, new()
    {
        private readonly TEntity _data;

        public ConfgContext(IOptions<TEntity> options)
        {
            _data = options.Value;
        }

        public TEntity Get()
        {
            return _data;
        }
    }
}