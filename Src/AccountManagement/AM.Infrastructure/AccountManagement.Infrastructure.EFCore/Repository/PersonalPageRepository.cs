using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Account;
using AccountManagement.Application.Contracts.PersonalPage;
using AccountManagement.Domain.PersonalPageAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PersonalPageRepository : BaseRepository<long, PersonalPage>, IPersonalPageRepository
    {
        private readonly AccountContext _accountContext;

        public PersonalPageRepository(AccountContext accountContext) : base(accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<EditPersonalPage> GetDetailsAsync(long id) =>
            await _accountContext.PersonalPages.Select(v => new EditPersonalPage
            {
                Id = v.Id,
                AccountId = v.AccountId,
                About = v.About,
                Age = v.Age,
                FullName = v.FullName,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                Slug = v.Slug,
                IsActive = v.IsActive,
                IsConfirm = v.IsConfirm,
                PayDate = v.PayDate,
                Keywords = v.Keywords,
                MetaDescription = v.MetaDescription,
                MaxPay = v.MaxPay,
                MinPay = v.MinPay,
                Card = v.Card
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<PersonalPageViewModel>> SearchAsync(PersonalPageSearchModel searchModel)
        {
            var query = _accountContext.PersonalPages.Select(v => new PersonalPageViewModel
            {
                Id = v.Id,
                Age = v.Age,
                CreationDate = v.CreationDate.ToFarsi(),
                FullName = v.FullName,
                Picture = v.Picture,
                AccountId = v.AccountId,
                IsActive = v.IsActive,
                IsConfirm = v.IsConfirm,
            });

            if (searchModel != null)
            {
                if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                    query = query.Where(v => v.FullName.Contains(searchModel.FullName));
            }

            var result = await query.AsNoTracking().ToListAsync();

            foreach (var personalPageViewModel in result)
            {
                var account = await _accountContext.Accounts
                    .FirstOrDefaultAsync(v => v.Id == personalPageViewModel.AccountId);
                personalPageViewModel.UserName = account.UserName;
            }

            return result;
        }

        public async ValueTask<long> GetPersonalPageIdByAsync(long accountId)
        {
            var result = await _accountContext.PersonalPages.AsNoTracking().FirstOrDefaultAsync(v => v.AccountId == accountId);
            return result == null ? 0 : result.Id;
        }

        public async ValueTask<string> GetPersonalPageSlugAsync(long id)
        {
            var personalPage = await _accountContext.PersonalPages.Select(v => new { v.Id, v.Slug })
                .FirstOrDefaultAsync(v => v.Id == id);
            return personalPage.Slug;
        }


        public async ValueTask<long> GetPersonalPageIdByAsync(string slug)
        {
            var result = await _accountContext.PersonalPages.FirstOrDefaultAsync(v => v.Slug == slug);
            return result == null ? 0 : result.Id;
        }


        public async ValueTask<PersonalPageViewModel> GetPageInfoByAsync(long pageId) =>
             await _accountContext.PersonalPages.Select(v => new PersonalPageViewModel
             {
                 Id = v.Id,
                 FullName = v.FullName,
                 Slug = v.Slug,
                 Picture = v.Picture,
                 PictureAlt = v.PictureAlt,
                 PictureTitle = v.PictureTitle,
                 IsConfirm = v.IsConfirm,
                 CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                 Age = v.Age,
                 AccountId = v.AccountId,
                 About = v.About,
                 IsActive = v.IsActive,
                 MaxPay = v.MaxPay,
                 MinPay = v.MinPay,
                 PayDate = v.PayDate
             }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == pageId);
        public async ValueTask<PersonalPageViewModel> GetPageInfoWithPlanByAsync(long pageId)
        {
            var page = await _accountContext.PersonalPages.Select(v => new PersonalPageViewModel
            {
                Id = v.Id,
                FullName = v.FullName,
                Slug = v.Slug,
                Picture = v.Picture,
                PictureAlt = v.PictureAlt,
                PictureTitle = v.PictureTitle,
                IsConfirm = v.IsConfirm,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                Age = v.Age,
                AccountId = v.AccountId,
                About = v.About,
                IsActive = v.IsActive,
                MaxPay = v.MaxPay,
                MinPay = v.MinPay,
                PayDate = v.PayDate,
                PlanExpireDate = v.ExpireDate,
                PlanChatUploadSizeLimit = v.ChatUploadSizeLimit,
                PlanPortfolioUploadSizeLimit = v.PortfolioUploadSizeLimit,
                PlanVipProjectOfferCount = v.VipProjectOfferCount,
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == pageId);

            page.PlanPortfolioUploadSizeLimitMB = page.PlanPortfolioUploadSizeLimit / 1024 / 1024;
            page.PlanChatUploadSizeLimitGB = page.PlanChatUploadSizeLimit / 1024 / 1024 / 1024;
            var expireDays = (page.PlanExpireDate - DateTime.Now).Days;
            if (expireDays < 0)
            {
                expireDays = 0;
            }

            if (expireDays == 0)
            {
                page.PlanChatUploadSizeLimitGB = 2;
                page.PlanChatUploadSizeLimit = 2147483648;
                page.PlanPortfolioUploadSizeLimitMB = 200;
                page.PlanPortfolioUploadSizeLimit = 200 * 1024 * 1024;
                page.PlanVipProjectOfferCount = 0;
            }
            page.PlanExpireDays = expireDays;

            page.HasPlan = page.PlanExpireDays != 0;

            return page;
        }

        public async ValueTask<string> GetEditorPhoneNumberByEditorPageIdAsync(long pageId)
        {
            var editorPage = await _accountContext.PersonalPages.FirstOrDefaultAsync(v => v.Id == pageId);
            if (editorPage == null) return string.Empty;
            var accountId = editorPage.AccountId;
            var number = _accountContext.Accounts.FirstOrDefault(a => a.Id == accountId)?.PhoneNumber;
            return number == null ? string.Empty : number.ToString();
        }
    }
}
