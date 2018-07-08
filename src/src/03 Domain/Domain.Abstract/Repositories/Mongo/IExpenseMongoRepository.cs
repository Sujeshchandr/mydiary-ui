using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;

namespace MyDiary.Domain.Abstract.Repositories.Mongo
{
    public interface IExpenseMongoRepository
    {
        IList<IExpense> GetAll(int userId, int currentPage, DateTime expenseDate, string expenseMonth,IList<IExpenseType> expenseTypes = null);
        [Obsolete("GetAllByLinq is depreciated, please use GetAll instead.")]
        IList<IExpense> GetAllByLinq(int userId, int currentPage, DateTime expenseDate, IList<IExpenseType> expenseTypes = null);
        bool Add(IExpense expense);
        bool Add(IList<IExpense> expenses);
        void Update(IExpense expense);
        bool RemoveAllDocuments();
        bool RemoveDocumentsByUserId(int userId);
        void RemoveDocumentByExpenseId(int expenseId);
    }
}
