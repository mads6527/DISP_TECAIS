﻿using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TECAIS.HeatSubmissionService.Config; 
using SagaContracts;

namespace TECAIS.HeatSubmissionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeatSubmissionController : ControllerBase
    {
        private readonly IPublishEndpoint _endpoint; 

        private readonly ILogger<HeatSubmissionController> _logger;

        public HeatSubmissionController(ILogger<HeatSubmissionController> logger, IPublishEndpoint endpoint)
        {
            _logger = logger;
            _endpoint = endpoint;
        }

        //POST - Creates a new heat submission
        [HttpPost]
        public async Task<IActionResult> CreateHeatSubmission(HeatSubmissionSubmitted heatSubmission)
        {
            await _endpoint.Publish<HeatSubmissionSubmitted>(new
            {
                Address = heatSubmission.Address,
                TimeOfMeasurement = heatSubmission.TimeOfMeasurement,
                HeatConsumption = heatSubmission.HeatConsumption
            });

            Console.WriteLine("Submitted heatsubmission: Address: " + heatSubmission.Address + " TimeOfMeasurement: " + heatSubmission.Address + " HeatConsumption: " + heatSubmission.HeatConsumption); 
            return Ok();
        }

        //POST - Creates a new heat submission
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hej");
        }
    }
}
