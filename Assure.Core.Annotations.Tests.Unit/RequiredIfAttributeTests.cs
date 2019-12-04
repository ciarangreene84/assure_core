using Assure.Core.Annotations.Implementation;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Assure.Core.Annotations.Tests.Unit
{
    public class RequiredIfAttributeTests
    {
        [Fact]
        public void ValidateConditionTrue_Success()
        {
            var request = new TestClass()
            {
                Condition = true,
                RequiredIfConditionTrue = "Success!"
            };
            Validator.ValidateObject(request, new ValidationContext(request), true);
        }

        [Fact]
        public void ValidateConditionTrue_Error()
        {
            var request = new TestClass()
            {
                Condition = true
            };
            var result = Assert.Throws<ValidationException>(() => Validator.ValidateObject(request, new ValidationContext(request), true));
            Assert.Equal("'RequiredIfConditionTrue' is required because 'Condition' has a value of 'True'.", result.Message);
        }

        [Fact]
        public void ValidateConditionTrue_Error_Empty()
        {
            var request = new TestClass()
            {
                Condition = true,
                RequiredIfConditionTrue = string.Empty
            };
            var result = Assert.Throws<ValidationException>(() => Validator.ValidateObject(request, new ValidationContext(request), true));
            Assert.Equal("'RequiredIfConditionTrue' is required because 'Condition' has a value of 'True'.", result.Message);
        }

        [Fact]
        public void ValidateConditionFalse_Success()
        {
            var request = new TestClass()
            {
                Condition = false,
                RequiredIfConditionFalse = "Success!"
            };
            Validator.ValidateObject(request, new ValidationContext(request), true);
        }

        [Fact]
        public void ValidateConditionFalse_Error()
        {
            var request = new TestClass()
            {
                Condition = false
            };
            var result = Assert.Throws<ValidationException>(() => Validator.ValidateObject(request, new ValidationContext(request), true));
            Assert.Equal("'RequiredIfConditionFalse' is required because 'Condition' has a value of 'False'.", result.Message);
        }

        [Fact]
        public void ValidateConditionFalse_Error_Empty()
        {
            var request = new TestClass()
            {
                Condition = false,
                RequiredIfConditionFalse = string.Empty
            };
            var result = Assert.Throws<ValidationException>(() => Validator.ValidateObject(request, new ValidationContext(request), true));
            Assert.Equal("'RequiredIfConditionFalse' is required because 'Condition' has a value of 'False'.", result.Message);
        }


        [Fact]
        public void ValidateCondition_InvalidProperty()
        {
            var request = new TestClass_Invalid();
            var result = Assert.Throws<ValidationException>(() => Validator.ValidateObject(request, new ValidationContext(request), true));
            Assert.Equal("Could not find a property named 'Invalid'.", result.Message);
        }
    }

    public class TestClass
    {
        public bool Condition { get; set; }

        [RequiredIf("Condition", true)]
        public string RequiredIfConditionTrue { get; set; }

        [RequiredIf("Condition", false)]
        public string RequiredIfConditionFalse { get; set; }
    }

    public class TestClass_Invalid
    {
        public bool Condition { get; set; }

        [RequiredIf("Invalid", true)]
        public string RequiredIfConditionTrue { get; set; }

        [RequiredIf("Invalid", false)]
        public string RequiredIfConditionFalse { get; set; }
    }
}
