using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PRS_System.Services
{
    public class FormService : IFormService
    {
        private readonly ILogger<FormService> _logger;
        private readonly string _connectionString;
        public FormService(ILogger<FormService> logger)
        {
            _logger = logger;
            _connectionString = Startup.ConnectionString;
        }
        public void AddFormDetailData(FormPRSDataModel formdetaildata)
        {
            SqlConnection connect = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand();
            connect.Open();
            command.Connection = connect;

            int maximum = GetMaximumID_TOR();
            command.CommandText = @"INSERT INTO PRS_PERSON (ID_TOR,ID_Room,NAME_TOR,DESC_TOR,DIRECTOR_1,DIRECTOR_2,DIRECTOR_3,AMT_QUTATATION,AMT_STUDENTLIST_PAGE,AMT_BUGGET_PAGE,NAME_OTHER_DC,AMT_OTHER_DOC,OWNER_ID,TOR_DATE) 
                                    VALUES(@ID_TOR,@ID_Room,@NAME_TOR,@DESC_TOR,@DIRECTOR_1,@DIRECTOR_2,@DIRECTOR_3,@AMT_QUTATATION,@AMT_STUDENTLIST_PAGE,@AMT_BUGGET_PAGE,@NAME_OTHER_DC,@AMT_OTHER_DOC,@OWNER_ID,@TOR_DATE) ";
            
            command.Parameters.Add(new SqlParameter("@ID_TOR", (object)maximum ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@NAME_TOR", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DESC_TOR", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DIRECTOR_1", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DIRECTOR_2", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@DIRECTOR_3", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@AMT_QUTATATION", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@AMT_STUDENTLIST_PAGE", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@AMT_BUGGET_PAGE", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@NAME_OTHER_DC", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@AMT_OTHER_DOC", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@TOR_DATE", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            command.Parameters.Add(new SqlParameter("@ID_USER", (object)formdetaildata.idRoom ?? DBNull.Value));
            throw new NotImplementedException();
        }

        public void AddProductData(SubjectDataModel formdetaildata)
        {
            throw new NotImplementedException();
        }

        public void AddSubjectData(ProductDataModel formdetaildata)
        {
            throw new NotImplementedException();
        }

        public int GetMaximumID_TOR()
        {
            try
            {
                /// DECLARE VARIABLE
                int maximum = 0;

                /// CONNECTION OPEN
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT max(ID_TOR) MAXIMUM FROM PRS_MAIN_TOR";
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    maximum = reader["MAXIMUM"] != DBNull.Value ? (int)reader["MAXIMUM"] : 0;
                }
                reader.Close();
                con.Close();

                return maximum;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
