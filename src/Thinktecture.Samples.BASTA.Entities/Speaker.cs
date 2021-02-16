using System;
using System.Collections.Generic;

namespace Thinktecture.Samples.BASTA.Entities
{
    public class Speaker: BaseEntity
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Bio { get; set; }
        public String Twitter { get; set; }
        public String Mail { get; set; }
        public ICollection<Session> Sessions { get; set; }
    }
}
