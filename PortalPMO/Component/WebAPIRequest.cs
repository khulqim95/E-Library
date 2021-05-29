using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using PortalPMO.ViewModels;
using PortalPMO.Component;

namespace PortalPMO.Content
{
    public class WebAPIRequest
    {
        public ResponseGetViewModel RequestPost(String datakirim,string alamatrequest)
        {
            String str = "";
            //String alamatrequest = GetConfig.AppSetting["AlamatService:Url_WallOfFrame"] + GetConfig.AppSetting["AlamatService:EndPoint:POST_AbsensiWallOfFrame"];
            ResponseGetViewModel objresp = new ResponseGetViewModel();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(alamatrequest);
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(datakirim);
                request.ContentType = "application/json";//"application/x-www-form-urlencoded";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                str = responseFromServer;
                reader.Close();
                dataStream.Close();
                response.Close();
                //Console.ReadLine();
            }
            catch (Exception ex)
            {
                objresp.ErrorMessage = ex.Message;
                str = "0";
            }

            objresp.HasilRespon = str;

            return objresp;
        }


        public ResponseGetViewModel RequestPost(String namamethod, String datakirim, String alamatrequest)
        {
            String str = "";
           // String alamatrequest = System.Web.Configuration.WebConfigurationManager.AppSettings["AbsensiServiceAddress"].ToString() + "/" + namamethod;
            ResponseGetViewModel objresp = new ResponseGetViewModel();
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(alamatrequest+namamethod);
                request.Method = "POST";

                byte[] byteArray = Encoding.UTF8.GetBytes(datakirim);
                request.ContentType = "application/json";
                request.ContentLength = byteArray.Length;

                Stream dataStream = request.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();

                WebResponse response = request.GetResponse();
                // Console.WriteLine(((HttpWebResponse)response).StatusDescription);
                dataStream = response.GetResponseStream();

                StreamReader reader = new StreamReader(dataStream);
                string responseFromServer = reader.ReadToEnd();
                str = responseFromServer;
                reader.Close();
                dataStream.Close();
                response.Close();
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                objresp.ErrorMessage = ex.Message;
                str = "0";
            }

            objresp.HasilRespon = str;

            return objresp;
        }


        public ResponseGetViewModel RequestGet(String namamethod, String datakirim)
        {
            ResponseGetViewModel objresp = new ResponseGetViewModel();
            String strRespon = "";
            String alamatrequest = GetConfig.AppSetting["AlamatService"] + "/" + namamethod + "?" + datakirim;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(alamatrequest);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    strRespon = reader.ReadToEnd();
                }
            }

            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                strRespon = "0";
                objresp.ErrorMessage = ex.Message;// +", Detail: " + ex.StackTrace;
            }

            objresp.HasilRespon = strRespon;

            return objresp;
        }


        public ResponseGetViewModel RequestGet(String namamethod, String datakirim, String alamatrequest)
        {
            ResponseGetViewModel objresp = new ResponseGetViewModel();
            String strRespon = "";
            //String alamatrequest = System.Web.Configuration.WebConfigurationManager.AppSettings["AbsensiServiceAddress"].ToString() + "/" + namamethod + "?" + datakirim;


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(alamatrequest+namamethod);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    strRespon = reader.ReadToEnd();
                }
            }

            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText
                }
                strRespon = "0";
                objresp.ErrorMessage = ex.Message;// +", Detail: " + ex.StackTrace;
            }

            objresp.HasilRespon = strRespon;

            return objresp;
        }


    }
}