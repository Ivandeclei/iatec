using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class ParticipantDto 
    {
        /// <summary>
        /// Name of the participant
        /// </summary>
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }
    }
}
