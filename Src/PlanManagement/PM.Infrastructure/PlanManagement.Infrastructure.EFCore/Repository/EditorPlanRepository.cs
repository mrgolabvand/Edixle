using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using PlanManagement.Application.Contracts.EditorPlan;
using PlanManagement.Domain.EditorPlanAgg;

namespace PlanManagement.Infrastructure.EFCore.Repository
{
    public class EditorPlanRepository : BaseRepository<long, EditorPlan>, IEditorPlanRepository
    {
        private readonly PlanContext _context;

        public EditorPlanRepository(PlanContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<List<EditorPlanViewModel>> GetListAsync() => 
            await _context.EditorPlans.Select(v => new EditorPlanViewModel
        {
            Id = v.Id,
            CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
            IsRemoved = v.IsRemoved,
            Description = v.Description,
            ChatUploadSizeLimit = v.ChatUploadSizeLimit,
            ExpireDays = v.ExpireDays,
            PortfolioUploadSizeLimit = v.PortfolioUploadSizeLimit,
            Price = v.Price.ToMoney(),
            Title = v.Title,
            VipProjectOfferCount = v.VipProjectOfferCount
        }).AsNoTracking().ToListAsync();

        public async ValueTask<EditEditorPlan> GetDetailsAsync(long id) => 
            await _context.EditorPlans.Select(v => new EditEditorPlan
        {
            Id = v.Id,
            Description = v.Description,
            ChatUploadSizeLimit = v.ChatUploadSizeLimit,
            ExpireDays = v.ExpireDays,
            PortfolioUploadSizeLimit = v.PortfolioUploadSizeLimit,
            Price = v.Price,
            Title = v.Title,
            VipProjectOfferCount = v.VipProjectOfferCount
        }).FirstOrDefaultAsync(v => v.Id == id);

      
        public async ValueTask<EditorPlanViewModel> GetPlanAsync(long id) =>
            await _context.EditorPlans.Select(v => new EditorPlanViewModel
        {
            Id = v.Id,
            CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
            IsRemoved = v.IsRemoved,
            Description = v.Description,
            ChatUploadSizeLimit = v.ChatUploadSizeLimit,
            ExpireDays = v.ExpireDays,
            PortfolioUploadSizeLimit = v.PortfolioUploadSizeLimit,
            Price = v.Price.ToMoney(),
            DoublePrice = v.Price,
            Title = v.Title,
            VipProjectOfferCount = v.VipProjectOfferCount
        }).FirstOrDefaultAsync(v => v.Id == id);
    }
}
