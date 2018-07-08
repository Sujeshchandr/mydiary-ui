using MyDairy.WebApi.Proxy.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;

namespace MyDiary.WebApi.Proxy.Clients
{
    public class MyDiaryWebApiClient
    {
        private readonly Uri _domainUrl;
        private readonly string _basicAuthenticatedUserName;
        private readonly string _basicAuthenticatedPassword;

        public MyDiaryWebApiClient(Uri domainUrl, string basicAuthenticatedUserName, string basicAuthenticatedPassword) 
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

        public IList<ExpenseJSON> GetExpensesByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId", "user id should be greater than zero");
            }

            return Task.Run(() => _domainUrl.AbsoluteUri.GetJsonAsync<IList<ExpenseJSON>>()).Result;

            ////.appendpathsegments("api","people","login")
            ////.withheader("key", "value")
            ////.withcookie(new cookie("diary", "dgxukjjobzky42o+q9eueq=="))
            ////.setqueryparams(new { a = 1, b = 2 })
            ////.withbasicauth("username","password")
            ////.getjsonasync<mydiary.odata.models.json.containers>().configureawait(false)
            ////.getodatavalueasync<string[]>().configureawait(false);
            ////.postjsonasync(new { emailid = "sujeshchandr@gmail.com", password = "sujesh" })
            ////.receivejson().configureawait(false);

        }

        public async Task<IList<ExpenseJSON>> GetExpensesByUserIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId", "user id should be greater than zero");
            }

            return await _domainUrl.AbsoluteUri.GetJsonAsync<IList<ExpenseJSON>>().ConfigureAwait(false);
        }

        public IList<IncomeJSON> GetIncomesById(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId", "user id should be greater than zero");
            }

            return Task.Run(() => _domainUrl.AbsoluteUri.GetJsonAsync<IList<IncomeJSON>>()).Result;
        }

        public async Task<IList<IncomeJSON>> GetIncomesByIdAsync(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentOutOfRangeException("userId", "user id should be greater than zero");
            }

            return await _domainUrl.AbsoluteUri.GetJsonAsync<IList<IncomeJSON>>().ConfigureAwait(false);
        }
    }
}
