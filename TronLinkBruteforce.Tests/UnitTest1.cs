namespace TronLinkBruteforce.Tests;

public class Tests
{
    [Test]
    public void Test1()
    {
        const string expectedPass = "ABCDEFgH1";
        var start = "ABCDEFgH1".ToLower();

        //var str = string.Join("\n", BruteForce.GenerateCaseCombinations(start, 0, ""));
        //File.WriteAllText("password.txt", str);

        Assert.That(BruteForce.GenerateCaseCombinations(start, 0, ""), Does.Contain(expectedPass));
    }
}