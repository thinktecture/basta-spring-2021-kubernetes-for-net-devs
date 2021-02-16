using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    public interface ISessionsRepository
    {
        public Task<IEnumerable<Session>> GetAllAsync();
        public Task<IEnumerable<Session>> GetAllBySpeakerAsync(Guid speakerId);
        public Task<IEnumerable<Session>> GetAllByAudienceAsync(Guid audienceId);
        public Task<Session> GetByIdAsync(Guid id);
        public Task<Session> CreateAsync(Session session);
        public Task<Session> UpdateAsync(Session session);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
