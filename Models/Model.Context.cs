﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Question_Paper_Management.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QPManagementEntities : DbContext
    {
        public QPManagementEntities()
            : base("name=QPManagementEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Approval> Approvals { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<QuestionPaper> QuestionPapers { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
