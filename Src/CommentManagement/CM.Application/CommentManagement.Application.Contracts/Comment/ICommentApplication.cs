using _0_Framework.Application;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentManagement.Application.Contracts.Comment
{
    public interface ICommentApplication
    {
        ValueTask<OperationResult> CancelAsync(long id);
        ValueTask<OperationResult> ConfirmAsync(long id);
        ValueTask<List<CommentViewModel>> SearchAsync(CommentSearchModel searchModel);
        ValueTask<OperationResult> AddAsync(AddComment command);
    }
}
