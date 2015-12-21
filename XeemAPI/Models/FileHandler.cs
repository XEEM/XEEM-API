using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace XeemAPI.Models
{
    public class FileHandler
    {

        public static bool RemoveFile(string url)
        {
            try
            {
                if(url == null)
                {
                    return true;
                }
                var splits = url.Split('/');

                StringBuilder urlBuilder = new StringBuilder();
                for(var i = 3; i < splits.Length; i++)
                {
                    urlBuilder.AppendFormat("/{0}", splits[i]);
                }
                string storagePath = HttpContext.Current.Server.MapPath(urlBuilder.ToString());

                File.Delete(storagePath);
            }
            catch(Exception e)
            {
                return false;
            }

            return true;
        }

        private static string Hashstring(string input)
        {
            // step 1, calculate MD5 hash from input
            MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();

        }
        private static string relativePath = "/Assets/Avatars";

        public static string AddFile(HttpPostedFile file)
        {
            var splits = file.FileName.Split('.');
            var extension = splits[splits.Length - 1];
            var newName = Hashstring(file.FileName + DateTime.Now.ToString()) + "." + extension;

            string storagePath = HttpContext.Current.Server.MapPath(relativePath)
                + @"\" + newName;

            file.SaveAs(storagePath);
            string domain = HttpContext.Current.Request.Url.Authority;
            string scheme = HttpContext.Current.Request.Url.Scheme;

            var url = scheme + "://" + domain + relativePath + "/" + newName;
            return url;
        }
    }
}