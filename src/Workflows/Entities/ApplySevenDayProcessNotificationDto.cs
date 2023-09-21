namespace Bis.ApplySevenDayProcessAutomation.Workflows.Entities;

public class ApplySevenDayProcessNotificationDto
{
    public string Branch { get; set; }
    public int Client { get; set; }
    public int Ref { get; set; }
    public DateTime DiaryCreated { get; set; }
    public int DiaryLine { get; set; }
    public string Name { get; set; }
    public string Address1 { get; set; }
    public string Town { get; set; }
    public string Postcode { get; set; }
    public string Email { get; set; }
    public string Phone1 { get; set; }
    public string Phone2 { get; set; }
    public decimal DebtAmount { get; set; }
    public string AffinityCode { get; set; }
    public string ProductType { get; set; }
    public decimal LedgerBalance { get; set; }

    public CdlReference GetPolicyReference()
    {
        return CdlReference.Parse(Branch + Client + "-" + Ref);
    }
    
    public bool Matches()
    {
        var debt = Math.Abs(DebtAmount);
        var balance = Math.Abs(LedgerBalance);
        var difference = Math.Abs(debt - balance);

        return difference <= 1;
    }
}