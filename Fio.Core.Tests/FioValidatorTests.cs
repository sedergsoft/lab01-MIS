using Fio.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fio.Core.Tests;

[TestClass]
public class FioValidatorTests
{
    [TestMethod]
    public void FirstValidDoubleSurnameCreatesTwoPartTemplate()
    {
        var isValid = FioValidator.TryParse("Novikov-Priboy A.S.", out var template);

        Assert.IsTrue(isValid);
        Assert.IsNotNull(template);
        Assert.AreEqual(2, template.SurnameParts);
        Assert.AreEqual(2, template.Initials);
    }

    [TestMethod]
    public void MatchingTemplateAcceptsAnotherDoubleSurname()
    {
        FioValidator.TryParse("Novikov-Priboy A.S.", out var template);
        FioValidator.TryParse("Ivanov-Petrov A.B.", out var candidate);

        Assert.IsNotNull(template);
        Assert.IsNotNull(candidate);
        Assert.IsTrue(template.Matches(candidate));
    }

    [TestMethod]
    public void DifferentSurnameStructureDoesNotMatchTemplate()
    {
        FioValidator.TryParse("Novikov-Priboy A.S.", out var template);
        FioValidator.TryParse("Ivanov A.B.", out var candidate);

        Assert.IsNotNull(template);
        Assert.IsNotNull(candidate);
        Assert.IsFalse(template.Matches(candidate));
    }

    [DataTestMethod]
    [DataRow("-Novikov A.S.")]
    [DataRow("Novikov- A.S.")]
    [DataRow("Novikov--Priboy A.S.")]
    [DataRow("Novikov-Priboy A.S")]
    [DataRow("Novikov-Priboy AB.")]
    public void InvalidFormatsAreRejected(string value)
    {
        Assert.IsFalse(FioValidator.TryParse(value, out _));
    }

    [TestMethod]
    public void InitialCountIsPartOfTheTemplate()
    {
        FioValidator.TryParse("Ivanov A.B.", out var template);
        FioValidator.TryParse("Petrov A.B.C.", out var candidate);

        Assert.IsNotNull(template);
        Assert.IsNotNull(candidate);
        Assert.AreEqual(2, template.Initials);
        Assert.AreEqual(3, candidate.Initials);
        Assert.IsFalse(template.Matches(candidate));
    }
}
