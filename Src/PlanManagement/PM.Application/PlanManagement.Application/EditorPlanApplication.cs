using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using PlanManagement.Application.Contracts.EditorPlan;
using PlanManagement.Domain.EditorPlanAgg;

namespace PlanManagement.Application
{
    public class EditorPlanApplication : IEditorPlanApplication
    {
        private readonly IEditorPlanRepository _repository;

        public EditorPlanApplication(IEditorPlanRepository repository)
        {
            _repository = repository;
        }

        public async ValueTask<List<EditorPlanViewModel>> GetListAsync() => await _repository.GetListAsync();
        public async ValueTask<EditEditorPlan> GetDetailsAsync(long id) => await _repository.GetDetailsAsync(id);

        public async ValueTask<OperationResult> CreateAsync(AddEditorPlan command)
        {
            var result = new OperationResult();

            if (await _repository.ExistsAsync(v => v.Title == command.Title))
                return result.Failed(ApplicationMessages.IsDuplicated);
            var plan = new EditorPlan(command.Title, command.Description, command.Price, command.ExpireDays,
                command.ChatUploadSizeLimit, command.PortfolioUploadSizeLimit, command.VipProjectOfferCount);

            await _repository.CreateAsync(plan);
            await _repository.SaveChangesAsync();

            return result.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditEditorPlan command)
        {
            var result = new OperationResult();

            if (await _repository.ExistsAsync(v => v.Title == command.Title && v.Id != command.Id))
                return result.Failed(ApplicationMessages.IsDuplicated);

            var plan = await _repository.GetAsync(command.Id);
           
            plan.Edit(command.Title, command.Description, command.Price, command.ExpireDays,
                command.ChatUploadSizeLimit, command.PortfolioUploadSizeLimit, command.VipProjectOfferCount);
            
            await _repository.SaveChangesAsync();
            
            return result.Succeeded();
        }

        public async ValueTask<EditorPlanViewModel> GetPlanAsync(long id) => await _repository.GetPlanAsync(id);

        public async ValueTask RemoveAsync(long id)
        {
            var plan = await _repository.GetAsync(id);
            plan.Remove();
            await _repository.SaveChangesAsync();
        }

        public async ValueTask RestoreAsync(long id)
        {
            var plan = await _repository.GetAsync(id);
            plan.Restore();
            await _repository.SaveChangesAsync();
        }
    }
}
