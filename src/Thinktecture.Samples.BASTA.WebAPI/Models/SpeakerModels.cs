using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.Samples.BASTA.WebAPI.Models
{
    public class SpeakerCreateModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Bio { get; set; }
        [Required]
        public String Twitter { get; set; }
        [Required]
        public String Mail { get; set; }
    }
    
    public class SpeakerUpdateModel
    {
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        public String Bio { get; set; }
        [Required]
        public String Twitter { get; set; }
        [Required]
        public String Mail { get; set; }
    }

    public class SpeakerListModel
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Twitter { get; set; }
        public String Mail { get; set; }
    }
    
    public class SpeakerDetailsModel
    {
        public Guid Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Bio { get; set; }
        public String Twitter { get; set; }
        public String Mail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }
}
