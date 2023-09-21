using Bis.ApplySevenDayProcessAutomation.Workflows.Data;
using Bis.ApplySevenDayProcessAutomation.Workflows.Entities;
using MediatR;
using Serilog;
using ILogger = Serilog.ILogger;

namespace Bis.ApplySevenDayProcessAutomation.Workflows;

public class ApplySevenDayProcessNotificationRequest : IRequest<WorkflowSummary>
{
}

public class ApplySevenDayProcessNotificationRequestHandler : IRequestHandler<ApplySevenDayProcessNotificationRequest, WorkflowSummary>
{
    private ILogger Logger { get; }
    private IRepository Repository { get; }
    
    public ApplySevenDayProcessNotificationRequestHandler(ILogger logger, IRepository repository)
    {
        Logger = logger;
        Repository = repository;
    }
    
    public Task<WorkflowSummary> Handle(ApplySevenDayProcessNotificationRequest request, CancellationToken cancellationToken)
    {
        var records = GetApplySevenDayProcessRecords();
        int debtCarped = 0, sentToLovetts = 0, noCardFound = 0, sentToRobot = 0, errored = 0, mismatched = 0;
        Parallel.ForEach(records, r =>
        {
            try
            {
                SendApplySevenDayProcessNotification(r);
            }
            catch (Exception ex)
            {
                Logger.Error(string.Format("Unable to send apply 7 day process notification for {0}.", r.GetPolicyReference()), ex);
            }
        });

        return null;
    }
    

    private IEnumerable<ApplySevenDayProcessNotificationDto> GetApplySevenDayProcessRecords()
    {
        try
        {
            return Repository.GetApplySevenDayProcessRecords().ToList().Take(1).ToList();
        }
        catch (Exception ex)
        {
            Logger.Error(ex,"Error while fetching apply 7 day process records");
            return new List<ApplySevenDayProcessNotificationDto>();
        }
    }
    
    private void SendApplySevenDayProcessNotification(ApplySevenDayProcessNotificationDto applySevenDayProcessNotificationDto)
    {
        throw new NotImplementedException();
    }
    
}