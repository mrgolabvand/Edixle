using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_Framework.Application
{
    public static class ValidationMessage
    {
        public const string isRequired = "مقدار نمیتواند خالی باشد.";
        public const string MaxFileSize = "حجم فایل بیشتر از حد مجاز است.";
        public const string InvalidFileFormat = "فرمت فایل مجاز نیست.";
        public const string MaxLength = "تعداد کاراکتر وارد شده بیشتر از حد مجاز است.";
        public const string MinLength = "تعداد کاراکتر وارد شده کمتر از حد مجاز است.";
        public const string InvalidId = "ایدی وارد شده نامعتبر است.";
        public const string InvalidAmount = "مقدار پول وارد شده نامعتبر است.";
    }
}
