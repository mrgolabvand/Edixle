using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;

namespace CommentManagement.Application.Contracts.Comment
{
    public class AddComment
    {
        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Name { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Email { get; set; }

        [MaxLength(500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Website { get; set; }

        [Required(ErrorMessage = ValidationMessage.isRequired)]
        [MaxLength(1500, ErrorMessage = ValidationMessage.MaxLength)]
        public string Message { get; set; }
        public int Type { get; set; }
        public long OwnerRecordId { get; set; }
        public long ParentId { get; set; }
    }
}
