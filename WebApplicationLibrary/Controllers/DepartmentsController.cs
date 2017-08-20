using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using WebApplicationLibrary.Models;
using WebApplicationLibrary.Services;

namespace WebApplicationLibrary.Controllers
{
    [Produces("application/json")]
    [Route("api/Departments")]
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;

        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var departmentFromRepo = _departmentRepository.GetDepartments();
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