using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Information;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;

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

        public List<InfomationModel> ShowInformation()
        {
            List<InfomationModel> info_data = new List<InfomationModel>();
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"SELECT * FROM PRS_INFORMATION ORDER BY TOPIC, POST_DATE DESC";
                connect.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    InfomationModel dt = new InfomationModel();
                    dt.Header = reader["TOPIC"] != DBNull.Value ? reader["TOPIC"].ToString() : null;
                    dt.Section = reader["SECTION"] != DBNull.Value ? reader["SECTION"].ToString() : null;
                    dt.Description = reader["DESCRIPTION"] != DBNull.Value ? reader["DESCRIPTION"].ToString() : null;
                    dt.FilePath = reader["FILE_NAME"] != DBNull.Value ? reader["FILE_NAME"].ToString() : null;
                    dt.Date = reader["POST_DATE"] != DBNull.Value ? reader["POST_DATE"].ToString() : null;
                    info_data.Add(dt);
                }
                connect.Close();
                return info_data;
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }

        public void AddNewsDetailData(InfomationModel infdata)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"if ((SELECT COUNT(FILE_NAME) FROM PRS_INFORMATION WHERE TOPIC = @header) = 0) begin UPDATE PRS_INFORMATION SET DESCRIPTION = @desc, FILE_NAME = @Name, POST_DATE = @date WHERE TOPIC = @header end else begin INSERT INTO PRS_INFORMATION (TOPIC, DESCRIPTION, FILE_NAME, POST_DATE) VALUES(@header, @desc, @Name, @date) end";
                command.Parameters.AddWithValue("@header", infdata.Header);
                command.Parameters.AddWithValue("@desc", infdata.Description);
                command.Parameters.AddWithValue("@Name", infdata.FilePath);
                command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")));
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }

        public void Del_data(string filename)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"DELETE FROM PRS_INFORMATION WHERE FILE_NAME = @Name";
                command.Parameters.AddWithValue("@Name", filename);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }

        public void Add_tab(string tabname)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"INSERT INTO PRS_INFORMATION (TOPIC, POST_DATE) VALUES(@header, @date)";
                command.Parameters.AddWithValue("@header", tabname);
                command.Parameters.AddWithValue("@date", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.CreateSpecificCulture("en-US")));
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }

        public void Del_tab(string tabname)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"DELETE FROM PRS_INFORMATION WHERE TOPIC = @header";
                command.Parameters.AddWithValue("@header", tabname);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }

        public void Rename_tab(string old_tabname, string new_tabname)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = @"UPDATE PRS_INFORMATION SET TOPIC = @new_header WHERE TOPIC = @old_header";
                command.Parameters.AddWithValue("@old_header", old_tabname);
                command.Parameters.AddWithValue("@new_header", new_tabname);
                connect.Open();
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception e)
            {
                _logger.LogError("Caught Exception: {0}", e.ToString());
                throw e;
            }
        }
    }
}
