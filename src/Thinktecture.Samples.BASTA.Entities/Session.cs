using System;
using System.Collections.Generic;

namespace Thinktecture.Samples.BASTA.Entities
{
    public class Session: BaseEntity
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int Level { get; set; }
        public Guid AudienceId { get; set; }
        public Audience Audience { get; set; }
        public Guid SpeakerId { get; set; }
        public Speaker Speaker { get; set; }
        
    }
}
