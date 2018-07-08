using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Common;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MYDiary.SQLProvider.Connection;

namespace MYDiary.SQLProvider.IncomeTypes.Managers
{
    public class IncomeTypeManager : IIncomeTypeRepository
    {
        #region PUBLIC METHODS

        public int Add(IIncomeType incomeType)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.IncomeTypes.INCOMETYPE_ADD);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.IncomeTypes.Parameters.INCOMETYPE, incomeType.Type);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.IncomeTypes.Parameters.USERID, incomeType.UserId);
            int incomeTypeId = int.Parse(cmd.ExecuteScalar().ToString());
            conn.Close();
            return incomeTypeId;

        }

        public List<IIncomeType> GetAll(int userId)
        {
            List<IIncomeType> incomeTypes = new List<IIncomeType>();

            DataTable dtIncomeTypes = GetAllIncomeTypesAsDataTable(userId);
            if (dtIncomeTypes != null && dtIncomeTypes.Rows.Count > 0)
            {

                foreach (DataRow dr in dtIncomeTypes.Rows)
                {
                    incomeTypes.Add(MapIncomeTypeFromDataRow(dr));
                }
            }
            return incomeTypes;

        }

         #endregion

        #region PRIVATE METHODS

        private DataTable GetAllIncomeTypesAsDataTable(int userId)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.IncomeTypes.INCOMETYPES_SELECTBYUSERID);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Expenses.Parameters.UserId, userId);
            SqlDataAdapter sDataAdapter = SQLDbConnection.GetNewSqlDataAdapterObject(cmd);
            conn.Close();
            return SQLDbConnection.FillDataSetFromDataAdapter(sDataAdapter);
        }

        private IIncomeType MapIncomeTypeFromDataRow(DataRow dr)
        {
            IIncomeType incomeTypes = new MyDiary.Domain.Domains.IncomeType();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.IncomeTypes.Parameters.INCOMETYPEID:
                        incomeTypes.TypeId = Convert.ToInt32(dr[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.IncomeTypes.Parameters.INCOMETYPE:
                        incomeTypes.Type = Convert.ToString(dr[dc.ColumnName].ToString());
                        break;
                }
            }
            return incomeTypes;

        }

        #endregion
    }

}
