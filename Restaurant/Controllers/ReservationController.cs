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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public ReservationController(
            IReservationRepository reservationRepository,
            IMapper mapper,
            UserManager<ApplicationUser> userManager)
        {
            this._reservationRepository = reservationRepository;
            this._mapper = mapper;
            this._userManager = userManager;
        }


        private ApplicationUser CurrentUser()
        {
            string userId = User.Claims.FirstOrDefault(a => a.Type == (ClaimTypes.NameIdentifier)).Value;
            ApplicationUser currentUser = _userManager.FindByIdAsync(userId).Result;
            return currentUser;
        }



        // GET: api/Reservation
        [HttpGet("Reservations")]
        public async Task<IActionResult> Reservations()
        {
            try
            {
                return Ok(await _reservationRepository.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Reservation/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetById(id);

                if (reservation == null)
                {
                    return NotFound();
                }

                return Ok(reservation);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("ReserveTable")]
        public async Task<IActionResult> ReserveTable([FromForm]ReservationDto reservationDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Reservation reservation = _mapper.Map<Reservation>(reservationDto);
                    reservation.UserId = CurrentUser().Id;

                    await _reservationRepository.Add(reservation);

                    return Ok(reservation);
                    //return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
                }
                return BadRequest(reservationDto);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeReservation([FromRoute] int id, [FromForm]ReservationUpdateDto reservationDto)
        {
            try
            {

                Reservation reservation = await _reservationRepository.GetById(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                else
                {
                    reservation.UserId = CurrentUser().Id;
                    reservation.PhoneNumber = !string.IsNullOrEmpty(reservationDto.PhoneNumber) ? reservationDto.PhoneNumber : reservation.PhoneNumber;
                    reservation.PersonsNumber = reservationDto.PersonsNumber.HasValue ? reservationDto.PersonsNumber.Value : reservation.PersonsNumber;
                    reservation.ReserveDate = reservationDto.ReserveDate.HasValue ? reservationDto.ReserveDate.Value : reservation.ReserveDate;
                    reservation.TableId = reservationDto.TableId.HasValue ? reservationDto.TableId.Value : reservation.TableId;

                    await _reservationRepository.Update(reservation);
                    return Ok(reservation);
                }

            }
            catch (DbUpdateConcurrencyException)
            {

            }

            return NoContent();
        }


        // DELETE: api/Reservation/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _reservationRepository.GetById(id);
                if (reservation == null)
                {
                    return NotFound();
                }
                else
                {
                   await _reservationRepository.Delete(reservation.Id);
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
