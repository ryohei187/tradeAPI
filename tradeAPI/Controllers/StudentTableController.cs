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
    public class StudentTableController : Controller
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";

        [HttpPut("{id}")]
        public bool PutStudentId(int id, tableAPI.Table table)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                bool result = false;
                string query = "UPDATE `tables` SET color = @color, isOccupied = @isOccupied, studentID = @studentID WHERE id = @Id ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                cmd.Parameters.Add(new MySqlParameter("@color", table.Color));
                cmd.Parameters.Add(new MySqlParameter("@isOccupied", table.IsOccupied));
                cmd.Parameters.Add(new MySqlParameter("@studentID", table.StudentID));


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
