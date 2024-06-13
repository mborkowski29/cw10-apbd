using cw10.DTOs;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly Service _service;

        public PatientController(Service prescriptionService)
        {
            _service = prescriptionService;
        }

        [HttpGet("{id}")]
        public IActionResult GetPatientDetails(int id)
        {
            var patientDetails = _service.GetPatientDetails(id);

            if (patientDetails == null)
            {
                return NotFound($"Patient with ID {id} not found.");
            }

            return Ok(patientDetails);
        }
    }
}