using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace Upload_File_FTP_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            // servidor FTP
            String oFtp = @"ftp://www.test.com";
            // implementamos un cliente FTP, mediante la peticion que hemos creado al recurso FTP
            FtpWebRequest oRequest = (FtpWebRequest)WebRequest.Create(oFtp);
            // establecemos el comando STOR a enviar al servidor ftp
            oRequest.Method = WebRequestMethods.Ftp.UploadFile;
            // establecemos las credenciales
            oRequest.Credentials = new NetworkCredential("user", "pass");
            // implementamos un flujo de bytes para leer el fichero TXT
            StreamReader oSourceStream = new StreamReader("file.txt");
            byte[] oFileContents = Encoding.UTF8.GetBytes(oSourceStream.ReadToEnd());
            oSourceStream.Close();
            oRequest.ContentLength = oFileContents.Length;
            // establecemos el flujo para la subida de datos al servidor ftp
            Stream oRequestStream = oRequest.GetRequestStream();
            oRequestStream.Write(oFileContents, 0, oFileContents.Length);
            oRequestStream.Close();
            // establecemos la respuesta del servidor.
            FtpWebResponse oResponse = (FtpWebResponse)oRequest.GetResponse();
            Console.WriteLine("Upload File Complete, status {0}", oResponse.StatusDescription);
            oResponse.Close();
        }
    }
}
