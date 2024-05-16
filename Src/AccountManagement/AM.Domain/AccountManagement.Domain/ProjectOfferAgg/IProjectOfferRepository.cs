using _0_Framework.Domain;
using AccountManagement.Application.Contracts.ProjectOffer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Domain.ProjectOfferAgg
{
    public interface IProjectOfferRepository : IBaseRepository<long, ProjectOffer>
    {
        ValueTask<EditProjectOffer> GetDetailsAsync(long id);
        ValueTask<List<ProjectOfferViewModel>> SearchAsync(string title);
        ValueTask<ProjectOfferViewModel> GetOfferAsync(long id);
    }
}
