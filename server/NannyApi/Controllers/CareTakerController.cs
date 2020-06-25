using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NannyApi.DAL;
using NannyApi.Models;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaretakersController : ControllerBase
    {
        private ICareTakerDAO careTakerDao;

        public CaretakersController(ICareTakerDAO careTakerDao)
        {
            this.careTakerDao = careTakerDao;
        }

        // Get for api/caretakers
        [HttpGet]
        public ActionResult<IList<CareTaker>> GetCareTakers()
        {
            return Ok(careTakerDao.GetAllCareTakers());
        }

        // Get for api/caretakers/:id
        [HttpGet("{id}")]
        public ActionResult<CareTaker> GetCareTaker(int id)
        {
            CareTaker careTaker = careTakerDao.GetCareTakerById(id);

            if (careTaker != null)
            {
                return careTaker;
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult<CareTaker> AddCareTaker(CareTaker careTaker)
        {
            return careTakerDao.AddCareTaker(careTaker);
        }

        [HttpPut("{id}")]
        public ActionResult<CareTaker> UpdateCareTaker(CareTaker careTaker, int id)
        {
            return careTakerDao.UpdateCareTaker(careTaker);
        }
    }
}