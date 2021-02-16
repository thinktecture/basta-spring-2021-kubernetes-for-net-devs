using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Services
{
    public interface IAudiencesService
    {
        public Task<IEnumerable<AudienceListModel>> GetAllAsync();
        public Task<AudienceDetailsModel> GetByIdAsync(Guid id);
        public Task<AudienceDetailsModel> CreateAsync(AudienceCreateModel model);
        public Task<AudienceDetailsModel> UpdateAsync(Guid id, AudienceUpdateModel model);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
