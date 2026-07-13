using CreditAccountApi.DbContext;
using CreditAccountApi.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CreditAccountApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoItemsController : ControllerBase
{
    private readonly CreditAccountDbContext _db;

    public TodoItemsController(CreditAccountDbContext db)
    {
        _db = db;
    }

    // GET: api/todoitems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoItem>>> GetAll()
    {
        var items = await _db.TodoItems
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
        return Ok(items);
    }

    // GET: api/todoitems/5
    [HttpGet("{id}")]
    public async Task<ActionResult<TodoItem>> GetById(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();
        return Ok(item);
    }

    // POST: api/todoitems
    [HttpPost]
    public async Task<ActionResult<TodoItem>> Create(TodoItem input)
    {
        var item = new TodoItem
        {
            Title = input.Title,
            Description = input.Description,
            IsCompleted = false,
            CreatedAt = DateTime.UtcNow
        };

        _db.TodoItems.Add(item);
        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    // PUT: api/todoitems/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TodoItem input)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();

        item.Title = input.Title;
        item.Description = input.Description;
        item.IsCompleted = input.IsCompleted;
        item.CompletedAt = input.IsCompleted && item.CompletedAt is null
            ? DateTime.UtcNow
            : item.CompletedAt;

        await _db.SaveChangesAsync();
        return NoContent();
    }

    // PATCH: api/todoitems/5/toggle
    [HttpPatch("{id}/toggle")]
    public async Task<IActionResult> ToggleComplete(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();

        item.IsCompleted = !item.IsCompleted;
        item.CompletedAt = item.IsCompleted ? DateTime.UtcNow : null;

        await _db.SaveChangesAsync();
        return Ok(item);
    }

    // DELETE: api/todoitems/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.TodoItems.FindAsync(id);
        if (item is null) return NotFound();

        _db.TodoItems.Remove(item);
        await _db.SaveChangesAsync();
        return NoContent();
    }
}