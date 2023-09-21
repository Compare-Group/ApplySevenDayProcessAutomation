using System.Text.RegularExpressions;

namespace Bis.ApplySevenDayProcessAutomation.Workflows.Entities;

public class CdlReference
{
    public static readonly CdlReference Empty = new CdlReference(' ', 0, null);
    public const string Pattern = @"^(?<Branch>[A-Za-z])(?<Customer>\d{1,6})(-(?<Policy>\d{1,3}))?$";

    public static CdlReference Parse(string value)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentException("Value cannot be null or an empty string.", "value");

        var match = new Regex(Pattern).Match(value);

        if (!match.Success)
        {
            throw new FormatException(string.Format("'{0}' is not a valid format for a CDL reference.", value));
        }

        var branch = match.Groups["Branch"].Value[0];
        var customer = int.Parse(match.Groups["Customer"].Value);
        var policy = match.Groups["Policy"].Success ? (int?)int.Parse(match.Groups["Policy"].Value) : null;

        return new CdlReference(branch, customer, policy);
    }

    private CdlReference(char branch, int customer, int? policy)
    {
        Branch = branch;
        Customer = customer;
        Policy = policy;
    }

    public char Branch { get; private set; }
    public int Customer { get; private set; }
    public int? Policy { get; private set; }

    public override string ToString()
    {
        return string.Format("{0}{1}", ToShortString(), Policy.HasValue ? "-" + Policy : string.Empty);
    }

    public string ToShortString()
    {
        return string.Format("{0}{1}", Branch, Customer);
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as CdlReference;

        if (compareTo == null)
        {
            return false;
        }

        return Branch == compareTo.Branch && Customer == compareTo.Customer && Policy == compareTo.Policy;
    }

    public override int GetHashCode()
    {
        return ToShortString().GetHashCode();
    }
}