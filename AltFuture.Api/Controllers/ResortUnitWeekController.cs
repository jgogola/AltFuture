using AltFuture.DataAccessLayer.Interfaces;
using AltFuture.DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AltFuture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ResortUnitWeekController : ControllerBase
    {


        private readonly ILogger<ResortUnitWeekController> _logger;
        private readonly IResortUnitWeekRepository _resortUnitWeekRepository;

        public ResortUnitWeekController(ILogger<ResortUnitWeekController> logger, IResortUnitWeekRepository resortUnitWeekRepository)
        {
            _logger = logger;
            _resortUnitWeekRepository = resortUnitWeekRepository;
        }

        [HttpGet(Name = "GetResortUnitWeeks")]
        public IEnumerable<ResortUnitWeek> GetResortUnitWeeks(string regionCode, DateTime startDate, DateTime endDate)
        {
            return  _resortUnitWeekRepository.GetAllByRegionWeek(regionCode, startDate, endDate);
        }
    }
}