using API.Data;
using Microsoft.EntityFrameworkCore;
using ModelTest.Controllers;

public class GetByIdCustomerService
{
    private readonly ApplicationDbContext _context;

    public GetByIdCustomerService(ApplicationDbContext context)
    {
        _context = context;
    }


}
