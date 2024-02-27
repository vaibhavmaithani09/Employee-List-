using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class Employeecontroller : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Employeecontroller(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult GetEmployee()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("GetEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        List<dynamic> employees = new List<dynamic>();

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                
                                var employee = new
                                {
                                    ID = reader["ID"],
                                    Name = reader["Name"],
                                    Email = reader["Email"],
                                    Phone = reader["Phone"]
                                 
                                };
                                employees.Add(employee);
                                
                            }
                            if(employees.Count()>0)
                                return Ok(employees);
                            else
                            {
                                return NotFound("Employee not found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpGet("{ID}")]
        public IActionResult GetEmployeeById(int ID)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("GetEmployeeById", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@EmployeeId",ID);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                
                                var result = new
                                {
                                    ID = reader["ID"],
                                    Name = reader["Name"],
                                    Email = reader["Email"],
                                    Phone = reader["Phone"]
                                   
                                };

                                return Ok(result);
                            }
                            else
                            {
                                return NotFound("Employee not found");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult PostEmployee([FromBody] EmployeeModel employee)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString)) 
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("PostEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        
                        cmd.Parameters.AddWithValue("@Name", employee.Name);
                        cmd.Parameters.AddWithValue("@Email", employee.Email);
                        cmd.Parameters.AddWithValue("@Phone", employee.Phone);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { Message = "Employee added successfully" });
                        }
                        else
                        {
                            return StatusCode(500, "Failed to add employee");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
        [HttpDelete("{ID}")]
        public IActionResult DeleteEmployee(int ID)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("DeleteEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { Message = "Employee deleted successfully" });
                        }
                        else
                        {
                            return NotFound("Employee not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPut("{ID}")]
        public IActionResult UpdateEmployee(int ID,  EmployeeModel updatedEmployee)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("DefaultConnection");

                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateEmployee", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ID", ID);
                        cmd.Parameters.AddWithValue("@Name", updatedEmployee.Name);
                        cmd.Parameters.AddWithValue("@Email", updatedEmployee.Email);
                        cmd.Parameters.AddWithValue("@Phone", updatedEmployee.Phone);

                        int rowsAffected = cmd.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            return Ok(new { Message = "Employee updated successfully" });
                        }
                        else
                        {
                            return NotFound("Employee not found");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}




