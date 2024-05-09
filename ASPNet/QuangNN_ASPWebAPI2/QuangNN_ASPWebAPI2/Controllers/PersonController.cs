using API.DTOs.RequestDTOs;
using API.DTOs.ResponseDTOs;
using API.Services;
using API.Services.Validations;
using AutoMapper;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace QuangNN_ASPWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPersonService _personService;
        private readonly IValidationPerson _validationPerson;
        private readonly IMapper _mapper;

        public PersonController(ILogger<PersonController> logger, IPersonService personService, IMapper mapper, IValidationPerson validationPerson)
        {
            _logger = logger;
            _personService = personService;
            _mapper = mapper;
            _validationPerson = validationPerson;
        }

        [HttpGet]
        public ActionResult<CustomResponseAPI> GetList()
        {
            var listPersons = _personService.GetPersons();
            return new CustomResponseAPI
            {
                Message = "Get list person successfully",
                Data = listPersons
            };
        }

        [HttpGet("{id}")]
        public ActionResult<CustomResponseAPI> GetById(Guid id)
        {

            var person = _personService.GetById(id);
            if (person == null)
            {
                return new CustomResponseAPI {
                    StatusCode = StatusCodes.Status400BadRequest, Message = "Person is not found" 
                };
            }
            var personResponse = _mapper.Map<ResponsePersonDTO>(person);
            return new CustomResponseAPI {
                Message = "Get person successfully", Data = personResponse
            };
        }

        [HttpPost]
        public ActionResult<CustomResponseAPI> Create([FromBody] RequestPersonDTO dto)
        {
            var validationResult = _validationPerson.Validate(dto);
            if (!validationResult.IsValid)
            {
                return new CustomResponseAPI
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = string.Join("; ", validationResult.Errors.Values)
                };
            }

            var newItem = _personService.Create(dto);
            var newItemResponse = _mapper.Map<ResponsePersonDTO>(newItem);
            return new CustomResponseAPI
            {
                Message = "Create person successfully",
                Data = newItemResponse
            };
        }

        [HttpPut("{id}")]
        public ActionResult<CustomResponseAPI> Update(Guid id, [FromBody] RequestPersonDTO dto)
        {
            var validationResult = _validationPerson.Validate(dto);
            if (!validationResult.IsValid)
            {
                return new CustomResponseAPI
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    Message = string.Join("; ", validationResult.Errors.Values)
                };
            }

            int status = _personService.UpdateById(id, dto);
            if (status == ConstantsStatus.Success)
            {
                return new CustomResponseAPI
                {
                    Message = "Update person successfully",
                    Data = dto
                };
            }

            return new CustomResponseAPI
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Update person failed!"
            };
        }


        [HttpDelete("{id}")]
        public ActionResult<CustomResponseAPI> Delete(Guid id)
        {
            int status = _personService.DeleteById(id);
            if (status == ConstantsStatus.Success)
            {
                return new CustomResponseAPI
                {
                    Message = "Delete person successfully",
                };
            }
            return new CustomResponseAPI
            {
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Delete person failed!",
            };
        }

        [HttpPost]
        [Route("filter")]
        public ActionResult<CustomResponseAPI> GetFilterPerson([FromBody] RequestFilterDTO requestFilterDto)
        {
            var listFilterPerson = _personService.GetFilterPerson(requestFilterDto);
            return new CustomResponseAPI
            {
                Message = "Filter person successfully",
                Data = listFilterPerson
            };
        }

    }
}
