namespace _0_Framework.Infrastructure
{
    public class PermissionDto
    {
        public PermissionDto(int code, string name)
        {
            Name = name;
            Code = code;
        }

        public string Name { get; set; }
        public int Code { get; set; }
    }
}
