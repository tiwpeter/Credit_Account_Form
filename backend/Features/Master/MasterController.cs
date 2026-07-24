using CreditAccountApi.Features.Master.Address;
using CreditAccountApi.Features.Master.All;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAccountApi.Features.Master;

[ApiController]
[Route("api/master")]
public class MasterController : ControllerBase
{
    private readonly IMediator _mediator;

    public MasterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // ============================================================
    // GET /api/master/all
    // ทุก Dropdown พร้อมกัน (ยกเว้น Cascade)
    // ============================================================
    [HttpGet("all")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(new GetAllMasterQuery(), cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // ============================================================
    // GET /api/master/amphures?provinceId=1
    // Cascade — อำเภอตามจังหวัด
    // ============================================================
    [HttpGet("amphures")]
    public async Task<IActionResult> GetAmphures(
        [FromQuery] int provinceId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetAmphuresQuery { ProvinceId = provinceId }, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    // ============================================================
    // GET /api/master/tambons?amphureId=1
    // Cascade — ตำบลตามอำเภอ + ZipCode
    // ============================================================
    [HttpGet("tambons")]
    public async Task<IActionResult> GetTambons(
        [FromQuery] int amphureId,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _mediator.Send(
                new GetTambonsQuery { AmphureId = amphureId }, cancellationToken);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }
}
