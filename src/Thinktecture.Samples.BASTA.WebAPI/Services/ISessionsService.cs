using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Services
{
    public interface ISessionsService
    {
        public Task<IEnumerable<SessionListModel>> GetAllAsync();
        public Task<IEnumerable<SessionListModel>> GetAllBySpeakerAsync(Guid speakerId);
        public Task<IEnumerable<SessionListModel>> GetAllByAudienceAsync(Guid audienceId);
        public Task<SessionDetailsModel> GetByIdAsync(Guid id);
        public Task<SessionDetailsModel> CreateAsync(SessionCreateModel model);
        public Task<SessionDetailsModel> UpdateAsync(Guid id, SessionUpdateModel model);
        public Task<bool> DeleteByIdAsync(Guid id);

    }
}
