using FirstWebApi.Data;
using FirstWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController] 
    public class ProductsController : ControllerBase
    {
        private readonly catalogoapidataContext _context;
        public ProductsController(catalogoapidataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Product>> Get() 
        {
            IEnumerable<Product> products = _context.Products.AsNoTracking().ToList();
            if(products is null)
            {
                return NotFound();
            }
            return Ok(products);
        }
        [HttpGet("{id:int}", Name ="GetById")]
        public ActionResult<Product> Get(int id) 
        {
            Product product = _context.Products.Where(pd => pd.Id == id).AsNoTracking().SingleOrDefault();
            if(product is null)
            {
                return NotFound("Product Not Found");
            }
            return Ok(product);
        }

        [HttpPost]

        public ActionResult Post(Product? product)
        {
            if(product is null)
            {
                return BadRequest();
            }
            try
            {
                product.Moment = DateTime.Now;
                _context.Products.Add(product);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return new CreatedAtRouteResult("GetById", new {id=product.Id}, product);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int? id, Product product)
        {
            if(id != product.Id)
            {
                return BadRequest();
            }
            try
            {
                product.Moment = DateTime.Now;
                _context.Products.Update(product);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            } 
            
            return Ok(product);
        }
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int? id)
        {
            Product product = _context.Products.SingleOrDefault(pd => pd.Id == id);
            if(product is null)
            {
                return NotFound("No Product with this Id");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok(product);
        }
    }
}
