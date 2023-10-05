using DapperRepo.Domain.Common.Attributes.Interfaces;

namespace DapperRepo.Domain.Common.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class KeyAttribute : Attribute, INamedAttribute
{
    public KeyAttribute(string columnName)
    {
        Name = columnName;
    }

    public string Name { get; set; }
}
