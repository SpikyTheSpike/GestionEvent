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
    [Route("api/inscription")]
    public class InscritpionController :  ControllerBase
    {
         private readonly IInscriptionService _inscriptionService;
         private readonly TokenManager _tokenManager;

        public InscritpionController(IInscriptionService inscriptionService, TokenManager tokenManager)
        {
            _inscriptionService = inscriptionService;
            _tokenManager = tokenManager;
        }

        [HttpPost("join")]
        [Authorize("connected")]
        public IActionResult Join(InscriptionViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                _inscriptionService.createInscription(form.ToBLL(), id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("getInscriptions")]
        [Authorize("connected")]
        public IActionResult getInscriptions()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                if (id == 1002)
                {
                    IEnumerable<Inscription> liste = _inscriptionService.getAllInscription();
                    return Ok(liste);
                }
             
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("seeMine")]
        [Authorize("connected")]
        public IActionResult SeeMine()
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                return Ok(_inscriptionService.getInscriptionByMember(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("SeeMineEventInscription")]
        [Authorize("connected")]
        public IActionResult SeeMineEventInscription(SeeInscriptionViewModel f)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                return Ok(_inscriptionService.getInscriptionList(f.Event_Id, id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("deleteAdmin")]
        [Authorize("connected")]
        public IActionResult DeleteEventAsAdmin(int ide)
        {
            if (!ModelState.IsValid) return BadRequest();
            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                if (id == 1002)
                {
                    _inscriptionService.DeleteAdmin(ide);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


    }
}
