using _0_Framework.Application;
using _0_Framework.Infrastructure;
using AccountManagement.Application.Contracts.Project;
using AccountManagement.Application.Contracts.ProjectOffer;
using AccountManagement.Domain.ProjectAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class ProjectRepository : BaseRepository<long, Project>, IProjectRepository
    {
        private readonly AccountContext _context;

        public ProjectRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public async ValueTask<EditProject> GetDetailsAsync(long id) =>
            await _context.Projects.Select(v => new EditProject
            {
                Id = v.Id,
                Budget = v.Budget,
                EmployerPageId = v.EmployerPageId,
                Tags = v.Tags,
                ExpireDate = v.ExpireDate.ToString(),
                Title = v.Title,
                Description = v.Description,
                IsConfirm = v.IsConfirm,
                IsOpened = v.IsOpened
            }).FirstOrDefaultAsync(v => v.Id == id);

        public async ValueTask<List<ProjectViewModel>> GetEmployerProjectsAsync(long employerId) =>
            await _context.Projects.Where(v => v.EmployerPageId == employerId).Select(v => new ProjectViewModel
            {
                Id = v.Id,
                Title = v.Title,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Tags = v.Tags,
                EmployerPageId = v.EmployerPageId,
                Budget = ((double)int.Parse(v.Budget)).ToMoney(),
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                IsOpened = v.IsOpened,
                Description = v.Description
            }).OrderByDescending(v => v.Id).AsNoTracking().ToListAsync();

        public async ValueTask<bool> IsEmployerOwnerOfProject(long pageId, long projectId) =>
            await _context.Projects.AnyAsync(v => v.Id == projectId && v.EmployerPageId == pageId);

        public async ValueTask<List<ProjectViewModel>> SearchAsync(string title)
        {
            var query = _context.Projects.Select(v => new ProjectViewModel
            {
                Id = v.Id,
                Title = v.Title,
                ExpireDate = v.ExpireDate.ToFarsi().ToPersianNumber(),
                Tags = v.Tags,
                EmployerPageId = v.EmployerPageId,
                Budget = v.Budget,
                CreationDate = v.CreationDate.ToFarsi().ToPersianNumber(),
                IsConfirm = v.IsConfirm,
                IsOpened = v.IsOpened,
                Description= v.Description
            });

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(v => v.Title.Contains(title));

            return await query.ToListAsync();
        }
    }
}
