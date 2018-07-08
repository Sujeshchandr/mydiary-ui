using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace MyDiary.Common
{
    public static class Constants
    {
        public static class StoredProcedures
        {
            public static class Expenses
            {
                public const string Expense_Add = "Expense_Add";
                public const string Expense_Update = "Expense_Update";
                public const string Expense_Delete = "Expense_Delete";

                public static class Parameters
                {
                    public const string ExpenseId = "ExpenseId";
                    public const string ExpenseTypeId = "ExpenseTypeId";
                    public const string ExpenseType = "ExpenseType";
                    public const string ExpenseDate = "ExpenseDate";
                    public const string Description = "Description";
                    public const string Comments = "Comments";
                    public const string Amount = "Amount";
                    public const string UserId = "UserId";
                    public const string Expenses = "@Expenses";

                    public const string CREATEDBY = "CreatedBy";
                    public const string CREATEDDATE = "CreatedDate";
                    public const string MODIFIEDBY = "ModifiedBy";
                    public const string MODIFIEDDATE = "ModifiedDate";
                    public const string ISDELETED = "IsDeleted";
                }

            }

            public static class ExpenseTypes
            {

                public const string EXPENSETYPES_SELECTBYUSERID = "ExpenseTypes_SelectByUserId";
                public const string EXPENSETYPE_ADD = "ExpenseType_Add";

                public const string EXPENSES_SELECTBYUSERID = "Expenses_SelectByUserId";
                public const string DRY_GETALLEXPENSES_BYDATEANDTYPE = "DRY_GetAllExpenses_ByDateAndType";

                public static class Parameters
                {
                    public const string ExpenseType = "ExpenseType";
                    public const string ExpenseTypeId = "ExpenseTypeId";
                    public const string UserId = "UserId";
                    
                }

            }

            public static class Incomes
            {
                public const string Incomes_SelectByUserId = "Incomes_SelectByUserId";
                public const string DRY_GETFILTEREDINCOMES = "DRY_GetFilteredIncomes";
                public const string INCOME_ADD = "Income_Add";
                public const string Income_Update = "Income_Update";
                public const string Income_Delete = "Income_Delete";

                public static class Parameters
                {
                    public const string ROWNUMBER = "ROWNUMBER";
                    public const string INCOMEID = "IncomeId";
                    public const string INCOMETYPE = "IncomeType";
                    public const string INCOMETYPEID = "IncomeTypeId";
                    public const string USERId = "UserId";
                    public const string DESCRIPTION = "Description";
                    public const string COMMENTS = "Comments";
                    public const string AMOUNT = "Amount";
                    public const string INCOMEDATE = "IncomeDate";
                    public const string CREATEDBY = "CreatedBy";
                    public const string CREATEDDATE = "CreatedDate";
                    public const string MODIFIEDBY = "ModifiedBy";
                    public const string MODIFIEDDATE = "ModifiedDate";
                    public const string ISDELETED = "IsDeleted";

                }
            }

            public static class IncomeTypes
            {

                public const string INCOMETYPES_SELECTBYUSERID = "IncomeTypes_SelectByUserId";
                public const string INCOMETYPE_ADD = "IncomeType_Add";
                
                public static class Parameters
                {
                    public const string INCOMETYPE = "IncomeType";
                    public const string INCOMETYPEID = "IncomeTypeId";
                    public const string USERID = "UserId";                   

                }

            }

            public static class People
            {
                public const string USER_ADD = "User_Add";
                public const string USERLOGIN_ADD = "UserLogin_Add";
                public const string USERLOGIN_SELECT = "UserLogin_Select";

                public const string DRY_GETALLUSERS = "DRY_GetAllUsers";
                public const string DRY_CHECKLOGIN = "DRY_CheckLogin";
                public const string DRY_LOGINUSER = "DRY_LoginUser";
                public const string DRY_GETLOGINUSER = "DRY_GetLoginUser";
                public const string DRY_LOGOUTUSER = "DRY_LogoutUser";
                public const string DRY_CHECKUSEREXISTS = "DRY_CheckUserExists";
                public const string DRY_CHECKUSEREXISTS_BYSITEUSERID = "DRY_CheckUserExists_BySiteUserId";
                public const string DRY_GETUSERLOGINDETAILS_BYUSERID = "DRY_GetUserLoginDetails_ByUserId";
                public const string DRY_USER_GET_BYOPENSITEDETAILS = "[dbo].[User_SelectBySiteUserId]";

                public static class Fields
                {
                    public const string LoginID = "LoginID";
                    public const string UserId = "UserId";
                    public const string EmailId = "EmailId";
                    public const string FirstName = "FirstName";
                    public const string MiddleName = "MiddleName";
                    public const string LastName = "LastName";
                    public const string Password = "Password";
                    public const string RoleId = "RoleId";
                    public const string RoleName = "RoleName";
                    public const string ImageId = "ImageId";
                    public const string SiteId = "SiteId";
                    public const string SiteUserId = "SiteUserId";

                }

                public static class Parameters
                {
                    public const string User_ID = "User_ID";
                    public const string UserId = "@UserId";
                    public const string EmailId = "EmailId";
                    public const string Password = "Password";
                    public const string FirstName = "FirstName";
                    public const string MiddleName = "MiddleName";
                    public const string LastName = "LastName";
                    public const string RoleId = "RoleId";
                    public const string UserImage = "UserImage";
                    public const string LOGIN_ID = "login_Id";
                    public const string SiteUserId = "SiteUserId";
                    public const string SiteId = "SiteId";

                }

            }

            public static class Image
            {
                public const string Image_Add = "Image_Add";
                public const string Image_SelectById = "Image_SelectById";
                public static class Fields
                {
                    public const string IMAGE = "Image";
                    public const string UPLOADID = "UploadId";
                }
                public static class Parameters
                {
                    public const string IMAGE = "Image";
                    public const string ImageId = "@ImageId";
                }
            }

        }


        //public static class Expenses
        //{
        //    public static class StoredProcedures
        //    {
        //        public const string DRY_ADDEXPENSES = "DRY_AddExpenses";

        //        public static class Parameters
        //        {
        //            public const string ExpenseTypeId = "ExpenseTypeId";
        //            public const string ExpenseType = "ExpenseType";
        //            public const string CreatedDate = "CreatedDate";
        //            public const string ModifiedDate = "ModifiedDate";
        //            public const string Description = "Description";
        //            public const string Comments = "Comments";
        //            public const string Amount = "Amount";
        //            public const string User_ID = "User_ID";
        //        }
        //    }
        //}

        //public static class ExpenseTypes
        //{
        //    public static class StoredProcedures
        //    {
        //        public const string DRY_GETALLEXPENSETYPES = "DRY_GetAllExpenseTypes";
        //        public const string DRY_INSERTEXPENSETYPE = "DRY_InsertExpenseType";

        //        public const string DRY_GETALLEXPENSES = "DRY_GetAllExpenses";
        //        public const string DRY_GETALLEXPENSES_BYDATEANDTYPE = "DRY_GetAllExpenses_ByDateAndType";

        //        public static class Parameters
        //        {
        //            public const string ExpenseType = "ExpenseType";
        //            public const string ExpenseTypeId = "ExpenseTypeId";
        //        }
        //    }
        //}

        //public static class People
        //{
        //    public static class StoredProcedures
        //    {
        //        public const string DRY_ADDUSER = "DRY_AddUser";
        //        public const string DRY_CHECKLOGIN = "DRY_CheckLogin";
        //        public const string DRY_LOGINUSER = "DRY_LoginUser";
        //        public static class Parameters
        //        {
        //            public const string User_ID = "User_ID";
        //            public const string EmailId = "EmailId";
        //            public const string Password = "Password";
        //            public const string FirstName = "FirstName";
        //            public const string MiddleName = "MiddleName";
        //            public const string LastName = "LastName";
        //            public const string Role_Id = "Role_Id";
        //            public const string UserImage = "UserImage";
        //        }
        //    }
        //}

        #region ExpenseTypes

        //SP
        
      
        //Columnames
        //public const string ExpenseTypeId = "ExpenseTypeId";
        //public const string ExpenseType = "ExpenseType";
        //public const string CreatedDate = "CreatedDate";
        //public const string ModifiedDate = "ModifiedDate";
        //public const string Description = "Description";
        //public const string Comments = "Comments";
        //public const string Amount = "Amount";
        //public const string User_ID = "User_ID";
       


        #endregion

        public static class DateFormats
        {
            public const string ddMMYYYY = "dd/MM/yyyy";
            public const string MMMM_dd = "MMMM dd";

        }
    }
}
