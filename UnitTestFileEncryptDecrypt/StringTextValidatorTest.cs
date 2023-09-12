using ServiceLayer.Utils.Interfaces;
using ServiceLayer.Validator;

namespace UnitTestFileEncryptDecrypt
{
   
    public class StringTextValidatorTest
    {
        IStringTextValidator _stringTextValidator;

        public StringTextValidatorTest()
        {
            _stringTextValidator = new StringTextValidator();
        }

        [Theory]
        //arrange
        [InlineData("abc", "abc", "")]
        [InlineData("", "abc", "WARNING: Keyword is Empty!")]
        [InlineData("abc", "", "WARNING: Confirm Keyword is Empty!")]
        [InlineData("abc", "def", "WARNING: Keyword and Confirm Keyword differ!")]
        public void CompareKeywords_InsertInputs(string password, string confirmPassword, string expected)
        {
            //Act
            var actual = _stringTextValidator.ValidateStrings(password, confirmPassword);
            //Assert
            Assert.Equal(expected, actual);
        }
    }
}
