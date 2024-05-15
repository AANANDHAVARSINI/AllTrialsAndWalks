using System.ComponentModel.DataAnnotations;

namespace AllTrialsAndWalks.Models.DTO
{
    public class AddLoginDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
