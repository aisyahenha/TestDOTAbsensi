using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TestAbsensi.DTO;
using TestAbsensi.Models;
using TestAbsensi.Services.Karyawan;

namespace TestAbsensi.Controllers
{
    [Authorize(Roles = "ADMIN")]
    [ApiController]
    [Route("api/karyawan")]
    public class KaryawanController : ControllerBase
    {
        
        private readonly IKaryawanService _karyawanService;
        public KaryawanController(IKaryawanService karyawanService)
        {
            _karyawanService = karyawanService;
        }

        // GET: KaryawanAll
        [HttpGet("get-all")]
        public async Task<Response<IEnumerable<KaryawanModel>>> GetAll()
        {
            IEnumerable<KaryawanModel> result = await _karyawanService.GetAll();
            return new Response<IEnumerable<KaryawanModel>>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        // GET: KaryawanController/Details/5
        [HttpGet("detail/{id}")]
        public async Task<Response<KaryawanModel>> Details(int id)
        {
            KaryawanModel result = await _karyawanService.GetById(id);
            return new Response<KaryawanModel>()
            {
                Status = true,
                Message = "Sukses",
                Data = result
            };
        }

        // POST: KaryawanController/Create
        [HttpPost("create")]
        public async Task<Response<CreateKaryawanViewModel>> Create(CreateKaryawanViewModel payload)
        {
            CreateKaryawanViewModel result = await _karyawanService.Create(payload);
            return new Response<CreateKaryawanViewModel>()
            {
                Status = true,
                Message = "Create Sukses",
                Data = result
            };
        }

        // PATCH: Update
        [HttpPatch("update")]
        public async Task<Response<KaryawanModel>> Update(UpdateKaryawanViewModel payload)
        {
            KaryawanModel result = await _karyawanService.Update(payload);
            return new Response<KaryawanModel>()
            {
                Status = true,
                Message = "Update Sukses",
                Data = result
            };
        }

        // DELETE
        [HttpDelete("delete")]
        public async Task<Response<int>> Delete(int id)
        {
            int result = await _karyawanService.Delete(id);
            return new Response<int>()
            {
                Status = true,
                Message = "Delete Sukses",
                Data = result
            };
        }



        }
}
