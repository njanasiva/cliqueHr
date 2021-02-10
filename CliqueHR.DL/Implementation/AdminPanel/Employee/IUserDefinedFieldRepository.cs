using CliqueHR.Common.Models;
using System.Collections.Generic;

namespace CliqueHR.DL.Implementation.AdminPanel.Employee
{
    

    public interface IUserDefinedFieldRepository
    {
        List<UserDefinedField> GetAllUserDefinedField(string DBName);
        UserDefinedField GetUserDefinedFieldById(int Id, string DBName);
        ApplicationResponse UpdateUserDefinedField(List<UserDefinedField> model, string DBName);

        List<FieldType> GetAllFieldType(string DbName);
        FieldType GetFieldTypeById(int id, string DbName);

        List<FieldValueMaster> GetAllFieldValue(string DbName);
        List<FieldValueMaster> GetFieldValueByTypeId(int TypeID, string DbName);
    }
}
