using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySqlConnector;

namespace tableAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class TableController : ControllerBase
    {
        string connString = @"Server=localhost;User ID=root;Database=sko_table_system";

        // GET: api/<PlayersController>
        [HttpGet]
        public IEnumerable<Table> Get()
        {
            var tables = new List<Table>();
            try
            {
                //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `id`, `color`, `isOccupied`, `studentID`, `classroomID`, `position` FROM `tables`";
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
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
        //    // GET api/<PlayersController>/5
        [HttpGet("{classroomID}")]
        public IEnumerable<Table> Get(string classroomID)
        {
            var tables = new List<Table>();
            try
            {   //create the SqlConnection object
                using (MySqlConnection conn = new MySqlConnection(connString))
                {
                    //retrieve the SQL Server instance version
                    string query = @"SELECT `id`, `color`, `isOccupied`, `studentID`, `classroomID`, `position` FROM `tables`
                        WHERE ClassroomID = @ClassroomID"; 
                    //create the SqlCommand object
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.Add(new MySqlParameter("@ClassroomID", classroomID));
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
        
        // PUT api/<PlayersController>/5
        [HttpPut("{id}")]
        public bool Put(int id, Table table)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                bool result = false;
                string query = "UPDATE tables SET position = @position WHERE id = @Id ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@id", id));
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

        // POST api/<PlayersController>
        [HttpPost]
        public bool Post(Table table)
        {
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                //string query = "INSERT INTO [dbo].[Devices]([name],[status_id], [tenant_id], [role_id], [device_type_id], [site_id], [facility_id], [ip_address], [qr_code]) " +
                //   "OUTPUT INSERTED.ID " +
                //   "VALUES (@Name, @StatusID, @TenantID, @InventoryRoleID, @DeviceTypeID, @SiteID, @FacilityID, @IPaddress, @QRcode) ";
                bool result = false;
                string query = "INSERT INTO `tables`(`color`, `isOccupied`, `studentID`, `classroomID`, `position`)" +
                    "VALUES (@color, @isOccupied, @studentID, @classroomID, @position); " +
                    "SELECT last_insert_id()";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@color", table.Color));
                cmd.Parameters.Add(new MySqlParameter("@isOccupied", table.IsOccupied));
                cmd.Parameters.Add(new MySqlParameter("@studentID", table.StudentID));
                cmd.Parameters.Add(new MySqlParameter("@classroomID", table.ClassroomID));
                cmd.Parameters.Add(new MySqlParameter("@position", table.Position));

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        // open database connection
                        conn.Open();
                        table.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        result = true;
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

        // DELETE api/<TableController>/5
            [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            bool result = false;
            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                string query = "DELETE FROM `tables` WHERE id=@id ";

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.Add(new MySqlParameter("@Id", id));

                if (conn.State == System.Data.ConnectionState.Closed)
                {
                    try
                    {
                        // open database connection
                        conn.Open();
                        cmd.ExecuteNonQuery();
                        result = true;
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




//namespace tradeAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class PlayersController : ControllerBase
    //{
    //    string connString = @"Data Source=LAPTOP-LLGCDKVI;Initial Catalog=database_sde;Integrated Security=True";

    

    //    // GET api/<PlayersController>/5
    //    [HttpGet("{id}")]
    //    public IEnumerable<Player> Get(int id)
    //    {
    //        var players = new List<Player>();
    //        try
    //        {
    //            //create the SqlConnection object
    //            using (SqlConnection conn = new SqlConnection(connString))
    //            {
    //                //retrieve the SQL Server instance version
    //                string query = @"SELECT [id]
    //                      ,[name]
    //                      ,[gil]
    //                      ,[quantity]
    //                      ,[item]
    //                  FROM [dbo].[Player]
    //                  WHERE Id = @Id";
    //                //create the SqlCommand object
    //                SqlCommand cmd = new SqlCommand(query, conn);
    //                cmd.Parameters.Add(new SqlParameter("@Id", id));
    //                //open connection
    //                conn.Open();

    //                SqlDataReader reader = cmd.ExecuteReader();
    //                while (reader.Read())
    //                {
    //                    var player = new Player()
    //                    {
    //                        Id = reader.GetInt32(0),
    //                        Name = reader.GetString(1),
    //                        Gil = reader.GetInt32(2),
    //                        Quantity = reader.GetInt32(3),
    //                        Item = reader.GetString(4),
    //                    };
    //                    players.Add(player);
    //                }
    //                //execute the SQL Command (UPDATE)
    //                cmd.ExecuteNonQuery();

    //                //close connection
    //                conn.Close();

    //                Console.WriteLine("Retrieval of data was successfully executed.");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            //display error message
    //            Console.WriteLine("Exception: " + ex.Message);
    //        }
    //        return players;
    //    }

    //    // POST api/<PlayersController>
    //    [HttpPost]
    //    public bool Post(Player player)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connString))
    //        {
    //            bool result = false;
    //            string query = "INSERT INTO [dbo].[Player] ([name], [gil], [quantity], [item]) " +
    //                "OUTPUT INSERTED.ID " +
    //                "VALUES (@name, 10000, 0, 'Wool') ";

    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            cmd.Parameters.Add(new SqlParameter("@name", player.Name));

    //            if (conn.State == System.Data.ConnectionState.Closed)
    //            {
    //                try
    //                {
    //                    // open database connection
    //                    conn.Open();
    //                    player.Id = (int)cmd.ExecuteScalar();
    //                    result = true;
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //            }
    //            conn.Close();

    //            return result;
    //        }

    //    }

    //    // PUT api/<PlayersController>/5
    //    [HttpPut("{id}")]
    //    public bool Put(int id, Player player)
    //    {
    //        using (SqlConnection conn = new SqlConnection(connString))
    //        {
    //            bool result = false;
    //            string query = "UPDATE Player SET name = @name, gil = @gil, quantity = @quantity WHERE id = @Id ";

    //            SqlCommand cmd = new SqlCommand(query, conn);
    //            cmd.Parameters.Add(new SqlParameter("@id", id));
    //            cmd.Parameters.Add(new SqlParameter("@name", player.Name));
    //            cmd.Parameters.Add(new SqlParameter("@gil", player.Gil));
    //            cmd.Parameters.Add(new SqlParameter("@quantity", player.Quantity));

    //            if (conn.State == System.Data.ConnectionState.Closed)
    //            {
    //                try
    //                {
    //                    // open database connection
    //                    conn.Open();
    //                    int rowsAffected = cmd.ExecuteNonQuery();
    //                    if (rowsAffected == 1)
    //                    {
    //                        result = true;
    //                    }
    //                }
    //                catch (Exception ex)
    //                {
    //                    throw new Exception(ex.Message);
    //                }
    //            }
    //            conn.Close();

    //            return result;
    //        }

    //    }

    //    
//    }
//}
