using System.ComponentModel.DataAnnotations;

namespace CommandsService.DTOs.CreateDTOs
{
    public class CreateCommandDTO
    {
        [Required]
        public string HowTo { get; set; }

        [Required]
        public string CommandLine { get; set; }
    }
}
