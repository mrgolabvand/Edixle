using _0_Framework.Application;
using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Domain.JobOfferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Application
{
    public class JobOfferApplication : IJobOfferApplication
    {

        private readonly IJobOfferRepository _jobOfferRepository;

        public JobOfferApplication(IJobOfferRepository jobOfferRepository)
        {
            _jobOfferRepository = jobOfferRepository;
        }

        public async ValueTask<OperationResult> AddAsync(AddJobOffer command)
        {
            var operation = new OperationResult();

            //if (await _jobOfferRepository.ExistsAsync(v => v.EmployerPageId == command.EmployerPageId && v.EditorPageId == command.EditorPageId))
            //    return operation.Failed(ApplicationMessages.IsDuplicated);

            var jobOffer = new JobOffer(command.Title, command.Price, command.Description, command.EditorPageId, command.EmployerPageId);
            await _jobOfferRepository.CreateAsync(jobOffer);
            await _jobOfferRepository.SaveChangesAsync();
            return operation.Succeeded();
        }

        public async ValueTask<OperationResult> EditAsync(EditJobOffer command)
        {
            var operation = new OperationResult();

            if (await _jobOfferRepository.ExistsAsync(v => v.EmployerPageId == command.EmployerPageId && v.EditorPageId == command.EditorPageId))
                return operation.Failed(ApplicationMessages.IsDuplicated);

            var jobOffer = await _jobOfferRepository.GetAsync(command.Id);
            jobOffer.Edit(command.Title, command.Price, command.Description, command.EditorPageId, command.EmployerPageId);
            await _jobOfferRepository.SaveChangesAsync();

            return operation.Succeeded();
        }

        public async ValueTask<EditJobOffer> GetDetailsAsync(long id) => await _jobOfferRepository.GetDetailsAsync(id);
        public async ValueTask<List<JobOfferViewModel>> SearchAsync(string title) => await _jobOfferRepository.SearchAsync(title);

        public async ValueTask<List<JobOfferViewModel>> GetEditorJobOffersAsync(long editorPageId) =>
           await _jobOfferRepository.GetEditorJobOffersAsync(editorPageId);
        public async ValueTask<List<JobOfferViewModel>> GetEmployerJobOffersAsync(long employerPageId) =>
            await _jobOfferRepository.GetEmployerJobOffersAsync(employerPageId);

        public async ValueTask<JobOfferViewModel> GetOfferAsync(long id) => await _jobOfferRepository.GetOfferAsync(id);
        public async ValueTask<OperationResult> AcceptAsync(long id)
        {
            var operation = new OperationResult();

            var jobOffer = await _jobOfferRepository.GetAsync(id);
            jobOffer.Accept();
            await _jobOfferRepository.SaveChangesAsync();

            return operation.Succeeded();
        }
    }
}
