using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Data;
using PRS_System.Models.Setting;

namespace PRS_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly string _connectionString;
        

        public AccountService(ILogger<AccountService> logger)
        {
            _logger = logger;
            _connectionString = Startup.ConnectionString;
        }

        public void AddNewUser(UserDataModel datauser)
        {
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"insert into PRS_PERSON (ID_USER,PRE_NAME,FULL_NAME,OPERATING_POS,MANAGEMENT_POS,EMAIL,[SIGNATURE],CATEGORY,[STATUS]) 
VALUES(@ID_USER,@PRE_NAME,@FULLNAME,@User_Type_Operation,@User_Type_Magnement,@Email,@SIGNATURE,@CATEGORY,@STATUS) ";
                command.Parameters.Add(new SqlParameter("@ID_USER",(object)datauser.UserID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FULLNAME", (object)datauser.Full_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PRE_NAME", (object)datauser.Prefix_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@User_Type_Operation", (object)datauser.User_Type_Operation ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@User_Type_Magnement", (object)datauser.User_Type_Magnement?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Email", (object)datauser.Email ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SIGNATURE", (object)datauser.ImgName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CATEGORY", (object)datauser.Category ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@STATUS", (object)datauser.Status ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch(Exception ex)
            {
                _logger.LogError("Caught Exception: {0}", ex.ToString());
                throw ex;
            }
            
        }

        public IEnumerable<UserDataModel> GetAllAccountWithKeyword(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<UserDataModel>GetDataUser(string keyword)
        {
            try
            {
                List<UserDataModel> userdata = new List<UserDataModel>();
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"select * from PRS_PERSON 
WHERE ID_USER like '%"+ keyword + "%' or PRE_NAME like '%" + keyword + "%' or FULL_NAME like '%" + keyword + "%' or OPERATING_POS like '%" + keyword + "%' or MANAGEMENT_POS like '%" + keyword + "%' or EMAIL like '%" + keyword + "%' or CATEGORY like '%" + keyword + "%' or [STATUS] like '%" + keyword + "%' ";
                //command.Parameters.Add(new SqlParameter("@ID_USER", (object)user_id ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    UserDataModel user = new UserDataModel();
                    user.UserID = reader["ID_USER"] != DBNull.Value ? reader["ID_USER"].ToString() : null;
                    user.Prefix_NameThai = reader["PRE_NAME"] != DBNull.Value ? reader["PRE_NAME"].ToString() : null;
                    user.Full_NameThai = reader["FULL_NAME"] != DBNull.Value ? reader["FULL_NAME"].ToString() : null;
                    user.Operate_Pos = reader["OPERATING_POS"] != DBNull.Value ? reader["OPERATING_POS"].ToString() : null;
                    user.Manage_Pos = reader["MANAGEMENT_POS"] != DBNull.Value ? reader["MANAGEMENT_POS"].ToString() : null;
                    user.Email = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : null;
                    user.ESignature = reader["SIGNATURE"] != DBNull.Value ? reader["SIGNATURE"].ToString() : null;
                    user.Category = reader["CATEGORY"] != DBNull.Value ? reader["CATEGORY"].ToString() : null;
                    user.Status = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null;
                    userdata.Add(user);
                }
                connect.Close();
                return userdata;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public string GetSignature(string user_id)
        {
            try
            {
                string namefile = "";
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"select * from PRS_PERSON WHERE ID_USER=@ID_USER";
                command.Parameters.Add(new SqlParameter("@ID_USER", (object)user_id ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    namefile = reader["SIGNATURE"] != DBNull.Value ? reader["SIGNATURE"].ToString() : null;
                   
                }
                connect.Close();
                return namefile;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public UserDataModel CheckLogin(string user_id)
        {
            UserDataModel userdata = new UserDataModel();
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();

                connect.Open();
                command.Connection = connect;
                command.CommandText = @"select * from PRS_PERSON WHERE ID_USER = @ID_USER";
                command.Parameters.Add(new SqlParameter("@ID_USER", (object)user_id ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userdata.UserID = reader["ID_USER"] != DBNull.Value ? reader["ID_USER"].ToString() : null;
                    userdata.Prefix_NameThai = reader["PRE_NAME"] != DBNull.Value ? reader["PRE_NAME"].ToString() : null;
                    //userdata.Prefix_NameEng = reader["ENG_NAME"] != DBNull.Value ? reader["ENG_NAME"].ToString() : null;
                    userdata.Full_NameThai = reader["FULL_NAME"] != DBNull.Value ? reader["FULL_NAME"].ToString() : null;
                    userdata.Operate_Pos = reader["OPERATING_POS"] != DBNull.Value ? reader["OPERATING_POS"].ToString() : "-";
                    userdata.Manage_Pos = reader["MANAGEMENT_POS"] != DBNull.Value ? reader["MANAGEMENT_POS"].ToString() : "-";
                    //userdata.Full_NameEng = reader["ENG_NAME_FULL"] != DBNull.Value ? reader["ENG_NAME_FULL"].ToString() : null;
                    //userdata.User_Type = reader["USER_TYPE"] != DBNull.Value ? reader["USER_TYPE"].ToString() : null;
                    userdata.Email = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : null;
                    userdata.ESignature = reader["SIGNATURE"] != DBNull.Value ? reader["SIGNATURE"].ToString() : null;
                    userdata.Category = reader["CATEGORY"] != DBNull.Value ? reader["CATEGORY"].ToString() : null;
                    userdata.Status = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null;
                }
                connect.Close();
                return userdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void EditUser(UserDataModel datauser)
        {

            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();
                connect.Open();
                command.Connection = connect;
                command.CommandText = @"UPDATE PRS_PERSON SET PRE_NAME=@PRE_NAME,FULL_NAME=@FULLNAME,OPERATING_POS=@User_Type_Operation,MANAGEMENT_POS=@User_Type_Magnement,EMAIL=@Email,[SIGNATURE]=@SIGNATURE,CATEGORY=@CATEGORY,[STATUS]=@STATUS
WHERE ID_USER=@ID_USER ";
                command.Parameters.Add(new SqlParameter("@ID_USER", (object)datauser.UserID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@FULLNAME", (object)datauser.Full_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@PRE_NAME", (object)datauser.Prefix_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@User_Type_Operation", (object)datauser.User_Type_Operation ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@User_Type_Magnement", (object)datauser.User_Type_Magnement ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@Email", (object)datauser.Email ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@SIGNATURE", (object)datauser.ImgName ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@CATEGORY", (object)datauser.Category ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@STATUS", (object)datauser.Status ?? DBNull.Value));
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception ex)
            {
                _logger.LogError("Caught Exception: {0}", ex.ToString());
                throw ex;
            }
        }

        public UserDataModel CheckPositionManegment(string position)
        {
            UserDataModel userdata = new UserDataModel();
            try
            {
                SqlConnection connect = new SqlConnection(_connectionString);
                SqlCommand command = new SqlCommand();

                connect.Open();
                command.Connection = connect;
                command.CommandText = @"select * from PRS_PERSON WHERE MANAGEMENT_POS = @MANAGEMENT_POS";
                command.Parameters.Add(new SqlParameter("@MANAGEMENT_POS", (object)position ?? DBNull.Value));
                SqlDataReader reader;
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    userdata.UserID = reader["ID_USER"] != DBNull.Value ? reader["ID_USER"].ToString() : null;
                    userdata.Prefix_NameThai = reader["PRE_NAME"] != DBNull.Value ? reader["PRE_NAME"].ToString() : null;
                    //userdata.Prefix_NameEng = reader["ENG_NAME"] != DBNull.Value ? reader["ENG_NAME"].ToString() : null;
                    userdata.Full_NameThai = reader["FULL_NAME"] != DBNull.Value ? reader["FULL_NAME"].ToString() : null;
                    userdata.Operate_Pos = reader["OPERATING_POS"] != DBNull.Value ? reader["OPERATING_POS"].ToString() : "-";
                    userdata.Manage_Pos = reader["MANAGEMENT_POS"] != DBNull.Value ? reader["MANAGEMENT_POS"].ToString() : "-";
                    //userdata.Full_NameEng = reader["ENG_NAME_FULL"] != DBNull.Value ? reader["ENG_NAME_FULL"].ToString() : null;
                    //userdata.User_Type = reader["USER_TYPE"] != DBNull.Value ? reader["USER_TYPE"].ToString() : null;
                    userdata.Email = reader["EMAIL"] != DBNull.Value ? reader["EMAIL"].ToString() : null;
                    userdata.ESignature = reader["SIGNATURE"] != DBNull.Value ? reader["SIGNATURE"].ToString() : null;
                    userdata.Category = reader["CATEGORY"] != DBNull.Value ? reader["CATEGORY"].ToString() : null;
                    userdata.Status = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null;
                }
                connect.Close();
                return userdata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
