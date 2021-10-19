using System;
using Xunit;
using static PartsTraderApi.Library;

namespace PartsTraderTests
{
    public class PartsTraderTests
    {
        [Fact]
        public void ValidatePartNumber_Test_True_1()
        {
            Assert.True(PartNumberIsValid("1234-abcd"));
        }
        [Fact]
        public void ValidatePartNumber_Test_True_2()
        {
            Assert.True(PartNumberIsValid("1234-a1b2c3d4"));
        }

        [Fact]
        public void ValidatePartNumber_Test_Case_Sensitive_True_3()
        {
            Assert.True(PartNumberIsValid("1234-QJBDJDA"));
        }
        [Fact]
        public void ValidatePartNumber_Test_PartId_False_1()
        {
            Assert.False(PartNumberIsValid("a234-abcd"));
        }
        [Fact]
        public void ValidatePartNumber_Test_PartId_Short_False_2()
        {
            Assert.False(PartNumberIsValid("123-abcd"));
        }
    }
}