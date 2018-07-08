using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.IncomeTypes;
using MyDiary.Domain.Abstract.Domains;


namespace MyDiary.Application.Services.IncomeTypes
{
    public class IncomeTypeService :IIncomeTypeService
    {
        #region PRIVATE PROPERTIES

        IIncomeType _incomeTypesDomain;

        #endregion

        #region CONSTRUCTOR

        public IncomeTypeService() { }
        public IncomeTypeService(IIncomeType incomeTypesDomain)
        {
            _incomeTypesDomain = incomeTypesDomain;

        }
        #endregion

        #region PUBLIC METHODS

        public List<MyDiary.Application.Services.Abstract.DTO.IIncomeType> GetAllIncomeTypes(int userId)
        {
            return (from incomeType in _incomeTypesDomain.GetAll(userId) select MapIncomeTypeDomainToDTO(incomeType)).ToList();

        }     

        public int AddIncomeType(Abstract.DTO.IIncomeType incomeTypeDTO)
        {
            return _incomeTypesDomain.Add(MapIncomeTypeDTOToDomain(incomeTypeDTO));
        }

        #endregion

        #region PRIVATE METHODS

        private MyDiary.Application.Services.Abstract.DTO.IIncomeType MapIncomeTypeDomainToDTO(MyDiary.Domain.Abstract.Domains.IIncomeType incomeTypeDomain)
        {
            return new MyDiary.Application.Services.DTO.IncomeType()
            {
                Type = incomeTypeDomain.Type,
                TypeId = incomeTypeDomain.TypeId
            };
        }

        private MyDiary.Domain.Abstract.Domains.IIncomeType MapIncomeTypeDTOToDomain(MyDiary.Application.Services.Abstract.DTO.IIncomeType incomeTypeDTO)
        {
            return _incomeTypesDomain.Create(incomeTypeDTO.TypeId, incomeTypeDTO.Type, incomeTypeDTO.UserId);
        }

        #endregion
    }

}