using BLL.Interfaces;
using BLL.Services;
using Domain.Entities;
using GestionEvent.Mappers;
using GestionEvent.Models;
using GestionEvent.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GestionEvent.Controllers
{

    [ApiController]
    [Route("api/Event")]
    public class EventController : ControllerBase
    {
        private readonly IEvenetService _eventService;
        private readonly TokenManager _tokenManager;

        public EventController(IEvenetService eventService, TokenManager tokenManager)
        {
            _eventService = eventService;
            _tokenManager = tokenManager;
        }

        [HttpGet("see")]
        public IActionResult SeeEvents()
        {
            return Ok(_eventService.SeeEveryEvent());
        }


        [HttpPost("post")]
        [Authorize("connected")]
        public IActionResult PostEvent(EventViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);
                
                _eventService.CreateNewEvent( form.ToBLL(), id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
