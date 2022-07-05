using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;

[Route("api/expenses")]
[ApiController]
public class ExpenseController : Controller
{
    private readonly IExpenseService _service;

    public ExpenseController(IExpenseService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var expenses = _service.GetAll();
        return Ok(expenses);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var expense = _service.GetById(id);
        return Ok(expense);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateExpenseDTO expense)
    {
        _service.Create(expense);
        return Created($"/api/expenses",null);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] CreateExpenseDTO expense)
    {
        _service.Update(id, expense);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _service.Remove(id);
        return NoContent();
    }
}