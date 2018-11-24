using Jose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using ToDoList.Models.obj;

namespace ToDoList.Security
{
    public class JwtToken
    {
        private string Key = "ABCD123";
        public string GetToken(string UserText, string IP)
        {
            Dictionary<string, Object> Data = new Dictionary<string, Object>();
            Data.Add("Account", UserText);
            Data.Add("IP", IP);

            string Token = Jose.JWT.Encode(Data, Encoding.UTF8.GetBytes(Key), JwsAlgorithm.HS512);

            return Token;
        } 

        public TokenCheckObj CheckToken(string Token, string IP)
        {
            if (Token is null)
                return new TokenCheckObj { Status = false, ErrMsg = "沒有Token" };
            var JwtObj = Jose.JWT.Decode<Dictionary<string, Object>>(
                Token,
                Encoding.UTF8.GetBytes(Key),
                JwsAlgorithm.HS512);

            if (JwtObj["IP"].ToString() != null && JwtObj["IP"].ToString() != "")
            {
                string Ans = "成功：" + JwtObj["Account"].ToString() + " " + JwtObj["IP"].ToString();
                TokenCheckObj Output = new TokenCheckObj { Status = true, Account = JwtObj["Account"].ToString(), IP = JwtObj["IP"].ToString() };
                return Output;
            }
            else
            {
                TokenCheckObj Output = new TokenCheckObj { Status = false,ErrMsg= "驗證失敗" };
                return Output;
            }
                
        }
    }
}