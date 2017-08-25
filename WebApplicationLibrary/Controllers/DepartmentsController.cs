using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Services;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json"), Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentsController> _logger;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper, ILogger<DepartmentsController> logger)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var departmentFromRepo = _departmentRepository.GetDepartments();
            _logger.LogInformation(100, "Department retrived sucessfully");
            return Ok(_mapper.Map<IEnumerable<DepartmentDto>>(departmentFromRepo));
        }

        [HttpGet("{departmentId}")]
        public IActionResult Get(Guid departmentId)
        {
            var getDepartment = _departmentRepository.GetDepartment(departmentId);
            if (getDepartment == null)
            {
                return NotFound($"Department with {departmentId} not found !");
            }
            return Ok(getDepartment);
        }
    }
}