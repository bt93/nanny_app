using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NannyApi.DAL;

namespace NannyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareTakerController : ControllerBase
    {
        private CareTakerSqlDAO careTakerDao;

        public CareTakerController()
        {
            
        }
    }
}