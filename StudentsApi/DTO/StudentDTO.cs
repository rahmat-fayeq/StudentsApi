using System.ComponentModel.DataAnnotations;

namespace StudentsApi.DTO
{
    public class StudentDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
        [Required]
        public double TutionFee { get; set; }
        [Required]
        public string Address { get; set; }
        [MaxLength(100)]
        public string? Remarks { get; set; }
    }
}
