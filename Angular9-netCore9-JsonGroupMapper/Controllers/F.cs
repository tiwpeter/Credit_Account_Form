using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

[Route("api/BusinessType")]
[ApiController]
public class BusinessTypeController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public BusinessTypeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/BusinessType
    [HttpGet]
    public async Task<ActionResult<IEnumerable<BusinessTypeDto>>> GetBusinessTypes()
    {
        var list = await _context.BusinessType
            .Select(bt => new BusinessTypeDto
            {
                busiTypeID = bt.busiTypeID,
                busiTypeCode = bt.busiTypeCode,
                busiTypeName = bt.busiTypeName,
                busiTypeDes = bt.busiTypeDes,
                RegistrationDate = bt.RegistrationDate,
                RegisteredCapital = bt.RegisteredCapital, // ui to nath

            })
            .ToListAsync();

        return list;
    }





}
