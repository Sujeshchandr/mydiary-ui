using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.SQL
{
    public interface IIncomeTypeRepository
    {
        int Add(IIncomeType incomeType);
        List<IIncomeType> GetAll(int userId);
        
    }
}
