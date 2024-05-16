namespace EdixleQuery.Contracts.Account
{
    public class AccountQueryModel
    {
        public long Id { get; set; }
        public string UserName { get;  set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public string Password { get;  set; }
        public long RoleId { get; set; }
    }
}
