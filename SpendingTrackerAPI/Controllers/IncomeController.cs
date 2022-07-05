using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;

[Route("api/incomes")]
[ApiController]
public class IncomeController : Controller
{
    private readonly IIncomeService _service;

    public IncomeController(IIncomeService service)
    {
        _service = service;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var incomes = _service.GetAll();
        return Ok(incomes);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var income = _service.GetById(id);
        return Ok(income);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] Income income)
    {
        _service.Create(income);
        return Created($"/api/incomes",null);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] Income income)
    {
        _service.Update(id, income);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _service.Remove(id);
        return NoContent();
    }
}