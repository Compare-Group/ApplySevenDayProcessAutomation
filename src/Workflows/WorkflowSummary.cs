namespace Bis.ApplySevenDayProcessAutomation.Workflows;

public class WorkflowSummary
{
    public string Description { get; }
    public int Attempted { get; set; }
    public int Success { get; set; }
    public int Fail { get; set; }
    public int Ignored { get; set; }
    
    public WorkflowSummary(string description, int attempted, int success, int fail, int ignored)
    {
        Description = description;
        Attempted = attempted;
        Success = success;
        Fail = fail;
        Ignored = ignored;
    }
}