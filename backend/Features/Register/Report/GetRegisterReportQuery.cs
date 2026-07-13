using CreditAccountApi.Common.Exceptions;
using CreditAccountApi.Features.Register.Get;
using FastReport;
using FastReport.Export.PdfSimple;
using MediatR;

namespace CreditAccountApi.Features.Register.Report;

// ============================================================
// Query — รับ id เข้ามา (คนละตัวกับ GetRegisterQuery)
// ============================================================
public class GetRegisterReportQuery : IRequest<byte[]>
{
    public int Id { get; set; }
}

// ============================================================
// Handler — ไม่ query DB เอง แต่เรียก GetRegisterQuery ผ่าน MediatR
// แล้วเอาผลลัพธ์ไปสร้าง PDF ด้วย FastReport
// ============================================================
public class GetRegisterReportHandler : IRequestHandler<GetRegisterReportQuery, byte[]>
{
    private readonly IMediator _mediator;
    private readonly IWebHostEnvironment _env;

    public GetRegisterReportHandler(IMediator mediator, IWebHostEnvironment env)
    {
        _mediator = mediator;
        _env = env;
    }

    public async Task<byte[]> Handle(
        GetRegisterReportQuery request,
        CancellationToken cancellationToken)
    {
        // 1) ขอข้อมูลจาก GetRegisterHandler ผ่าน mediator (reuse logic เดิม ไม่ query ซ้ำเอง)
        var registerData = await _mediator.Send(
            new GetRegisterQuery { Id = request.Id }, cancellationToken);

        if (registerData is null)
            throw new NotFoundException(nameof(GetRegisterResponse), request.Id);

        // 2) โหลด template .frx
        var reportPath = Path.Combine(_env.ContentRootPath, "Reports", "RegisterReport.frx");

        using var report = new FastReport.Report();

        // ต้อง add assembly reference ก่อน Load เพราะ .frx อ้าง type
        // CreditAccountApi.Features.Register.Get.GetRegisterResponse แบบ full namespace
        report.ReferencedAssemblies = report.ReferencedAssemblies
            .Append(typeof(GetRegisterResponse).Assembly.Location)
            .ToArray();

        report.Load(reportPath);

        // 3) ส่ง object เข้าไปแทนที่ [RegisterData.xxx] ใน .frx
        // ชื่อ "RegisterData" ต้องตรงกับ Name="RegisterData" ใน <BusinessObjectDataSource>
        report.RegisterData(
            new List<GetRegisterResponse> { registerData },
            "RegisterData");

        report.GetDataSource("RegisterData")!.Enabled = true;

        // 4) Prepare + Export เป็น PDF
        report.Prepare();

        using var pdfExport = new PDFSimpleExport();
        using var ms = new MemoryStream();
        report.Export(pdfExport, ms);

        return ms.ToArray();
    }
}
