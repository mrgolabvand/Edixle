using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace BlogManagement.Infrastructure.Configuration.Permissions
{
    public class BlogPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Blog", new List<PermissionDto>
                    {
                        new PermissionDto(BlogPermissions.CreateAndEditArticle, "ایجاد و ویرایش مقاله"),
                        new PermissionDto(BlogPermissions.CreateAndEditArticleCategory, "ایجاد و ویرایش گروه مقاله"),
                    }

                }
            };
        }
    }
}
