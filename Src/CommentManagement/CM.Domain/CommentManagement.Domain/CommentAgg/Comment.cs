using _0_Framework.Domain;
using System.Collections.Generic;

namespace CommentManagement.Domain.CommentAgg
{
    public class Comment : BaseEntity
    {

        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Website { get; private set; }
        public string Message { get; private set; }
        public bool IsConfirmed { get; private set; }
        public bool IsCanceled { get; private set; }
        public int Type { get; private set; }
        public long OwnerRecordId { get; private set; }
        public long ParentId { get; private set; }
        public Comment Parent { get; private set; }
        public List<Comment> Children { get; private set; }

        public Comment(string name, string email, string website, string message, int type, long ownerRecordId, long parentId)
        {
            Name = name;
            Email = email;
            Website = website;
            Message = message;
            Type = type;
            OwnerRecordId = ownerRecordId;
            ParentId = parentId;
            IsConfirmed = false;
            IsCanceled = false;
        }


        public void Cancel()
        {
            IsCanceled = true;
            IsConfirmed = false;
        }

        public void Confirm()
        {
            IsConfirmed = true;
            IsCanceled = false;
        }

    }
}
