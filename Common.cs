using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MahAppsTestt
{
    public static class Common
    {
        private static string appKey = "5e62b542675667357a504335b4a9da01";

        #region 获取天气预报根据城市名
        
        public static string RequestWeather(string CityName)
        {
            string strURL = "http://apis.baidu.com/apistore/weatherservice/cityname?cityname=" + CityName;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", appKey);
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
        #endregion
        
        #region 获取本机外网IP
        public static string GetIP()
        {
            string tempip = "";
            try
            {
                //http://1212.ip138.com/ic.asp
                //http://city.ip138.com/ip2city.asp
                HttpWebRequest wr = (HttpWebRequest)WebRequest.Create("http://1212.ip138.com/ic.asp");
                wr.Method = "GET";
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                string all = sr.ReadToEnd(); //读取网站的数据
                int start = all.IndexOf("[") + 1;
                int end = all.IndexOf("]", start);
                tempip = all.Substring(start, end - start);
                sr.Close();
                s.Close();
            }
            catch(Exception ee)
            {
            }
            return tempip;
        }
        #endregion

        #region 根据外网IP获取地点
        public static string RequestLocation(string url, string param)
        {
            string strURL = url + '?' + param;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "GET";
            // 添加header
            request.Headers.Add("apikey", appKey);
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)request.GetResponse();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = Reader.ReadLine()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }

        #region 解析地址JSON
        /// <summary>
        /// 解析地址JSON
        /// </summary>
        /// <param name="StrJson"></param>
        /// <returns></returns>
        public static string JieXiLocationJson(string StrJson)
        {
            string rest = "error";
            JObject googleSearch = JObject.Parse(StrJson);
            // get JSON result objects into a list
            string errMsg = googleSearch["errMsg"].ToString();
            if (errMsg == "success")
            {
                rest = googleSearch["retData"]["city"].ToString();
            }
            return rest;
        }
        #endregion

        #region 解析天气预报城市码JSON
        public static string JieXiCodeJson(string StrJson)
        {
            string rest = "";
            JObject googleSearch = JObject.Parse(StrJson);
            // get JSON result objects into a list
            string errMsg = googleSearch["errMsg"].ToString();
            
            if (errMsg == "success")
            {
                rest = googleSearch["retData"]["weather"].ToString();
            }
            return rest;
        }
        #endregion
        #endregion
        
    }
    public class Weather
    {
        /* 摘要
        city: "北京", //城市
   pinyin: "beijing", //城市拼音
   citycode: "101010100",  //城市编码	
   date: "15-02-11", //日期
   time: "11:00", //发布时间
   postCode: "100000", //邮编
   longitude: 116.391, //经度
   latitude: 39.904, //维度
   altitude: "33", //海拔	
   weather: "晴",  //天气情况
   temp: "10", //气温
   l_tmp: "-4", //最低气温
   h_tmp: "10", //最高气温
   WD: "无持续风向",	 //风向
   WS: "微风(<10m/h)", //风力
   sunrise: "07:12", //日出时间
   sunset: "17:44" //日落时间*/
        public string city { get; set; }
        public string pinyin { get; set; }
        public string citycode { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string postCode { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string altitude { get; set; }
        public string weather { get; set; }
        public string temp { get; set; }
        public string l_tmp { get; set; }
        public string h_tmp { get; set; }
        public string WD { get; set; }
        public string WS { get; set; }
        public string sunrise { get; set; }
        public string sunset { get; set; }
    }
}
