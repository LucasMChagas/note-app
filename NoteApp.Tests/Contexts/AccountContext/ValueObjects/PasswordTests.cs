using NoteApp.Domain.Contexts.AccountContext.ValueObjects;

namespace NoteApp.Tests.Contexts.AccountContext.ValueObjects;

[TestClass]
public class PasswordTests
{
    [TestMethod]
    public void GivenANewPasswordItShouldGenerateAResetCodeWith8Characters()
    {
        var password = new Password("123456789@");

        Assert.AreEqual(8, password.ResetCode.Length);
    }

    [TestMethod]
    public void GivenANewPasswordItShouldGenerateAHashPropertie()
    {
        var password = new Password("123456789@");

        Assert.IsNotNull(password.Hash);
    }

    [TestMethod]
    public void PassingANullArgumentItShouldGenerateAResetCodePropertieWith8Characters()
    {
        var password = new Password(null);

        Assert.AreEqual(8, password.ResetCode.Length);
    }

    [TestMethod]
    public void PassingANullArgumentItShouldGenerateAHash()
    {
        var password = new Password(null);

        Assert.IsNotNull(password.Hash);
    }


    [TestMethod]
    public void PassingAnInvalidPasswordShouldReturnFalse()
    {
        var password = new Password("asdfghjklç");
        var plainTextPassword = "qwerrteyyuu";

        var result = password.Challenge(plainTextPassword);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public void PassingAvalidPasswordShouldReturnTrue()
    {
        var password = new Password("asdfghjklç");
        var plainTextPassword = "asdfghjklç";

        var result = password.Challenge(plainTextPassword);

        Assert.IsTrue(result);
    }
}
