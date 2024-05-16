using System;
using System.Threading.Tasks;
using AccountManagement.Domain.PersonalPageAgg;
using PlanManagement.Application.Contracts.EditorPlanOrder;
using PlanManagement.Domain.EditorPlanAgg;
using PlanManagement.Domain.EditorPlanOrderAgg;

namespace PlanManagement.Application
{
    public class EditorPlanOrderApplication : IEditorPlanOrderApplication
    {
        private readonly IEditorPlanRepository _editorPlanRepository;
        private readonly IPersonalPageRepository _personalPageRepository;
        private readonly IEditorPlanOrderRepository _editorPlanOrderRepository;

        public EditorPlanOrderApplication(IEditorPlanOrderRepository editorPlanOrderRepository, IPersonalPageRepository personalPageRepository, IEditorPlanRepository editorPlanRepository)
        {
            _editorPlanOrderRepository = editorPlanOrderRepository;
            _personalPageRepository = personalPageRepository;
            _editorPlanRepository = editorPlanRepository;
        }

        public async ValueTask<long> PlaceOrderAsync(long pageId, long planId, double payAmount)
        {
            var order = new EditorPlanOrder(pageId, planId, payAmount);

            await _editorPlanOrderRepository.CreateAsync(order);
            await _editorPlanOrderRepository.SaveChangesAsync();

            return order.Id;
        }

        public async ValueTask PaymentSucceededAsync(long orderId, long refId)
        {
            var order = await _editorPlanOrderRepository.GetAsync(orderId);
            order.PaymentSucceeded(refId);
            await _editorPlanOrderRepository.SaveChangesAsync();

            var page = await _personalPageRepository.GetAsync(order.EditorPageId);

            var plan = await _editorPlanRepository.GetPlanAsync(order.EditorPlanId);

            var expireDate = DateTime.Now.AddDays(plan.ExpireDays);

            page.SetPlan(expireDate, plan.ChatUploadSizeLimit, plan.PortfolioUploadSizeLimit, plan.VipProjectOfferCount);

            await _personalPageRepository.SaveChangesAsync();
        }

        public async ValueTask<double> GetAmountByAsync(long id) => await _editorPlanOrderRepository.GetAmountByAsync(id);
    }
}
