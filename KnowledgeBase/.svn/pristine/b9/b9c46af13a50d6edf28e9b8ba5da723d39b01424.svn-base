using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Repository.Interface;
using BoeSj.KnowledgeBase.Repository.UnitOfWork;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BoeSj.KnowledgeBase.Repository.Repository
{
    public class TestRepository : ITestRepository
    {
        public KnowledgeContext DbContext;

        public TestRepository(KnowledgeContext dbContext)
        {
            DbContext = dbContext;
        }

        public BiAuth_Role Get(Expression<Func<BiAuth_Role, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<BiAuth_Role> GetAsync(Expression<Func<BiAuth_Role, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public bool Save(BiAuth_Role entity, bool IsCommit = true)
        {
            DbContext.Add(entity);
            if (IsCommit)
                return DbContext.SaveChanges() > 0;
            else
                return false;
        }
    }
}