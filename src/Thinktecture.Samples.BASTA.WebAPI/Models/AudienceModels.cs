using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.Samples.BASTA.WebAPI.Models
{
    public class AudienceCreateModel
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }
    }

    public class AudienceUpdateModel
    {
        [Required]
        [MaxLength(100)]
        public String Name { get; set; }
    }

    public class AudienceListModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
    }

    public class AudienceDetailsModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
