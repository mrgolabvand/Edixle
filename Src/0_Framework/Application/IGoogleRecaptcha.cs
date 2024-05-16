using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public interface IGoogleRecaptcha
    {
        ValueTask<bool> IsVerified();
    }
}
