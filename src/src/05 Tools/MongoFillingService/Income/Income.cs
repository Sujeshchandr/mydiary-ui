using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Newtonsoft.Json;
using MyDiary.WebApi.Proxy.Clients;

namespace MyDiary.MongoFillingService.Income
{
    public static class Income
    {
        private static MyDiaryWebApiClient _myDiaryWebApiClient = new MyDiaryWebApiClient(new Uri("http://localhost:64780/api"), string.Empty, string.Empty);

        public static void Fill()
        {
            
            ////Task.Run(()=> TestDistributedLock());

            ////Console.ReadLine();
            string userInput;
            Console.WriteLine("If you run the initializer ,It will delete all income collections of users \n " +
                              "and initialize all entries from SQL DB to MongoDB. \n " +
                              "Are you sure to Initialize the Income Collections ? Y/N");
            userInput = Console.ReadLine();
            switch (userInput.ToUpper().Trim())
            {
                case "Y":
                    Run();
                    break;
                case "N":
                    break;
                default:
                    Console.Beep();
                    Console.WriteLine("Plese enter Y/N");
                    userInput = Console.ReadLine();
                    if (userInput.ToUpper().Trim() == "Y")
                    {
                        Run();
                    }

                    break;
            }
        }

        private static void Run()
        {
            try
            {

                Console.WriteLine("\n");
                Console.WriteLine("Income Initilaizer starting ...................");

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");  

                #region FETCHING ALL USERS

                Console.WriteLine("\n");
                Console.WriteLine("  Fetching all users............ ");               

                List<PeopleJSON> peopleJsonList = ExecuteWebAPIRequest_GetAllUsers("http://localhost:64780/api/people/getall");

                Console.WriteLine("\n");
                Console.WriteLine("  Successfully fetched all users !!!!!!!!!!!");
              

                #endregion

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");  

                #region FETCHING ALL INCOMES

                Console.WriteLine("\n");
                Console.WriteLine("  Getting all incomes by userId ...................");
                Console.WriteLine("\n");

                List<IncomeJSON> incomes = new List<IncomeJSON>();
                if (peopleJsonList.Any())
                {
                    incomes = GetAllIncomes(peopleJsonList);
                }

                Console.WriteLine("\n");
                Console.WriteLine("  Suuccessfully Fetched all incomes by userId !!!!!!!!!!!");
               

                #endregion

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");  

                #region INSERTING ALL INCOMES

                Console.WriteLine("\n");
                Console.WriteLine("  Inserting " + incomes.Count() + "  incomes to mongoDB...................");
               

                if (incomes.Any())
                {
                    ExecuteWebAPIRequest_InsertAllIncomes_ToMongoDB("http://localhost:64780/api/mongo/InsertAll",incomes);

                }

                Console.WriteLine("\n");
                Console.WriteLine("  Successfully inserted  " + incomes.Count() + " Incomes to MongoDB !!!!!!!!!!!");
               

                #endregion

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");  

                Console.WriteLine("\n");
                Console.WriteLine("Successfully completed Initializing incomes to MongoDB !!!!!!!!!!!");

                Console.Beep();

                Console.ReadLine();
            }
            catch (Exception ex)
            {
               Console.WriteLine(" ERROR !!!!!!!!!!!!!! :::: " + ex.ToString());
               Console.ReadLine();
            }
        }

        // Convert an object to a byte array
        private static byte[] ObjectToByteArray(Object obj)
        {
            if (obj == null)
            {
                return null;
            }

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private static List<IncomeJSON> GetAllIncomes(List<PeopleJSON> peopleJsonList)
        {
            List<IncomeJSON> incomes = new List<IncomeJSON>();

            foreach (var user in peopleJsonList)
            {
               
                Console.WriteLine(string.Format("   Getting all incomes of user {0}: ", user.UserId));

                incomes.AddRange(ExecuteWebAPIRequest_GetAllIncomes_ByUserId("http://localhost:64780/api/incomes/sql/get/", user.UserId));  
      
                Console.WriteLine(string.Format("    Successfully fetched incomes of user {0}: ", user.UserId));

            }
            return incomes;
        }

        private static async Task<string[]> TestDistributedLock()
        {
            
                IList<Task<string>> tasks = new List<Task<string>>();
                for (int i = 0; i < 30; i++)
                {
                    System.Net.WebClient client = InitializeWebClient();

                    string uri = "http://localhost:64888/mydiary/v2/Containers('articles" + (i + 1) + "')";
                    tasks.Add(client.DownloadStringTaskAsync(new Uri(uri))); 
                }

               return await System.Threading.Tasks.Task.WhenAll(tasks);
           
        }

        private static List<PeopleJSON> ExecuteWebAPIRequest_GetAllUsers(string apiUri)
        {
            List<PeopleJSON> peopleJsonList = new List<PeopleJSON>();
            try
            {
                System.Net.WebClient client = InitializeWebClient();
                string peopleJson = client.DownloadString(apiUri);
                peopleJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<PeopleJSON>>(peopleJson);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR !!!!!!!!!!!!!! :::: " + ex.ToString());
                Console.ReadLine();
            }
            return peopleJsonList;

        }

        private static List<IncomeJSON> ExecuteWebAPIRequest_GetAllIncomes_ByUserId(string apiUrl, int userId)
        {
            List<IncomeJSON> incomeJsonList = new List<IncomeJSON>();
            try
            {
                
                System.Net.WebClient client = InitializeWebClient();
                string uri = apiUrl + userId;
                string incomesJson = client.DownloadString(uri);
                if (!string.IsNullOrEmpty(incomesJson))
                {
                  incomeJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IncomeJSON>>(incomesJson);
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("GetIncomes Failed for UserId: {0} ", userId));
                Console.WriteLine("ERROR !!!!!!!!!!!!!! :::: " + ex.ToString());
                Console.ReadLine();
            }
            return incomeJsonList;
        }

        private static bool ExecuteWebAPIRequest_InsertAllIncomes_ToMongoDB(string apiUrl,List<IncomeJSON> incomes)
        {
            bool isDeleted = false;
            try
            {
                System.Net.WebClient client = InitializeWebClient();
                string json = JsonConvert.SerializeObject(incomes);
                isDeleted = bool.Parse(client.UploadString(apiUrl, json));//InsertAll

            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR !!!!!!!!!!!!!! :::: " + ex.ToString());
                Console.ReadLine();
            }
            return isDeleted;
        }

        private static System.Net.WebClient InitializeWebClient()
        {
            System.Net.WebClient client = new System.Net.WebClient();
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            return client;
        }
    }
}
