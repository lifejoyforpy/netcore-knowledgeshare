using Core.Cache.Memory;
using Microsoft.Extensions.Options;
using System;

namespace Core.Cache
{
    public class CacheFactory : ICacheFactory
    {
        private readonly IOptions<CacheOption> _options;
        private readonly CacheOption _option;

        public CacheFactory(IOptions<CacheOption> options)
        {
            _options = options;
            _option = options.Value;
        }

        public ICache CreateCacher()
        {
            if (_option.UserRedis)
                return new RedisCache(_options);
            return new MemoryCache();
        }

        public ICacheProvider CreateProvider()
        {
            throw new NotImplementedException();
        }
    }
}