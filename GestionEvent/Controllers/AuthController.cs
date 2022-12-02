using BLL.Interfaces;
using BLL.Services;
using Domain.Entities;
using GestionEvent.Mappers;
using GestionEvent.Models;
using GestionEvent.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;

namespace GestionEvent.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {

        private readonly IMemberService _memberService;
        private readonly TokenManager _tokenManager;
        public AuthController(IMemberService memberService, TokenManager tokenManager)
        {
            _memberService = memberService;
            _tokenManager = tokenManager;
        }

        
        [HttpPost("register")]

        public IActionResult Register(AuthRegisterViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                _memberService.Register(form.ToBLL());
                return Ok();
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
                    _memberService.DeleteAdmin(ide);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(AuthLoginViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                Member? connectedUser = _memberService.Login(form.Identifiant, form.Password);
                if (connectedUser == null) return BadRequest("Utilisateur inexistant");

                ConnectedUserDTO user = connectedUser.ToDTO();
                user.Token = _tokenManager.GenerateToken(connectedUser);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [HttpPost("loginAdmin")]
        public IActionResult LoginAdmin(AuthLoginViewModel form)
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                Member? connectedUser = _memberService.LoginAdmin(form.Identifiant, form.Password);
                if (connectedUser == null) return BadRequest("Utilisateur inexistant");

                ConnectedUserDTO user = connectedUser.ToDTO();
                user.Token = _tokenManager.GenerateToken(connectedUser);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpGet("getComptes")]
        [Authorize("connected")]
        public IActionResult listeCompte()
        {
            if (!ModelState.IsValid) return BadRequest();

            try
            {
                ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);
                if (id == 1002)
                {
                    IEnumerable<Member> connectedUser = _memberService.getListCompte();
                    if (connectedUser == null) return BadRequest("Utilisateur inexistant");
                    return Ok(connectedUser);
                }
                return BadRequest();


            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }


        [HttpPatch("update")]
        [Authorize("connected")]
        public IActionResult UpdateProfile(AuthRegisterViewModel form)
        {
            {
                if (!ModelState.IsValid) return BadRequest();
                try
                {
                    ClaimsIdentity identity = HttpContext.User.Identity as ClaimsIdentity;
                    int id = int.Parse(identity.Claims.First(x => x.Type == "MemberId").Value);

                    _memberService.UpdateProfile(form.ToBLL(), id);
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
