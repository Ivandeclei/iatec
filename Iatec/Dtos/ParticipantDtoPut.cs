using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class ParticipantDtoPut : ParticipantDto
    {
        /// <summary>
        /// Identifier 
        /// </summary>
        [Required(ErrorMessage = "Id field is required")]
        public Guid Id { get; set; }
    }
}
