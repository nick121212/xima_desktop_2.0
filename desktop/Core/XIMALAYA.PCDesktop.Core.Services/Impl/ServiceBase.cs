﻿using System;
using System.IO;
using System.Net;
using FluentJson;

namespace XIMALAYA.PCDesktop.Core.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class ServiceBase<T> : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        protected JsonDecoder<T> Decoder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        protected Action<object> Act { get; set; }
        protected void GetDataCallBack(IAsyncResult result)
        {
            HttpWebRequest request = result.AsyncState as HttpWebRequest;
            HttpWebResponse response = request.EndGetResponse(result) as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);
            string responseString = reader.ReadToEnd();
            object fr = this.Decoder.Decode(responseString);

            if (this.Act != null)
            {
                this.Act.BeginInvoke(fr, null, null);
            }

            response.Dispose();
            responseStream.Dispose();
            reader.Dispose();
            result = null;
            request = null;
            response = null;
            responseStream = null;
            reader = null;
        }

        #region IDisposable 成员

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            this.Act.EndInvoke(null);
            this.Decoder = null;
            this.Act = null;
            GC.Collect(0, GCCollectionMode.Forced);
        }

        #endregion
    }
}
