using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Domain.Abstract.Repositories.SQL;

namespace MyDiary.Domain.Domains
{
    public class IncomeType : IIncomeType
    {

        #region PRIVATE PROPERTIES

        IIncomeTypeRepository _incomeTypeRepository;

        #endregion

        #region CONSTRUCTOR

        public IncomeType() {}
        public IncomeType(IIncomeTypeRepository  incomeTypeRepository)
        {
            _incomeTypeRepository = incomeTypeRepository;
        }

        #endregion

        #region PUBLIC PROPERTIES

        public int TypeId {get;set;}       

        public string Type {get;set;}

        public int UserId { get; set; }

       #endregion

        #region PUBLIC METHODS

        public int Add(IIncomeType incomeType)
        {
            return _incomeTypeRepository.Add(incomeType);
        }

        public List<IIncomeType> GetAll(int userId)
        {
            return _incomeTypeRepository.GetAll(userId);
        }       
        
        public IIncomeType Create(int TypeId, string Type, int userId)
        {
            return new IncomeType()
            {
                TypeId = TypeId,
                Type = Type,
                UserId =userId
            };
        }

        #endregion

    }
}
