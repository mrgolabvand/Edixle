using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.JobOffer
{
    public interface IJobOfferApplication
    {
        ValueTask<OperationResult> AddAsync(AddJobOffer command);
        ValueTask<OperationResult> EditAsync(EditJobOffer command);
        ValueTask<EditJobOffer> GetDetailsAsync(long id);
        ValueTask<List<JobOfferViewModel>> SearchAsync(string title);
        ValueTask<List<JobOfferViewModel>> GetEditorJobOffersAsync(long editorPageId);
        ValueTask<List<JobOfferViewModel>> GetEmployerJobOffersAsync(long employerPageId);
        ValueTask<JobOfferViewModel> GetOfferAsync(long id);
        ValueTask<OperationResult> AcceptAsync(long id);
    }
}
