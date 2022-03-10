using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Data;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
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
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                int maximum = GetMaximumID_TOR() + 1;
                command.CommandText = @"INSERT INTO PRS_MAIN_TOR (ID_TOR,ID_Room,NAME_TOR,DESC_TOR,DIRECTOR_1,DIRECTOR_2,DIRECTOR_3,AMT_QUTATATION,AMT_STUDENTLIST_PAGE,AMT_BUGGET_PAGE,NAME_OTHER_DOC,AMT_OTHER_DOC,DOC_FILE,OWNER_ID,TOR_DATE) VALUES(@ID_TOR,@ID_Room,@NAME_TOR,@DESC_TOR,@DIRECTOR_1,@DIRECTOR_2,@DIRECTOR_3,@AMT_QUTATATION,@AMT_STUDENTLIST_PAGE,@AMT_BUGGET_PAGE,@NAME_OTHER_DC,@AMT_OTHER_DOC,@DOC_FILE,@OWNER_ID,@TOR_DATE) ";

                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)maximum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_Room", (object)formdetaildata.idRoom ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_TOR", (object)formdetaildata.nameProcument ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DESC_TOR", (object)formdetaildata.description_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_1", (object)formdetaildata.diractor_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_2", (object)formdetaildata.diractor_2 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_3", (object)formdetaildata.diractor_3 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_QUTATATION", (object)formdetaildata.scopeWork ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_STUDENTLIST_PAGE", (object)formdetaildata.docNum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_BUGGET_PAGE", (object)formdetaildata.budgetDoc ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_OTHER_DC", (object)formdetaildata.otherSupport ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_OTHER_DOC", (object)formdetaildata.otherSupport_num ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DOC_FILE", (object)formdetaildata.FilePath ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OWNER_ID", (object)formdetaildata.User_ID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TOR_DATE", DateTime.Now.ToString("yyyy-MM-dd ", new CultureInfo("en-US"))));
                command.ExecuteNonQuery();

                connect.Close();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void AddSubjectData(List<SubjectDataModel> formdetaildata,int id_tor)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                foreach (SubjectDataModel item in formdetaildata)
                {
                    SqlCommand command = new SqlCommand();
                    connect.Open();
                    command.Connection = connect;
                    int maximum = GetMaximumID_SUBJECT_LIST();
                    command.CommandText = @"Insert Into PRS_TOR_SUBJECT (ID_SUBJECTT_LIST,ID_TOR,SUBJECT) 
                                        VALUES(@ID_SUBJECTT_LIST,@ID_TOR,@SUBJECT)";
                    command.Parameters.Add(new SqlParameter("@ID_SUBJECTT_LIST", (object)(maximum + 1) ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@NSUBJECT", (object)item.Subject ?? DBNull.Value));
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddProductData(List<ProductDataModel> formdetaildata,int id_tor)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                foreach (ProductDataModel item in formdetaildata)
                {
                    SqlCommand command = new SqlCommand();
                    connect.Open();
                    command.Connection = connect;
                    int maximum = GetMaximumID_PRODUCT_LIST();
                    command.CommandText = @"Insert Into PRS_TOR_PRODUCT_LIST(ID_PRODUCT_LIST,ID_TOR,NAME_PRODUCT,AMT_PRODUCT,UNIT_PRODUCT,PRICE_PER_PIECE) 
                                            VALUES(@IDPRODUCT,@IDTOR,@NAMEPRODUCT,@AMT_PRODUCT,@UNT_PRODUCT,@PRICE_PER_PIECE)";
                    command.Parameters.Add(new SqlParameter("@IDPRODUCT", (object)(maximum+1) ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@NAMEPRODUCT", (object)item.NameProduct ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@AMT_PRODUCT", (object)item.AmtProduct ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UNT_PRODUCT", (object)item.Unit ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PRICE_PER_PIECE", (object)item.Price_Per_Piece ?? DBNull.Value));
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
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

        public int GetMaximumID_PRODUCT_LIST()
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
                command.CommandText = "SELECT max(ID_PRODUCT_LIST) MAXIMUM FROM PRS_TOR_PRODUCT_LIST";
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

        public int GetMaximumID_COM()
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
                command.CommandText = "SELECT max(ID_COM) MAXIMUM FROM PRS_COM_COMMENT";
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

        public int GetMaximumID_ASSIST()
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
                command.CommandText = "SELECT max(ID_ASSIST) MAXIMUM FROM PRS_TOR_ASSIST";
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

        public int GetMaximumID_SUBJECT_LIST()
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
                command.CommandText = "SELECT max(ID_SUBJECT_LIST) MAXIMUM FROM PRS_TOR_SUBJECT";
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
