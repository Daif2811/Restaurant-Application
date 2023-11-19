using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
        private readonly ILogger<MealController> _logger;
        private readonly IMapper _mapper;

        public MealController(IMealRepository mealRepository, ILogger<MealController> logger, IMapper mapper)
        {
            _mealRepository = mealRepository;
            this._logger = logger;
            this._mapper = mapper;
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] MealDto mealDto)
        {
            try
            {
                string image = await ProcessImageUpload(mealDto.Image);

                Meal meal = _mapper.Map<Meal>(mealDto);

                meal.Image = image;

                await _mealRepository.Add(meal);
                return Ok(meal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/Meal/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromForm] MealUpdateDto mealDto)
        {
            Meal oldMeal = await _mealRepository.GetById(id);
            if (oldMeal == null)
            {
                return NotFound("Meal not found");
            }

            string image = await ProcessImageUpload(mealDto.Image);

            _mapper.Map<Meal>(mealDto);

            oldMeal.Image = !string.IsNullOrEmpty(image) ? image : oldMeal.Image;
            oldMeal.Name = !string.IsNullOrEmpty(mealDto.Name) ? mealDto.Name : oldMeal.Name;
            oldMeal.Description = !string.IsNullOrEmpty(mealDto.Description) ? mealDto.Description : oldMeal.Description;
            oldMeal.Price = mealDto.Price.HasValue ? mealDto.Price.Value : oldMeal.Price;
            oldMeal.CategoryId = mealDto.CategoryId ?? oldMeal.CategoryId;


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



        [HttpPost("UplaodImage")]
        private async Task<string> ProcessImageUpload(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }
            else
            {
                var fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                // var fileName = Path.GetFileName(imageFile.FileName);
                var filePath = Path.Combine("wwwroot/Meal_Images", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
                return fileName;
            }
        }


    }
}
