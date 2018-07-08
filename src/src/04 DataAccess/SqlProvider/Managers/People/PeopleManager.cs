using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Common;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MYDiary.SQLProvider.Connection;

namespace MYDiary.SQLProvider.People.Managers
{
    public class PeopleManager : IPeopleRepository
    {

        #region PUBLIC METHODS

        public int AddUser(IPeople user)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.People.USER_ADD);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.FirstName, user.FirstName);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.MiddleName, user.MiddleName == null ? string.Empty : user.MiddleName);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.LastName, user.LastName);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.EmailId, user.EmailId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.SiteUserId, user.SiteUserId);//optional
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.SiteId, user.SiteId);//optional
            var userRole = user.UserRoles.FirstOrDefault();
            if (userRole != null)
            {
                cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.RoleId, userRole.RoleId);
                var userImage = user.UserImages.FirstOrDefault();
                if (userImage != null)
                {
                    cmd.Parameters.AddWithValue(Constants.StoredProcedures.Image.Parameters.ImageId, userImage.ImageId);
                }
            }

            object result = cmd.ExecuteScalar(); //if image exists 3 tables has to be affected otherwise two
            return int.Parse(result.ToString());

        }

        public void AddUserLoginInformation(IUserLogin userLogin)
        {
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.People.USERLOGIN_ADD);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.UserId, userLogin.UserId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.EmailId, userLogin.EmailId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.Password, userLogin.Password);
            cmd.ExecuteNonQuery(); //if image exists 3 tables has to be affected otherwise two

        }

        public int UploadImage(IImage image)
        {
            int uploadedImageId = -1;
            if (image == null) return uploadedImageId;

            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Image.Image_Add);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Image.Parameters.IMAGE, image.UserImage);
            object result = cmd.ExecuteScalar();
            if (result != null)
                uploadedImageId = int.Parse(result.ToString()); ;
            return uploadedImageId;
        }

        public List<IPeople> GetAll()
        {
            List<IPeople> users = new List<IPeople>();
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.People.DRY_GETALLUSERS);
            DataTable dtUsers = SQLDbConnection.FillDataSetFromDataAdapter(SQLDbConnection.GetNewSqlDataAdapterObject(cmd));
            if (dtUsers != null && dtUsers.Rows.Count > 0)
            {
                users.AddRange(this.Map_PeopleDataTable_To_PeopleList(dtUsers));
            }
            return users;
        }

        public IPeople GetByLoginDetails(string emailId, string password)
        {
            IPeople user = new MyDiary.Domain.Domains.People();
            user.UserRoles = new List<IRole>() { new Role() };
            user.UserImages = new List<IImage>() { new Image() };

            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.People.USERLOGIN_SELECT);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.EmailId, emailId);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.Password, password);
            DataTable dtUser = SQLDbConnection.FillDataSetFromDataAdapter(SQLDbConnection.GetNewSqlDataAdapterObject(cmd));
            if (dtUser != null && dtUser.Rows.Count > 0)
            {
                user = Map_PeopleDataRow_To_PeopleDomain(dtUser.Rows[0]);
            }
            return user;
        }

        public IPeople GetByOpenSiteDetails(int siteId, string siteUserId)
        {

            IPeople user = new MyDiary.Domain.Domains.People();
            user.UserRoles = new List<IRole>() { new Role() };
            user.UserImages = new List<IImage>() { new Image() };

            try
            {
                SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
                conn.Open();
                SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.People.DRY_USER_GET_BYOPENSITEDETAILS);
                cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.SiteId, siteId);
                cmd.Parameters.AddWithValue(Constants.StoredProcedures.People.Parameters.SiteUserId, siteUserId);
                DataTable dtUser = SQLDbConnection.FillDataSetFromDataAdapter(SQLDbConnection.GetNewSqlDataAdapterObject(cmd));
                if (dtUser != null && dtUser.Rows.Count > 0)
                {
                    user = Map_PeopleDataRow_To_PeopleDomain(dtUser.Rows[0]);
                }
                return user;
            }
            catch (Exception)
            {

                return user;
            }
        }

        #endregion

        #region PRIVATE METHODS

        private IUserLogin GetLoginUserFromDataTable(DataTable dtLoginUser)
        {

            if (dtLoginUser != null && dtLoginUser.Rows.Count > 0)
            {

                foreach (DataRow dr in dtLoginUser.Rows)
                {
                    return GetLoginUserFromDataRow(dr);
                }
            }
            return new UserLogin();
        }

        private IUserLogin GetLoginUserFromDataRow(DataRow dr)
        {
            IUserLogin login = new MyDiary.Domain.Domains.UserLogin();
            login.CurrentUser = new MyDiary.Domain.Domains.People();
            foreach (DataColumn dc in dr.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.People.Fields.LoginID:
                        login.LoginId = Convert.ToInt32(dr[Constants.StoredProcedures.People.Fields.LoginID].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.UserId:
                        login.UserId = Convert.ToInt32(dr[Constants.StoredProcedures.People.Fields.UserId].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.EmailId:
                        login.EmailId = Convert.ToString(dr[Constants.StoredProcedures.People.Fields.EmailId].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.FirstName:
                        login.CurrentUser.FirstName = Convert.ToString(dr[Constants.StoredProcedures.People.Fields.FirstName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.MiddleName:
                        login.CurrentUser.MiddleName = Convert.ToString(dr[Constants.StoredProcedures.People.Fields.MiddleName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.LastName:
                        login.CurrentUser.LastName = Convert.ToString(dr[Constants.StoredProcedures.People.Fields.LastName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.Password:
                        login.Password = Convert.ToString(dr[Constants.StoredProcedures.People.Fields.Password].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.ImageId:
                        login.ImageId = Convert.ToInt32(dr[Constants.StoredProcedures.People.Fields.ImageId].ToString());
                        break;
                }
            }
            return login;
            //throw new NotImplementedException();
        }

        private IEnumerable<IPeople> Map_PeopleDataTable_To_PeopleList(DataTable dtPeople)
        {
            return (from DataRow drPeople in dtPeople.AsEnumerable() select Map_PeopleDataRow_To_PeopleDomain(drPeople));

        }

        private IPeople Map_PeopleDataRow_To_PeopleDomain(DataRow drPeople)
        {
            IPeople people = new MyDiary.Domain.Domains.People();
            List<IRole> userRoles = new List<IRole>() { new Role() };
            List<IImage> userImages = new List<IImage>() { new Image() };
            foreach (DataColumn dc in drPeople.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.People.Fields.UserId:
                        people.UserId = (string.IsNullOrEmpty(drPeople[dc.ColumnName].ToString()) ? 0 : Convert.ToInt32(drPeople[dc.ColumnName].ToString()));
                        break;
                    case Constants.StoredProcedures.People.Fields.FirstName:
                        people.FirstName = Convert.ToString(drPeople[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.MiddleName:
                        people.MiddleName = Convert.ToString(drPeople[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.LastName:
                        people.LastName = Convert.ToString(drPeople[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.EmailId:
                        people.EmailId = Convert.ToString(drPeople[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.RoleId:
                        userRoles.FirstOrDefault().RoleId = (string.IsNullOrEmpty(drPeople[dc.ColumnName].ToString()) ? 0 : Convert.ToInt32(drPeople[dc.ColumnName].ToString()));
                        break;
                    case Constants.StoredProcedures.People.Fields.RoleName:
                        userRoles.FirstOrDefault().RoleCode = (drPeople[dc.ColumnName].ToString());
                        break;
                    case Constants.StoredProcedures.People.Fields.ImageId:
                        userImages.FirstOrDefault().ImageId = (string.IsNullOrEmpty(drPeople[dc.ColumnName].ToString()) ? 0 : Convert.ToInt32(drPeople[dc.ColumnName].ToString()));
                        break;
                    case Constants.StoredProcedures.People.Fields.SiteId:
                        people.SiteId = (string.IsNullOrEmpty(drPeople[dc.ColumnName].ToString()) ? 0 : Convert.ToInt32(drPeople[dc.ColumnName].ToString()));
                        break;
                    case Constants.StoredProcedures.People.Fields.SiteUserId:
                        people.SiteUserId = drPeople[dc.ColumnName].ToString();
                        break;

                }
            }

            people.UserImages = userImages;
            people.UserRoles = userRoles;
            return people;
        }

        #endregion

    }
}
