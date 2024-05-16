using _0_Framework.Domain;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.ProjectOffer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.JobOfferAgg
{
    public interface IJobOfferRepository : IBaseRepository<long, JobOffer>
    {
        ValueTask<EditJobOffer> GetDetailsAsync(long id);
        ValueTask<List<JobOfferViewModel>> SearchAsync(string title);
        ValueTask<List<JobOfferViewModel>> GetEditorJobOffersAsync(long editorPageId);
        ValueTask<List<JobOfferViewModel>> GetEmployerJobOffersAsync(long employerPageId);
        ValueTask<JobOfferViewModel> GetOfferAsync(long id);
    }
}
