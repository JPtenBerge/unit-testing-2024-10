namespace DemoProject.xUnit;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        Assert.Equal(1, 1);
        Assert.Equal("hoi", "doei");
    }

    [Theory]
    [InlineData(4, 9, 13)]
    [InlineData(-4, -9, -13)]
    [InlineData(4, -9, -5)]
    [InlineData(-4, 9, 5)]
    public void Test2(int first, int second, int expected)
    {

    }

    //[Theory]
    //[MemberData()]
    public void Test4()
    {

    }
}