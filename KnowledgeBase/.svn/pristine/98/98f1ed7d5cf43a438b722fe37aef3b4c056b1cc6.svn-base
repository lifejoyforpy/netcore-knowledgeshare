using System.Data.Common;

namespace BoeSj.KnowledgeBase.Repository.Interface
{
    public interface IUnitOfWork
    {
        DbTransaction BeginTransaction();

        void CommitTransaction();

        void RollbackTransaction();
    }
}