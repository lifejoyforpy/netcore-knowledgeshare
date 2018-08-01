﻿using BoeSj.KnowledgeBase.Repository.Interface;
using BoeSj.KnowledgeBase.Repository.UnitOfWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Repository
{
    public class KnowlegdeRepository : IKnowlegdeRepository
    {
        public KnowledgeContext DbContext;

        public KnowlegdeRepository(KnowledgeContext dbContex)
        {
            DbContext = dbContex;
        }

        public T Get<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class
        {
            throw new NotImplementedException();
        }

        public bool Save<T>(T entity, bool IsCommit = true) where T : class
        {
            DbContext.Add(entity);
            if (IsCommit)
                return DbContext.SaveChanges() > 0;
            else
                return false;
        }
    }
}