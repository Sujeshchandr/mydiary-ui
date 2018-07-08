using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using MyDiary.MongoFillingService.JSON;
using Newtonsoft.Json;

namespace MyDiary.MongoFillingService.Expense
{
    public static class Expense
    {
        #region PUBLIC METHODS

        public static void Fill()
        {
            string userInput;
            Console.WriteLine("If you run the initializer ,It will delete all expense collections of users \n " +
                              "and initialize all entries from SQL DB to MongoDB. \n " +
                              "Are you sure to Initialize the Expense Collections ? Y/N");
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

        #endregion

        #region PRIVATE METHODS

        private static void Run()
        {
            try
            {

                Console.WriteLine("\n");
                Console.WriteLine("Expense Initilaizer starting ...................");

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

                #region FETCHING ALL EXPENSES

                Console.WriteLine("\n");
                Console.WriteLine("  Getting all expenses by userId ...................");
                Console.WriteLine("\n");

                List<ExpenseJSON> expenses = new List<ExpenseJSON>();
                if (peopleJsonList.Any())
                {
                    expenses = GetAllExpenses(peopleJsonList);
                }

                Console.WriteLine("\n");
                Console.WriteLine("  Suuccessfully Fetched all expenses by userId !!!!!!!!!!!");


                #endregion

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");

                #region INSERTING ALL EXPENSES

                Console.WriteLine("\n");
                Console.WriteLine("  Inserting " + expenses.Count() + "  expenses to mongoDB...................");


                if (expenses.Any())
                {
                    ExecuteWebAPIRequest_InsertAllExpenses_ToMongoDB("http://localhost:64780/api/expense/mongo/InsertAll", expenses);

                }

                Console.WriteLine("\n");
                Console.WriteLine("  Successfully inserted  " + expenses.Count() + " expenses to MongoDB !!!!!!!!!!!");


                #endregion

                Console.WriteLine("\n");
                Console.WriteLine("*************************************************");

                Console.WriteLine("\n");
                Console.WriteLine("Successfully completed Initializing expenses to MongoDB !!!!!!!!!!!");

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
                return null;
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();
            bf.Serialize(ms, obj);
            return ms.ToArray();
        }

        private static List<ExpenseJSON> GetAllExpenses(List<PeopleJSON> peopleJsonList)
        {
            List<ExpenseJSON> expenses = new List<ExpenseJSON>();
            foreach (var user in peopleJsonList)
            {

                Console.WriteLine(string.Format("   Getting all expenses of user {0}: ", user.UserId));

                expenses.AddRange(ExecuteWebAPIRequest_GetAllExpenses_ByUserId("http://localhost:64780/api/expenses/sql/get/", user.UserId));

                Console.WriteLine(string.Format("    Successfully fetched expenses of user {0}: ", user.UserId));

            }
            return expenses;
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

        private static List<ExpenseJSON> ExecuteWebAPIRequest_GetAllExpenses_ByUserId(string apiUrl, int userId)
        {
            List<ExpenseJSON> expenseJsonList = new List<ExpenseJSON>();
            try
            {

                System.Net.WebClient client = InitializeWebClient();
                string uri = apiUrl + userId;
                string expensesJson = client.DownloadString(uri);
                if (!string.IsNullOrEmpty(expensesJson))
                {
                    expenseJsonList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<ExpenseJSON>>(expensesJson);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("GetExpenses Failed for UserId: {0} ", userId));
                Console.WriteLine("ERROR !!!!!!!!!!!!!! :::: " + ex.ToString());
                Console.ReadLine();
            }
            return expenseJsonList;
        }

        private static bool ExecuteWebAPIRequest_InsertAllExpenses_ToMongoDB(string apiUrl, List<ExpenseJSON> expenses)
        {
            bool isDeleted = false;
            try
            {
                System.Net.WebClient client = InitializeWebClient();
                string json = JsonConvert.SerializeObject(expenses);
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

        #endregion
    }
}
