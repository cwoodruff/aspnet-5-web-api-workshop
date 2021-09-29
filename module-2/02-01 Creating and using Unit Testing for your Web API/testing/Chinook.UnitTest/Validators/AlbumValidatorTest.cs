using System.Threading.Tasks;
using Chinook.Domain.ApiModels;
using Chinook.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Chinook.UnitTest.Validators
{
    public class AlbumValidatorTest
    {
        private readonly AlbumValidator _validator;

        public AlbumValidatorTest()
        {
            _validator = new AlbumValidator();
        }

        [Fact]
        public void Should_have_error_when_Name_is_null()
        {
            // Arrange
            var model = new AlbumApiModel { Title = null };
            
            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(album => album.Title);
        }

        [Fact]
        public void Should_not_have_error_when_name_is_specified()
        {
            // Arrange
            var model = new AlbumApiModel { Title = "Abbey Road" };
            
            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveValidationErrorFor(album => album.Title);
        }
    }
}