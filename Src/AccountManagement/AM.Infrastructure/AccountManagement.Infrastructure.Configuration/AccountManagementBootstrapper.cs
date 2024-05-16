using _0_Framework.Infrastructure;
using AccountManagement.Application;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.EmployerPage;
using AccountManagement.Application.Contracts.Feature;
using AccountManagement.Application.Contracts.JobHistory;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Application.Contracts.Portfolio;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;
using AccountManagement.Application.Contracts.PortfolioDownload;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Application.Contracts.Role;
using AccountManagement.Application.Contracts.Skill;
using AccountManagement.Application.Contracts.TextSlider;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.FeatureAgg;
using AccountManagement.Domain.JobHistoryAgg;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using AccountManagement.Domain.PortfolioBaseCategoryAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;
using AccountManagement.Domain.PortfolioDownloadAgg;
using AccountManagement.Domain.ProjectAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Domain.SkillAgg;
using AccountManagement.Domain.TextSliderAgg;
using AccountManagement.Infrastructure.Configuration.Permissions;
using AccountManagement.Infrastructure.EFCore;
using AccountManagement.Infrastructure.EFCore.Repository;
using EdixleQuery.Contracts.Account;
using EdixleQuery.Contracts.Category;
using EdixleQuery.Contracts.EmployerPage;
using EdixleQuery.Contracts.Feature;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioBaseCategory;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Project;
using EdixleQuery.Contracts.ProjectOffer;
using EdixleQuery.Contracts.Skill;
using EdixleQuery.Contracts.TextSlider;
using EdixleQuery.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EdixleQuery.Contracts.Account;
using EdixleQuery.Contracts.Category;
using EdixleQuery.Contracts.EmployerPage;
using EdixleQuery.Contracts.Feature;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.PortfolioBaseCategory;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Project;
using EdixleQuery.Contracts.ProjectOffer;
using EdixleQuery.Contracts.Skill;
using EdixleQuery.Contracts.TextSlider;
using EdixleQuery.Query;

namespace AccountManagement.Infrastructure.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ISkillApplication, SkillApplication>();
            services.AddTransient<ISkillRepository, SkillRepository>();

            services.AddTransient<IJobHistoryApplication, JobHistoryApplication>();
            services.AddTransient<IJobHistoryRepository, JobHistoryRepository>();

            services.AddTransient<IEmployerPageApplication, EmployerPageApplication>();
            services.AddTransient<IEmployerPageRepository, EmployerPageRepository>();

            services.AddTransient<IProjectApplication, ProjectApplication>();
            services.AddTransient<IProjectRepository, ProjectRepository>();

            services.AddTransient<IProjectOfferApplication, ProjectOfferApplication>();
            services.AddTransient<IProjectOfferRepository, ProjectOfferRepository>();

            services.AddTransient<IJobOfferApplication, JobOfferApplication>();
            services.AddTransient<IJobOfferRepository, JobOfferRepository>();

            services.AddTransient<IPortfolioCategoryApplication, PortfolioCategoryApplication>();
            services.AddTransient<IPortfolioCategoryRepository, PortfolioCategoryRepository>();

            services.AddTransient<IPortfolioBaseCategoryApplication, PortfolioBaseCategoryApplication>();
            services.AddTransient<IPortfolioBaseCategoryRepository, PortfolioBaseCategoryRepository>();

            services.AddTransient<IPortfolioApplication, PortfolioApplication>();
            services.AddTransient<IPortfolioRepository, PortfolioRepository>();

            services.AddTransient<IPersonalPageApplication, PersonalPageApplication>();
            services.AddTransient<IPersonalPageRepository, PersonalPageRepository>();
            
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>(); 

            services.AddTransient<ITextSliderApplication, TextSliderApplication>();
            services.AddTransient<ITextSliderRepository, TextSliderRepository>();

            services.AddTransient<IFeatureApplication, FeatureApplication>();
            services.AddTransient<IFeatureRepository, FeatureRepository>();

            services.AddTransient<IPortfolioDownloadApplication, PortfolioDownloadApplication>();
            services.AddTransient<IPortfolioDownloadRepository, PortfolioDownloadRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddTransient<IPortfolioAndCategoryLinkedApplication, PortfolioAndCategoryLinkedApplication>();
            services.AddTransient<IPortfolioAndCategoryLinkedRepository, PortfolioAndCategoryLinkedRepository>();

            services.AddTransient<IPersonalPageQuery, PersonalPageQuery>();

            services.AddTransient<IPortfolioQuery, PortfolioQuery>();

            services.AddTransient<IPortfolioCategoryQuery, PortfolioCategoryQuery>();

            services.AddTransient<IEmployerPageQuery, EmployerPageQuery>();

            services.AddTransient<ISkillQuery, SkillQuery>();

            services.AddTransient<ITextSliderQuery, TextSliderQuery>();

            services.AddTransient<ICategoryQuery, CategoryQuery>();

            services.AddTransient<IFeatureQuery, FeatureQuery>();

            services.AddTransient<IProjectQuery, ProjectQuery>();

            services.AddTransient<IAccountQuery, AccountQuery>();

            services.AddTransient<IProjectOfferQuery, ProjectOfferQuery>();

            services.AddTransient<IPortfolioBaseCategoryQuery, PortfolioBaseCategoryQuery>();

            services.AddTransient<IPermissionExposer, AccountPermissionExposer>();

            services.AddDbContext<AccountContext>(v => v.UseSqlServer(connectionString));
        }
    }
}
