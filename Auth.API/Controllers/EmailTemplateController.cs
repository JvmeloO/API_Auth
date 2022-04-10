using Auth.API.Models.DTOs;
using Auth.Domain.Entities;
using Auth.Infra.Repositories.Abstract;
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

        public EmailTemplateController(IEmailTemplateRepository emailTemplateRepository) 
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        [HttpPost]
        [Route("Create")]
        [Authorize(Roles = "Administrador")]
        public IActionResult CreateEmailTemplate([FromForm] EmailTemplateCreateDTO emailTemplateCreateDTO)
        {
            try
            {
                if (_emailTemplateRepository.GetEmailTemplateByTemplateName(emailTemplateCreateDTO.TemplateName) != null)
                    return BadRequest(new { Message = "Nome do template já cadastrado" });

                var emailTemplate = new EmailTemplate
                {
                    TemplateName = emailTemplateCreateDTO.TemplateName,
                    Content = emailTemplateCreateDTO.Content,
                    ContentIsHtml = emailTemplateCreateDTO.ContentIsHtml,
                    EmailSubject = emailTemplateCreateDTO.EmailSubject,
                    EmailTypeId = Convert.ToInt32(emailTemplateCreateDTO.EmailTypeId)
                };
                _emailTemplateRepository.InsertEmailTemplate(emailTemplate);
                _emailTemplateRepository.Save();

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
                var emailTemplate = _emailTemplateRepository.GetEmailTemplateByEmailTemplateId(emailTemplateId);

                emailTemplate.TemplateName = emailTemplateUpdateDTO.TemplateName ?? emailTemplate.TemplateName;
                emailTemplate.EmailSubject = emailTemplateUpdateDTO.EmailSubject ?? emailTemplate.EmailSubject;
                emailTemplate.Content = emailTemplateUpdateDTO.Content ?? emailTemplate.Content;
                emailTemplate.ContentIsHtml = emailTemplateUpdateDTO.ContentIsHtml ?? emailTemplate.ContentIsHtml;
                emailTemplate.EmailTypeId = emailTemplateUpdateDTO.EmailTypeId != null ? Convert.ToInt32(emailTemplateUpdateDTO.EmailTypeId) : emailTemplate.EmailTypeId;
                _emailTemplateRepository.UpdateEmailTemplate(emailTemplate);
                _emailTemplateRepository.Save();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
