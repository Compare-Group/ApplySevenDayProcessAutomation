using Bis.ApplySevenDayProcessAutomation.Workflows.Entities;

namespace Bis.ApplySevenDayProcessAutomation.Workflows.Data;

public interface IRepository
{
    object ExecuteScalar(string sql);
    IEnumerable<ApplySevenDayProcessNotificationDto> GetApplySevenDayProcessRecords();
}