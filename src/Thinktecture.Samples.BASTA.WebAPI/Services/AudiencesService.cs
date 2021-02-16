using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;
using Thinktecture.Samples.BASTA.WebAPI.Repositories;

namespace Thinktecture.Samples.BASTA.WebAPI.Services
{
    // ReSharper disable once UnusedType.Global
    public class AudiencesService : IAudiencesService
    {
        protected IAudiencesRepository Repository { get; }
        public IAuditLogRepository Audit { get; }
        protected IMapper Mapper { get; }

        public AudiencesService(IAudiencesRepository repository, IAuditLogRepository audit, IMapper mapper)
        {
            Repository = repository;
            Audit = audit;
            Mapper = mapper;
        }
        
        public async Task<IEnumerable<AudienceListModel>> GetAllAsync()
        {
            var allAudiences = await Repository.GetAllAsync();
            return Mapper.Map<IEnumerable<AudienceListModel>>(allAudiences);
        }

        public async Task<AudienceDetailsModel> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            
            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;
            return Mapper.Map<AudienceDetailsModel>(found);
        }

        public async Task<AudienceDetailsModel> CreateAsync(AudienceCreateModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var audience = Mapper.Map<Audience>(model);
            var created = await Repository.CreateAsync(audience);
            await Audit.AuditCreatedAsync($"Audience {created.Name} has been created");
            return Mapper.Map<AudienceDetailsModel>(created);
        }

        public async Task<AudienceDetailsModel> UpdateAsync(Guid id, AudienceUpdateModel model)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;
            
            
            Mapper.Map<AudienceUpdateModel, Audience>(model, found);
            var updated = await Repository.UpdateAsync(found);
            await Audit.AuditCreatedAsync($"Audience {updated.Name} has been updated");
            return Mapper.Map<AudienceDetailsModel>(updated);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));

            var deleted = await Repository.DeleteByIdAsync(id);
            if (deleted)
            {
                await Audit.AuditCreatedAsync($"Audience {id} has been deleted");
            }

            return deleted;
        }
    }
}
