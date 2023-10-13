using Microsoft.AspNetCore.Mvc;
using Restaurant.IRepository;
using Restaurant.Models;


namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
           _categoryRepository = categoryRepository;
        }




        // GET api/Category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryRepository.GetAll();
            return Ok(categories);
        }

        // GET api/Category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           var category = await _categoryRepository.GetById(id);
            return Ok(category);
        }

        // POST api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            await _categoryRepository.Add(category);
            return Ok(category);
        }

        // PUT api/Category/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id,[FromBody] Category category)
        {
            await _categoryRepository.GetById(id);
            await _categoryRepository.Update(category);
            return Ok(category);
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.Delete(id);
            return Ok();
        }
    }
}
