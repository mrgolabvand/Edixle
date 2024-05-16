using System.Threading.Tasks;

namespace PlanManagement.Application.Contracts.EditorPlanOrder
{
    public interface IEditorPlanOrderApplication
    {
        ValueTask<long> PlaceOrderAsync(long pageId, long planId, double payAmount);
        ValueTask PaymentSucceededAsync(long orderId, long refId);
        ValueTask<double> GetAmountByAsync(long id);
    }
}
