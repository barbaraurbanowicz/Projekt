using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;

[Route("api/expense/categories")]
[ApiController]
public class ExpenseCategoryController : Controller
{
    private readonly IExpenseCategoryService _service;

    public ExpenseCategoryController(IExpenseCategoryService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var expenseCategories = _service.GetAll();
        return Ok(expenseCategories);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var expenseCategory = _service.GetById(id);
        return Ok(expenseCategory);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ExpenseCategory expenseCategory)
    {
        _service.Create(expenseCategory);
        return Created($"/api/expense/categories",null);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] ExpenseCategory expenseCategory)
    {
        _service.Update(id, expenseCategory);
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _service.Remove(id);
        return NoContent();

    }
}