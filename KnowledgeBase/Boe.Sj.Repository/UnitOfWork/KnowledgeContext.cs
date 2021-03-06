﻿using BoeSj.KnowledgeBase.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace BoeSj.KnowledgeBase.Repository.UnitOfWork
{
    public class KnowledgeContext : DbContext
    {
        public KnowledgeContext(DbContextOptions<KnowledgeContext> options)
       : base(options)
        {
        }

        public virtual DbSet<BiAuth_Role> BiAuth_Role { get; set; }
        public virtual DbSet<QuestionsPublish> QuestionsPublish { get; set; }

        public virtual DbSet<FileShare> FileShare { get; set; }

        public virtual DbSet<Category> Category { get; set; }
    }
}