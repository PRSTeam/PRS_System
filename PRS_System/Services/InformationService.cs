using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Information;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PRS_System.Services
{
    public class InformationService : IInformationService
    {
        private readonly ILogger<InformationService> _logger;
        private readonly string _connectionString;


        public InformationService(ILogger<InformationService> logger)
        {
            _logger = logger;
            _connectionString = Startup.ConnectionString;
        }

        public List<InformationModel> ShowInformation()
        {
            List<InformationModel> info_data = new List<InformationModel>();
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                
                command.Connection = connect;
                command.CommandText = @"SELECT * FROM PRS_INFORMATION";
                connect.Open();

                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InformationModel dt = new InformationModel();
                    dt.Header = reader["TOPIC"] != DBNull.Value ? reader["TOPIC"].ToString() : null;
                    dt.Description = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null;
                    dt.Name = reader["FILE_NAME"] != DBNull.Value ? reader["FILE_NAME"].ToString() : null;
                    dt.Date = reader["POST_DATE"] != DBNull.Value ? reader["POST_DATE"].ToString() : null;
                    info_data.Add(dt);
                }
                connect.Close();
                return info_data;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
