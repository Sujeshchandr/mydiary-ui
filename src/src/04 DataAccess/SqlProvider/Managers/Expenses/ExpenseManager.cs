using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MYDiary.SQLProvider.Connection;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Domain.Domains;
using MyDiary.Common;

namespace MYDiary.SQLProvider.Expense.Managers
{
    #region EXPENSE MANAGER

    public class ExpenseManager : IExpenseRepository
    {
        #region PUBLIC METHODS

        #region EXPENSE

        public int Add(IExpense expenseDomain)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Expenses.Expense_Add);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseTypeId, expenseDomain.ExpenseType.TypeId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseDate, expenseDomain.ExpenseDate);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Description, expenseDomain.Description);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Comments, expenseDomain.Comments);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Amount, expenseDomain.Amount);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, expenseDomain.CurrentUser.UserId);
            object result = cmd.ExecuteScalar();
            conn.Close();
            return int.Parse(result.ToString());

        }

        public void Update(IExpense expenseDomain)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Expenses.Expense_Update);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseId, expenseDomain.Id);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, expenseDomain.CurrentUser.UserId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseTypeId, expenseDomain.ExpenseType.TypeId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseDate, expenseDomain.ExpenseDate);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Description, expenseDomain.Description);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Comments, expenseDomain.Comments);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.Amount, expenseDomain.Amount);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.MODIFIEDBY, expenseDomain.CurrentUser.UserId);
            cmd.ExecuteNonQuery();
            cmd.Dispose(); //ToDo ==> If this is success ,implement this in all methods...
            conn.Close();
        }

        public IList<IExpense> Get(int userId)
        {
            IList<IExpense> expenseList = new List<IExpense>();
            DataTable dtExenses = GetAllExpensesDataTable(userId);          

            if (dtExenses != null && dtExenses.Rows.Count > 0)
            {
                foreach (DataRow dr in dtExenses.Rows)
                {
                    expenseList.Add(GetExpenseFromDataRow(dr));
                }
            }
            return expenseList;
        }

        public void Delete(int expenseId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Expenses.Expense_Delete);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseId, expenseId);
            cmd.ExecuteNonQuery();
            cmd.Dispose(); 
            conn.Close();
        }

        [Obsolete("GetFilteredExpenses is depreciated, please use Get instead.")]
        public List<IExpense> GetFilteredExpenses(string date, string type,int userId)
        { 
            List<IExpense> expenseList = new List<IExpense>();
            DataTable dtExenses = GetFilteredExpensesDataTable(date,type,userId);
            if (dtExenses != null && dtExenses.Rows.Count > 0)
            {
                foreach (DataRow dr in dtExenses.Rows)
                {
                    expenseList.Add(GetExpenseFromDataRow(dr));
                }
            }
            return expenseList;

        }


        #endregion

        #region EXPENSE TYPES

        public bool AddExpenseType(string expenseType, int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.ExpenseTypes.EXPENSETYPE_ADD);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.ExpenseTypes.Parameters.ExpenseType, expenseType);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.ExpenseTypes.Parameters.UserId, userId);
            bool result = cmd.ExecuteNonQuery() == 1 ? true : false;
            conn.Close();
            return bool.Parse(result.ToString());
        }

        public IList<IExpenseType> GetAllExpenseTypes(int userId)
        {
            List<IExpenseType> expenseTypesList = new List<IExpenseType>();

            DataTable dtExenseTypes = GetAllExpenseTypesDatatTable(userId);
            if (dtExenseTypes != null && dtExenseTypes.Rows.Count > 0)
            {

                foreach (DataRow dr in dtExenseTypes.Rows)
                {
                    expenseTypesList.Add(GetExpenseTypeFromDataRow(dr));
                }
            }
            return expenseTypesList;

        }

        #endregion

        #endregion

        #region PRIVATE METHODS

        private DataTable GetAllExpensesDataTable(int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.ExpenseTypes.EXPENSES_SELECTBYUSERID);  
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            DataTable dtExpenses =  SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
            conn.Close();
            return dtExpenses;
        }

        private DataTable GetFilteredExpensesDataTable(string date, string type,int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.ExpenseTypes.DRY_GETALLEXPENSES_BYDATEANDTYPE);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseDate, date);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.ExpenseType, type);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            DataTable dtExpenses = SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
            conn.Close();
            return dtExpenses;
        }

        private IExpense GetExpenseFromDataRow(DataRow dr)
        {
            IExpense expense = new MyDiary.Domain.Domains.Expense();
            IExpenseType expenseType = new MyDiary.Domain.Domains.ExpenseType();
            IPeople currentUser = new MyDiary.Domain.Domains.People();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.Expenses.Parameters.ExpenseId:
                        expense.Id = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.ExpenseType:
                        expenseType.Type = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.ExpenseTypeId:
                        expenseType.TypeId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.ExpenseDate:
                        expense.ExpenseDate = Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.Description:
                        expense.Description = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.Comments:
                        expense.Comments = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Expenses.Parameters.Amount:
                        expense.Amount = float.Parse(dr[dc.ColumnName].ToString(), System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case Constants.StoredProcedures.ExpenseTypes.Parameters.UserId:
                        currentUser.UserId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Expenses.Parameters.CREATEDBY:
                        expense.CreatedBy = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Expenses.Parameters.CREATEDDATE:
                        expense.CreatedDate = Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Expenses.Parameters.MODIFIEDBY:
                        expense.ModifiedBy = (dr[dc.ColumnName]) == DBNull.Value ? (int?)null : Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Expenses.Parameters.MODIFIEDDATE:
                        expense.ModifiedDate = (dr[dc.ColumnName]) == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break;  

                }
                
            }

            expense.ExpenseType = expenseType;
            expense.CurrentUser = currentUser;
            return expense;            
        }

        #region EXPENSE TYPE

        private DataTable GetAllExpenseTypesDatatTable(int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.ExpenseTypes.EXPENSETYPES_SELECTBYUSERID);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            DataTable dtExpenseTypes = SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
            conn.Close();
            return dtExpenseTypes;
        }

        private IExpenseType GetExpenseTypeFromDataRow(DataRow dr)
        {
            IExpenseType expenseTypes = new MyDiary.Domain.Domains.ExpenseType(null);
            foreach(DataColumn dc in dr.Table.Columns)
            {
            switch (dc.ColumnName)
            {
                case Constants.StoredProcedures.ExpenseTypes.Parameters.ExpenseTypeId:
                    expenseTypes.TypeId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                    break;
                case Constants.StoredProcedures.ExpenseTypes.Parameters.ExpenseType:
                    expenseTypes.Type = Convert.ToString(dr[dc.ColumnName].ToString());
                    break;
            }
            }
            return expenseTypes;
            //throw new NotImplementedException();
        }

        #endregion

        #endregion
    }

    #endregion

    #region EXTENSIONS

    public static class Extensions
    {
        public static DataTable ToDataTable(this IList<IExpense> expenses)
        {
            DataTable dt = new DataTable("UDT_Expense");
            dt.Columns.Add("ExpenseId");
            dt.Columns.Add("ExpenseTypeId");
            dt.Columns.Add("User_Id");
            dt.Columns.Add("Amount");
            dt.Columns.Add("Description");
            dt.Columns.Add("Comments");
            dt.Columns.Add("ModifiedBy");
            dt.Columns.Add("ExpenseDate");
            foreach (IExpense expense in expenses)
            {
                dt.Rows.Add(expense.Id, expense.ExpenseType.TypeId, expense.UserId, expense.Amount, expense.Description, expense.Comments, expense.ModifiedBy,expense.ExpenseDate);
            }

            return dt;
        }
    }

   #endregion
}
