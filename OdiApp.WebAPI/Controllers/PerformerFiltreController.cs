using Microsoft.AspNetCore.Mvc;
using OdiApp.BusinessLayer.Services.PerformerLogicServices.PerformerFiltre;
using OdiApp.DTOs.SharedDTOs.OrtakDTOs;

namespace OdiApp.WebAPI.Controllers;

[Route("api")]
[ApiController]
//[AllAuthorize]
public class PerformerFiltreController : ControllerBase
{
    private readonly IPerformerFiltreLogicService _logicService;
    public PerformerFiltreController(IPerformerFiltreLogicService logicService)
    {
        _logicService = logicService;
    }

    [HttpGet("proje-onerilen-performer-list")]
    public async Task<IActionResult> ProjeOnerilenPerformerList()
    {
        return Ok(await _logicService.ProjeOnerilenOyuncular());
    }

    [HttpPost("performer-detay-listesi")]
    public async Task<IActionResult> PerformerDetayListesi(List<PerformerIdDTO> idList)
    {
        return Ok(await _logicService.PerformerDetayListesi(idList));
    }
}