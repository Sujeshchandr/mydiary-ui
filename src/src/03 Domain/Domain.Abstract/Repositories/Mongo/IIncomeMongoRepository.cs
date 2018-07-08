using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.Mongo
{
    public interface IIncomeMongoRepository
    {
        List<IIncome> GetAll(int userId, int currentPage, DateTime incomeDate, List<IIncomeType> incomeTypes = null);
        List<IIncome> GetAllByMongoQuery(int userId, int currentPage, DateTime incomeDate, List<IIncomeType> incomeTypes = null);
        bool Add(IIncome income);
        bool Add(List<IIncome> incomes);
        void Update(IIncome income);
        bool RemoveAllDocuments();
        bool RemoveDocumentsByUserId(int userId);
        void RemoveDocumentByIncomeId(int incomeId);
    }

}
