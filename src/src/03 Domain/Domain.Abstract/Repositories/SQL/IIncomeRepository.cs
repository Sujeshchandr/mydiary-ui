using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.SQL
{
    public   interface IIncomeRepository
    {
        int AddIncome(IIncome income);
        void Update(IIncome incomeDomain);
        void Delete(int incomeId);
        List<IIncome> GetAll(int userId);
    }
}
