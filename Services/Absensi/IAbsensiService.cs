using TestAbsensi.DTO;
using TestAbsensi.Models;

namespace TestAbsensi.Services.Absensi
{
    public interface IAbsensiService
    {
        Task<IEnumerable<AbsensiModel>> GetAll();
        Task<AbsensiModel>? GetById(int id);
        Task<IEnumerable<AbsensiModel>> GetByKaryawanId(int id);
        Task<IEnumerable<AbsensiModel>> GetByDate(DateOnly dateIn);
        Task<AbsensiModel> Create(int karyawanId);
        Task<AbsensiModel>? ExitTime(int idKaryawan);
    }
}
