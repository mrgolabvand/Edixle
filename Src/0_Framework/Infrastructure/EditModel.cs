using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Infrastructure
{
    public class EditModel<TEditModel>
    {
        public TEditModel Model { get; set; }
    }
    public class BaseModel<T>
    {
        public T Model { get; set; }

    }
}
