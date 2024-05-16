namespace AccountManagement.Domain.RoleAgg
{
    public class Permission
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        public int Code { get; private set; }
        public long RoleId { get; set; }
        public Role Role { get; set; }

        public Permission(int code)
        {
            Code = code;
        }
        public Permission( string name, int code)
        {
            Name = name;
            Code = code;
        }

    }
}
