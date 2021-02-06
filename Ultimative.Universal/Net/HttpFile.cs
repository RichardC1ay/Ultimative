using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Ultimative.Universal.Net
{
    public static class HttpFile
    {
        public static void Download(string from, string to, Callback callback)
        {
            if (from == null)
                return;

            var indexesFolder = Environment.CurrentDirectory + "\\.minecraft\\assets\\indexes";
            Directory.CreateDirectory(indexesFolder);

            WebResponse response = null;
            //获取远程文件
            WebRequest request = WebRequest.Create(from);
            response = request.GetResponse();
            if (response == null) 
                return;

            //下载远程文件
            ThreadPool.QueueUserWorkItem((obj) =>
            {
                Stream netStream = response.GetResponseStream();
                Stream fileStream = new FileStream(to, FileMode.Create);
                byte[] read = new byte[1024];
                long progressBarValue = 0;
                int realReadLen = netStream.Read(read, 0, read.Length);
                while (realReadLen > 0)
                {
                    fileStream.Write(read, 0, realReadLen);
                    progressBarValue += realReadLen;
                    realReadLen = netStream.Read(read, 0, read.Length);
                }
                netStream.Close();
                fileStream.Close();

                Application.Current.Dispatcher.Invoke(delegate {
                    if (callback != null)
                        callback.Invoke();
                });
                
            }, null);
        }

        public static bool HttpFileExist(string http_file_url)
        {
            WebResponse response = null;
            bool result = false;//下载结果
            try
            {
                response = WebRequest.Create(http_file_url).GetResponse();
                result = response == null ? false : true;
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                }
            }
            return result;
        }

        public delegate void Callback();
    }
}
