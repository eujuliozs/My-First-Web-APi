using FirstWebApi.Data;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Storage.Internal;
using System.Collections.Generic;

namespace FirstWebApi.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly catalogoapidataContext _context;

        public CategoriesController(catalogoapidataContext context) 
        {
            _context = context;
        }

        [HttpGet("Products")]
        public ActionResult<IEnumerable<Category>> GetCategoriesProucts()
        {
            List<Category> category = _context.Categories.Include(cat => cat.Products).AsNoTracking().ToList();
            if (category == null)
            {
                BadRequest("Category is null");
            }
            return Ok(category);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Category>> Get()
        {
            List<Category> categories = new();
            try
            {
                categories = _context.Categories.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred while attending your request");
            }
            return Ok(categories);
        }

        [HttpGet("{id:int}", Name ="GetCategory")]
        public ActionResult<Category> GetById(int? id)
        {
            Category category = new();
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                category = _context.Categories.AsNoTracking().FirstOrDefault(category => category.Id == id);
                if (category is null)
                {
                    return NotFound("No Category with this id");
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "A problem ocurred while attending your request");
            }
            return Ok(category);
        }

        [HttpPost]
        public ActionResult Post(Category category)
        {
            if(category is null)
            {
                return BadRequest();
            }
            _context.Add(category);
            return Ok();
        }

        [HttpPut]
        public ActionResult Put(int? id, Category category) 
        {
            if(id != category.Id  || id == null || category.Id == null)
            {
                return BadRequest();
            }
            _context.Categories.Update(category);
            _context.SaveChanges();
            return CreatedAtAction("GetById", new {id=category.Id}, category);
        
        }

        [HttpDelete]
        public ActionResult Delete(int? id) 
        {
            if(id == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
