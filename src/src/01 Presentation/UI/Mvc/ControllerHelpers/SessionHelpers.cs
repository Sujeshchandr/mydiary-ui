using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyDiary.UI.ControllerHelpers
{
    public static class SessionHelpers
    {
        public static class SessionKeys
        {
            public const string UserId = "UserId";
            public const string LoginId = "LoginId";
            public const string UserName = "UserName";
            public const string RoleID = "RoleID";
            public const string ImageID = "ImageID";
        }

        #region GET

        public static int LoginId(this HttpSessionStateBase session)
        {
            return session[SessionKeys.LoginId] == null ? 0 : (int)session[SessionKeys.LoginId];
        }

        public static int UserId(this HttpSessionStateBase session)
        {
            return session[SessionKeys.UserId] == null ? 0 : (int)session[SessionKeys.UserId];
        }

        public static string UserName(this HttpSessionStateBase session)
        {
            return session[SessionKeys.UserName] == null ? string.Empty : session[SessionKeys.UserName].ToString();
        }


        public static int ImageId(this HttpSessionStateBase session)
        {
            return session[SessionKeys.ImageID] == null ? 0: (int)session[SessionKeys.ImageID];
        }
        public static int RoleID(this HttpSessionStateBase session)
        {
            return session[SessionKeys.RoleID] == null ? -1 : (int)session[SessionKeys.RoleID];
        }

        #endregion

        #region SET

        public static void SetUserID(this HttpSessionStateBase session, int UserID)
        {

            session[SessionKeys.UserId] = UserID;

        }

        public static void SetLoginId(this HttpSessionStateBase session, int loginId)
        {

            session[SessionKeys.LoginId] = loginId;

        }

        public static void SetUserName(this HttpSessionStateBase session, string userName)
        {
            session[SessionKeys.UserName] = userName;
        }

        public static void SetRoleID(this HttpSessionStateBase session, string roleId)
        {

            session[SessionKeys.RoleID] = roleId;

        }
        public static void SetImageID(this HttpSessionStateBase session, int imageId)
        {

            session[SessionKeys.ImageID] = imageId;

        }
        #endregion
    }
}