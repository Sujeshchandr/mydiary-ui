using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyDiary.Application.Services.Abstract.DTO;
using MyDiary.Application.Services.Abstract.Incomes;
using MyDiary.Domain.Abstract.Domains;
using MyDiary.Common.Extensions;
namespace MyDiary.Application.Services.Incomes
{
    public class IncomeService :IIncomeService
    {
        #region PRIVATE PROPERTIES

        Domain.Abstract.Domains.IIncomeType _incomeTypesDomain;
        Domain.Abstract.Domains.IIncome _incomeDomain;

        #endregion

        #region CONSTRUCTOR

        public IncomeService() { }
        public IncomeService(MyDiary.Domain.Abstract.Domains.IIncome incomeDomain , MyDiary.Domain.Abstract.Domains.IIncomeType incomeTypesDomain)
        {
            _incomeDomain = incomeDomain;
            _incomeTypesDomain = incomeTypesDomain;

        }

        #endregion

        #region PUBLIC METHODS

        public List<MyDiary.Application.Services.Abstract.DTO.IIncomeType> GetAllIncomeTypes(int userId)
        {
           return  (from incomeType in  _incomeTypesDomain.GetAll(userId) select MapIncomeTypeDomainToDTO(incomeType)).ToList();

        }

        public int Add(Abstract.DTO.IIncome incomeDTO)
        {
            return _incomeDomain.Add(MapIncomeDTOToDomain(incomeDTO));
        }

        public void Edit(Abstract.DTO.IIncome incomeDTO)
        {
             _incomeDomain.Edit(MapIncomeDTOToDomain(incomeDTO));
        }

        public int AddIncomeType(Abstract.DTO.IIncomeType incomeTypeDTO)
        {
            return _incomeTypesDomain.Add(MapIncomeTypeDTOToDomain(incomeTypeDTO));
        }

        public List<MyDiary.Application.Services.Abstract.DTO.IIncome> GetAll(int userId,IIncomeFilter filters, int currentPage,bool fromSQLServer)
        {
            List<MyDiary.Domain.Abstract.Domains.IIncomeType> filteredIncomeTypes = new List<MyDiary.Domain.Abstract.Domains.IIncomeType>();
            if (filters != null && filters.IncomeTypes.Any())
            {
               // _incomeDomain.IncomeTypes = this.Map_IncomeTypeIds_To_IncomeTypes(filters.IncomeTypes);
                filteredIncomeTypes = this.Map_IncomeTypeIds_To_IncomeTypes(filters.IncomeTypes);
            }
            if (filters != null && !filters.IncomeDate.IsDefault())
            {
                _incomeDomain.IncomeDate = filters.IncomeDate;
            }
            else
            {
                _incomeDomain.IncomeDate = DateTime.MinValue;
            }

            return MapIncomeDomainListToIncomeDTOList(_incomeDomain.GetAll(userId, currentPage, fromSQLServer, filteredIncomeTypes));
        }

        public bool InsertAll(List<Abstract.DTO.IIncome> incomes, bool toSQLServer)
        {
            return _incomeDomain.InsertAll(this.MapIncomeDTOListToIncomeDomainList(incomes), toSQLServer);
        }

        public bool DeleteAll(bool toSQLServer)
        {
            return _incomeDomain.DeleteAll(toSQLServer);
        }

        public void DeleteById(int expenseId)
        {
            _incomeDomain.IncomeId = expenseId;
            _incomeDomain.DeleteById();
        }

        #endregion

        #region PRIVATE METHODS


        private MyDiary.Application.Services.Abstract.DTO.IIncome MapIncomeDomainToDTO(MyDiary.Domain.Abstract.Domains.IIncome incomeDomain)
        {
            return new MyDiary.Application.Services.DTO.Income()
            {
                //RowNumber = incomeDomain.RowNumber,
                IncomeId = incomeDomain.IncomeId,
                IncomeType = this.MapIncomeTypeDomainToDTO(incomeDomain.IncomeType),
                UserId = incomeDomain.UserId,
                Amount = incomeDomain.Amount,
                Description = incomeDomain.Description,
                IncomeDate = incomeDomain.IncomeDate,
                CreatedBy = incomeDomain.CreatedBy,
                ModifiedBy = incomeDomain.ModifiedBy,
                TotalCount = incomeDomain.TotalCount,
                Comments = incomeDomain.Comments,
                ModifiedDate = incomeDomain.ModifiedDate

            };
        }


        private List<MyDiary.Application.Services.Abstract.DTO.IIncome> MapIncomeDomainListToIncomeDTOList(List<MyDiary.Domain.Abstract.Domains.IIncome> incomesDomainList)
        {
            return (from i in incomesDomainList select MapIncomeDomainToDTO(i)).ToList();

        }

        private List<MyDiary.Domain.Abstract.Domains.IIncome> MapIncomeDTOListToIncomeDomainList(List<MyDiary.Application.Services.Abstract.DTO.IIncome> incomesDTOList)
        {
            return (from i in incomesDTOList select MapIncomeDTOToDomain(i)).ToList();

        }

        private List<MyDiary.Application.Services.Abstract.DTO.IIncomeType> Map_IncomeTypeDomainList_To_DTOList(List<MyDiary.Domain.Abstract.Domains.IIncomeType> incomeTypeDomainList)
        {
            return (from i in incomeTypeDomainList select MapIncomeTypeDomainToDTO(i)).ToList();
        }

        private MyDiary.Application.Services.Abstract.DTO.IIncomeType MapIncomeTypeDomainToDTO(MyDiary.Domain.Abstract.Domains.IIncomeType incomeTypeDomain)
        {
            return new MyDiary.Application.Services.DTO.IncomeType()
            {
                Type = incomeTypeDomain.Type,
                TypeId = incomeTypeDomain.TypeId
            };
        }

        private MyDiary.Domain.Abstract.Domains.IIncome MapIncomeDTOToDomain(MyDiary.Application.Services.Abstract.DTO.IIncome incomeDTO)
        {
            return _incomeDomain.CreateIncome(incomeDTO.IncomeId, incomeDTO.UserId, this.MapIncomeTypeDTOToDomain(incomeDTO.IncomeType), incomeDTO.Amount, incomeDTO.IncomeDate, incomeDTO.CreatedBy, incomeDTO.ModifiedBy, incomeDTO.Description, incomeDTO.Comments);
        }

        private List<MyDiary.Domain.Abstract.Domains.IIncomeType> Map_IncomeTypeDTOList_To_DomainList(List<MyDiary.Application.Services.Abstract.DTO.IIncomeType> incomeTypeDTOList)
        {
            return (from i in incomeTypeDTOList select MapIncomeTypeDTOToDomain(i)).ToList();  
        }

        private MyDiary.Domain.Abstract.Domains.IIncomeType MapIncomeTypeDTOToDomain(MyDiary.Application.Services.Abstract.DTO.IIncomeType incomeTypeDTO)
        {

            return _incomeTypesDomain.Create(incomeTypeDTO.TypeId, incomeTypeDTO.Type, incomeTypeDTO.UserId);
        }

        private List<MyDiary.Domain.Abstract.Domains.IIncomeType> Map_IncomeTypeIds_To_IncomeTypes(  List<int> incomeTypeIds)
        {
            List<MyDiary.Domain.Abstract.Domains.IIncomeType> incomeTypes = new List<Domain.Abstract.Domains.IIncomeType>();
            foreach (int incomeTypeId in incomeTypeIds)
            {
                incomeTypes.Add(_incomeTypesDomain.Create(incomeTypeId, string.Empty, 0));

            }
            return incomeTypes;
        }


       

        #endregion

    }
}
