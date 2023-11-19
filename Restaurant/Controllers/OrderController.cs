using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurant.DAL;
using Restaurant.IRepository;
using Restaurant.Models;
using Restaurant.Models.DTO;

namespace Restaurant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public OrderController(
            IOrderRepository orderRepository,
            UserManager<ApplicationUser> userManager,
            IMapper mapper)
        {
            this._orderRepository = orderRepository;
            this._userManager = userManager;
            this._mapper = mapper;
        }


        [HttpGet("CurrentUser")]
        private ApplicationUser CurrentUser()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = _userManager.FindByIdAsync(userId).Result;
            return currentUser;
        }




        // GET: api/Order
        [HttpGet("Orders")]
        public IActionResult Orders()
        {
            var orders = _orderRepository.GetAll();
            return Ok(orders);
        }

        // GET: api/Order/5
        [HttpGet("Details/{id}")]
        public IActionResult Details(int id)
        {
            var order = _orderRepository.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        [HttpGet("OrdersForUser")]
        public IActionResult OrdersForUser(string userId)
        {
            var order = _orderRepository.GetByUserId(userId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }
        [HttpGet("OdersForMeal/{mealId:int}")]
        public IActionResult OdersForMeal(int mealId)
        {
            var order = _orderRepository.GetByMealId(mealId);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        [HttpPost("BuyOrder")]
        public async Task<IActionResult> BuyOrder(OrderDto orderDto)
        {
            if (ModelState.IsValid)
            {
               
                Order order = _mapper.Map<Order>(orderDto);

                order.OrderDate = DateTime.Now;
                order.UserId = CurrentUser().Id;

                await _orderRepository.Add(order);
                return Ok(order);
            }
            return BadRequest(orderDto);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> Edit([FromRoute] int id, OrderUpdateDto orderDto)
        {

            Order oldOrder = _orderRepository.GetById(id);

            if (oldOrder == null)
            {
                return NotFound();
            }

            try
            {
                _mapper.Map( oldOrder, orderDto);

                oldOrder.OrderDate = DateTime.Now;
                oldOrder.UserId = CurrentUser().Id;

                await _orderRepository.Update(oldOrder);
                return Ok(orderDto);

            }
            catch (DbUpdateConcurrencyException ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // DELETE: api/Order/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var order = _orderRepository.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            await _orderRepository.Delete(order.Id);


            return NoContent();
        }


    }
}
