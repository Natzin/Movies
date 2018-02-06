/*TASK NetworkConect*/
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Movies
{
    //=================================================================================================================

    public class NetworkConect
    {
        //-------------------------------------------------------------------------------------------------------------

        //                                                  //Variable de Obtencion de token despues del login
        public String Access
        {
            get;
            set;
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static void Login(
            String strUser,
            String strPassword,
            out String Token
            )
        {
            String Url = "https://baas.kinvey.com/user/kid_SyXkBdMVM/login";

            //                                              //String del Json que se utiliza para la obtencion del
            //                                              //      token de login
            string PostData = "{\"username\":\"" + strUser + "\",\"password\":\"" + strPassword + "\"}";

            DataConect(Url, PostData, out Token);

        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static void Register(
            UserData Datos,
            out String Token
            )
        {
            String Url = "https://baas.kinvey.com/user/kid_SyXkBdMVM";

            //                                              //String del Json que se utilizara para dar de alta a nuevo
            //                                              //      usuario en el web service
            string PostData = "{\"username\":\"" + Datos.username + "\",\"password\":\"" + Datos.password +
                "\",\"name\":\"" + Datos.name + "\",\"phone\":\"" + Datos.phone + "\",\"mail\":\"" + Datos.mail
                + "\"}";

            DataConect(Url, PostData, out Token);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public void MoviesList(
            Action<Movie[]> success,
            Action error
            )
        {
            String url = "https://baas.kinvey.com/appdata/kid_SyXkBdMVM/movies";

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(new Uri(url));

            httpWReq.Headers["Authorization"] = "Kinvey " + Access;
            httpWReq.Headers["X-Kinvey-API-Version"] = "3";
            httpWReq.ContentType = "application/json";

            httpWReq.Method = "GET";

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String Json = reader.ReadToEnd();

            Movie[] Movies = JsonConvert.DeserializeObject<Movie[]>(Json);
            success(Movies);
        }

        //- - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
        public static void DataConect(
            String Url,
            String PostData,
            out String Token
            )
        {
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(Url);
            Encoding encoding = new UTF8Encoding();

            byte[] data = encoding.GetBytes(PostData);

            httpWReq.ProtocolVersion = HttpVersion.Version11;
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/json";
            httpWReq.Headers[HttpRequestHeader.Authorization] = "Basic a2lkX1N5WGtCZE1WTToyOGQwOThmOTQ0N2Q0OWNmYmI0MTUwN2FjOWU3MDZiOA== ";
            httpWReq.Headers["X-Kinvey-API-Version"] = "3";

            httpWReq.ContentLength = data.Length;
            Stream stream = httpWReq.GetRequestStream();
            stream.Write(data, 0, data.Length);
            stream.Close();

            try
            {
                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String Json = reader.ReadToEnd();

                UserData Dato = JsonConvert.DeserializeObject<UserData>(Json);
                Token = Dato._kmd.authtoken;
            }
            catch
            {
                Token = String.Empty;
            }
        }

        //-------------------------------------------------------------------------------------------------------------
    }

    //=================================================================================================================
}
/*END-TASK*/