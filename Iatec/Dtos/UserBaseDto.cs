using System.ComponentModel.DataAnnotations;

namespace Iatec.Dtos
{
    public class UserBaseDto
    {
        /// <summary>
        /// Password
        /// </summary>
        [Required(ErrorMessage = "Password field is required")]
        public string? Password { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email field is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
    }
}
