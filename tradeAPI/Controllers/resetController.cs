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
    public class resetController : Controller
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";
        [HttpPut("{classroomID}")]
        public bool ResetTables(string classroomID, tableAPI.Table table)
        {

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                bool result = false;
                string query = "UPDATE SKPtables SET color = @color, isOccupied = @isOccupied, studentID = @studentID, position = @position WHERE classroomID = @ClassroomID ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@classroomID", classroomID));
                cmd.Parameters.Add(new MySqlParameter("@color", table.Color));
                cmd.Parameters.Add(new MySqlParameter("@isOccupied", table.IsOccupied));
                cmd.Parameters.Add(new MySqlParameter("@studentID", table.StudentID));
                cmd.Parameters.Add(new MySqlParameter("@position", table.Position));


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
