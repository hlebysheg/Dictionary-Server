﻿using Microsoft.EntityFrameworkCore;

namespace WordBook.Models
{
    public class ApplicationDbContext : DbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<StudentLogin> StudentLogin { get; set; }
        public DbSet<Dictionary> Dictionary { get; set; }
        public DbSet<Letter> Letters { get; set; }
        public DbSet<RefreshToken>  RefreshTokens { get; set; }
        public DbSet<Test> Tests { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<TestToStudent> TestToStudents { get; set; }
        public DbSet<StudentInfo> StudentInfo { get; set; }
    }
}
