// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DummyHttpRequest.cs" company="Copyright ©2013 John Allberg & Jonas Fredriksson">
//   This program is free software; you can redistribute it and/or
//   modify it under the terms of the GNU General Public License
//   as published by the Free Software Foundation; either version 2
//   of the License, or (at your option) any later version.
//   
//   This program is distributed in the hope that it will be useful,
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY OR FITNESS FOR A PARTICULAR PURPOSE. See the
//   GNU General Public License for more details.
//   
//   You should have received a copy of the GNU General Public License
//   along with this program; if not, write to the Free Software
//   Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
// </copyright>
// <summary>
//   A fake http request.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace WinShooter.Api.Tests.Dummys
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Net;

    using ServiceStack.ServiceHost;

    /// <summary>
    /// A fake http request.
    /// </summary>
    internal class DummyHttpRequest : IHttpRequest
    {
        /// <summary>
        /// Gets or sets the underlying ASP.NET or HttpListener HttpRequest
        /// </summary>
        public object OriginalRequest { get; set; }

        /// <summary>
        /// Gets or sets the name of the service being called (e.g. Request DTO Name)
        /// </summary>
        public string OperationName { get; set; }

        /// <summary>
        /// Gets or sets the request ContentType
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the http method.
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is local.
        /// </summary>
        public bool IsLocal { get; set; }

        /// <summary>
        /// Gets or sets the user agent.
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// Gets or sets the cookies.
        /// </summary>
        public IDictionary<string, Cookie> Cookies { get; set; }

        /// <summary>
        /// Gets or sets the expected Response ContentType for this request
        /// </summary>
        public string ResponseContentType { get; set; }

        /// <summary>
        /// Gets or sets any data to this request that all filters and services can access.
        /// </summary>
        public Dictionary<string, object> Items { get; set; }

        /// <summary>
        /// Gets or sets the headers.
        /// </summary>
        public NameValueCollection Headers { get; set; }

        /// <summary>
        /// Gets or sets the query string.
        /// </summary>
        public NameValueCollection QueryString { get; set; }

        /// <summary>
        /// Gets or sets the form data.
        /// </summary>
        public NameValueCollection FormData { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the buffer the Request InputStream so it can be re-read
        /// </summary>
        public bool UseBufferedStream { get; set; }

        /// <summary>
        /// Gets or sets the raw url.
        /// </summary>
        public string RawUrl { get; set; }

        /// <summary>
        /// Gets or sets the absolute uri.
        /// </summary>
        public string AbsoluteUri { get; set; }

        /// <summary>
        /// Gets or sets the Remote IP as reported by Request.UserHostAddress
        /// </summary>
        public string UserHostAddress { get; set; }

        /// <summary>
        /// Gets or sets the Remote IP as reported by X-Forwarded-For, X-Real-IP or Request.UserHostAddress
        /// </summary>
        public string RemoteIp { get; set; }

        /// <summary>
        /// Gets or sets the value of the X-Forwarded-For header, null if null or empty
        /// </summary>
        public string XForwardedFor { get; set; }

        /// <summary>
        /// Gets or sets the value of the X-Real-IP header, null if null or empty
        /// </summary>
        public string XRealIp { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether e.g. is https or not
        /// </summary>
        public bool IsSecureConnection { get; set; }

        /// <summary>
        /// Gets or sets the accept types.
        /// </summary>
        public string[] AcceptTypes { get; set; }

        /// <summary>
        /// Gets or sets the path info.
        /// </summary>
        public string PathInfo { get; set; }

        /// <summary>
        /// Gets or sets the input stream.
        /// </summary>
        public Stream InputStream { get; set; }

        /// <summary>
        /// Gets or sets the content length.
        /// </summary>
        public long ContentLength { get; set; }

        /// <summary>
        /// Gets or sets the multi-part/form data files posted on this request
        /// </summary>
        public IFile[] Files { get; set; }

        /// <summary>
        /// Gets or sets the application file path.
        /// </summary>
        public string ApplicationFilePath { get; set; }

        /// <summary>
        /// Gets or sets the value of the Referrer, null if not available
        /// </summary>
        public Uri UrlReferrer { get; set; }

        /// <summary>
        /// Resolve a dependency from the AppHost's IOC.
        /// </summary>
        /// <typeparam name="T">
        /// The type to be resolved.
        /// </typeparam>
        /// <returns>
        /// The resolved dependency from the AppHost's IOC.
        /// </returns>
        public T TryResolve<T>()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the entire string contents of Request.InputStream
        /// </summary>
        /// <returns>
        /// The entire string contents of Request.InputStream
        /// </returns>
        public string GetRawBody()
        {
            throw new NotImplementedException();
        }
    }
}
