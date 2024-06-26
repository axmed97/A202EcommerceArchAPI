﻿using Business.Abstract;
using Entities.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.V1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [MapToApiVersion("1.0")]
        [HttpPost("[action]")]
        public IActionResult CreateCategory([FromBody]CategoryAddDTO categoryAddDTO)
        {
            var result = _categoryService.AddCategory(categoryAddDTO);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
