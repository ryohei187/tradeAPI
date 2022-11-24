using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace tradeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";

        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            var students = new List<Student>();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `id`, `unilogin`, `firstname`, `lastname`, `isSeated`, `role` FROM `students`";
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    //open connection
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var student = new Student()
                        {
                            Id = reader.GetInt32(0),
                            Unilogin = reader.GetString(1),
                            Firstname = reader.GetString(2),
                            Lastname = reader.GetString(3),
                            IsSeated = reader.GetInt32(4),
                            Role = reader.GetInt32(5),
                        };
                        students.Add(student);
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
            return students;
        }
        //    // GET api/<PlayersController>/5
        [HttpGet("{id}")]
        public IEnumerable<Student> Get(int id)
        {
            var students = new List<Student>();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `id`, `unilogin`, `firstname`, `lastname`, `isSeated`, `role` 
                      FROM `students`
                      WHERE Id = @Id";
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add(new MySqlParameter("@Id", id));
                    //open connection
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var student = new Student()
                        {
                            Id = reader.GetInt32(0),
                            Unilogin = reader.GetString(1),
                            Firstname = reader.GetString(2),
                            Lastname = reader.GetString(3),
                            IsSeated = reader.GetInt32(4),
                            Role = reader.GetInt32(5),
                        };
                        students.Add(student);
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
            return students;
        }
        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        public bool Put(int id, Student student)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                bool result = false;
                string query = "UPDATE Students SET isSeated = @isSeated WHERE id = @Id ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                cmd.Parameters.Add(new MySqlParameter("@isSeated", student.IsSeated));


                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        // open database connection
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 1)
                        {
                            result = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
                conn.Close();

                return result;
            }

        }
    }
}
