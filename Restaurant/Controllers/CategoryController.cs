using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Restaurant.IRepository;
using Restaurant.Models;
using Restaurant.Models.DTO;


namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
           _categoryRepository = categoryRepository;
            this._mapper = mapper;
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
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Post([FromForm] CategoryDto categoryDto)
        {
            string image = await ProcessImageUpload(categoryDto.Image);
            
            Category category = _mapper.Map<Category>(categoryDto);
            category.Image = image;




            await _categoryRepository.Add(category);
            return Ok(category);
        }



        // PUT api/Category/5
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Put([FromRoute] int id,[FromForm] CategoryUpdateDto categoryDto)
        {
           Category oldCategory =  await _categoryRepository.GetById(id);

            //_mapper.Map<Category>(categoryDto);
            string image = await ProcessImageUpload(categoryDto.Image);


            oldCategory.Description =!string.IsNullOrEmpty(categoryDto.Description)? categoryDto.Description : oldCategory.Description ;
            oldCategory.Name = !string.IsNullOrEmpty(categoryDto.Name)? categoryDto.Name : oldCategory.Name;
            oldCategory.Image =!string.IsNullOrEmpty(image)? image : oldCategory.Image; 

            await _categoryRepository.Update(oldCategory);
            return Ok(oldCategory);
        }

        // DELETE api/Category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryRepository.Delete(id);
            return Ok();
        }



        [HttpGet("UplaodImage")]
        public async Task<string> ProcessImageUpload(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                return null;
            }
            else
            {
                var fileName = Guid.NewGuid().ToString() + "_" + imageFile.FileName;
                //var fileName = Path.GetFileName(imageFile.FileName);
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
