using System;
using System.Collections.Generic;
using System.Text;

namespace BoeSj.KnowledgeBase.Repository.Interface
{
    public interface ICacheRepository
    {
        bool Exists(string key);

        bool Set(string key, object value, TimeSpan expiresSliding, TimeSpan expiressAbsoulte);

        bool Set(string key, object value, TimeSpan expiresIn, bool isSliding = false);

        void Remove(string key);

        void RemoveAll(IEnumerable<string> keys);

        T Get<T>(string key) where T : class;

        object Get(string key);

        IDictionary<string, object> GetAll(IEnumerable<string> keys);
    }
}
