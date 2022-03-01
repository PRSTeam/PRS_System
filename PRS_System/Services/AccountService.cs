using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PRS_System.IServices;
using PRS_System.Models.Data;

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

        public void EditUser()
        {
            throw new NotImplementedException();
        }
    }
}
