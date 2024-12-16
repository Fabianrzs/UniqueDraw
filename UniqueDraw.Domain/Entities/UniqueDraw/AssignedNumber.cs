using UniqueDraw.Domain.Entities.Base;

namespace UniqueDraw.Domain.Entities.UniqueDraw;

public class AssignedNumber : EntityBase
{
    public Guid ClientId { get; set; }
    public Client Client { get; set; } = new();
    public Guid UserId { get; set; }
    public User User { get; set; } = new();
    public string Number { get; set; } = string.Empty;
    public Guid RaffleId { get; set; }
    public Raffle Raffle { get; set; } = new();

    private const int MinNumber = 1;
    private const int MaxNumber = 99999;

    public void AssignUniqueNumber(IEnumerable<AssignedNumber> existingNumbers)
    {
        ArgumentNullException.ThrowIfNull(existingNumbers);

        var assignedNumbers = new HashSet<string>(existingNumbers.Select(e => e.Number));

        for (int number = MinNumber; number <= MaxNumber; number++)
        {
            var candidate = number.ToString("D5");

            if (!assignedNumbers.Contains(candidate) && IsValidNumber(candidate))
            {
                Number = candidate;
                return; 
            }
        }

        throw new InvalidOperationException("No hay números disponibles para asignar.");
    }

    private static bool IsValidNumber(string number)
    {
        for (int i = 0; i < number.Length - 2; i++)
        {
            if (number[i] == number[i + 1] 
                && number[i] == number[i + 2])  return false;
        }
        return true;
    }
}
