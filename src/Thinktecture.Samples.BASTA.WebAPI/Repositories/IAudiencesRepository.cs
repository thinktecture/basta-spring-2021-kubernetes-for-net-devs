using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    public interface IAudiencesRepository
    {
        public Task<IEnumerable<Audience>> GetAllAsync();
        public Task<Audience> GetByIdAsync(Guid id);
        public Task<Audience> CreateAsync(Audience audience);
        public Task<Audience> UpdateAsync(Audience audience);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
