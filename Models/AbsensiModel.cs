using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestAbsensi.Models
{
    [Table("t_absensi")]
    public class AbsensiModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Id Karyawan Cannot ne empty!")]
        public KaryawanModel Karyawan { get; set; }
        public DateOnly DateIn { get; set; }
        public TimeOnly TimeIn { get; set; }
        public string StatusMasuk {  get; set; }
        public TimeOnly TimeOut { get; set; }
        public string StatusKeluar {  get; set; }


    }
}
