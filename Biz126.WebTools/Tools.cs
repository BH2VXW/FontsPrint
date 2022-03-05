using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Biz126.WebTools
{
    public class Tools
    {
        #region 取IP地址

        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetIp(HttpContext context)
        {
            string ip = "";
            ip = context.Request.Headers["X-Forwarded-For"].ToString();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Request.Headers["X-Real-IP"].ToString();
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Request.Headers["REMOTE-ADDR"].ToString();
            }
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            ip = ip.Split(',')[0].Trim();

            return ip;
        }

        #endregion
        

        #region 获取指定文件的MD5码
        /// <summary>
        /// 获取指定文件的MD5码
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns>文件MD5值</returns>
        public static string GetFileMd5(string FilePath)
        {
            byte[] data = File.ReadAllBytes(FilePath);
            //byte[] data = System.IO.File.ReadAllBytes(System.Web.HttpContext.Current.Request.MapPath(FilePath));
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] result = md5.ComputeHash(data);
            string Md5str = BitConverter.ToString(result).Replace("-", "");
            return Md5str;
        }
        #endregion

        #region 加解密及编码
        
        /// <summary>
        /// Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Encode(string str)
        {
            byte[] bytes = Encoding.Default.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Decode(string str)
        {
            byte[] outputb = Convert.FromBase64String(str);
            string orgStr = Encoding.Default.GetString(outputb);
            return orgStr;
        }

        /// <summary>
        /// 对字符串MD5加密
        /// </summary>
        /// <param name="Str"></param>
        /// <returns></returns>
        public static string MD5Cryptog(string Str)
        {
            //MD5 md5 = new MD5CryptoServiceProvider();
            //byte[] result=md5.ComputeHash(Encoding.UTF8.GetBytes(Str));
            //return Encoding.UTF8.GetString(result);
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] encryptedBytes = md5.ComputeHash(Encoding.ASCII.GetBytes(Str));
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < encryptedBytes.Length; i++)
            {
                sb.AppendFormat("{0:x2}", encryptedBytes[i]);
            }
            return sb.ToString();

        }
        #endregion

        #region 自定义加密解密

        /// <summary>
        /// 创建Key
        /// </summary>
        /// <returns></returns>
        public static string GenerateKey()
        {
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str">值</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string CreateMD5(string str, string charset)
        {
            StringBuilder sb = new StringBuilder(32);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(Encoding.GetEncoding(charset).GetBytes(str));
            for (int i = 0; i < t.Length; i++)
            {
                sb.Append(t[i].ToString("x").PadLeft(2, '0'));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 自定义MD5加密
        /// </summary>
        /// <param name="pToEncrypt">待加密字符</param>
        /// <param name="sKey">密钥</param>
        /// <returns>加密后字符</returns>
        public static string MD5Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            ret.ToString();
            return ret.ToString();
        }


        /// <summary>
        ///  自定义MD5解密
        /// </summary>
        /// <param name="pToDecrypt">待解密字符串</param>
        /// <param name="sKey">密钥</param>
        /// <returns>原文</returns>
        public static string MD5Decrypt(string pToDecrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];
            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return System.Text.Encoding.Default.GetString(ms.ToArray());
        }
        #endregion
        
        /// <summary>
        /// 自定义正式验证
        /// </summary>
        /// <param name="str"></param>
        /// <param name="regex"></param>
        /// <returns></returns>
        public static bool CustomRegex(string str,string regex)
        {
            return Regex.IsMatch(str, regex);
        }

        /// <summary>
        /// 验证密码只能为数字或字母"#","*","-"
        /// </summary>
        /// <param name="newpassword"></param>
        /// <returns></returns>
        public static bool IsTruePassword(string newpassword)
        {
            return System.Text.RegularExpressions.
                Regex.IsMatch(newpassword, @"^[A-Za-z0-9#*-]*$");
        }

    }
}
