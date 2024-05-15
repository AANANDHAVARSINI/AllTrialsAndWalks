using System.ComponentModel.DataAnnotations;

namespace AllTrialsAndWalks.Models.Domain
{
    public class Difficulty
    {
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
