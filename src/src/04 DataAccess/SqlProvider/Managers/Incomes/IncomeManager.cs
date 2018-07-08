using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using MyDiary.Common;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Domain.Domains;
using MYDiary.SQLProvider.Connection;

namespace MYDiary.SQLProvider.Incomes.Managers
{
    public class IncomeManager : IIncomeRepository
    {
        #region PUBLIC METHODS
       
        public int AddIncome(IIncome income)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Incomes.INCOME_ADD);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.USERId, income.UserId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMETYPEID, income.IncomeType.TypeId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.AMOUNT, income.Amount);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMEDATE, income.IncomeDate);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.DESCRIPTION, income.Description);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.COMMENTS, income.Comments);
            int incomeTypeId = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return incomeTypeId;

        }

        public void Update(IIncome incomeDomain)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Incomes.Income_Update);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMEID, incomeDomain.IncomeId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.USERId, incomeDomain.UserId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMETYPEID, incomeDomain.IncomeType.TypeId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMEDATE, incomeDomain.IncomeDate);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.DESCRIPTION, incomeDomain.Description);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.COMMENTS, incomeDomain.Comments);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.AMOUNT, incomeDomain.Amount);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.MODIFIEDBY, incomeDomain.UserId);
            cmd.ExecuteNonQuery();
            cmd.Dispose(); //ToDo ==> If this is success ,implement this in all methods...
            conn.Close();
        }

        public List<IIncome> GetAll(int userId)
        {
            List<IIncome> incomes = new List<IIncome>();

            DataTable dtIncomes = GetAllIncomesDatatTable(userId);

            //TESTING EQUALIITY COMPARER --DISTINCT 

            //var distinctvalues = (from i in dtIncomes.AsEnumerable().Distinct(new TestComparer())
            //                      select i).ToList();
            //if (distinctvalues.Count > 0)
            //{
            //    foreach (DataRow dr in distinctvalues)
            //    {
            //        incomes.Add(MapIncomeFromDataRow(dr));
            //    }
            //}

            ///////////////////


            if (dtIncomes != null && dtIncomes.Rows.Count > 0)
            {

                foreach (DataRow dr in dtIncomes.Rows)
                {
                    incomes.Add(MapIncomeFromDataRow(dr));
                }
            }
            return incomes;

        }

        public List<IIncome> GetFilteredIncomes(int userId,string date, List<IIncomeType> incomeTypes)
        {
             List<IIncome> incomes = new List<IIncome>();

             DataTable dtIncomes = GetFilteredIncomesDatatTable(userId);
                    if (dtIncomes != null && dtIncomes.Rows.Count > 0)
                    {

                        foreach (DataRow dr in dtIncomes.Rows)
                        {
                            incomes.Add(MapIncomeFromDataRow(dr));
                        }
                    }
                    return incomes;

        }

        public void Delete(int incomeId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Incomes.Income_Delete);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.INCOMEID, incomeId);
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            conn.Close();
        }


        #endregion

        #region PRIVATE METHODS 
        
        private DataTable GetAllIncomesDatatTable(int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Incomes.Incomes_SelectByUserId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Incomes.Parameters.USERId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            conn.Close();
            return SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
        }

        private DataTable GetFilteredIncomesDatatTable(int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Incomes.DRY_GETFILTEREDINCOMES);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            conn.Close();
            return SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
        }
       
        private IIncome MapIncomeFromDataRow(DataRow dr)
        {
            IIncome income = new MyDiary.Domain.Domains.Income();
            income.IncomeType = new IncomeType();

            foreach (DataColumn dc in dr.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.Incomes.Parameters.ROWNUMBER:
                        //income.RowNumber = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.Incomes.Parameters.INCOMEID:
                        income.IncomeId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.INCOMETYPEID:
                        income.IncomeType.TypeId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                     case Constants.StoredProcedures.Incomes.Parameters.INCOMETYPE:
                        income.IncomeType.Type = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;
                            
                    case Constants.StoredProcedures.Incomes.Parameters.AMOUNT:
                        income.Amount = float.Parse(dr[dc.ColumnName].ToString(),System.Globalization.CultureInfo.InvariantCulture);
                        break;
                        
                    case Constants.StoredProcedures.Incomes.Parameters.DESCRIPTION:
                        income.Description = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.COMMENTS:
                        income.Comments = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.USERId:
                        income.UserId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break; 

                    case Constants.StoredProcedures.Incomes.Parameters.INCOMEDATE:
                        income.IncomeDate = Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break; 
                    
                    case Constants.StoredProcedures.Incomes.Parameters.CREATEDBY:
                        income.CreatedBy = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.CREATEDDATE:
                        income.CreatedDate = Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.MODIFIEDBY:
                        income.ModifiedBy = (dr[dc.ColumnName]) == DBNull.Value ? (int?)null : Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;

                    case Constants.StoredProcedures.Incomes.Parameters.MODIFIEDDATE:
                        income.ModifiedDate = (dr[dc.ColumnName]) == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(dr[dc.ColumnName].ToString());
                        break;  


                }
            }

            return income;

        }       

        #endregion

    }
}
