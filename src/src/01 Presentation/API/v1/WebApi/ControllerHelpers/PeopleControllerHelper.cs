using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyDiary.API.JSON;
using MyDiary.Application.Services.Abstract.DTO;

namespace MyDiary.API.ControllerHelpers
{
    public class PeopleControllerHelper
    {
        #region PUBLIC METHODS

        public List<PeopleJSON> Map_IncomesDTOList_To_IncomeJSONList(List<IPeople> peopleDTOList)
        {
            return (from p in peopleDTOList select Map_PeopleDTO_To_JSON(p)).ToList();
        }

        public PeopleJSON Map_PeopleDTO_To_JSON(IPeople peopleDTO)
        {
            return new PeopleJSON()
            {
                UserId = peopleDTO.UserId,
                FirstName = peopleDTO.FirstName,
                MiddleName = peopleDTO.MiddleName,
                LastName = peopleDTO.LastName,
                EmailId = peopleDTO.EmailId,
                UserRoles = this.Map_RoleDTOList_To_JSON(peopleDTO.UserRoles),
                UserImages = this.Map_ImageDTOList_To_JSON(peopleDTO.UserImages),
                SiteId = peopleDTO.SiteId,
                SiteUserId = peopleDTO.SiteUserId
            };
        }

        #endregion

        #region PRIVATE METHODS


        private List<RoleJSON> Map_RoleDTOList_To_JSON(List<IRole> roleDTOList)
        {
 	        return (from r in roleDTOList select this.Map_RoleDTO_To_JSON(r)).ToList();
        }

        private List<ImageJSON> Map_ImageDTOList_To_JSON(List<IImage> imageList)
        {
 	        return (from i in imageList select this.Map_ImageDTO_To_JSON(i)).ToList();
        }

        private RoleJSON Map_RoleDTO_To_JSON(IRole roleDTO)
        {
            return new RoleJSON()
            {
                RoleId = roleDTO.RoleId,
                RoleName = roleDTO.RoleCode
            };
        }

        private ImageJSON Map_ImageDTO_To_JSON(IImage imageDTO)
        {
            return new ImageJSON()
            {
                ImageId = imageDTO.ImageId,
                UserImage = imageDTO.UserImage
            };
        }

        #endregion
    }
}