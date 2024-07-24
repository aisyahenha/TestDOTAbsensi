using TestAbsensi.DTO;
using TestAbsensi.Models;

namespace TestAbsensi.Services.Karyawan
{
    public interface IKaryawanService
    {
        Task<IEnumerable<KaryawanModel>> GetAll();
        Task<KaryawanModel>? GetById(int id);
        Task<KaryawanModel>? Update(UpdateKaryawanViewModel karyawan);
        Task<CreateKaryawanViewModel> Create(CreateKaryawanViewModel karyawan);
        
        Task<int> Delete(int id);
    
    }
}
