﻿using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Interface
{
    public interface IKnowlegdeRepository
    {
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate) where T : class;

        T Get<T>(Expression<Func<T, bool>> predicate) where T : class;

        bool Save<T>(T entity, bool IsCommit = true) where T : class;
    }
}