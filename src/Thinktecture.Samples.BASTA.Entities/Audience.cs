using System;
using System.Collections.Generic;

namespace Thinktecture.Samples.BASTA.Entities
{
    public class Audience : BaseEntity
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
