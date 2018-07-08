using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.WebApi.Proxy.Clients
{
    public class MyDiaryWebApiMongoDbClient
    {
        private readonly Uri _domainUrl;
        private readonly string _basicAuthenticatedUserName;
        private readonly string _basicAuthenticatedPassword;

        public MyDiaryWebApiMongoDbClient(Uri domainUrl, string basicAuthenticatedUserName, string basicAuthenticatedPassword) 
        {
            if (domainUrl == null)
            {
                throw new ArgumentNullException("domainUrl");
            }

            if (string.IsNullOrEmpty(domainUrl.AbsoluteUri))
            {
                throw new ArgumentNullException("domainUrl.AbsoluteUri","domainUrl.AbsoluteUri cannot be null or empty");
            }

            if (basicAuthenticatedUserName == null)
            {
                throw new ArgumentNullException("basicAuthenticatedUserName");
            }

            if (basicAuthenticatedPassword == null)
            {
                throw new ArgumentNullException("basicAuthenticatedPassword");
            }

            _domainUrl = domainUrl;
            _basicAuthenticatedUserName = basicAuthenticatedUserName;
            _basicAuthenticatedPassword = basicAuthenticatedPassword;

        }
    }
}
