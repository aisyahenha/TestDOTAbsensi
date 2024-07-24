using Microsoft.EntityFrameworkCore.Storage.Json;
using System.Diagnostics;
using TestAbsensi.DTO;
using TestAbsensi.Exeptions;
using TestAbsensi.Models;
using TestAbsensi.Repositories;
using TestAbsensi.Services.Karyawan;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestAbsensi.Services.Absensi
{
    public class AbsensiService : IAbsensiService
    {
        private readonly IRepository<AbsensiModel> _repository;
        private readonly IKaryawanService _karyawanService;
        private readonly TimeOnly JADWAL_MASUK = new TimeOnly(8,0);
        private readonly TimeOnly JADWAL_KELUAR = new TimeOnly(17, 0);
        private const int TOLERANSI_MASUK = 15;
        private const int TOLERANSI_KELUAR = 30;
        public AbsensiService(IRepository<AbsensiModel> repository, IKaryawanService karyawanService)
        {
            _repository = repository;
            _karyawanService = karyawanService;
        }
        public async Task<AbsensiModel> Create(int karyawanId)
        {
            try
            {
                var karyawan = await _karyawanService.GetById(karyawanId);
                var absensiInfo = await _repository.FindBy(obj => obj.Karyawan.Id == karyawanId && obj.DateIn == DateOnly.FromDateTime(DateTime.Now));
                if (absensiInfo != null) throw new BadRequest("Karyawan sudah absen masuk");

                var waktuMasuk = TimeOnly.FromDateTime(DateTime.Now);
                string statusMasuk;
                TimeSpan selisih = waktuMasuk - JADWAL_MASUK;
                if (selisih.TotalMinutes < 0)
                {
                    statusMasuk = "Datang Lebih Awal";
                }
                else if (selisih.TotalMinutes >= 0 && selisih.TotalMinutes <= TOLERANSI_MASUK)
                {
                    statusMasuk = "Tepat Waktu";
                }
                else { statusMasuk = "Terlambat"; }

                var absensi = new AbsensiModel()
                {
                    Karyawan = karyawan,
                    DateIn = DateOnly.FromDateTime(DateTime.Now),
                    TimeIn = waktuMasuk,
                    StatusMasuk= statusMasuk,
                    StatusKeluar = ""
            };
                var result = await _repository.Save(absensi);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<AbsensiModel>? ExitTime(int idKaryawan)
        {
            try
            {
                var karyawanInfo = await _karyawanService.GetById(idKaryawan);
                var absensiInfo = await _repository.FindBy(obj => obj.Karyawan.Id == idKaryawan && obj.DateIn == DateOnly.FromDateTime(DateTime.Now));
                if (absensiInfo == null)  throw new BadRequest("Karyawan belum absen masuk"); ;
                if (absensiInfo.StatusKeluar != "") throw new BadRequest("Karyawan sudah absen keluar"); ;
              
                
                var waktuKeluar = TimeOnly.FromDateTime(DateTime.Now);
                string statusKeluar;
                TimeSpan selisih = waktuKeluar - JADWAL_KELUAR;
                if (selisih.TotalMinutes < 0)
                {
                    statusKeluar = "Pulang Lebih Awal";
                } else if (selisih.TotalMinutes >= 0 && selisih.TotalMinutes <= TOLERANSI_KELUAR)
                {
                    statusKeluar = "Tepat Waktu";
                }
                else { statusKeluar = "Overtime"; }


                var absensi = new AbsensiModel()
                {
                    Karyawan = absensiInfo.Karyawan,
                    Id = absensiInfo.Id,
                    DateIn = absensiInfo.DateIn,
                    TimeIn = absensiInfo.TimeIn,
                    TimeOut = waktuKeluar,
                    StatusMasuk= absensiInfo.StatusMasuk,
                    StatusKeluar = statusKeluar
                   
                };
                _repository.detach(absensiInfo);
                var result = await _repository.Update(absensi);
                return result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AbsensiModel>> GetAll()
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

        public async Task<IEnumerable<AbsensiModel>> GetByDate(DateOnly dateIn)
        {
            try
            {
                if(dateIn > DateOnly.FromDateTime(DateTime.Now)) 
                    throw new BadRequest("Tanggal Input tidak boleh Lebih Besar dari Hari Ini!");
                return await _repository.FindByGroup(absensi => absensi.DateIn == dateIn);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<AbsensiModel>? GetById(int id)
        {
            try
            {
                var result = await _repository.FindById(id);
                if (result == null)
                    throw new NotFound("Absensi tidak ditemukan");

                return result;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

        public async Task<IEnumerable<AbsensiModel>> GetByKaryawanId(int id)
        {
            try
            {

                var result = await _repository.FindByGroup(absensi => absensi.Karyawan.Id == id);
                return result;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }

    }
}
