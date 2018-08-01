﻿using BoeSj.KnowledgeBase.Domain.Model;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Interface
{
    public interface ITestRepository
    {
        Task<BiAuth_Role> GetAsync(Expression<Func<BiAuth_Role, bool>> predicate);

        BiAuth_Role Get(Expression<Func<BiAuth_Role, bool>> predicate);

        bool Save(BiAuth_Role entity, bool IsCommit = true);
    }
}