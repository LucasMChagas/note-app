using NoteApp.Domain.Contexts.AccountContext.ValueObjects;

namespace NoteApp.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class VerificationTests
{

    [TestMethod]
    public void GivenANewVerificationItShouldGenerateACodeWith6Characters()
    {
        //Arrange
        var verification = new Verification();

        //Act

        //Assert
        Assert.AreEqual(6, verification.Code.Length);
    }

    [TestMethod]
    public void GivenANewVerificationItShouldGenerateAExpiresAtPropertie()
    {
        //Arrange
        var verification = new Verification();

        //Act

        //Assert
        Assert.IsNotNull(verification.ExpiresAt);
    }

    [TestMethod]
    public void GivenANewVerificationExpiresAtPropertieMustHaveAValidityOf15Minutes()
    {
        //Arrange
        var verification = new Verification();

        //Act
        var now = DateTime.UtcNow;
        now = now.AddMinutes(15);

        //Assert
        Assert.AreEqual(verification.ExpiresAt.Value.Minute, now.Minute);
    }

    [TestMethod]
    public void GivenANewVerificationVerifiedAtPropertieMustBeNull()
    {
        //Arrange
        var verification = new Verification();

        //Act

        //Assert
        Assert.IsNull(verification.VerifiedAt);
    }

    [TestMethod]
    public void GivenANewVerificationIsActivePropertieMustFalse()
    {
        //Arrange
        var verification = new Verification();

        //Act

        //Assert
        Assert.IsFalse(verification.IsActive);
    }

    [TestMethod]
    public void GivenAValidCodeIsActivePropertieMustTrue()
    {
        //Arrange
        var verification = new Verification();

        //Act
        var code = verification.Code;
        verification.Verify(code);
        //Assert
        Assert.IsTrue(verification.IsActive);
    }

    [TestMethod]
    public void GivenAInvalidCodeIsActivePropertieMustFalse()
    {
        //Arrange
        var verification = new Verification();

        //Act
        var code = "WWWWWWW";
        verification.Verify(code);
        //Assert
        Assert.IsFalse(verification.IsActive);
    }

    [TestMethod]
    public void NewCodeMethodMustGenerateANewCode()
    {
        //Arrange
        var verification = new Verification();

        //Act
        var code = verification.Code;
        verification.NewCode();
        //Assert
        Assert.AreNotEqual(verification.Code, code);
    }

    [TestMethod]
    public void NewCodeMethodMustGenerateANewExpiresAt()
    {
        //Arrange
        var verification = new Verification();

        //Act
        var expiresAt = verification.ExpiresAt;
        verification.NewCode();
        //Assert
        Assert.AreNotEqual(verification.ExpiresAt, expiresAt);
    }


}
