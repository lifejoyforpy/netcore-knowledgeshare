using System;
using System.Linq.Expressions;

namespace BoeSj.KnowledgeBase.Infrastructure.Mongo
{
    public interface ISpecification<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; set; }
    }
}