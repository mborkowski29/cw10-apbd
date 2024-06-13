using cw10.DTOs;
using cw10.Services;
using Microsoft.AspNetCore.Mvc;

namespace cw10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly Service _service;

        public PrescriptionController(Service prescriptionService)
        {
            _service = prescriptionService;
        }

        [HttpPost]
        public IActionResult AddPrescription(PrescriptionDTOs prescriptionDto)
        {
            try
            {
                _service.AddPrescription(prescriptionDto);
                return Ok("Prescription added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to add prescription: {ex.Message}");
            }
        }
    }
}