namespace DapperRepo.Application;

public interface IQueryCondition
{
    string Field { get; set; }
    string Operator { get; set; }
    string Value { get; set; }
}