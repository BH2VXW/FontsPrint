using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Biz126.WebTools
{
    public static class Rand
    {
        /// <summary>        
        /// 随机系数        
        /// </summary>
        public static int _RandIndex = 0;

        #region RndInt
        /// <summary>        
        /// 数字随机数        
        /// </summary>        
        /// <param name="num1">开始</param>        
        /// <param name="num2">结束</param>        
        /// <returns>从多少到多少之间的数据 包括开始不包括结束</returns>
        public static int RndInt(int num1, int num2) 
        { 
            if (_RandIndex >= 1000000) _RandIndex = 1; 
            Random rnd = new Random(DateTime.Now.Millisecond + _RandIndex); 
            _RandIndex++; 
            return rnd.Next(num1, num2);
        }        
        
        public static IList<int> RndInt(int num1, int num2, int len) 
        { 
            IList<int> list = new List<int>(); 
            for (int i = 0; i < len; i++) 
                list.Add(RndInt(num1, num2)); 
            return list; 
        }        
        public static IList<int> RndInt(int len) 
        { 
            IList<int> list = RndInt(0, int.MaxValue, len); 
            return list; 
        }
        #endregion

        #region RndNum        
        /// <summary>        
        /// 数字随机数        
        /// </summary>       
        /// /// <param name="length">生成长度</param>        
        /// <returns>返回指定长度的数字随机串</returns>        
        
        public static string RndNum(int length)        
        {            
            if (_RandIndex >= 1000000) _RandIndex = 1;            
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };            
            StringBuilder num = new StringBuilder();            
            Random rnd = new Random(DateTime.Now.Millisecond + _RandIndex);            
            for (int i = 0; i < length; i++) 
            {               
                num.Append(arrChar[rnd.Next(0, 9)].ToString());            
            }            
            return num.ToString();
        } 
        #endregion

        #region RndDateStr
        /// <summary>        
        /// 日期随机函数        
        /// </summary>        
        /// <returns>返回日期随机串</returns>        
        
        public static string RndDateStr()        
        {            
            return DateTime.Now.ToString("yyyyMMddHHmmssfff") + Rand.RndInt(1000, 9999).ToString();        
        }        
        
        public static IList<string> RndDateStr(int len) 
        {            
            IList<string> list = new List<string>();            
            for (int i = 0; i < len; i++) list.Add(RndDateStr());            
            return list;        
        }        
        
        #endregion
        
        #region RndCode
        /// <summary>        
        /// 数字和字母随机数        
        /// </summary>        
        /// <param name="length">生成长度</param>        
        /// <returns>返回指定长度的数字和字母的随机串</returns>        
        
        public static string RndCode(int length)        
        {            
            if (_RandIndex >= 1000000) _RandIndex = 1;            
            char[] arrChar = new char[]
            {               
                'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',               
                '0','1','2','3','4','5','6','7','8','9',               
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
            };            
            System.Text.StringBuilder num = new System.Text.StringBuilder();            
            Random rnd = new Random(DateTime.Now.Millisecond + _RandIndex);            
            for (int i = 0; i < length; i++) 
            {                
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());            
            }            
            return num.ToString();        
        }        
        public static IList<string> RndCodeList(int len) 
        {            
            IList<string> list = new List<string>();            
            for (int i = 0; i < len; i++) 
                list.Add(RndCode(len));            
            return list;        
        }        
        
        #endregion        
        
        #region RndLetter        
        /// <summary>        
        /// 字母随机数        
        /// </summary>        
        /// <param name="length">生成长度</param>        
        /// <returns>返回指定长度的字母随机数</returns>        
        
        public static string RndLetter(int length) 
        {            
            if (_RandIndex >= 1000000) _RandIndex = 1;            
            char[] arrChar = new char[]{                
                'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',                
                '_',                
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
            };            
            StringBuilder num = new StringBuilder();            
            Random rnd = new Random(DateTime.Now.Millisecond + _RandIndex);            
            for (int i = 0; i < length; i++) 
            {                
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());            
            }            
            return num.ToString();        
        }        
        public static IList<string> RndLetterList(int len) 
        {            
            IList<string> list = new List<string>();            
            for (int i = 0; i < len; i++) 
                list.Add(RndLetter(len));            
            return list;        
        }        
        #endregion        
        
        #region GetGuid        
        /// <summary>        
        /// 生成GUID        
        /// </summary>        
        /// <returns></returns>        
        public static string GetGuid()        
        {            
            System.Guid g = System.Guid.NewGuid();           
            return g.ToString();        
        }

        public static string RndByGuid(int length)
        {
            if (_RandIndex >= 1000000) _RandIndex = 1;
            char[] arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            StringBuilder num = new StringBuilder();
            Random rnd = new Random(Guid.NewGuid().GetHashCode());
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, 9)].ToString());
            }
            return num.ToString();
        }
        
        public static IList<string> GetGuid(int len) 
        {            
            IList<string> list = new List<string>();            
            for (int i = 0; i < len; i++) 
                list.Add(GetGuid());            
            return list;        
        }        
        #endregion
    }
}
