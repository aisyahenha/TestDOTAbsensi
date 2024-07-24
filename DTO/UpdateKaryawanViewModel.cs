using System.ComponentModel.DataAnnotations;

namespace TestAbsensi.DTO
{
    public class UpdateKaryawanViewModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(10)]
        public string NIK { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Position { get; set; }
    }

}