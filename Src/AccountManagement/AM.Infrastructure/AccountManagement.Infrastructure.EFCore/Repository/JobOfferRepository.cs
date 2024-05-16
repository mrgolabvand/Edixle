using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class JobOfferRepository : BaseRepository<long, JobOffer>, IJobOfferRepository
    {
        private readonly AccountContext _context;

        public JobOfferRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<EditJobOffer> GetDetailsAsync(long id) =>
            await _context.JobOffers.Select(v => new EditJobOffer
            {
                Id = v.Id,
                Description = v.Description,
                EmployerPageId = v.EmployerPageId,
                Price = v.Price,
                EditorPageId = v.EditorPageId,
                Title = v.Title
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<JobOfferViewModel>> SearchAsync(string title)
        {
            var query = _context.JobOffers
                .Include(v => v.EditorPage)
                .Include(v => v.EmployerPage)
                .Select(v => new JobOfferViewModel
                {
                    Id = v.Id,
                    Title = v.Title,
                    EditorPageId = v.EditorPageId,
                    Price = v.Price.ToMoney(),
                    EmployerPageId = v.EmployerPageId,
                    Description = v.Description,
                    IsAccept = v.IsAccept,
                    IsCancel = v.IsCancel,
                    DoublePrice = v.Price,
                    EditorName = v.EditorPage.FullName,
                    EmployerName = v.EmployerPage.FullName
                });

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(v => v.Title.Contains(title));

            return await query.AsNoTracking().ToListAsync();
        }

        public async ValueTask<List<JobOfferViewModel>> GetEditorJobOffersAsync(long editorPageId) =>
            await _context.JobOffers
                .Where(v => v.EditorPageId == editorPageId)
                .Include(v => v.EmployerPage)
                .Select(v => new JobOfferViewModel
                {
                    Id = v.Id,
                    Title = v.Title,
                    EditorPageId = v.EditorPageId,
                    Price = v.Price.ToMoney(),
                    EmployerPageId = v.EmployerPageId,
                    Description = v.Description,
                    IsAccept = v.IsAccept,
                    IsCancel = v.IsCancel,
                    DoublePrice = v.Price,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    EmployerPicture = v.EmployerPage.Picture,
                    EmployerName = v.EmployerPage.FullName,
                    EmployerAccountId = v.EmployerPage.AccountId
                }).AsNoTracking().ToListAsync();

        public async ValueTask<List<JobOfferViewModel>> GetEmployerJobOffersAsync(long employerPageId) =>
            await _context.JobOffers
                .Where(v => v.EmployerPageId == employerPageId)
                .Include(v => v.EditorPage)
                .Select(v => new JobOfferViewModel
                {
                    Id = v.Id,
                    Title = v.Title,
                    EditorPageId = v.EditorPageId,
                    Price = v.Price.ToMoney(),
                    EmployerPageId = v.EmployerPageId,
                    Description = v.Description,
                    IsAccept = v.IsAccept,
                    IsCancel = v.IsCancel,
                    DoublePrice = v.Price,
                    CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                    EditorName = v.EditorPage.FullName,
                    EditorPicture = v.EditorPage.Picture,
                    EditorSlug = v.EditorPage.Slug,
                    EditorAccountId = v.EditorPage.AccountId
                }).AsNoTracking().ToListAsync();

        public async ValueTask<JobOfferViewModel> GetOfferAsync(long id) =>
            await _context.JobOffers.Select(v => new JobOfferViewModel
            {
                Id = v.Id,
                Title = v.Title,
                EditorPageId = v.EditorPageId,
                Price = v.Price.ToMoney(),
                EmployerPageId = v.EmployerPageId,
                Description = v.Description,
                IsAccept = v.IsAccept,
                IsCancel = v.IsCancel,
                DoublePrice = v.Price,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
            }).AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
    }
}
