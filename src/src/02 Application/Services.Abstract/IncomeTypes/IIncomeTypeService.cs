using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDiary.Application.Services.Abstract.IncomeTypes
{
    public interface IIncomeTypeService
    {
        List<MyDiary.Application.Services.Abstract.DTO.IIncomeType> GetAllIncomeTypes(int userId);
        int AddIncomeType(Abstract.DTO.IIncomeType incomeTypeDTO);
    }
}
