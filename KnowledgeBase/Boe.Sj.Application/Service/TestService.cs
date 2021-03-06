﻿using BoeSj.KnowledgeBase.Application.Interface;
using BoeSj.KnowledgeBase.Domain.Model;
using BoeSj.KnowledgeBase.Repository.Interface;

namespace BoeSj.KnowledgeBase.Application.Service
{
    public class TestService : ITestApp
    {
        private ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }

        public bool Save(BiAuth_Role ent)
        {
            return _testRepository.Save(ent);
        }
    }
}