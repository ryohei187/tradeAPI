using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

using MySqlConnector;


namespace tradeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassroomController : Controller
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";

        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<Classroom> Get()
        {
            var classrooms = new List<Classroom>();
            try
            {   //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `name`, `subject`, `teacher` FROM `classrooms`";
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //open connection
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var classroom = new Classroom()
                        {
                            Name = reader.GetString(0),
                            Subject = reader.GetString(1),
                            Teacher = reader.GetString(2),
                        };
                        classrooms.Add(classroom);
                    }
                    //execute the SQL Command (UPDATE)
                    cmd.ExecuteNonQuery();

                    //close connection
                    conn.Close();

                    Console.WriteLine("Retrieval of data was successfully executed.");
                }
               
                    
            }
            catch (Exception ex)
            {
                //display error message
                Console.WriteLine("Exception: " + ex.Message);
            }
            return classrooms;
        }
    }
}
