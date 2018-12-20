using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mock3_Meassurements.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeassurementsController : ControllerBase
    {
	    private const string conn = "Server=tcp:serveropgave.database.windows.net,1433;Initial Catalog=test_2018-12-19T07 -54Z;Persist Security Info=False;User ID=arlind;Password=Test1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
		// GET: api/Meassurements
		[HttpGet]
        public List<Meassurement> Get()
        {
			List<Meassurement> ml = new List<Meassurement>();
	        string sql = "SELECT * FROM Meassurement";
	        using (SqlConnection dbCon = new SqlConnection(conn))
	        {
		        dbCon.Open();
		        using (SqlCommand cmd = new SqlCommand(sql, dbCon))
		        {
			        using (SqlDataReader reader = cmd.ExecuteReader())
			        {
				        if (reader.HasRows)
				        {
					        while (reader.Read())
					        {
						        Meassurement m = new Meassurement(
							        reader.GetInt32(0),
							        reader.GetString(1),
							        reader.GetString(2),
							        reader.GetString(3),
							        reader.GetDateTime(4)
						        );

						        ml.Add(m);
					        }
				        }
			        }
		        }
	        }
	        return ml;
		}

        // GET: api/Meassurements/5
        [HttpGet("{id}", Name = "Get")]
        public Meassurement Get(int id)
        {
	        Meassurement m = null;
	        string sql = "SELECT * FROM Meassurement WHERE Id = " + id;
	        using (SqlConnection dbCon = new SqlConnection(conn))
	        {
		        dbCon.Open();
		        using (SqlCommand cmd = new SqlCommand(sql, dbCon))
		        {
			        using (SqlDataReader reader = cmd.ExecuteReader())
			        {
				        if (reader.HasRows)
				        {
					        while (reader.Read())
					        {
						        m = new Meassurement(
							        reader.GetInt32(0),
							        reader.GetString(1),
							        reader.GetString(2),
							        reader.GetString(3),
							        reader.GetDateTime(4)
						        );
					        }
				        }
			        }
		        }
	        }
			
	        return m;
		}

        // POST: api/Meassurements
        [HttpPost]
        public int Post([FromBody] Meassurement meassurement)
        {
	        int insId = 0;
	        string sql = "INSERT INTO Meassurement(Pressure, Humidity, Temperature, TimeStamp) " +
	                     "OUTPUT INSERTED.id " + //SQL Command - OUTPUT INSERTED.id udskriver den valgte kolonne 'id', fra den indsatte række.
						 "VALUES(@p, @h, @t, @ts);";

	        using (SqlConnection dbCon = new SqlConnection(conn))
	        {
		        dbCon.Open();

		        using (SqlCommand cmd = new SqlCommand(sql, dbCon))
		        {
			        cmd.Parameters.AddWithValue("@p", meassurement.Pressure); //@p er en param og bliver erstattet af 'meassurement.Pressure'
			        cmd.Parameters.AddWithValue("@h", meassurement.Humidity);
			        cmd.Parameters.AddWithValue("@t", meassurement.Temperature);
			        cmd.Parameters.AddWithValue("@ts", meassurement.TimeStamp);

			        insId = (int)cmd.ExecuteScalar(); //Tager kolonnen som bliver returneret i SQL Command (OUTPUT INSERTED.id) og indsætter som 'insId'
		        }
	        }

	        return insId;

		}

		/* NOT IN SCOPE
        // PUT: api/Meassurements/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
		*/

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public int Delete(int id)
        {
	        int delId = 0;

	        string sql = "DELETE FROM Meassurement OUTPUT DELETED.id WHERE id = @id;"; //SQL Command 

	        using (SqlConnection databaseConnection = new SqlConnection(conn))
	        {
		        databaseConnection.Open();

		        using (SqlCommand cmd = new SqlCommand(sql, databaseConnection))
		        {
			        cmd.Parameters.AddWithValue("@id", id);

			        delId = (int)cmd.ExecuteScalar(); //Tager kolonnen som bliver returneret i SQL Command (OUTPUT DELETED.id) og indsætter som 'insId'
		        }
	        }
	        return delId;
		}
    }
}
