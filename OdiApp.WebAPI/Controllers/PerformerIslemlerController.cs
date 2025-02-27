using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Services.IslemlerLogicServices.PerformerIslemler;
using OdiApp.DTOs.IslemlerDTOs.PerformerIslemler;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.WebAPI.Controllers
{
    [Route("api")]
    [ApiController]

    public class PerformerIslemlerController : ControllerBase
    {
        private readonly IPerforlerIslemlerLogicService _performerIslemServis;

        public PerformerIslemlerController(IPerforlerIslemlerLogicService performerIslemServis)
        {
            _performerIslemServis = performerIslemServis;
        }

        /// <summary>
        /// Performer a gelen işlem talepleri listesi
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("performer-islem-listesi")]
        //[YapimAuthorize]
        public async Task<IActionResult> ProjeOnerilenPerformerList(PerformerIdDTO id)
        {
            return Ok(await _performerIslemServis.PerformerIslemListesi(id));
        }
        [HttpPost("menajer-proje-islem")]
        public async Task<IActionResult> MenajerProjeIslem(MenajerIslemInputDTO input)
        {
            return Ok(await _performerIslemServis.MenajerProjeIslem(input));
        }

    }
}
