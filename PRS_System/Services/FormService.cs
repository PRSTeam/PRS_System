using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Data;
using PRS_System.Models.FormModel;
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
        public int AddFormDetailData(FormPRSDataModel formdetaildata)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                int maximum = GetMaximumID_TOR() + 1;
                command.CommandText = @"INSERT INTO PRS_MAIN_TOR (ID_TOR,TYPE_PRS,ID_Room,NAME_TOR,DESC_TOR,DIRECTOR_1,DIRECTOR_2,DIRECTOR_3,AMT_QUTATATION,AMT_SCOP_PAGE,AMT_STUDENTLIST_PAGE,AMT_BUGGET_PAGE,NAME_OTHER_DOC,AMT_OTHER_DOC,DOC_FILE,STATUS,OWNER_ID,CERRENT_FLOW,TOR_DATE) 
                                        VALUES(@ID_TOR,@TYPE_PRS,@ID_Room,@NAME_TOR,@DESC_TOR,@DIRECTOR_1,@DIRECTOR_2,@DIRECTOR_3,@AMT_QUTATATION,@AMT_SCOP_PAGE,@AMT_STUDENTLIST_PAGE,@AMT_BUGGET_PAGE,@NAME_OTHER_DC,@AMT_OTHER_DOC,@DOC_FILE,@STATUS,@OWNER_ID,@CERRENT_FLOW,@TOR_DATE) ";

                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)maximum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TYPE_PRS", (object)formdetaildata.type_PRS ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_Room", (object)formdetaildata.idRoom ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_TOR", (object)formdetaildata.nameProcument ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DESC_TOR", (object)formdetaildata.description_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_1", (object)formdetaildata.diractor_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_2", (object)formdetaildata.diractor_2 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_3", (object)formdetaildata.diractor_3 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_QUTATATION", (object)formdetaildata.quotationNum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_SCOP_PAGE", (object)formdetaildata.scopeWork ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_STUDENTLIST_PAGE", (object)formdetaildata.docNum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_BUGGET_PAGE", (object)formdetaildata.budgetDoc ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_OTHER_DC", (object)formdetaildata.otherSupport ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_OTHER_DOC", (object)formdetaildata.otherSupport_num ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DOC_FILE", (object)formdetaildata.FilePath ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@STATUS", (object)formdetaildata.buttonstatus ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OWNER_ID", (object)formdetaildata.User_ID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CERRENT_FLOW", (object)"ฝ่ายพัสดุ" ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TOR_DATE", DateTime.Now.ToString("yyyy-MM-dd ", new CultureInfo("en-US"))));
                command.ExecuteNonQuery();

                connect.Close();
                return maximum;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void AddSubjectData(List<SubjectDataModel> formdetaildata, int id_tor)
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
                    command.CommandText = @"Insert Into PRS_TOR_SUBJECT (ID_SUBJECT_LIST,ID_TOR,SUBJECT) 
                                        VALUES(@ID_SUBJECT_LIST,@ID_TOR,@SUBJECT)";
                    command.Parameters.Add(new SqlParameter("@ID_SUBJECT_LIST", (object)(maximum + 1) ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@SUBJECT", (object)item.Subject ?? DBNull.Value));
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddProductData(List<ProductDataModel> formdetaildata, int id_tor)
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
                    command.Parameters.Add(new SqlParameter("@IDPRODUCT", (object)(maximum + 1) ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@NAMEPRODUCT", (object)item.NameProduct ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@AMT_PRODUCT", (object)item.AmtProduct ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@UNT_PRODUCT", (object)item.Unit ?? DBNull.Value));
                    command.Parameters.Add(new SqlParameter("@PRICE_PER_PIECE", (object)item.Price_Per_Piece ?? DBNull.Value));
                    command.ExecuteNonQuery();
                    connect.Close();
                }
            }
            catch (Exception ex)
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

        public FormPRSModel GetValuesFormPRS(int id_tor)
        {
            try
            {
                FormPRSModel model = new FormPRSModel();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT *  FROM PRS_MAIN_TOR WHERE id_tor =@id_tor";
                command.Parameters.Add(new SqlParameter("id_tor", (object)id_tor ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    model.idRoom = reader["ID_ROOM"] != DBNull.Value ? (string)reader["ID_ROOM"] : "";
                    model.type_PRS = reader["TYPE_PRS"] != DBNull.Value ? (string)reader["TYPE_PRS"] : "";
                    model.nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : "";
                    model.description_1 = reader["DESC_TOR"] != DBNull.Value ? (string)reader["DESC_TOR"] : "";
                    model.diractor_1 = reader["DIRECTOR_1"] != DBNull.Value ? (string)reader["DIRECTOR_1"] : "";
                    model.diractor_2 = reader["DIRECTOR_2"] != DBNull.Value ? (string)reader["DIRECTOR_2"] : "";
                    model.diractor_3 = reader["DIRECTOR_3"] != DBNull.Value ? (string)reader["DIRECTOR_3"] : "";
                    model.quotationNum = reader["AMT_QUTATATION"] != DBNull.Value ? (int)reader["AMT_QUTATATION"] : 0;
                    model.scopeWork = reader["AMT_SCOP_PAGE"] != DBNull.Value ? (int)reader["AMT_SCOP_PAGE"] : 0;
                    model.prsnum = reader["AMT_STUDENTLIST_PAGE"] != DBNull.Value ? (int)reader["AMT_STUDENTLIST_PAGE"] : 0;
                    model.budgetDoc = reader["AMT_BUGGET_PAGE"] != DBNull.Value ? (int)reader["AMT_BUGGET_PAGE"] : 0;
                    model.otherSupport = reader["NAME_OTHER_DOC"] != DBNull.Value ? (string)reader["NAME_OTHER_DOC"] : "";
                    model.otherSupport_num = reader["AMT_OTHER_DOC"] != DBNull.Value ? (int)reader["AMT_OTHER_DOC"] : 0;
                    model.FilePath = reader["DOC_FILE"] != DBNull.Value ? (string)reader["DOC_FILE"] : "";
                    model.status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : "";
                    model.User_ID = reader["OWNER_ID"] != DBNull.Value ? (string)reader["OWNER_ID"] : "";
                    model.TOR_DATE = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null;
                    model.cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : "";
                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ProductDataModel> GetValuesFormPRSProduct(int id_tor)
        {
            try
            {
                List<ProductDataModel> model = new List<ProductDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT *  FROM PRS_TOR_PRODUCT_LIST WHERE ID_TOR =@id_tor";
                command.Parameters.Add(new SqlParameter("id_tor", (object)id_tor ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    model.Add(new ProductDataModel
                    {
                        Id_Product = reader["ID_PRODUCT_LIST"] != DBNull.Value ? (int)reader["ID_PRODUCT_LIST"] : 0
                        ,
                        NameProduct = reader["NAME_PRODUCT"] != DBNull.Value ? (string)reader["NAME_PRODUCT"] : ""
                        ,
                        AmtProduct = reader["AMT_PRODUCT"] != DBNull.Value ? (int)reader["AMT_PRODUCT"] : 0
                        ,
                        Unit = reader["UNIT_PRODUCT"] != DBNull.Value ? (string)reader["UNIT_PRODUCT"] : ""
                        ,
                        Price_Per_Piece = reader["PRICE_PER_PIECE"] != DBNull.Value ? (double)reader["PRICE_PER_PIECE"] : 0
                        ,
                        status = "Open"
                    });
                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubjectDataModel> GetValuesFormPRSSubject(int id_tor)
        {
            try
            {
                List<SubjectDataModel> model = new List<SubjectDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT *  FROM PRS_TOR_SUBJECT WHERE ID_TOR =@id_tor";
                command.Parameters.Add(new SqlParameter("id_tor", (object)id_tor ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    model.Add(new SubjectDataModel
                    {
                        Id_Subject = reader["ID_SUBJECT_LIST"] != DBNull.Value ? (int)reader["ID_SUBJECT_LIST"] : 0
                        ,
                        Subject = reader["SUBJECT"] != DBNull.Value ? (string)reader["SUBJECT"] : ""
                        ,
                        status = "Open"
                    });
                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> GetnamePRS(string user_id)
        {
            try
            {
                List<FormPRSDataModel> model = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT ID_TOR,NAME_TOR,STATUS,CERRENT_FLOW,TOR_DATE   FROM PRS_MAIN_TOR WHERE OWNER_ID =@OWNER_ID";
                command.Parameters.Add(new SqlParameter("@OWNER_ID", (object)user_id ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    model.Add(new FormPRSDataModel()
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow= reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""
                    });


                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        public void EditFormDetailData(FormPRSDataModel formdetaildata, int id_tor)
        {

            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;


                command.CommandText = @"UPDATE PRS_MAIN_TOR SET ID_TOR=@ID_TOR,TYPE_PRS=@TYPE_PRS,ID_Room=@ID_Room,NAME_TOR=@NAME_TOR,DESC_TOR=@DESC_TOR,DIRECTOR_1=@DIRECTOR_1,DIRECTOR_2=@DIRECTOR_2,DIRECTOR_3=@DIRECTOR_3,AMT_QUTATATION=@AMT_QUTATATION,AMT_SCOP_PAGE=@AMT_SCOP_PAGE,AMT_STUDENTLIST_PAGE=@AMT_STUDENTLIST_PAGE,AMT_BUGGET_PAGE=@AMT_BUGGET_PAGE,NAME_OTHER_DOC=@NAME_OTHER_DOC,AMT_OTHER_DOC=@AMT_OTHER_DOC,DOC_FILE=@DOC_FILE,STATUS=@STATUS,OWNER_ID=@OWNER_ID,CERRENT_FLOW=@CERRENT_FLOW,TOR_DATE=@TOR_DATE WHERE ID_TOR=@ID_TOR";

                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TYPE_PRS", (object)formdetaildata.type_PRS ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_Room", (object)formdetaildata.idRoom ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_TOR", (object)formdetaildata.nameProcument ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DESC_TOR", (object)formdetaildata.description_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_1", (object)formdetaildata.diractor_1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_2", (object)formdetaildata.diractor_2 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DIRECTOR_3", (object)formdetaildata.diractor_3 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_QUTATATION", (object)formdetaildata.quotationNum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_SCOP_PAGE", (object)formdetaildata.scopeWork ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_STUDENTLIST_PAGE", (object)formdetaildata.docNum ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_BUGGET_PAGE", (object)formdetaildata.budgetDoc ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAME_OTHER_DOC", (object)formdetaildata.otherSupport ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_OTHER_DOC", (object)formdetaildata.otherSupport_num ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DOC_FILE", (object)formdetaildata.FilePath ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@STATUS", (object)formdetaildata.buttonstatus ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@OWNER_ID", (object)formdetaildata.User_ID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CERRENT_FLOW", (object)"ฝ่ายพัสดุ" ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TOR_DATE", DateTime.Now.ToString("yyyy-MM-dd ", new CultureInfo("en-US"))));
                command.ExecuteNonQuery();

                connect.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditFormProductlData(ProductDataModel formdetaildata)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"UPDATE PRS_TOR_PRODUCT_LIST 
SET NAME_PRODUCT = @NAMEPRODUCT ,AMT_PRODUCT=@AMT_PRODUCT,UNIT_PRODUCT=@UNT_PRODUCT,PRICE_PER_PIECE=@PRICE_PER_PIECE 
                                            WHERE ID_PRODUCT_LIST=@IDPRODUCT";

                command.Parameters.Add(new SqlParameter("@IDPRODUCT", (object)formdetaildata.Id_Product ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAMEPRODUCT", (object)formdetaildata.NameProduct ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_PRODUCT", (object)formdetaildata.AmtProduct ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UNT_PRODUCT", (object)formdetaildata.Unit ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PRICE_PER_PIECE", (object)formdetaildata.Price_Per_Piece ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void EditFormSubjectData(SubjectDataModel formdetaildata)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"UPDATE PRS_TOR_SUBJECT 
SET ID_SUBJECT_LIST = @IDSUBJECT ,SUBJECT=@SUBJECT 
                                            WHERE ID_SUBJECT_LIST=@IDSUBJECT";

                command.Parameters.Add(new SqlParameter("@IDSUBJECT", (object)formdetaildata.Id_Subject ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SUBJECT", (object)formdetaildata.Subject ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteFormProductData(int ID_Product)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"DELETE FROM PRS_TOR_PRODUCT_LIST WHERE ID_PRODUCT_LIST=@ID_Product;";
                command.Parameters.Add(new SqlParameter("@ID_Product", (object)ID_Product ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }

        public void DeleteFormSubjectData(int ID_Subject)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"DELETE FROM PRS_TOR_SUBJECT WHERE ID_SUBJECT_LIST=@ID_Subject;";
                command.Parameters.Add(new SqlParameter("@ID_Subject", (object)ID_Subject ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public void UpdateAddProductData(ProductDataModel formdetaildata, int id_tor)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                int maximum = GetMaximumID_PRODUCT_LIST();
                command.CommandText = @"Insert Into PRS_TOR_PRODUCT_LIST(ID_PRODUCT_LIST,ID_TOR,NAME_PRODUCT,AMT_PRODUCT,UNIT_PRODUCT,PRICE_PER_PIECE) 
                                            VALUES(@IDPRODUCT,@IDTOR,@NAMEPRODUCT,@AMT_PRODUCT,@UNT_PRODUCT,@PRICE_PER_PIECE)";
                command.Parameters.Add(new SqlParameter("@IDPRODUCT", (object)(maximum + 1) ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@NAMEPRODUCT", (object)formdetaildata.NameProduct ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@AMT_PRODUCT", (object)formdetaildata.AmtProduct ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@UNT_PRODUCT", (object)formdetaildata.Unit ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PRICE_PER_PIECE", (object)formdetaildata.Price_Per_Piece ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateAddSubjectData(SubjectDataModel formdetaildata, int id_tor)
        {

            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                int maximum = GetMaximumID_SUBJECT_LIST();
                command.CommandText = @"Insert Into PRS_TOR_SUBJECT(ID_SUBJECT_LIST,ID_TOR,SUBJECT) 
                                            VALUES(@IDSUBJECT,@IDTOR,@SUBJECT)";
                command.Parameters.Add(new SqlParameter("@IDSUBJECT", (object)(maximum + 1) ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SUBJECT", (object)formdetaildata.Subject ?? DBNull.Value));

                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddDataSupplies(FormPRSModel datasupplies, int id_tor)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                int maximum = GetMaximumID_ORDER_DIRACT() + 1;
                command.CommandText = @"Insert Into PRS_ORDER_DIRACT(ID_ORDER,ID_TOR,ORDER_DIRACT1,ORDER_DIRACT2,DEFINITION_GROUP) 
                                            VALUES(@ID_ORDER,@IDTOR,@ORDER_DIRACT1,@ORDER_DIRACT2,@DEFINITION_GROUP)";
                command.Parameters.Add(new SqlParameter("@ID_ORDER", (object)(maximum) ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ORDER_DIRACT1", (object)datasupplies.name_select1 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ORDER_DIRACT2", (object)datasupplies.name_select2 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DEFINITION_GROUP", (object)datasupplies.definition ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetMaximumID_ORDER_DIRACT()
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
                command.CommandText = "SELECT max(ID_ORDER) MAXIMUM FROM PRS_ORDER_DIRACT";
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

        public void updatestatusform(string status ,int id_tor, string cerrent_flow)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"update PRS_MAIN_TOR set STATUS=@STATUS ,CERRENT_FLOW=@CERRENT_FLOW WHERE ID_TOR=@ID_TOR";
                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@STATUS", (object)status ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CERRENT_FLOW", (object)cerrent_flow ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public FormPRSModel Get_PRS_ORDER_DIRACT(int id_tor)
        {
            try
            {
                FormPRSModel data = new FormPRSModel();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT *  FROM PRS_ORDER_DIRACT WHERE ID_TOR=@ID_TOR";
                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.id_order = reader["ID_ORDER"] != DBNull.Value ? (int)reader["ID_ORDER"] : 0;
                    data.name_select1 = reader["ORDER_DIRACT1"] != DBNull.Value ? (string)reader["ORDER_DIRACT1"] : "";
                    data.name_select2 = reader["ORDER_DIRACT2"] != DBNull.Value ? (string)reader["ORDER_DIRACT2"] : "";
                    data.definition = reader["DEFINITION_GROUP"] != DBNull.Value ? (string)reader["DEFINITION_GROUP"] : "";
                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDataOrder_Suppies(FormPRSModel data, int id_tor)
        {
            throw new NotImplementedException();
        }

        public List<FormPRSDataModel> GetListSuppies()
        {
            try
            {
                List<FormPRSDataModel> data = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = @"SELECT *
                                        from PRS_MAIN_TOR
                                        LEFT JOIN PRS_PERSON
                                        ON PRS_MAIN_TOR.OWNER_ID = PRS_PERSON.ID_USER";
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new FormPRSDataModel
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Fullname_PRS = reader["FULL_NAME"] != DBNull.Value ? (string)reader["FULL_NAME"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""

                    });

                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public FormPRSModel.CommentDataModel GetCommentApproval(int id_tor, string id_com)
        {
            try
            {
                FormPRSModel.CommentDataModel data = new FormPRSModel.CommentDataModel();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT ID_COM,COMMENT,COMMENT_DATE  FROM PRS_COM_COMMENT WHERE ID_TOR=@ID_TOR AND ID_COM=@ID_COM";
                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)id_com ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.comment = reader["COMMENT"] != DBNull.Value ? (string)reader["COMMENT"] : null;
                    data.com_date = reader["COMMENT_DATE"] != DBNull.Value ? (DateTime?)reader["COMMENT_DATE"] : null;
                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddCommentApproval(FormPRSModel data)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"Insert Into PRS_COM_COMMENT(ID_TOR,ID_COM,COMMENT,COMMENT_DATE) 
                                            VALUES(@IDTOR,@ID_COM,@COMMENT,@COMMENT_DATE)";

                command.Parameters.Add(new SqlParameter("@IDTOR", (object)data.id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)data.login_userid ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@COMMENT", (object)data.des_approval0 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@COMMENT_DATE", DateTime.Now.ToString("yyyy-MM-dd ", new CultureInfo("en-US"))));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditCommentApproval(FormPRSModel data)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"UPDATE PRS_COM_COMMENT
                                        SET COMMENT=@COMMENT,COMMENT_DATE=@COMMENT_DATE
                                        WHERE ID_TOR=@IDTOR AND ID_COM=@ID_COM";
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)data.id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)data.login_userid ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@COMMENT", (object)data.des_approval0 ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@COMMENT_DATE", DateTime.Now.ToString("yyyy-MM-dd ", new CultureInfo("en-US"))));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> GetListApproval(string id_com)
        {
            try
            {
                List<FormPRSDataModel> data = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = @"SELECT *
                                        from PRS_COM_COMMENT
                                        left join PRS_MAIN_TOR
                                        on PRS_COM_COMMENT.ID_TOR=PRS_MAIN_TOR.ID_TOR
                                        where PRS_COM_COMMENT.ID_COM=@ID_COM";
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)id_com ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new FormPRSDataModel
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Fullname_PRS = reader["FULL_NAME"] != DBNull.Value ? (string)reader["FULL_NAME"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""

                    });

                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddAssist_TOR(FormPRSModel data)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                int maximum = GetMaximumID_ASSIST();
                command.CommandText = @"Insert Into PRS_TOR_ASSIST(ID_TOR,ID_ASSIST,TYPE_ASSIST,REASON_ASSIST,DESC_ASSIST) 
                                            VALUES(@IDTOR,@ID_ASSIST,@TYPE_ASSIST,@REASON_ASSIST,@DESC_ASSIST)";
                
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)(data.id_tor) ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_ASSIST", (object)(maximum+1) ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TYPE_ASSIST", (object)data.type_assitst ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@REASON_ASSIST", (object)data.supportType ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DESC_ASSIST", (object)data.desc_assist3 ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditAssist_TOR(FormPRSModel data)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"UPDATE PRS_TOR_ASSIST
                                        SET TYPE_ASSIST=@TYPE_ASSIST,DETAIL_ASSIST=@DETAIL_ASSIST,REASON_ASSIST=@REASON_ASSIST,DESC_ASSIST=@DESC_ASSIST
                                        WHERE ID_TOR=@IDTOR";
                command.Parameters.Add(new SqlParameter("@IDTOR", (object)data.id_tor ?? DBNull.Value));
                //command.Parameters.Add(new SqlParameter("@ID_ASSIST", (object)data.id_assist ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DETAIL_ASSIST", (object)data.detail_assist ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TYPE_ASSIST", (object)data.type_assitst ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@REASON_ASSIST", (object)data.supportType ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@DESC_ASSIST", (object)data.des_approval0 ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> GetListApprovalCerrent(string cerrent)
        {
            try
            {
                List<FormPRSDataModel> model = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = @"SELECT *
                                        from PRS_MAIN_TOR
                                        LEFT JOIN PRS_PERSON
                                        ON PRS_MAIN_TOR.OWNER_ID = PRS_PERSON.ID_USER WHERE CERRENT_FLOW =@CERRENT_FLOW";
                command.Parameters.Add(new SqlParameter("@CERRENT_FLOW", (object)cerrent ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    model.Add(new FormPRSDataModel()
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""
                        ,
                        Fullname_PRS=reader["FULL_NAME"] != DBNull.Value ? (string)reader["FULL_NAME"] : ""
                    });


                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormPRSModel GetCommentApproval2(int id_tor, string id_com)
        {
            try
            {
                FormPRSModel data = new FormPRSModel();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT ID_COM,COMMENT,COMMENT_DATE  FROM PRS_COM_COMMENT WHERE ID_TOR=@ID_TOR AND ID_COM=@ID_COM";
                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)id_com ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.comment = reader["COMMENT"] != DBNull.Value ? (string)reader["COMMENT"] : null;
                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FormPRSModel GetAssist(int id_tor)
        {
            try
            {
                FormPRSModel data = new FormPRSModel();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT *  FROM PRS_TOR_ASSIST WHERE ID_TOR=@ID_TOR";
                command.Parameters.Add(new SqlParameter("@ID_TOR", (object)id_tor ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.type_assitst = reader["TYPE_ASSIST"] != DBNull.Value ? (string)reader["TYPE_ASSIST"] : null;
                    data.detail_assist= reader["DETAIL_ASSIST"] != DBNull.Value ? (string)reader["DETAIL_ASSIST"] : null;
                    data.supportType= reader["REASON_ASSIST"] != DBNull.Value ? (string)reader["REASON_ASSIST"] : null;
                    data.desc_assist3= reader["DESC_ASSIST"] != DBNull.Value ? (string)reader["DESC_ASSIST"] : null;
                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> SearchPRS_Proqument(string user_id,string keyword)
        {
            try
            {
                List<FormPRSDataModel> model = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = "SELECT ID_TOR,NAME_TOR,STATUS,CERRENT_FLOW,TOR_DATE   FROM PRS_MAIN_TOR WHERE OWNER_ID =@OWNER_ID AND NAME_TOR like '%" + keyword+"%'"+"or STATUS like '%"+keyword+"%' or CERRENT_FLOW like'%"+keyword+"%'";
                command.Parameters.Add(new SqlParameter("@OWNER_ID", (object)user_id ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    model.Add(new FormPRSDataModel()
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""
                    });


                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> SearchPRS_Suppies(string keyword)
        {
            try
            {
                List<FormPRSDataModel> data = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = @"SELECT *
                                        from PRS_MAIN_TOR
                                        LEFT JOIN PRS_PERSON
                                        ON PRS_MAIN_TOR.OWNER_ID = PRS_PERSON.ID_USER
                                        WHERE PRS_MAIN_TOR.NAME_TOR like '%" + keyword + "%'" + "or PRS_MAIN_TOR.STATUS like '%" + keyword + "%' or PRS_MAIN_TOR.CERRENT_FLOW like'%" + keyword + "%'";
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    data.Add(new FormPRSDataModel
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Fullname_PRS = reader["FULL_NAME"] != DBNull.Value ? (string)reader["FULL_NAME"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""

                    });

                }
                reader.Close();
                con.Close();

                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FormPRSDataModel> SearchPRS_Approval(string keyword , string id_com ,string cerrent_flow)
        {
            try
            {
                List<FormPRSDataModel> model = new List<FormPRSDataModel>();
                SqlConnection con = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                con.Open();
                command.Connection = con;
                command.CommandText = @"SELECT PRS_MAIN_TOR.ID_TOR,NAME_TOR,PRS_MAIN_TOR.[STATUS] AS[STATUS],CERRENT_FLOW,FULL_NAME,TOR_DATE
                                        from PRS_MAIN_TOR												
                                        Left join PRS_PERSON
                                        ON PRS_MAIN_TOR.OWNER_ID = PRS_PERSON.ID_USER WHERE  PRS_MAIN_TOR.CERRENT_FLOW=@CERRENT_FLOW AND(NAME_TOR like '%" + keyword + "%'" + "or PRS_MAIN_TOR.STATUS like '%" + keyword + "%' or CERRENT_FLOW like'%" + keyword + "%')";
                command.Parameters.Add(new SqlParameter("@ID_COM", (object)id_com ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CERRENT_FLOW", (object)cerrent_flow ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {

                    model.Add(new FormPRSDataModel()
                    {
                        id_tor = reader["ID_TOR"] != DBNull.Value ? (int)reader["ID_TOR"] : 0
                        ,
                        nameProcument = reader["NAME_TOR"] != DBNull.Value ? (string)reader["NAME_TOR"] : ""
                        ,
                        Status = reader["STATUS"] != DBNull.Value ? (string)reader["STATUS"] : ""
                        ,
                        Date = reader["TOR_DATE"] != DBNull.Value ? (DateTime?)reader["TOR_DATE"] : null
                        ,
                        cerrent_flow = reader["CERRENT_FLOW"] != DBNull.Value ? (string)reader["CERRENT_FLOW"] : ""
                        ,
                        Fullname_PRS = reader["FULL_NAME"] != DBNull.Value ? (string)reader["FULL_NAME"] : ""
                    });


                }
                con.Close();
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAllFormTOR(int id_tor)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);

                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;

                command.CommandText = @"DELETE FROM PRS_MAIN_TOR WHERE ID_TOR=@IDTOR
                                        DELETE FROM PRS_TOR_PRODUCT_LIST WHERE ID_TOR=@IDTOR
                                        DELETE FROM PRS_TOR_SUBJECT WHERE ID_TOR=@IDTOR
                                        DELETE FROM PRS_ORDER_DIRACT WHERE ID_TOR=@IDTOR
                                        DELETE FROM PRS_COM_COMMENT WHERE ID_TOR=@IDTOR
                                        DELETE FROM PRS_TOR_ASSIST WHERE ID_TOR=@IDTOR";

                command.Parameters.Add(new SqlParameter("@IDTOR", (object)id_tor ?? DBNull.Value));
                
                command.ExecuteNonQuery();
                connect.Close();

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
