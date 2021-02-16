using System;
using System.ComponentModel.DataAnnotations;

namespace Thinktecture.Samples.BASTA.WebAPI.Models
{
    public class SessionListModel
    {
        public Guid Id { get; set; }
        public SpeakerListModel Speaker { get; set; }
        public AudienceListModel Audience { get; set; }
        public String Title { get; set; }
        public int Level { get; set; }
    }

    public class SessionDetailsModel
    {
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public int Level { get; set; }
        public Guid SpeakerId { get; set; }
        public SessionDetailsModel Speaker { get; set; }
        public Guid AudienceId { get; set; }
        public AudienceDetailsModel Audience { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }

    public class SessionCreateModel
    {
        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public Guid AudienceId { get; set; }
        [Required]
        public Guid SpeakerId { get; set; }
    }
    
    public class SessionUpdateModel
    {
        [Required]
        public String Title { get; set; }
        [Required]
        public String Description { get; set; }
        [Required]
        public int Level { get; set; }
        [Required]
        public Guid AudienceId { get; set; }
        [Required]
        public Guid SpeakerId { get; set; }
    }
}
