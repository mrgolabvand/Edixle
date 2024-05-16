using _0_Framework.Application;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommentManagement.Application
{
    public class CommentApplication : ICommentApplication
    {
        private readonly ICommentRepository _commentRepository;

        public CommentApplication(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddComment command)
        {
            var operation = new OperationResult();

            var comment = new Comment(command.Name, command.Email, command.Website, command.Message, command.Type, command.OwnerRecordId, command.ParentId);

            await _commentRepository.CreateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> CancelAsync(long id)
        {
            var operation = new OperationResult();

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Cancel();
            await _commentRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> ConfirmAsync(long id)
        {
            var operation = new OperationResult();

            var comment = await _commentRepository.GetAsync(id);

            if (comment == null)
                return operation.Failed(ApplicationMessages.RecordNotFound);

            comment.Confirm();
            await _commentRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<List<CommentViewModel>> SearchAsync(CommentSearchModel searchModel) =>
            await _commentRepository.SearchAsync(searchModel);
    }
}
