using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestAbsensi.DTO;
using TestAbsensi.Models;
using TestAbsensi.Services.Absensi;
using TestAbsensi.Services.Karyawan;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAbsensi.Controllers
{
    [Route("api/absesnsi")]
    [ApiController]
    public class AbsensiController : ControllerBase
    {
        
        private readonly IAbsensiService _absensiService;
        public AbsensiController(IAbsensiService absensiService)
        {
            _absensiService = absensiService;
        }

        // GET: All 
        [Authorize(Roles = "ADMIN")]
        [HttpGet("get-all")]
        public async Task<Response<IEnumerable<AbsensiModel>>> GetAll()
        {
            IEnumerable<AbsensiModel> result = await _absensiService.GetAll();
            
            return new Response<IEnumerable<AbsensiModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        [Authorize(Roles = "ADMIN")]
        // GET: KaryawanController/Details/5
        [HttpGet("detail/{id}")]
        public async Task<Response<AbsensiModel>> Details(int id)
        {
            AbsensiModel result = await _absensiService.GetById(id);
            return new Response<AbsensiModel>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        [Authorize(Roles = "ADMIN")]
        //GET BY KARYAWAN ID
        [HttpGet("detail-karyawan/{id}")]
        public async Task<Response<IEnumerable<AbsensiModel>>> KaryawanDetails(int id)
        {
            IEnumerable <AbsensiModel> result = await _absensiService.GetByKaryawanId(id);
            return new Response<IEnumerable<AbsensiModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        
        // POST: Create
        [HttpPost("absen-in")]
        public async Task<Response<AbsensiModel>> Create(int idKaryawan)
        {
            AbsensiModel result = await _absensiService.Create(idKaryawan);
            return new Response<AbsensiModel>()
            {
                Status = true,
                Message = "Create Sukses",
                Data = result
            };
        }
        // post: time Out
        [HttpPost("absen-out/{idKaryawan}")]
        public async Task<Response<AbsensiModel>> ExitTime(int idKaryawan)
        {
            AbsensiModel result = await _absensiService.ExitTime(idKaryawan);
            return new Response<AbsensiModel>()
            {
                Status = true,
                Message = "Update Sukses",
                Data = result
            };
        }

    }
}
