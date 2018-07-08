using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MyDiary.API.ControllerHelpers;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.People;
using Hangfire;
using System.Threading.Tasks;
using NLog;

namespace MyDiary.API.Controllers
{
    public class PeopleController : ApiController
    {
        #region INSTANCE FIELDS

        private readonly IPeopleService _peopleService;
        private readonly Hangfire.IBackgroundJobClient _backgroundJobClient;
        private readonly ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public PeopleController(IPeopleService peopleService, Hangfire.IBackgroundJobClient backgroundJobClient,ILogger logger)
        {
            if (peopleService == null)
            {
                throw new ArgumentNullException("peopleSerice");
            }

            if (backgroundJobClient == null)
            {
                throw new ArgumentNullException("backgroundJobClient");
            }

            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            _peopleService = peopleService;
            _backgroundJobClient = backgroundJobClient;
            _logger = logger;
            
        }

        #endregion

        #region PUBLIC METHODS

        ////[Queue("file_submit_priority")]
        ////public LoginJson LogInTest([FromBody]LoginJson loginJson)
        ////{
        ////    return loginJson;

        ////}

        [HttpPost]
        public HttpResponseMessage LogIn([FromBody]LoginJson loginJson)
        {

#if DEBUG

            loginJson = new LoginJson()
            {
                EmailId = "sujeshchandr@gmail.com",
                Password = "sujesh"
            };

#endif
            try
            {
                if (loginJson == null)
                {
                    throw new ArgumentNullException("loginJson");
                }

                if (string.IsNullOrEmpty(loginJson.EmailId))
                {
                    throw new ArgumentNullException("Emaild");
                }

                if (string.IsNullOrEmpty(loginJson.Password))
                {
                    throw new ArgumentNullException("Password");
                }

                PeopleJSON user = new PeopleControllerHelper().Map_PeopleDTO_To_JSON(_peopleService.LogIn(new LoginControllerHelper().Map_LoginJSON_To_DTO(loginJson)));

                return Request.CreateResponse(HttpStatusCode.OK, user);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return  Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        // GET api/<controller>
        public HttpResponseMessage GetAll()
        {
            try
            {
                //new StatusChecker.Hangfire.StatusChecker(@"Data Source=USER-PC\SQLEXPRESS;Initial Catalog=HangfireDb;Integrated Security=False;User Id=sa;Password=sa1234;").GetStatusAsync();
               //// var jobId = _backgroundJobClient.Enqueue(() => LogInTest(new LoginJson()));

                List<PeopleJSON> userJSONList = new PeopleControllerHelper().Map_IncomesDTOList_To_IncomeJSONList(_peopleService.GetAll());
                return this.Request.CreateResponse(HttpStatusCode.OK, userJSONList);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                return this.Request.CreateResponse(HttpStatusCode.InternalServerError);
            }

        }

        [Queue("default")]
        [AutomaticRetry(Attempts=1)]
        public void TrySync(int articleId)
        {
            TryAsync(articleId).Wait();

        }

        private async Task<int> TryAsync(int articleId)
        {
            var dummyTask = new Task<int>(() => articleId);

            return await dummyTask;
        }

        #endregion

    }
}