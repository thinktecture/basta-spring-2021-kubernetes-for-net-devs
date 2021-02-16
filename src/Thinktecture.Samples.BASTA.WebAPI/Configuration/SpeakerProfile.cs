using AutoMapper;
using Thinktecture.Samples.BASTA.Entities;
using Thinktecture.Samples.BASTA.WebAPI.Models;

namespace Thinktecture.Samples.BASTA.WebAPI.Configuration
{
    public class SpeakerProfile : Profile
    {
        public SpeakerProfile()
        {
            // outbound
            CreateMap<Speaker, SpeakerListModel>()
                .ForMember(slm => slm.Name, action => action.MapFrom(speaker => $"{speaker.FirstName} {speaker.LastName}"));
            
            CreateMap<Speaker, SpeakerDetailsModel>();

            // inbound
            CreateMap<SpeakerCreateModel, Speaker>();
            CreateMap<SpeakerUpdateModel, Speaker>();
        }
    }
}
