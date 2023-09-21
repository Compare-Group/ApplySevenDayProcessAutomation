using Bis.ApplySevenDayProcessAutomation.Workflows.Entities;

namespace Bis.ApplySevenDayProcessAutomation.Workflows.Data;

public class Repository : IRepository
{
    private string ConnectionString { get; set; }

    public Repository(string connectionString)
    {
        ConnectionString = connectionString;
    }
    
    public object ExecuteScalar(string sql)
    {
        return Sql.ExecuteScalar<object>(ConnectionString, sql);
    }

    public IEnumerable<ApplySevenDayProcessNotificationDto> GetApplySevenDayProcessRecords()
    {
        var mapper = new DataRowMapper();
        var debtXDiaries = Sql.ExecuteSp("up_select_Debt3_Alertlist", ConnectionString)
            .Select(x => mapper.Map(x, new ApplySevenDayProcessNotificationDto()))
            .ToList();
        var matches = debtXDiaries.Where(x => x.Matches()).ToList();
        return matches;
    }
}