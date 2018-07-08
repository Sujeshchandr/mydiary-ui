using MyDiary.Application.Services.Abstract.People;
using MyDiary.UI.ControllerHelpers;
using MyDiary.UI.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyDiary.UI.Controllers
{

    public class PeopleController : Controller
    {
        #region PRIVATE CONSTANTS

        private const string USER_IMAGE_PATH="~/Content/Diary/UserImages";

        #endregion

        #region PRIVATE VARIABLES

        private IPeopleService _peopleService;
        private ILogger _logger;

        #endregion

        #region CONSTRUCTOR

        public PeopleController(ILogger logger,IPeopleService peopleService)
        {
            if (logger == null)
            {
                throw new ArgumentNullException("logger");
            }

            if (peopleService == null)
            {
                throw new ArgumentNullException("peopleService");
            }

            _logger = logger;
            _peopleService = peopleService;
        }

        #endregion

        #region PUBLIC METHODS

        [Route("People")]
        public ActionResult Index()
        {
            try
            {
                if (Session.UserId() <= 0)
                {
                    return View("~/Views/People/AddUser.cshtml");
                }
                else
                {
                    // To Do -- > redirect to home page
                    return RedirectToAction("Index", "Home"); //default page for a logged in user
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }

        public int AddUser(UserViewModel userViewModel)
        {
            try
            {
                if (userViewModel.UserImages == null)
                {
                    throw new ArgumentNullException("user images cannot be null");
                }

                if (!userViewModel.UserImages.Any())
                {
                    throw new ArgumentNullException("user images cannot be empty");
                }

                int userId = _peopleService.Add(MapUserViewModelToDTO(userViewModel));

                return userId;
            }
            catch (Exception ex)
            {
               _logger.Error(ex); 
                throw;
            }

        }

        [Route("People/LogIn")]
        public JsonResult LogIn(LoginViewModel loginViewModel)
        {
            try
            {
                if (loginViewModel == null)
                {
                    throw new ArgumentNullException("loginViewModel");
                }

                if (string.IsNullOrEmpty(loginViewModel.EmailId))
                {
                    throw new ArgumentNullException("Emaild");
                }

                if (string.IsNullOrEmpty(loginViewModel.Password))
                {
                    throw new ArgumentNullException("Password");
                }

                UserViewModel user = this.MapUserDTOTOViewModel(_peopleService.LogIn(MapLoginViewModelToDTO(loginViewModel)));
                if (user.UserId > 0)
                {
                    this.setLoginUserDetailsInSession(user);
                    CookieHelper.SetCookie(user.UserId);
                }
                return Json(user.UserId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }

        [Route("People/LogInByFacebook")]
        public JsonResult LogInByFacebook(OpenLoginViewModel openLoginViewModel)
        {
            try
            {
                if (openLoginViewModel == null)
                {
                    throw new ArgumentNullException("openLoginViewModel");
                }

                if (string.IsNullOrEmpty(openLoginViewModel.OpenUserId))
                {
                    throw new ArgumentNullException("OpenUserId");
                }

                if (openLoginViewModel.SiteId == 0)
                {
                    throw new ArgumentNullException("SiteId");
                }

                UserViewModel user = this.MapUserDTOTOViewModel(_peopleService.LogInByOpenId(MapOpenLoginViewModelToDTO(openLoginViewModel)));
                if (user.UserId > 0)
                {
                    this.setLoginUserDetailsInSession(user);
                }
                return Json(user.UserId, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
          
        }

        [Route("People/LogOut")]
        public JsonResult LogOut(LoginViewModel loginViewModel)
        {
            try
            {
                Response.Cookies.Clear();
                Session.Clear();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }

        [Route("People/GetLoginUser")]
        public JsonResult GetLoginUser()
        {

            try
            {
                LoginViewModel loginUser = new LoginViewModel()
                    {
                        LoginId = Session.LoginId(),
                        UserId = Session.UserId(),
                        FirstName = Session.UserName(),
                        ImageId = Session.ImageId()

                    };
                _logger.Trace("get login user", loginUser.LoginId,loginUser.UserId,loginUser.FirstName,loginUser.ImageId);
                return Json(loginUser, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        #region IMAGE UPLOADER TO FOLDER

        [Route("People/UploadImage")]
        public ContentResult UploadImage()
        {
            try
            {
                var r = new List<ImageViewModel>();
                if (Request.Files == null)
                    throw new ArgumentNullException("Files cannot be null");
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;

                    Guid userGuid = Guid.NewGuid();
                    string folderPath = Server.MapPath(USER_IMAGE_PATH) + "/" + userGuid;
                    System.IO.Directory.CreateDirectory(folderPath);
                    string savedFileName = System.IO.Path.Combine(folderPath, System.IO.Path.GetFileName(hpf.FileName));
                    hpf.SaveAs(savedFileName);

                    r.Add(new ImageViewModel()
                    {
                        Name = hpf.FileName,
                        //Length = hpf.ContentLength,
                        //Type = hpf.ContentType
                        UserGuid = userGuid.ToString()
                    });
                }
                //return Content("{\"name\":\"" + r[0].Name + "\",\"type\":\"" + r[0].Type + "\",\"size\":\"" + string.Format("{;0} bytes", r[0].Length) + "\"}", "application/json");
                return Content("{\"name\":\"" + r[0].Name + "\",\"userGuid\":\"" + r[0].UserGuid + "\"}", "application/json");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [Route("People/UploadImageToDB")]
        public JsonResult UploadImageToDB()
        {
            try
            {
               ImageViewModel uploadImageViewModel = new ImageViewModel();
                int uploadImageId = 0;
               var r = new List<ImageViewModel>();
                if (Request.Files == null)
                    throw new ArgumentNullException("Files cannot be null");
                foreach (string file in Request.Files)
                {
                    HttpPostedFileBase hpf = Request.Files[file] as HttpPostedFileBase;
                    if (hpf.ContentLength == 0)
                        continue;
                    Application.Services.Abstract.DTO.IImage image = new Application.Services.DTO.Image();
                    image.UserImage = uploadImageViewModel.UserImage = ImageHelper.Get(hpf);

                   uploadImageId = _peopleService.UploadImage(image);
                   uploadImageViewModel.UploadedImageId  = uploadImageId;              
                   uploadImageViewModel.Name = hpf.FileName;
                  
                }
                
                return Json( uploadImageViewModel, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        [Route("People/DeleteUploadedImage")]
        public JsonResult DeleteUploadedImage(Guid userGuid)
        {
            try
            {
                string userProfileImageFolder = Server.MapPath(USER_IMAGE_PATH) + "/" + userGuid;
                if (Directory.Exists(userProfileImageFolder))     // Remove the user image from folder as it is saved to db.
                    Directory.Delete(userProfileImageFolder, true);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw ex;
            }
        }


        [Route("People/GetProfileImageByUserGuid")]
        public void GetProfileImageByUserGuid(Guid userGuid)
        {
            try
            {
                String searchFolder = Server.MapPath(USER_IMAGE_PATH) + "/" + userGuid;
                var filters = new String[] { "jpg", "jpeg", "png", "gif", "tiff", "bmp" };
                var files = GetFilesFrom(searchFolder, filters, false);
            }
            catch (Exception ex)
            {
                _logger.Error(ex); 
                throw;
            }
        }

        #endregion

        #endregion

        #region PRIVATE METHODS

        private static String[] GetFilesFrom(String searchFolder, String[] filters, bool isRecursive)
        {
            List<String> filesFound = new List<String>();
            var searchOption = isRecursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
            foreach (var filter in filters)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, String.Format("*.{0}", filter), searchOption));
            }
            return filesFound.ToArray();
        }

        private int setLoginUserDetailsInSession(UserViewModel user)
        {
            Session.SetUserID(user.UserId);
            Session.SetUserName(user.FirstName);
            if (user.UserImages.Any())
            {
                Session.SetImageID(user.UserImages.FirstOrDefault().ImageId);
            }
            return user.UserId;
        }

        //private void SetUserIdInCookie(int userId)
        //{
        //    System.Web.HttpCookie cookie = new HttpCookie("UserId", userId.ToString());
        //    cookie.Expires = DateTime.MaxValue;
        //    Response.Cookies.Add(cookie);

        //}


        #region MAPPING METHODS

        private MyDiary.Application.Services.Abstract.DTO.IPeople MapUserViewModelToDTO(UserViewModel userViewModel)
        {
            return new MyDiary.Application.Services.DTO.People
            {

                FirstName = userViewModel.FirstName,
                MiddleName = userViewModel.MiddleName,
                LastName = userViewModel.LastName,
                EmailId = userViewModel.EmailId,
                Password = userViewModel.Password,
                SiteId = userViewModel.SiteId,
                SiteUserId = userViewModel.SiteUserId,
                UserRoles = (from r in userViewModel.UserRoles select MapRoleViewModelToDTO(r)).ToList(),
                UserImages = (from i in userViewModel.UserImages select MapImageViewModelToDTO(i)).ToList()


            };
        }

        private MyDiary.Application.Services.Abstract.DTO.IRole MapRoleViewModelToDTO(RoleViewModel roleViewModel)
        {
            return new MyDiary.Application.Services.DTO.Role {
                RoleId = roleViewModel.RoleId,
                RoleCode = roleViewModel.RoleCode

            };
        } 

        private MyDiary.Application.Services.Abstract.DTO.IImage MapImageViewModelToDTO(ImageViewModel imageViewModel)
        {
            return new MyDiary.Application.Services.DTO.Image {
                ImageId = imageViewModel.ImageId,
                UserImage = imageViewModel.UserImage
            };
        }

        private MyDiary.Application.Services.Abstract.DTO.ILogin MapLoginViewModelToDTO(LoginViewModel loginViewModel)
        {
            return new Application.Services.DTO.Login() { 
                EmailId =loginViewModel.EmailId,
                Password =loginViewModel.Password,
                UserId =loginViewModel.UserId,
                LoginId =loginViewModel.LoginId
            };
        }

        private MyDiary.Application.Services.Abstract.DTO.IOpenLogin MapOpenLoginViewModelToDTO(OpenLoginViewModel openLoginViewModel)
        {
            return new Application.Services.DTO.OpenLogin()
            {
                OpenUserId = openLoginViewModel.OpenUserId,
                SiteId = openLoginViewModel.SiteId,
                UserId = openLoginViewModel.UserId
            };
        }

        private LoginViewModel MapLoginDTOToViewModel(MyDiary.Application.Services.Abstract.DTO.ILogin loginDTO)
            {
                return new LoginViewModel()
                {
                    EmailId = loginDTO.EmailId,
                    Password = loginDTO.Password,
                    UserId = loginDTO.UserId,
                    LoginId =loginDTO.LoginId,
                    ImageId =loginDTO.ImageId,
                    FirstName = loginDTO.CurrentUser.FirstName,
                    MiddleName =loginDTO.CurrentUser.MiddleName,
                    LastName =loginDTO.CurrentUser.LastName
                };
            }

        private UserViewModel MapUserDTOTOViewModel(MyDiary.Application.Services.Abstract.DTO.IPeople peopleDTO)
        {
            return new UserViewModel()
            {
                UserId = peopleDTO.UserId,
                FirstName = peopleDTO.FirstName,
                MiddleName = peopleDTO.MiddleName,
                LastName = peopleDTO.LastName,
                EmailId = peopleDTO.EmailId,
                UserRoles = this.Map_RoleDTOList_To_ViewModelList(peopleDTO.UserRoles),
                UserImages = this.Map_ImageDTOList_To_ViewModelList(peopleDTO.UserImages),
                SiteId = peopleDTO.SiteId,
                SiteUserId = peopleDTO.SiteUserId
            
            };
        }

        private List<RoleViewModel> Map_RoleDTOList_To_ViewModelList(List<MyDiary.Application.Services.Abstract.DTO.IRole> roleDTOList)
        {
            return (from r in roleDTOList select this.Map_RoleDTO_To_ViewModel(r)).ToList();
        }

        private RoleViewModel Map_RoleDTO_To_ViewModel(MyDiary.Application.Services.Abstract.DTO.IRole roleDTO)
        {
            return new RoleViewModel()
            {
                RoleId = roleDTO.RoleId,
                RoleCode = roleDTO.RoleCode
            };
        }

        private List<ImageViewModel> Map_ImageDTOList_To_ViewModelList(List<MyDiary.Application.Services.Abstract.DTO.IImage> imageList)
        {
            return (from i in imageList select this.Map_ImageDTO_To_ViewModel(i)).ToList();
        }

        private ImageViewModel Map_ImageDTO_To_ViewModel(MyDiary.Application.Services.Abstract.DTO.IImage imageDTO)
        {
            return new ImageViewModel()
            {
                ImageId = imageDTO.ImageId,
                UserImage = imageDTO.UserImage
            };
        }

       
        #endregion

        #endregion

    }
}