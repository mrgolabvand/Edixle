using System;
using _0_Framework.Domain;

namespace PlanManagement.Domain.EditorPlanOrderAgg
{
    public class EditorPlanOrder : BaseEntity
    {
        public long EditorPageId { get; private set; }
        public long EditorPlanId { get; private set; }
        public double PayAmount { get; private set; }
        public bool IsPaid { get; private set; }
        public bool IsCanceled { get; private set; }
        public long RefId { get; private set; }

        public EditorPlanOrder(long editorPageId, long editorPlanId, double payAmount)
        {
            EditorPageId = editorPageId;
            EditorPlanId = editorPlanId;
            PayAmount = payAmount;
            IsPaid = false;
            IsCanceled = false;
            RefId = 0;
        }

        public void PaymentSucceeded(long refId)
        {
            IsPaid = true;
            IsCanceled = false;
            if (refId != 0)
                RefId = refId;
        }

        public void Cancel()
        {
            IsCanceled = true;
            IsPaid = false;
        }
    }
}
