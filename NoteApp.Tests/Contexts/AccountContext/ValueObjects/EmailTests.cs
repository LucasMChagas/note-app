using NoteApp.Domain.AccountContext.ValueObjects;
using NoteApp.Domain.Contexts.AccountContext.ValueObjects;
using System.Runtime.InteropServices;

namespace NoteApp.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class EmailTests
{
    [TestMethod]
    [DataRow("")]
    [DataRow("emialexample")]
    [DataRow("@gmail.com")]
    [DataRow("1231542313215446456")]
    public void GivenWrongEmailFormatTheEmailValueObjectMustBeInvalid(string email)
    {
        var emailObject = new Email(email);

        Assert.IsFalse(emailObject.IsValid);
    }

    [TestMethod]
    [DataRow("lucas@gmail.com")]
    [DataRow("lucas@outlook.com")]
    [DataRow("lucas@yahoo.com.br")]
    public void GivenACertainEmailFormatTheEmailValueObjectMustBeValid(string email)
    {
        var emailObject = new Email(email);

        Assert.IsTrue(emailObject.IsValid);
    }

    [TestMethod]
    public void GivenANewEmailITheAddressPropertieMustBeNotNull()
    {        
        var emailObject = new Email("lucas@gmail.com");
        
        Assert.IsNotNull(emailObject.Address);
    }

    [TestMethod]
    public void GivenANewEmailITheHashPropertieMustBeNotNull()
    {
        var emailObject = new Email("lucas@gmail.com");

        Assert.IsNotNull(emailObject.Hash);
    }

    [TestMethod]
    public void GivenANewEmailITheVerificationPropertieMustBeNotNull()
    {
        var emailObject = new Email("lucas@gmail.com");

        Assert.IsNotNull(emailObject.Verification);
    }

    [TestMethod]
    public void ResetVerificationCodeMethodMustGenerateANewVerification()
    {
        var emailObject = new Email("lucas@gmail.com");
        var oldVerification = emailObject.Verification;

        emailObject.ResetVerificationCode();

        Assert.AreNotEqual(oldVerification, emailObject.Verification);
    }
}
