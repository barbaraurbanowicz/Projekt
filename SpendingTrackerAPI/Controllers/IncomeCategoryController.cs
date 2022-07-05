﻿using Microsoft.AspNetCore.Mvc;
using SpendingTrackerAPI.Entities;
using SpendingTrackerAPI.Services;

namespace SpendingTrackerAPI.Controllers;

[Route("api/income/categories")]
[ApiController]
public class IncomeCategoryController : Controller
{
    private readonly IncomeCategoryService _service;

    public IncomeCategoryController(IncomeCategoryService service)
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
    public IActionResult Create([FromBody] IncomeCategory incomeCategory)
    {
        _service.Create(incomeCategory);
        return Created($"/api/incomes/categories",null);
    }
    
    [HttpPut("{id}")]
    public IActionResult Update([FromRoute]int id, [FromBody] IncomeCategory incomeCategory)
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