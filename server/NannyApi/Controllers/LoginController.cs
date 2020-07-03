﻿using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;
using NannyApi.Models;
using NannyApi.Security;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ITokenGenerator tokenGenerator;
        private readonly IPasswordHasher passwordHasher;
        private readonly ICareTakerDAO careTakerDAO;

        public LoginController(ITokenGenerator _tokenGenerator, IPasswordHasher _passwordHasher, ICareTakerDAO _careTakerDAO)
        {
            tokenGenerator = _tokenGenerator;
            passwordHasher = _passwordHasher;
            careTakerDAO = _careTakerDAO;
        }

        [HttpPost]
        public IActionResult Authenticate(CareTaker userParam)
        {
            // Default to bad username/password message
            IActionResult result = BadRequest(new { message = "Username or password is incorrect" });

            // Get the user by username
            CareTaker user = careTakerDAO.GetCareTakerByEmail(userParam.EmailAddress);

            // If we found a user and the password hash matches
            if (user != null && passwordHasher.VerifyHashMatch(user.EmailAddress, userParam.Password, user.Salt))
            {
                // Create an authentication token
                string token = tokenGenerator.GenerateToken(user.CareTakerId, user.EmailAddress /*, user.Role*/);

                // Create a ReturnUser object to return to the client
                CareTaker retUser = new CareTaker() { CareTakerId = user.CareTakerId, EmailAddress = user.EmailAddress, /*Role = user.Role,*/ Token = token };

                // Switch to 200 OK
                result = Ok(retUser);
            }

            return result;
        }

        [HttpPost("register")]
        public IActionResult Register(CareTaker userParam)
        {
            IActionResult result;

            CareTaker existingUser = careTakerDAO.GetCareTakerByEmail(userParam.EmailAddress);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email address already taken. Please choose a different email." });
            }

            CareTaker user = careTakerDAO.AddCareTaker(userParam);
            if (user != null)
            {
                result = Created(user.EmailAddress, null); //values aren't read on client
            }
            else
            {
                result = BadRequest(new { message = "An error occurred and caretaker was not created." });
            }

            return result;
        }
    }
}
