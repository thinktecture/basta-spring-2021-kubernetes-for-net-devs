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
    public class SpeakersService : ISpeakersService
    {
        protected ISpeakersRepository Repository { get; }
        public IAuditLogRepository Audit { get; }
        public IMapper Mapper { get; }

        public SpeakersService(ISpeakersRepository repository, IAuditLogRepository audit, IMapper mapper)
        {
            Repository = repository;
            Audit = audit;
            Mapper = mapper;
        }
        public async Task<IEnumerable<SpeakerListModel>> GetAllAsync()
        {
            var all = await Repository.GetAllAsync();

            return Mapper.Map<IEnumerable<SpeakerListModel>>(all);
        }

        public async Task<SpeakerDetailsModel> GetByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentNullException(nameof(id));
            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;

            return Mapper.Map<SpeakerDetailsModel>(found);
        }

        public async Task<SpeakerDetailsModel> CreateAsync(SpeakerCreateModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            var speaker = Mapper.Map<Speaker>(model);
            await Repository.CreateAsync(speaker);
            await Audit.AuditCreatedAsync($"Speaker {speaker.FirstName} {speaker.LastName} has been created");
            return Mapper.Map<SpeakerDetailsModel>(speaker);
        }

        public async Task<SpeakerDetailsModel> UpdateAsync(Guid id, SpeakerUpdateModel model)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;
            
            
            Mapper.Map<SpeakerUpdateModel, Speaker>(model, found);
            var updated = await Repository.UpdateAsync(found);
            await Audit.AuditCreatedAsync($"Speaker {updated.FirstName} {updated.LastName} has been updated");
            return Mapper.Map<SpeakerDetailsModel>(updated);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));

            var deleted = await Repository.DeleteByIdAsync(id);
            if (deleted)
            {
                await Audit.AuditCreatedAsync($"Speaker {id} has been deleted");
            }

            return deleted;
        }
    }
}
