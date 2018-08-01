using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Repository.Interface;

namespace BoeSj.KnowledgeBase.Application.Service
{
    public class KnowledegeApp : IKnowledegeApp
    {
        private IKnowlegdeRepository _knowlegdeRepository;

        public KnowledegeApp(IKnowlegdeRepository knowlegdeRepository)
        {
            _knowlegdeRepository = knowlegdeRepository;
        }

        public bool Save<T>(T entity) where T : class
        {
            return _knowlegdeRepository.Save(entity);
        }
    }
}