using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.DTOModels;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;

[Route("api/income/categories")]
[ApiController]
public class IncomeCategoryController : Controller
{
    private readonly IIncomeCategoryService _service;

    public IncomeCategoryController(IIncomeCategoryService service)
    {
        _service = service;
    }
    
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var incomesCategories = _service.GetAll();
        return Ok(incomesCategories);
    }
    
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var incomeCategory = _service.GetById(id);
        return Ok(incomeCategory);
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] CreateIncomeCategoryDTO incomeCategory)
    {
        _service.Create(incomeCategory);
        return Created($"/api/income/categories",null);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] CreateIncomeCategoryDTO incomeCategory)
    {
        _service.Update(id, incomeCategory);
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public ActionResult Delete([FromRoute] int id)
    {
        _service.Remove(id);
        return NoContent();
    }
}