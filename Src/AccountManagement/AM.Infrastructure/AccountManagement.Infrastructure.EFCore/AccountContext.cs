using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.FeatureAgg;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.PortfolioBaseCategoryAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;
using AccountManagement.Domain.PortfolioDownloadAgg;
using AccountManagement.Domain.ProjectAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.SkillAgg;
using AccountManagement.Domain.TextSliderAgg;
using AccountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using AccountManagement.Domain.JobHistoryAgg;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;

namespace AccountManagement.Infrastructure.EFCore
{ 
    public class AccountContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
        public DbSet<PersonalPage> PersonalPages { get; set; }
        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<PortfolioCategory> PortfolioCategories { get; set; }
        public DbSet<PortfolioBaseCategory> PortfolioBaseCategories { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobHistory> JobHistories { get; set; }
        public DbSet<TextSlider> TextSliders { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<PortfolioDownload> PortfolioDownloads { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ProjectOffer> ProjectOffers { get; set; }
        public DbSet<EmployerPage> EmployerPages { get; set; }
        public DbSet<JobOffer> JobOffers { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<PortfolioAndCategoryLinked> PortfolioAndCategoryLinkeds { get; set; }
        public AccountContext(DbContextOptions<AccountContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(AccountMapping).Assembly;
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
