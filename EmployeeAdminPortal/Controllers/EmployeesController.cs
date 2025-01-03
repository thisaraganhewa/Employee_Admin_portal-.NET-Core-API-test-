﻿using EmployeeAdminPortal.Data;
using EmployeeAdminPortal.Models;
using EmployeeAdminPortal.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeesController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAllEmployees()
        {

            var allEmployees = dbContext.Employees.ToList();

            return Ok(allEmployees);

        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetEmployeeById( Guid id ) 
        {

            var employee = dbContext.Employees.Find(id);

            if( employee is null)
            {

                return NotFound();

            }
            else
            {

                return Ok(employee);

            }


        }

        [HttpPost]
        public IActionResult AddNewEmployee( AddEmployeesDto addEmployeeDto)
        {

            var newEmployee = new Employee()
            {

                Name = addEmployeeDto.Name,
                Email = addEmployeeDto.Email,
                Phone = addEmployeeDto.Phone,
                salary = addEmployeeDto.salary

            };

            dbContext.Employees.Add(newEmployee);
            dbContext.SaveChanges();

            return Ok(newEmployee);



        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateEmployee( Guid id, UpdateEmployeeDto updateEmployeeDto) 
        {

            var employee = dbContext.Employees.Find(id);

            if(employee is null)
            {

                return NotFound("Employee Not Found");

            }

            employee.Name = updateEmployeeDto.Name;
            employee.Email = updateEmployeeDto.Email;
            employee.Phone = updateEmployeeDto.Phone;
            employee.salary = updateEmployeeDto.salary;

            dbContext.SaveChanges();

            return Ok(employee);

        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteEmployee( Guid id)
        {

            var employee = dbContext.Employees.Find(id);

            if( employee is null)
            {

                return NotFound("Employee Not Found");

            }


            dbContext.Employees.Remove(employee);
            dbContext.SaveChanges();

            return Ok(employee.Name + " is Deleted");

        }
    }
}
