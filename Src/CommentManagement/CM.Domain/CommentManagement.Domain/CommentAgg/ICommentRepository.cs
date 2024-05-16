using _0_Framework.Domain;
using CommentManagement.Application.Contracts.Comment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentManagement.Domain.CommentAgg
{
    public interface ICommentRepository : IBaseRepository<long, Comment>
    {
        ValueTask<List<CommentViewModel>> SearchAsync(CommentSearchModel searchModel);
    }
}
