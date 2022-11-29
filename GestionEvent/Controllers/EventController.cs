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

        [HttpGet("seeFutur")]
        [Authorize("connected")]
        public IActionResult SeeFuturEvents()
        {
            return Ok(_eventService.SeeFuturEvent());
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

        [HttpDelete("delete")]
        [Authorize("connected")]
        public IActionResult DeleteEvent(EventDeleteOrCancelViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                _eventService.DeleteOneOfMyEvent(form.EventId, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("cancel")]
        [Authorize("connected")]
        public IActionResult CancelEvent(EventDeleteOrCancelViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                _eventService.CancelOneOfMyEvent(form.EventId, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("uncancel")]
        [Authorize("connected")]
        public IActionResult UnCancelEvent(EventDeleteOrCancelViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                _eventService.UnCancelOneOfMyEvent(form.EventId, id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch("update")]
        [Authorize("connected")]
        public IActionResult UpdateProfile(EventUpdateViewModel form)
        {
            {
                if (!ModelState.IsValid) return BadRequest();
                try
                {
                    ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                    int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                    _eventService.UpdateEvent(form.ToBLL(), id);
                    return Ok();
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

        }
    }
}
