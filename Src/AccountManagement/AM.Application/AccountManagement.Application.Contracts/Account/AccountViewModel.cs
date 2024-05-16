namespace AccountManagement.Application.Contracts.Account
{
    public class AccountViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreationDate { get; set; }
        public string PhoneNumber { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
    }
}