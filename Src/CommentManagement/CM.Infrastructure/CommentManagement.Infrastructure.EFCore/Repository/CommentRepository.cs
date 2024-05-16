using _0_Framework.Application;
using _0_Framework.Infrastructure;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommentManagement.Infrastructure.EFCore.Repository
{
    public class CommentRepository : BaseRepository<long, Comment>, ICommentRepository
    {
        private readonly CommentContext _commentContext;

        public CommentRepository(CommentContext commentContext) : base(commentContext)
        {
            _commentContext = commentContext;
        }

        public async ValueTask<List<CommentViewModel>> SearchAsync(CommentSearchModel searchModel)
        {
            var query = _commentContext.Comments.Select(v => new CommentViewModel
            {
                Id = v.Id,
                Email = v.Email,
                IsCanceled = v.IsCanceled,
                IsConfirmed = v.IsConfirmed,
                Message = v.Message,
                Name = v.Name,
                Website = v.Website,
                OwnerRecordId = v.OwnerRecordId,
                Type  =v.Type,
                CommentDate = v.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(v => v.Email.Contains(searchModel.Email));

            return await query.AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();
        }
    }
}
