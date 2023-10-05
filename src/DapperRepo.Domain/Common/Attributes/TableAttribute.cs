using DapperRepo.Domain.Common.Attributes.Interfaces;

namespace DapperRepo.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class TableAttribute : Attribute, INamedAttribute
{
    public TableAttribute(string tableName)
    {
        Name = tableName;
    }

    public string Name { get; set; }
}
