namespace DapperRepo.Infrastructure.Common;

public class QueryCondition
{
    public required string Field { get; set; }
    public required string Operator { get; set; }
    public required string Value { get; set; }
}
