using DapperRepo.Domain.Common.Attributes.Interfaces;

namespace DapperRepo.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class ColumnAttribute : Attribute, INamedAttribute
{
    public ColumnAttribute(string columnName)
    {
        Name = columnName;
    }

    public string Name { get; set; }
}
