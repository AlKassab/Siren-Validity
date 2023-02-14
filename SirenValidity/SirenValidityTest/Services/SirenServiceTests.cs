using SirenValidity.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SirenValidityTest.Services
{
    public class SirenServiceTests
    {
        [Fact]
        public void CheckSirenValidity_ReturnsTrueForValidSiren()
        {
            // Arrange
            var service = new SirenService();
            var validSiren = "123456782";

            // Act
            var result = service.CheckSirenValidity(validSiren);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CheckSirenValidity_ReturnsFalseForInvalidSiren()
        {
            // Arrange
            var service = new SirenService();
            var invalidSiren = "123456788";

            // Act
            var result = service.CheckSirenValidity(invalidSiren);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void ComputeFullSiren_ReturnsFullSirenWithControlNumber()
        {
            // Arrange
            var service = new SirenService();
            var sirenWithoutControlNumber = "12345678";
            var expectedFullSiren = "123456782";

            // Act
            var result = service.ComputeFullSiren(sirenWithoutControlNumber);

            // Assert
            Assert.Equal(expectedFullSiren, result);
        }

        [Fact]
        public void ComputeFullSiren_ThrowsExceptionForInvalidSirenWithoutControlNumber()
        {
            // Arrange
            var service = new SirenService();
            var invalidSirenWithoutControlNumber = "1234567";

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => service.ComputeFullSiren(invalidSirenWithoutControlNumber));
            Assert.Equal("Invalid SIREN without control number.", ex.Message);
        }
    }
}
