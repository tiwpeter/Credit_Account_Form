using CreditAccountApi.Features.Register.Get;
using CreditAccountApi.Features.Register.GetAll;
using CreditAccountApi.Features.Register.Report;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditAccountApi.Controllers;

[ApiController]
[Route("api/register")]
public class RegisterController : ControllerBase
{
    private readonly IMediator _mediator;

    public RegisterController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET /api/register?page=1&pageSize=20&search=abc
    // คืนรายการ register ทั้งหมดแบบมี paging (ใช้ GetAllRegistersQuery/Handler)
    [HttpGet]
    public async Task<ActionResult<GetAllRegistersResponse>> GetAllRegisters(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20,
        [FromQuery] string? search = null,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(
            new GetAllRegistersQuery { Page = page, PageSize = pageSize, Search = search }, ct);
        return Ok(result);
    }

    // GET /api/register/{id}
    // คืนข้อมูล register แบบ JSON (ใช้ GetRegisterQuery/Handler)
    [HttpGet("{id}")]
    public async Task<ActionResult<GetRegisterResponse>> GetRegister(int id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetRegisterQuery { Id = id }, ct);
        return Ok(result);
    }

    // GET /api/register/{id}/report
    // คืนไฟล์ PDF report (ใช้ GetRegisterReportQuery/Handler ซึ่งเรียก GetRegisterQuery ต่ออีกที)
    [HttpGet("{id}/report")]
    public async Task<IActionResult> GetRegisterReport(int id, CancellationToken ct)
    {
        var pdfBytes = await _mediator.Send(new GetRegisterReportQuery { Id = id }, ct);
        return File(pdfBytes, "application/pdf", $"register-{id}.pdf");
    }
}