using ClinicService.Models.Request;
using ClinicService.Models;
using ClinicService.Services;
using ClinicService.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private IConsultationRepository _consultationRepository;

        public ConsultationController(IConsultationRepository consultationRepository)
        {
            _consultationRepository = consultationRepository;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] CreateConsultationRequest createRequest)
        {
            int res = _consultationRepository.Create(new Consultation
            {
                ClientId = createRequest.ClientId,
                PetId = createRequest.PetId,
                Description = createRequest.Description,
                ConsultationDate = createRequest.ConsultationDate
            });
            return Ok(res);
        }

        [HttpPut("update")]
        public IActionResult Update([FromBody] UpdateConsultationRequest updateRequest)
        {
            int res = _consultationRepository.Update(new Consultation
            {
                ConsultationId= updateRequest.ConsultationId,
                ClientId = updateRequest.ClientId,
                PetId = updateRequest.PetId,
                ConsultationDate = updateRequest.ConsultationDate,
                Description = updateRequest.Description
            });
            return Ok(res);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int consultationId) 
        {
            int res = _consultationRepository.Delete(consultationId);
            return Ok(res);
        }

        [HttpGet("get-all")]
        public IActionResult GetAll()
        {
            return Ok(_consultationRepository.GetAll());
        }

        [HttpGet("get-by-id")]
        public IActionResult GetById (int consultationId)
        {
            return Ok(_consultationRepository.GetById(consultationId));
        }

        [HttpGet("get-by-petid-and-period")]
        public IActionResult GetByPetidAndPeriod(int petId, DateTime dateFrom, DateTime dateTo)
        {
            return Ok(_consultationRepository.GetAllByPetIdForPeriod(petId, dateFrom, dateTo));
        }

    }
}
