using System;

namespace _0_Framework.Infrastructure
{
    public static class Roles
    {
        public const string Admin = "1";
        public const string User = "3";
        public const string ArticleUploader = "4";
        public const string ContentController = "5";

        internal static string GetRoleBy(long roleId)
        {
            switch (roleId)
            {
                case 1:
                    return "مدیر سیستم";
                case 4:
                    return "مقاله نویس";
                case 5:
                    return "کنترل کننده محتوا";
                default:
                    return "نامشخص";
            }
        }
    }
}
