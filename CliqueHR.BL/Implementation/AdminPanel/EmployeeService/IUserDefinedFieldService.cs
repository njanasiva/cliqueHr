using CliqueHR.Common.Models;
using System.Collections.Generic;

namespace CliqueHR.BL.Implementation.AdminPanel.Employee
{
  
    public interface IUserDefinedFieldService
    {
        ApplicationResponse UpdateUserDefinedField(List<UserDefinedField> model, UserContextModel objUser);
        List<UserDefinedField> GetAllUserDefinedField(UserContextModel objUser);
        UserDefinedField GetUserDefinedFieldById(int ID, UserContextModel objUser);

        List<FieldType> GetAllFieldType(UserContextModel objUser);
        FieldType GetFieldTypeById(int ID, UserContextModel objUser);
        List<FieldValueMaster> GetAllFieldValue(UserContextModel objUser);
        List<FieldValueMaster> GetFieldValueByTypeId(int TypeID, UserContextModel objUser);

    }
}
