using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    // ReSharper disable once UnusedType.Global
    public class AudiencesRepository : IAudiencesRepository
    {
        protected BASTAContext Context { get; }

        public AudiencesRepository(BASTAContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Audience>> GetAllAsync()
        {
            return await Context.Audiences.OrderBy(a => a.Name)
                .ToListAsync();
        }

        public async Task<Audience> GetByIdAsync(Guid id)
        {
            return await Context.Audiences.FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        public async Task<Audience> CreateAsync(Audience audience)
        {
            await Context.Audiences.AddAsync(audience);
            await Context.SaveChangesAsync();
            return audience;
        }

        public async Task<Audience> UpdateAsync(Audience audience)
        {
            Context.Update(audience);
            await Context.SaveChangesAsync();
            return audience;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var found = await GetByIdAsync(id);
            if (found == null) return false;
            Context.Audiences.Remove(found);
            await Context.SaveChangesAsync();
            return true;

        }
    }
}
