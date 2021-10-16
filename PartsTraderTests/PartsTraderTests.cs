using System;
using Xunit;
using static PartsTraderApi.Library;

namespace PartsTraderTests
{
    public class PartsTraderTests
    {
        [Fact]
        public void ValidatePartNumber_TestTrue_1()
        {
            Assert.True(PartNumberIsValid("1234-abcd"));
        }
        [Fact]
        public void ValidatePartNumber_TestTrue_2()
        {
            Assert.True(PartNumberIsValid("1234-a1b2c3d4"));
        }
        [Fact]
        public void ValidatePartNumber_TestFalse_1()
        {
            Assert.False(PartNumberIsValid("a234-abcd"));
        }
        [Fact]
        public void ValidatePartNumber_TestFalse_2()
        {
            Assert.False(PartNumberIsValid("123-abcd"));
        }

    }
}
