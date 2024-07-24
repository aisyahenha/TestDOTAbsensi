using System.Diagnostics;
using TestAbsensi.DTO;
using TestAbsensi.Exeptions;
using TestAbsensi.Models;
using TestAbsensi.Repositories;

namespace TestAbsensi.Services.Karyawan
{
    public class KaryawanService : IKaryawanService
    {
        private readonly IRepository<KaryawanModel> _repository;
        public KaryawanService(IRepository<KaryawanModel> repository)
        {
            _repository = repository;
        }
        public async Task<CreateKaryawanViewModel> Create(CreateKaryawanViewModel payload)
        {
            try
            {
                var karyawan = new KaryawanModel()
                {
                    NIK = payload.NIK,
                    Name = payload.Name,
                    Position = payload.Position,
                    Address = payload.Address,
                    CratedAt = DateTime.UtcNow
                };
                await _repository.Save(karyawan);
                return payload;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<int> Delete(int id)
        {
            try
            {
                var user = await GetById(id);

                await _repository.Delete(user);
                return id;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<KaryawanModel>> GetAll()
        {
            try
            {
                return await _repository.FindAll();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<KaryawanModel>? GetById(int id)
        {
            try
            {
                var result = await _repository.FindById(id);
                if (result == null)
                    throw new NotFound("Karyawan tidak ditemukan");

                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<KaryawanModel>? Update(UpdateKaryawanViewModel payload)
        {

            try
            {

                var karyawanInfo = await GetById(payload.Id);
                _repository.detach(karyawanInfo);
                var karyawan = new KaryawanModel()
                {
                    Id = payload.Id,
                    NIK = payload.NIK,
                    Name = payload.Name,
                    Position = payload.Position,
                    Address = payload.Address,
                    UpdatedAt = DateTime.UtcNow,
                    CratedAt = karyawanInfo.CratedAt
                };

                return await _repository.Update(karyawan);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }


    }
}
