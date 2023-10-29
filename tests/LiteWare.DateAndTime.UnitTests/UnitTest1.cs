namespace LiteWare.DateAndTime.UnitTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            DateTime referenceDateTime = new(2023, 06, 15, 12, 30, 30);
            DateTime expectedDateTime = new(2023, 06, 14, 8, 30, 0);

            RelativeDateTime relativeDateTime = "-1d @ 8H 30m 0s";
            DateTime evaluatedRelativeDateTime = relativeDateTime.Evaluate(referenceDateTime);

            Assert.Equal(evaluatedRelativeDateTime, expectedDateTime);
        }
    }
}