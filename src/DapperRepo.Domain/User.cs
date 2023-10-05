using DapperRepo.Domain.Common.Attributes;

namespace DapperRepo.Domain;

[Table("USER")]
public class User
{
    [Key("ID")]
    public int Id { get; set; }

    [Column("Name")]
    public required string Name { get; set; }
}
