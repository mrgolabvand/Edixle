using System.Collections.Generic;
using _0_Framework.Infrastructure;

namespace AccountManagement.Infrastructure.Configuration.Permissions
{
    public class AccountPermissionExposer : IPermissionExposer
    {
        public Dictionary<string, List<PermissionDto>> Expose()
        {
            return new Dictionary<string, List<PermissionDto>>
            {
                {
                    "Accounts", new List<PermissionDto>
                    {
                        new PermissionDto(AccountPermissions.ConfirmAndCancelPage, "تایید و رد کردن صفحه"),
                        new PermissionDto(AccountPermissions.ConfirmAndCancelPortfolio, "تایید و رد کردن نمونه کار"),
                        new PermissionDto(AccountPermissions.CreateAndEditAccount, "ایجاد و ویرایش حساب های کاربری"),
                        new PermissionDto(AccountPermissions.CreateAndEditCategory, "ایجاد و ویرایش گروه"),
                        new PermissionDto(AccountPermissions.CreateAndEditFeature, "ایجاد و ویرایش ویژگی"),
                        new PermissionDto(AccountPermissions.CreateAndEditRole, "ایجاد و ویرایش نقش"),
                        new PermissionDto(AccountPermissions.CreateAndEditSlider, "ایجاد و ویرایش اسلایدر"),
                    }

                }
            };
        }
    }
}
