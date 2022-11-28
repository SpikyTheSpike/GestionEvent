using BLL.Interfaces;
using Domain.Entities;
using GestionEvent.Mappers;
using GestionEvent.Models;
using GestionEvent.Tools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

       
        //public IActionResult Update()
        //{
        //    return Ok();
        //}
       

    }
}
