using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.SQL
{
    public interface IPeopleRepository
    {
        int AddUser(IPeople user);
        void AddUserLoginInformation(IUserLogin userLogin);
        int UploadImage(IImage image);
        List<IPeople> GetAll();
        IPeople GetByLoginDetails(string emailId, string password);
        IPeople GetByOpenSiteDetails(int siteId, string siteUserId);
    }
}
