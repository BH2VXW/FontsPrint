using System;
using System.Collections.Generic;
using System.Web;
using System.Net;
using System.IO;
using System.Text;
using System.Net.Security;    
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Biz126.WebTools
{
    /// <summary>
    /// http连接基础类，负责底层的http通信
    /// </summary>
    public class HttpService
    {

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开    
            return true;
        }


        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="url">服务器</param>
        /// <returns></returns>
        public static string Post(string data, string url)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            int timeout = 30;
            string charset = "utf-8";

            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);
                
                request.Method = "POST";
                request.Timeout = timeout * 1000;

                ////设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = string.Format("application/x-www-form-urlencoded;charset={0}", charset);
                byte[] res = System.Text.Encoding.GetEncoding(charset).GetBytes(data);
                request.ContentLength = res.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(res, 0, res.Length);
                reqStream.Close();
                
                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(charset));              

                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    //log.write(string.Format("[HttpService] [StatusCode]={0}; [StatusDescription]={1}", ((HttpWebResponse)e.Response).StatusCode, ((HttpWebResponse)e.Response).StatusDescription), "error");
                }
            }
            catch (Exception e)
            {
                //log.write(string.Format("[HttpService] [Exception]={0}", e.ToString()), "error");
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// Post提交数据
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="url">目标地址</param>
        /// <param name="contentType">数据类型</param>
        /// <param name="charset">编码</param>
        /// <returns></returns>
        public static string Post(string data, string url,string contentType,string charset= "utf-8")
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            int timeout = 30;
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            Stream reqStream = null;

            try
            {
                //设置最大连接数
                ServicePointManager.DefaultConnectionLimit = 200;                
                //设置https验证方式
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                {
                    ServicePointManager.ServerCertificateValidationCallback =
                            new RemoteCertificateValidationCallback(CheckValidationResult);
                }

                /***************************************************************
                * 下面设置HttpWebRequest的相关属性
                * ************************************************************/
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "POST";
                request.Timeout = timeout * 1000;

                ////设置代理服务器
                //WebProxy proxy = new WebProxy();                          //定义一个网关对象
                //proxy.Address = new Uri(WxPayConfig.PROXY_URL);              //网关服务器端口:端口
                //request.Proxy = proxy;

                //设置POST的数据类型和长度
                request.ContentType = $"{contentType};charset={charset}";
                byte[] res = System.Text.Encoding.GetEncoding(charset).GetBytes(data);
                request.ContentLength = res.Length;

                //往服务器写入数据
                reqStream = request.GetRequestStream();
                reqStream.Write(res, 0, res.Length);
                reqStream.Close();

                //获取服务端返回
                response = (HttpWebResponse)request.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(response.GetResponseStream(), System.Text.Encoding.GetEncoding(charset));

                result = sr.ReadToEnd().Trim();
                sr.Close();
            }
            catch (System.Threading.ThreadAbortException e)
            {
                Logger.Log4Net.ErrorInfo("[POST提交]-[ThreadAbortException]异常", e);
                System.Threading.Thread.ResetAbort();
            }
            catch (WebException e)
            {
                Logger.Log4Net.ErrorInfo("[POST提交]-[WebException]异常", e);
                if (e.Status == WebExceptionStatus.ProtocolError)
                {
                    Logger.Log4Net.ErrorInfo("[POST提交]-[WebException-ProtocolError]异常", e);
                }
            }
            catch (Exception e)
            {
                Logger.Log4Net.ErrorInfo("[POST提交]-[Exception]异常", e);
            }
            finally
            {
                //关闭连接和流
                if (response != null)
                {
                    response.Close();
                }
                if (request != null)
                {
                    request.Abort();
                }
            }
            return result;
        }

        /// <summary>
        /// 异步Post
        /// </summary>
        /// <param name="dictParams">需要提交的数据字典</param>
        /// <param name="url">目标地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(Dictionary<string,string> dictParams, string url,int timeout=90)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            using(var http=new HttpClient())
            {
                http.DefaultRequestHeaders.Add("KeepAlive", "false");   //不保持连接  
                http.DefaultRequestHeaders.ExpectContinue = false;  //关闭Expect:[100-continue]，默认为开启状态   很多旧的HTTP/1.0和HTTP/1.1应用不支持Expect头部
                http.Timeout = TimeSpan.FromSeconds(timeout);
                var responseData = await http.PostAsync(url, new FormUrlEncodedContent(dictParams));
                result = await responseData.Content.ReadAsStringAsync();
            }
            return result;
        }

        /// <summary>
        /// 异步Post提交
        /// </summary>
        /// <param name="paramsData">提交的参数（格式：key1=value1&key2=value2&key3=value3...）</param>
        /// <param name="url">目标地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static async Task<string> PostAsync(string paramsData, string url,int timeout=90,string charset="utf-8")
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果
            try
            {
                using (var http = new HttpClient())
                {
                    //http.DefaultRequestHeaders.Add("KeepAlive", "false");   //不保持连接
                    http.DefaultRequestHeaders.ExpectContinue = false;  //关闭Expect:[100-continue]，默认为开启状态   很多旧的HTTP/1.0和HTTP/1.1应用不支持Expect头部
                    //http.DefaultRequestHeaders.Add("Method", "Post");
                    http.Timeout = TimeSpan.FromSeconds(timeout);
                    
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    //if (!string.IsNullOrEmpty(paramsData))
                    //{
                    //    string[] arr_params = paramsData.Split('&');
                    //    foreach (string param in arr_params)
                    //    {
                    //        string[] arr_param = param.Split('=');
                    //        if (!string.IsNullOrEmpty(arr_param[1]))
                    //            dict.Add(arr_param[0], arr_param[1]);
                    //    }
                    //}

                    //var content = new FormUrlEncodedContent(dict);               
                    //content.Headers.ContentType.CharSet = charset;

                    var content = new StringContent(paramsData, Encoding.GetEncoding(charset), "application/x-www-form-urlencoded");
                    
                    var responseData = await http.PostAsync(url, content);

                    result = await responseData.Content.ReadAsStringAsync();
                }
            }
            catch(Exception e)
            {
                result = e.ToString();
            }
            
            return result;
        }

        /// <summary>
        /// 异步Post提交
        /// </summary>
        /// <param name="paramsData">提交的参数（格式：key1=value1&key2=value2&key3=value3...）</param>
        /// <param name="url">目标地址</param>
        /// <param name="timeout">超时时间</param>
        /// <returns></returns>
        public static async Task<Stream> PostStreamAsync(string paramsData, string url, int timeout = 90)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            Stream result ;//返回结果
            try
            {
                using (var http = new HttpClient())
                {
                    http.DefaultRequestHeaders.Add("KeepAlive", "false");   //不保持连接
                    http.DefaultRequestHeaders.ExpectContinue = false;  //关闭Expect:[100-continue]，默认为开启状态   很多旧的HTTP/1.0和HTTP/1.1应用不支持Expect头部
                    //http.DefaultRequestHeaders.Add("Method", "Post");
                    http.Timeout = TimeSpan.FromSeconds(timeout);
                    Dictionary<string, string> dict = new Dictionary<string, string>();
                    if (!string.IsNullOrEmpty(paramsData))
                    {
                        string[] arr_params = paramsData.Split('&');
                        foreach (string param in arr_params)
                        {
                            string[] arr_param = param.Split('=');
                            if (!string.IsNullOrEmpty(arr_param[1]))
                                dict.Add(arr_param[0], arr_param[1]);
                        }
                    }

                    var content = new FormUrlEncodedContent(dict);
                    var responseData = await http.PostAsync(url, content);
                    result = await responseData.Content.ReadAsStreamAsync();
                }
            }
            catch (Exception e)
            {
                result = null;
            }

            return result;
        }

        //public static async Task<string> PostAsync(JObject json, string url)
        //{
        //    System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

        //    string result = "";//返回结果

        //    using (var http = new HttpClient())
        //    {
        //        http.DefaultRequestHeaders.Add("KeepAlive", "false");
        //        http.DefaultRequestHeaders.Add("Method", "Post");
        //        http.Timeout = TimeSpan.FromSeconds(30);
        //        Dictionary<string, string> dict = new Dictionary<string, string>();
        //        if (!string.IsNullOrEmpty(paramsData))
        //        {
        //            string[] arr_params = paramsData.Split('&');
        //            foreach (string param in arr_params)
        //            {
        //                string[] arr_param = param.Split('=');
        //                if (!string.IsNullOrEmpty(arr_param[1]))
        //                    dict.Add(arr_param[0], arr_param[1]);
        //            }
        //        }

        //        var content = new FormUrlEncodedContent(dict);
        //        var responseData = await http.PostAsync(url, content);
        //        result = await responseData.Content.ReadAsStringAsync();
        //    }
        //    return result;
        //}

        public static async Task<string> GetAsync(string url)
        {
            System.GC.Collect();//垃圾回收，回收没有正常关闭的http连接

            string result = "";//返回结果

            using (var http = new HttpClient())
            {
                http.DefaultRequestHeaders.Add("KeepAlive", "false");
                http.DefaultRequestHeaders.Add("Method", "GET");
                http.Timeout = TimeSpan.FromSeconds(30);
                var responseData = await http.GetAsync(url);
                result = await responseData.Content.ReadAsStringAsync();
            }
            return result;
        }

        /// <summary>
        /// GET
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string Get(string url)
        {

            string result = "";//返回结果
            Dictionary<string, string> rest = new Dictionary<string, string>();
            try
            {
                HttpWebRequest myHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myHttpWebRequest.Timeout = 20 * 1000; //连接超时
                myHttpWebRequest.Accept = "*/*";
                myHttpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 5.1; Trident/4.0;)";
                myHttpWebRequest.CookieContainer = new CookieContainer(); //暂存到新实例
                string charset = "utf-8";
                myHttpWebRequest.Method = "get";

                HttpWebResponse myHttpWebResponse = (HttpWebResponse)myHttpWebRequest.GetResponse();

                //获取服务端返回数据
                StreamReader sr = new StreamReader(myHttpWebResponse.GetResponseStream(), System.Text.Encoding.GetEncoding(charset));

                result = sr.ReadToEnd().Trim();
                sr.Close();



                //FileStream writer = new FileStream(System.Web.HttpContext.Current.Server.MapPath("\\temp\\image\\vericode.jpg"), FileMode.OpenOrCreate, FileAccess.Write);
                //byte[] buff = new byte[512];
                //int c = 0; //实际读取的字节数
                //while ((c = stream.Read(buff, 0, buff.Length)) > 0)
                //{
                //    writer.Write(buff, 0, c);
                //}
                //writer.Close();
                //writer.Dispose();
                myHttpWebRequest.GetResponse().Close();


            }
            catch
            {

            }
            return result;
        }

        
    }
}