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
                command.CommandText = @"INSERT INTO PRS_PERSON (ID_USER,TH_NAME,ENG_NAME,TH_NAME_FULL,ENG_NAME_FULL,USER_TYPE,SIGNATURE,CATEGORY,STATUS) VALUES(@ID_USER,@TH_NAME,@ENG_NAME,@TH_NAME_FULL,@ENG_NAME_FULL,@USER_TYPE,@SIGNATURE,@CATEGORY,@STATUS) ";
                command.Parameters.Add(new SqlParameter("@ID_USER",(object)datauser.UserID ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TH_NAME", (object)datauser.Full_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ENG_NAME", (object)datauser.Full_NameEng ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@TH_NAME_FULL", (object)datauser.Prefix_NameThai ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@ENG_NAME_FULL", (object)datauser.Prefix_NameEng ?? DBNull.Value));
                command.Parameters.Add(new SqlParameter("@USER_TYPE", (object)datauser.User_Type ?? DBNull.Value));
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

        public List<UserDataModel>GetDataUser(string user_id)
        {
            try
            {
                List<UserDataModel> userdata = new List<UserDataModel>();
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
                    UserDataModel user = new UserDataModel();
                    user.UserID=reader["ID_USER"] != DBNull.Value ? reader["ID_USER"].ToString() : null;
                    user.UserID = reader["TH_NAME"] != DBNull.Value ? reader["TH_NAME"].ToString() : null;
                    user.UserID = reader["ENG_NAME"] != DBNull.Value ? reader["ENG_NAME"].ToString() : null;
                    user.UserID = reader["THAI_NAME_FULL"] != DBNull.Value ? reader["THAI_NAME_FULL"].ToString() : null;
                    user.UserID = reader["ENG_NAME_FULL"] != DBNull.Value ? reader["ENG_NAME_FULL"].ToString() : null;
                    user.UserID = reader["USER_TYPE"] != DBNull.Value ? reader["USER_TYPE"].ToString() : null;
                    user.UserID = reader["SIGNATURE"] != DBNull.Value ? reader["SIGNATURE"].ToString() : null;
                    user.UserID = reader["CATEGORY"] != DBNull.Value ? reader["CATEGORY"].ToString() : null;
                    user.UserID = reader["STATUS"] != DBNull.Value ? reader["STATUS"].ToString() : null;
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
                    userdata.Prefix_NameThai = reader["TH_NAME"] != DBNull.Value ? reader["TH_NAME"].ToString() : null;
                    userdata.Prefix_NameEng = reader["ENG_NAME"] != DBNull.Value ? reader["ENG_NAME"].ToString() : null;
                    userdata.Full_NameThai = reader["TH_NAME_FULL"] != DBNull.Value ? reader["TH_NAME_FULL"].ToString() : null;
                    userdata.Full_NameEng = reader["ENG_NAME_FULL"] != DBNull.Value ? reader["ENG_NAME_FULL"].ToString() : null;
                    userdata.User_Type = reader["USER_TYPE"] != DBNull.Value ? reader["USER_TYPE"].ToString() : null;
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
