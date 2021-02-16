using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    public interface ISpeakersRepository
    {
        public Task<IEnumerable<Speaker>> GetAllAsync();
        public Task<Speaker> GetByIdAsync(Guid id);
        public Task<Speaker> CreateAsync(Speaker speaker);
        public Task<Speaker> UpdateAsync(Speaker speaker);
        public Task<bool> DeleteByIdAsync(Guid id);
    }
}
