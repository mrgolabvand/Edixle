using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Domain;

namespace AccountManagement.Domain.AccountAgg;

public class VerificationCode : BaseEntity
{
    public string PhoneNumber { get; private set; }
    public string Code { get; private set; }
    public DateTime ExpireDate { get; private set; }

    public VerificationCode(string phoneNumber, string code)
    {
        PhoneNumber = phoneNumber;
        Code = code;
        ExpireDate = DateTime.Now.AddMinutes(2);
    }
}