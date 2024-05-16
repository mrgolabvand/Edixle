using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;

namespace ChatManagement.Domain.ChatAgg
{
    public interface IChatRepository : IBaseRepository<long, Chat>
    {
    }
}
