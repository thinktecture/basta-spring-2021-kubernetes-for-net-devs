using AutoMapper;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Configuration
{
    public class SessionProfile : Profile
    {
        public SessionProfile()
        {
            // outbound
            CreateMap<Session, SessionListModel>();
            CreateMap<Session, SessionDetailsModel>();
            
            // inbound
            CreateMap<SessionCreateModel, Session>();
            CreateMap<SessionUpdateModel, Session>();

        }
    }
}
