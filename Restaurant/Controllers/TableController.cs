using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.IRepository;
using Restaurant.Models;
using Restaurant.Models.DTO;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableRepository _tableRepository;
        private readonly IMapper _mapper;

        public TableController(
            ITableRepository tableRepository,
            IMapper mapper)
        {
            this._tableRepository = tableRepository;
            this._mapper = mapper;
        }


        [HttpGet("Tables")]
        public async Task<IActionResult> Tables()
        {
            try
            {
                var tables = await _tableRepository.GetAll();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                Table table = await _tableRepository.GetById(id);
                if (table == null)
                {
                    return NotFound();
                }
                return Ok(table);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddTable")]
        public async Task<IActionResult> PostTable([FromForm]TableDto tableDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Table table = _mapper.Map<Table>(tableDto); 
                    await _tableRepository.Add(table);

                    return Ok(table);

                }
                return BadRequest(tableDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("UpdateTable/{id}")]
        public async Task<IActionResult> Edit([FromRoute]int id, [FromForm] TableDto tableDto)
        {
            try
            {
                Table table = await _tableRepository.GetById(id);
                if(table == null)
                {
                    return NotFound();
                }
                else
                {
                    _mapper.Map(tableDto , table);
                    table.TableName = !string.IsNullOrEmpty(tableDto.TableName) ? tableDto.TableName : table.TableName;
                    return Ok(table);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Table table = await _tableRepository.GetById(id);
                if (table == null)
                {
                    return NotFound();
                }
                else
                {
                    await _tableRepository.Delete(table.Id);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



    }

}
