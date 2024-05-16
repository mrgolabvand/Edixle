using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.Feature;

namespace AccountManagement.Domain.FeatureAgg
{
    public interface IFeatureRepository : IBaseRepository<long, Feature>
    {
        ValueTask<List<FeatureViewModel>> GetListAsync();
        ValueTask<EditFeature> GetDetailsAsync(long id);
    }
}
