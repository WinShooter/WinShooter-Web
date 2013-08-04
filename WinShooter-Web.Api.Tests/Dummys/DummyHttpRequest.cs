using System;
using System.Collections.Generic;

namespace WinShooter.Api.Tests.Dummys
{
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;

    using ServiceStack.ServiceHost;

    class DummyHttpRequest : IHttpRequest
    {
        public T TryResolve<T>()
        {
            throw new NotImplementedException();
        }

        public string GetRawBody()
        {
            throw new NotImplementedException();
        }

        public object OriginalRequest { get; private set; }
        public string OperationName { get; set; }
        public string ContentType { get; private set; }
        public string HttpMethod { get; private set; }
        public bool IsLocal { get; private set; }
        public string UserAgent { get; private set; }
        public IDictionary<string, Cookie> Cookies { get; private set; }
        public string ResponseContentType { get; set; }
        public Dictionary<string, object> Items { get; private set; }
        public NameValueCollection Headers { get; private set; }
        public NameValueCollection QueryString { get; private set; }
        public NameValueCollection FormData { get; private set; }
        public bool UseBufferedStream { get; set; }
        public string RawUrl { get; private set; }
        public string AbsoluteUri { get; private set; }
        public string UserHostAddress { get; private set; }
        public string RemoteIp { get; private set; }
        public string XForwardedFor { get; private set; }
        public string XRealIp { get; private set; }
        public bool IsSecureConnection { get; private set; }
        public string[] AcceptTypes { get; private set; }
        public string PathInfo { get; private set; }
        public Stream InputStream { get; private set; }
        public long ContentLength { get; private set; }
        public IFile[] Files { get; private set; }
        public string ApplicationFilePath { get; private set; }
        public Uri UrlReferrer { get; private set; }
    }
}
