using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;
using MyDiary.Common;
using MYDiary.SQLProvider.Connection;
using System.Data.SqlClient;

namespace MYDiary.SQLProvider.Images.Managers
{
    public class ImageManager :IImageRepository
    {
        #region PUBLIC METHODS

        public IImage GetUploadImageById(int imageId)
        {
            IImage uploadImage = new MyDiary.Domain.Domains.Image(null);
            SqlConnection conn = SQLDbConnection.GetNewSqlConnectionObject();
            conn.Open();
            SqlCommand cmd = SQLDbConnection.GetNewSqlCommandObject(conn, Constants.StoredProcedures.Image.Image_SelectById);
            cmd.Parameters.AddWithValue(Constants.StoredProcedures.Image.Parameters.ImageId, imageId);
            DataTable dtUploadImage = SQLDbConnection.FillDataSetFromDataAdapter(SQLDbConnection.GetNewSqlDataAdapterObject(cmd));
            if (dtUploadImage != null && dtUploadImage.Rows.Count > 0)
            {
                uploadImage = GetImageFromDataTable(dtUploadImage);
            }
            return uploadImage;
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
        #endregion

        #region PRIVATE METHODS


        private IImage GetImageFromDataTable(DataTable dtImage)
        {
            if (dtImage != null && dtImage.Rows.Count > 0)
            {

                foreach (DataRow dr in dtImage.Rows)
                {
                    return GetImageFomDataRow(dr);
                }
            }
            return new MyDiary.Domain.Domains.Image(null);

        }

        private IImage GetImageFomDataRow(DataRow drImage)
        {
            IImage image = new MyDiary.Domain.Domains.Image(null);
            foreach (DataColumn dc in drImage.Table.Columns)
            {
                switch (dc.ColumnName)
                {
                    case Constants.StoredProcedures.Image.Fields.IMAGE:
                        image.UserImage = (byte[])(drImage[Constants.StoredProcedures.Image.Fields.IMAGE]);
                        break;
                }
            }
            return image;
        }
        #endregion
    }
}
