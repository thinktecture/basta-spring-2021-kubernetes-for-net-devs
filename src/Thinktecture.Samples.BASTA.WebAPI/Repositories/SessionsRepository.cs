using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Thinktecture.Samples.BASTA.Entities;

namespace Thinktecture.Samples.BASTA.WebAPI.Repositories
{
    public class SessionsRepository : ISessionsRepository
    {
        public BASTAContext Context { get; }

        public SessionsRepository(BASTAContext context)
        {
            Context = context;
        }
        public async Task<IEnumerable<Session>> GetAllAsync()
        {
            return await Context
                .Sessions
                .OrderBy(s => s.Title).ThenByDescending(s => s.CreatedAt)
                .Include(s => s.Audience)
                .Include(s => s.Speaker)
                .ToListAsync();

        }

        public async Task<IEnumerable<Session>> GetAllBySpeakerAsync(Guid speakerId)
        {
            return await Context
                .Sessions
                .Where(s => s.SpeakerId.Equals(speakerId))
                .OrderBy(s => s.Title).ThenByDescending(s => s.CreatedAt)
                .Include(s => s.Audience)
                .Include(s => s.Speaker)
                .ToListAsync();
        }

        public async Task<IEnumerable<Session>> GetAllByAudienceAsync(Guid audienceId)
        {
            return await Context
                .Sessions
                .Where(s => s.AudienceId.Equals(audienceId))
                .OrderBy(s => s.Title).ThenByDescending(s => s.CreatedAt)
                .Include(s => s.Audience)
                .Include(s => s.Speaker)
                .ToListAsync();
        }

        public async Task<Session> GetByIdAsync(Guid id)
        {
            return await Context
                .Sessions
                .Include(s => s.Audience)
                .Include(s => s.Speaker)
                .FirstOrDefaultAsync(s => s.Id.Equals(id));
        }

        public async Task<Session> CreateAsync(Session session)
        {
            await Context.Sessions.AddAsync(session);
            await Context.SaveChangesAsync();
            return session;
        }

        public async Task<Session> UpdateAsync(Session session)
        {
            Context.Update(session);
            await Context.SaveChangesAsync();
            return session;
        }

        public async Task<bool> DeleteByIdAsync(Guid id)
        {
            var found = await GetByIdAsync(id);
            if (found == null) return false;
            Context.Sessions.Remove(found);
            await Context.SaveChangesAsync();
            return true;
        }
        
        
    }
}
