//using FluentValidation.TestHelper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.ChangeTracking;
//using Moq;
//using Moq.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TheFullStackTeam.Application.Countries.Commands.CreateCountry;
//using TheFullStackTeam.Application.Model.EntityModel;
//using TheFullStackTeam.Domain.Entities;
//using TheFullStackTeam.Persistence.App;
//using Xunit;

//namespace TheFullStackTeam.Application.Tests.Countries.Commands
//{
//    public class CreateCountryValidatorTests
//    {
//        private readonly CreateCountryValidator _validator;

//        public CreateCountryValidatorTests()
//        {
//            _validator = new CreateCountryValidator();
//        }



//        [InlineData(null)]
//        [InlineData("")]
//        [InlineData(" ")]
//        [InlineData("     ")]
//        [Theory]
//        public void CommonName_ShouldHaveValidationErrorFor(string commonName)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                CommonName = commonName
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.CommonName);
//        }


//        [InlineData(null)]
//        [InlineData("")]
//        [InlineData(" ")]
//        [InlineData("     ")]
//        [Theory]
//        public void NativeName_ShouldHaveValidationErrorFor(string nativeName)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                NativeName = nativeName
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.NativeName);
//        }

//        [InlineData(null)]
//        [InlineData("")]
//        [InlineData(" ")]
//        [InlineData("     ")]
//        [Theory]
//        public void OfficialName_ShouldHaveValidationErrorFor(string officialName)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                OfficialName = officialName
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.OfficialName);
//        }

//        [InlineData("12345678901")]
//        [Theory]
//        public void Tld_ShouldHaveValidationErrorFor(string tld)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                Tld = tld
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.Tld);
//        }

//        [InlineData("ABC")]
//        [Theory]
//        public void Cca2_ShouldHaveValidationErrorFor(string cca2)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                Cca2 = cca2
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.Cca2);
//        }

//        [InlineData("ABCD")]
//        [Theory]
//        public void Cca3_ShouldHaveValidationErrorFor(string cca3)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                Cca3 = cca3
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.Cca3);
//        }

//        [InlineData("ABCD")]
//        [Theory]
//        public void Ccn3_ShouldHaveValidationErrorFor(string ccn3)
//        {
//            // Arrange
//            var model = new CountryModel
//            {
//                Ccn3 = ccn3
//            };
//            var command = new CreateCountryCommand(model);

//            // Act
//            var result = _validator.TestValidate(command);

//            // Assert
//            result.ShouldHaveValidationErrorFor(x => x.Model.Ccn3);
//        }

//    }

//}
