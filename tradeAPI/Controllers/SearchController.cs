using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;
using tableAPI;

namespace tradeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SearchController : Controller
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";

        [HttpGet("{studentID}")]
        public IEnumerable<Table> Get(string studentID)
        {
            var tables = new List<Table>();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `id`, `color`, `isOccupied`, `studentID`, `classroomID`, `position` 
                      FROM `tables`
                      WHERE StudentID = @StudentID";
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add(new MySqlParameter("@StudentID", studentID));
                    //open connection
                    conn.Open();

                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var table = new Table()
                        {
                            Id = reader.GetInt32(0),
                            Color = reader.GetString(1),
                            IsOccupied = reader.GetInt32(2),
                            StudentID = reader.GetInt32(3),
                            ClassroomID = reader.GetString(4),
                            Position = reader.GetString(5),
                        };
                        tables.Add(table);
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
            return tables;
        }
    }
}
