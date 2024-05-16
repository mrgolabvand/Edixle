using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AccountManagement.Application.Contracts.ProjectOffer
{
    public interface IProjectOfferApplication
    {
        ValueTask<OperationResult> AddAsync(AddProjectOffer command);
        ValueTask<OperationResult> EditAsync(EditProjectOffer command);
        ValueTask<OperationResult> AcceptAsync(long id);
        ValueTask<EditProjectOffer> GetDetailsAsync(long id);
        ValueTask<List<ProjectOfferViewModel>> SearchAsync(string title);
        ValueTask<ProjectOfferViewModel> GetOfferAsync(long id);
    }
}
