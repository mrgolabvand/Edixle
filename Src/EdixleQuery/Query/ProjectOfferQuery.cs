using _0_Framework.Application;
using AccountManagement.Infrastructure.EFCore;
using EdixleQuery.Contracts.ProjectOffer;
using Microsoft.EntityFrameworkCore;

namespace EdixleQuery.Query
{
    public class ProjectOfferQuery : IProjectOfferQuery
    {
        private readonly AccountContext _accountContext;

        public ProjectOfferQuery(AccountContext accountContext)
        {
            _accountContext = accountContext;
        }

        public async ValueTask<List<ProjectOfferQueryModel>> GetProjectOffersAsync(long projectId) =>
            await _accountContext.ProjectOffers.Where(v => v.ProjectId == projectId)
                .Include(v => v.PersonalPage)
                .Select(v => new ProjectOfferQueryModel
                {
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Description = v.Description,
                    EditorName = v.PersonalPage.FullName,
                    EditorPageId = v.EditorPageId,
                    EditorPageSlug = v.PersonalPage.Slug,
                    EditorPicture = v.PersonalPage.Picture,
                    Id = v.Id,
                    Price = v.Price.ToMoney(),
                    ProjectId = v.ProjectId,
                    Title = v.Title,
                    IsAccept = v.IsAccept,
                    AccountId = v.PersonalPage.AccountId
                }).AsNoTracking().ToListAsync();

        public async ValueTask<List<ProjectOfferQueryModel>> GetEditorProjectOffers(long editorPageId)
        {
            var offers = await _accountContext.ProjectOffers
                .Where(v => v.EditorPageId == editorPageId)
                .Include(v => v.Project)
                    .ThenInclude(v => v.EmployerPage)
                .Select(v => new ProjectOfferQueryModel
                {
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    Description = v.Description,
                    EditorName = v.PersonalPage.FullName,
                    EditorPageId = v.EditorPageId,
                    EditorPageSlug = v.PersonalPage.Slug,
                    EditorPicture = v.PersonalPage.Picture,
                    Id = v.Id,
                    Price = v.Price.ToMoney(),
                    ProjectId = v.ProjectId,
                    Title = v.Title,
                    IsAccept = v.IsAccept,
                    IsCancel = v.IsCancel,
                    ProjectBudget = ((double)int.Parse(v.Project.Budget)).ToMoney(),
                    ProjectDescription = v.Project.Description,
                    ProjectEmployerName = v.Project.EmployerPage.FullName,
                    ProjectEmployerPicture = v.Project.EmployerPage.Picture,
                    ProjectTitle = v.Project.Title,
                    ProjectIsOpened = v.Project.IsOpened,
                }).AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();

            foreach (var offer in offers)
            {
                if (!offer.IsAccept && !offer.IsCancel)
                {
                    offer.ProjectOfferStatus = "در انتظار";
                }
                if (offer.IsAccept)
                {
                    offer.ProjectOfferStatus = "پذیرفته شده";
                }
                if (offer.IsCancel)
                {
                    offer.ProjectOfferStatus = "رد شده";
                }

                offer.ProjectStatus = offer.ProjectIsOpened ? "باز" : "بسته";
            }

            return offers;
        }
    }
}
