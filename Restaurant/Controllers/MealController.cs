using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.IRepository;
using Restaurant.IRepository.Repository;
using Restaurant.Models;
using Restaurant.Models.DTO;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MealController : ControllerBase
    {
        private readonly IMealRepository _mealRepository;

        public MealController(IMealRepository mealRepository)
        {
            _mealRepository = mealRepository;
        }




        // GET api/Meal
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var meals = await _mealRepository.GetAll();
            return Ok(meals);
        }

        // GET api/Meal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var meal = await _mealRepository.GetById(id);
            return Ok(meal);
        }

        // POST api/Meal
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MealDTO mealDto)
        {
            Meal meal = new Meal()
            {
                Name = mealDto.Name,
                CategoryId = mealDto.CategoryId,
                Price = mealDto.Price,
                Description = mealDto.Description,
            };

            await _mealRepository.Add(meal);
            return Ok(meal);
        }

        // PUT api/Meal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MealDTO mealDto)
        {
            Meal oldMeal = await _mealRepository.GetById(id);

            oldMeal.Name = mealDto.Name;
            oldMeal.Description = mealDto.Description;
            oldMeal.CategoryId = mealDto.CategoryId;
            oldMeal.Price = mealDto.Price;

            await _mealRepository.Update(oldMeal);
            return Ok(oldMeal);
        }

        // DELETE api/Meal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mealRepository.Delete(id);
            return Ok();
        }


    }
}
