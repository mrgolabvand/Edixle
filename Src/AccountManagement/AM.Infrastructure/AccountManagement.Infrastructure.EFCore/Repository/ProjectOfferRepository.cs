using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.ProjectOfferAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class ProjectOfferRepository : BaseRepository<long, ProjectOffer>, IProjectOfferRepository
    {
        private readonly AccountContext _context;

        public ProjectOfferRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<EditProjectOffer> GetDetailsAsync(long id) =>
            await _context.ProjectOffers.Select(v => new EditProjectOffer
            {
                Id = id,
                Description = v.Description,
                EditorPageId = v.EditorPageId,
                Price = v.Price,
                ProjectId = v.ProjectId,
                Title = v.Title
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<ProjectOfferViewModel>> SearchAsync(string title)
        {
            var query = _context.ProjectOffers.Select(v => new ProjectOfferViewModel
            {
                Id = v.Id,
                Title = v.Title,
                ProjectId = v.ProjectId,
                Price = v.Price.ToMoney(),
                EditorPageId = v.EditorPageId,
                Description = v.Description,
                IsAccept = v.IsAccept,
                IsCancel = v.IsCancel,
            });

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(v => v.Title.Contains(title));

            return await query.ToListAsync();
        }

        public async ValueTask<ProjectOfferViewModel> GetOfferAsync(long id) =>
            await _context.ProjectOffers.Select(v => new ProjectOfferViewModel
            {
                Id = v.Id,
                Title = v.Title,
                ProjectId = v.ProjectId,
                Price = v.Price.ToMoney(),
                EditorPageId = v.EditorPageId,
                Description = v.Description,
                IsAccept = v.IsAccept,
                IsCancel = v.IsCancel,
                DoublePrice = v.Price
            }).FirstOrDefaultAsync(v => v.Id == id);
    }
}
