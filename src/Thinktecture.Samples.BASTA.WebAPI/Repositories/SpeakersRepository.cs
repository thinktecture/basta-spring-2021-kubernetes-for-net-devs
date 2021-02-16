using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    // ReSharper disable once UnusedType.Global
    public class SpeakersRepository : ISpeakersRepository
    {
        protected BASTAContext Context { get; }

        public SpeakersRepository(BASTAContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Speaker>> GetAllAsync()
        {
            return await Context
                .Speakers
                .OrderBy(s => s.LastName).ThenBy(s => s.FirstName)
                .ToListAsync();
        }

        public async Task<Speaker> GetByIdAsync(Guid id)
        {
            return await Context
                .Speakers
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<Speaker> CreateAsync(Speaker speaker)
        {
            await Context.Speakers.AddAsync(speaker);
            await Context.SaveChangesAsync();
            return speaker;
        }

        public async Task<Speaker> UpdateAsync(Speaker speaker)
        {
            Context.Update(speaker);
            await Context.SaveChangesAsync();
            return speaker;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var found = await GetByIdAsync(id);
            if (found == null) return false;
            Context.Speakers.Remove(found);
            await Context.SaveChangesAsync();
            return true;
        }
    }
}
