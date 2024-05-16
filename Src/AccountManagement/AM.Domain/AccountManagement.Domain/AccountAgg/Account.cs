using _0_Framework.Domain;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Domain.AccountAgg
{
    public class Account : BaseEntity
    {
        public string UserName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string PhoneNumber { get; private set; }
        public long RoleId { get; private set; }
        public PersonalPage Page { get; private set; }
        public EmployerPage EmployerPage { get; private set; }
        public Role Role { get; private set; }

        public Account(string userName, string email, string password, long roleId, string phoneNumber)
        {
            UserName = userName;
            Email = email;
            if (roleId == 0)
                RoleId = 3;
            Password = password;
            PhoneNumber = phoneNumber;
        }

        public void Edit(string userName, string email, long roleId)
        {
            UserName = userName;
            Email = email;
            RoleId = roleId;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }

    }
}
