using UniqueDraw.Domain.Entities.UniqueDraw;
using UniqueDraw.Domain.Exceptions;

namespace UniqueDraw.Test.Domain.Entities;

[TestFixture]
public class AssignedNumberTests
{
    private readonly List<AssignedNumber> _existingNumbers = [];

    [Test]
    public void AssignUniqueNumber_WithAvailableNumbers_AssignsFirstAvailable()
    {
        var assignedNumber = new AssignedNumber();
        assignedNumber.AssignUniqueNumber(_existingNumbers);
        Assert.That(assignedNumber.Number, Is.EqualTo("00100"));
    }

    [Test]
    public void AssignUniqueNumber_WithNoAvailableNumbers_ThrowsBusinessRuleViolationException()
    {
        for (int i = 1; i <= 99999; i++)
        {
            _existingNumbers.Add(new AssignedNumber { Number = i.ToString("D5") });
        }
        var assignedNumber = new AssignedNumber();
        var exception = Assert.Throws<BusinessRuleViolationException>(
            () => assignedNumber.AssignUniqueNumber(_existingNumbers)
        );

        Assert.That(exception.Message, Is.EqualTo("No hay números disponibles para asignar."));
    }

    [Test]
    public void AssignUniqueNumber_WithNullExistingNumbers_ThrowsArgumentNullException()
    {
        var assignedNumber = new AssignedNumber();
        Assert.Throws<ArgumentNullException>(() => assignedNumber.AssignUniqueNumber(null!));
    }

    [Test]
    public void AssignUniqueNumber_DoesNotAssignInvalidNumbers()
    {
        _existingNumbers.AddRange(
        [
            new AssignedNumber { Number = "11111" },
            new AssignedNumber { Number = "22222" },
            new AssignedNumber { Number = "33333" },
            new AssignedNumber { Number = "44444" }
        ]);

        var assignedNumber = new AssignedNumber();
        assignedNumber.AssignUniqueNumber(_existingNumbers);
        Assert.That(assignedNumber.Number, Is.Not.EqualTo("11111"));
        Assert.That(assignedNumber.Number, Is.Not.EqualTo("22222"));
        Assert.That(assignedNumber.Number, Is.Not.EqualTo("33333"));
        Assert.That(assignedNumber.Number, Is.Not.EqualTo("44444"));
    }
}
