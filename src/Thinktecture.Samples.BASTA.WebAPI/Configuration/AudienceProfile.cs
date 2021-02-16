using AutoMapper;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Configuration
{
    public class AudienceProfile: Profile
    {
        public AudienceProfile()
        {
            // outbound

            CreateMap<Audience, AudienceListModel>();
            CreateMap<Audience, AudienceDetailsModel>();
            
            // inbound

            CreateMap<AudienceCreateModel, Audience>();
            CreateMap<AudienceUpdateModel, Audience>();
        }
    }
}
