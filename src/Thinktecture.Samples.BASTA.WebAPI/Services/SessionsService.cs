using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.VisualBasic;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;
using Thinktecture.Samples.BASTA.WebAPI.Repositories;

namespace Thinktecture.Samples.BASTA.WebAPI.Services
{
    public class SessionsService : ISessionsService
    {
        protected ISessionsRepository Repository { get; }
        public ISpeakersRepository SpeakersRepository { get; }
        public IAudiencesRepository AudiencesRepository { get; }
        public IAuditLogRepository Audit { get; }
        public IMapper Mapper { get; }

        public SessionsService(ISessionsRepository repository,
            ISpeakersRepository speakersRepository,
            IAudiencesRepository audiencesRepository,
            IAuditLogRepository audit, 
            IMapper mapper)
        {
            Repository = repository;
            SpeakersRepository = speakersRepository;
            AudiencesRepository = audiencesRepository;
            Audit = audit;
            Mapper = mapper;
        }
        public async Task<IEnumerable<SessionListModel>> GetAllAsync()
        {
            var all = await Repository.GetAllAsync();
            return Mapper.Map<IEnumerable<SessionListModel>>(all);
        }

        public async Task<IEnumerable<SessionListModel>> GetAllBySpeakerAsync(Guid speakerId)
        {
            if (speakerId.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(speakerId));
            
            var all = await Repository.GetAllBySpeakerAsync(speakerId);
            return Mapper.Map<IEnumerable<SessionListModel>>(all);
        }

        public async Task<IEnumerable<SessionListModel>> GetAllByAudienceAsync(Guid audienceId)
        {
            if (audienceId.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(audienceId));
            
            var all = await Repository.GetAllByAudienceAsync(audienceId);
            return Mapper.Map<IEnumerable<SessionListModel>>(all);
        }

        public async Task<SessionDetailsModel> GetByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            
            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;
            
            return Mapper.Map<SessionDetailsModel>(found);
        }

        public async Task<SessionDetailsModel> CreateAsync(SessionCreateModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            var session = Mapper.Map<Session>(model);
            
            // verify speaker id and audience id are valid
            var speaker = await SpeakersRepository.GetByIdAsync(session.SpeakerId);
            var audience = await AudiencesRepository.GetByIdAsync(session.AudienceId);
            if (speaker == null || audience == null) throw new IndexOutOfRangeException();
            
            session.Speaker = speaker;
            session.Audience = audience;
            var created = await Repository.CreateAsync(session);
            await Audit.AuditCreatedAsync($"Session {session.Title} has been created");
            return Mapper.Map<SessionDetailsModel>(created);
        }

        public async Task<SessionDetailsModel> UpdateAsync(Guid id, SessionUpdateModel model)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));
            if (model == null) throw new ArgumentNullException(nameof(model));

            var found = await Repository.GetByIdAsync(id);
            if (found == null) return null;
            
            
            Mapper.Map<SessionUpdateModel, Session>(model, found);
            // verify speaker id and audience id are valid
            var speaker = await SpeakersRepository.GetByIdAsync(found.SpeakerId);
            var audience = await AudiencesRepository.GetByIdAsync(found.AudienceId);
            if (speaker == null || audience == null) throw new IndexOutOfRangeException();

            found.Speaker = speaker;
            found.Audience = audience;
            
            var updated = await Repository.UpdateAsync(found);
            await Audit.AuditCreatedAsync($"Session {updated.Title} has been updated");
            return Mapper.Map<SessionDetailsModel>(updated);
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            if (id.Equals(Guid.Empty)) throw new ArgumentNullException(nameof(id));

            var deleted = await Repository.DeleteByIdAsync(id);
            if (deleted)
            {
                await Audit.AuditCreatedAsync($"Session {id} has been deleted");
            }

            return deleted; 
        }
    }
}
