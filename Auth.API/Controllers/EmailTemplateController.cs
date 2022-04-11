using Auth.API.Models.DTOs;
using Auth.API.Models.Mappings;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
using Auth.Infra.UnitOfWork.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Auth.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmailTemplateController : ControllerBase
    {
        private readonly IEmailTemplateRepository _emailTemplateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EmailTemplateController(IEmailTemplateRepository emailTemplateRepository, IUnitOfWork unitOfWork) 
        {
            _emailTemplateRepository = emailTemplateRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Administrador")]
        public IActionResult CreateEmailTemplate([FromForm] EmailTemplateCreateDTO emailTemplateCreateDTO)
        {
            try
            {
                if (_emailTemplateRepository.GetWithSingleOrDefault(e => e.TemplateName == emailTemplateCreateDTO.TemplateName) != null)
                    return BadRequest(new { Message = "Nome do template já cadastrado" });

                _emailTemplateRepository.Insert(EmailTemplateMap.CreateDTOToEntity(emailTemplateCreateDTO));
                _unitOfWork.Save();

                return StatusCode((int)HttpStatusCode.Created);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [Route("Update/{emailTemplateId}")]
        [Authorize(Roles = "Administrador")]
        public IActionResult UpdateEmailTemplate(int emailTemplateId, [FromForm] EmailTemplateUpdateDTO emailTemplateUpdateDTO)
        {
            try
            {
                var emailTemplate = _emailTemplateRepository.GetById(emailTemplateId);

                _emailTemplateRepository.Update(EmailTemplateMap.UpdateDTOToEntity(emailTemplateUpdateDTO, emailTemplate));
                _unitOfWork.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
