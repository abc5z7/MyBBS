using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyBBS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        [HttpGet]
        public string Get(string userNo, string password)
        {
            DataRow dr = null;
            using SqlConnection conn = new SqlConnection("server=.;database=MyBBS;uid=sa;pwd=123456");

            conn.Open();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users", conn);
            var sda = new SqlDataAdapter(cmd);
            var ds = new DataSet();
            sda.Fill(ds);
            var res = ds.Tables[0];
            dr = res.Rows[0];


            var resUserNo = dr["UserNo"].ToString();
            var resPassword = dr["Password"].ToString();
            if (resUserNo != userNo || resPassword != password)
            {
                return "用户名或密码错误";
            }
            else
            {
                return "登录成功";
            }
        }

        [HttpPost]
        public string Insert(string userNo, string userName, string userLevel, string password)
        {
            using SqlConnection conn = new SqlConnection("server=.;database=MyBBS;uid=sa;pwd=123456");
            conn.Open();
            var cmd= new SqlCommand($"INSERT INTO Users (UserNo, UserName, UserLevel, Password) VALUES ('{userNo}', '{userName}', '{userLevel}', '{password}')", conn);
            cmd.ExecuteNonQuery();
            return "注册成功";
        }

        [HttpPut]
        public string Update()
        {

            using SqlConnection conn = new SqlConnection("server=.;database=MyBBS;uid=sa;pwd=123456");
            conn.Open();
            var cmd = new SqlCommand("UPDATE Users SET UserName = 'testUser' WHERE UserNo = 'testUser'", conn);
            cmd.ExecuteNonQuery();
            return "更新成功";
        }

        [HttpDelete]
        public string Remove(string userNo, string userName)
        {

              using SqlConnection conn = new SqlConnection("server=.;database=MyBBS;uid=sa;pwd=123456");
            conn.Open();
            var cmd = new SqlCommand("DELETE FROM Users WHERE UserNo=@userNo AND UserName=@userName", conn);
            var sqlParameter = new SqlParameter[]
            {
                new SqlParameter
                {
                    ParameterName = "@userNo",
                    Value = userNo
                },
                new SqlParameter
                {
                    ParameterName = "@UserName",
                    Value = userName
                }
            };
            cmd.Parameters.AddRange(sqlParameter);
            cmd.ExecuteNonQuery();
            return "删除成功";
        }

    }
}
