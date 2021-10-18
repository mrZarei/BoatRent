using BoatRent.Core.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace BoatRent.Core.Tests
{
    [TestClass]
    public class BoatTest
    {
        [TestMethod]
        public void ShouldAllTypesHaveImplementation()
        {
            //Arrange
            var boatTypes = Enum.GetValues(typeof(Boat.BoatType)).Cast<Boat.BoatType>();
            var boatNumber = "12345";

            //Act
            foreach (var boatType in boatTypes)
            {
                var boat = Boat.Build(boatType, boatNumber);
                Assert.IsInstanceOfType(boat, typeof(Boat), $"Boat type {boatType} has no implementation");
            }

            var dinghyBoat = Boat.Build(Boat.BoatType.DinghyBoat, boatNumber);

            //Assert
            Assert.IsInstanceOfType(dinghyBoat, typeof(DinghyBoat));
            Assert.AreEqual(boatNumber, dinghyBoat.BoatNumber);

        }

        [TestMethod]
        public void DinghyBoatCalculatePrice()
        {
            //Arrange
            var dinghyBoat = new DinghyBoat("11111");
            var basicFee = 10;
            var hourlyFee = 15;

            var startDate1 = new DateTime(2021, 1, 1, 12, 5, 0); // Start date 2021-01-01 12:05
            var endDate1 = new DateTime(2021, 1, 1, 12, 45, 0); // End date 2021-01-01 12:45

            var startDate2 = new DateTime(2021, 1, 1, 12, 30, 0); // Start date 2021-01-01 12:30
            var endDate2 = new DateTime(2021, 1, 1, 15, 35, 0); // End date 2021-01-01 15:35


            //Act
            var price1 = dinghyBoat.CalculatePrice(startDate1, endDate1, hourlyFee, basicFee);
            var price2 = dinghyBoat.CalculatePrice(startDate2, endDate2, hourlyFee, basicFee);


            //Assert
            Assert.AreEqual(25, price1);
            Assert.AreEqual(70, price2);

        }

        [TestMethod]
        public void SmallSailboatCalculatePrice()
        {
            //Arrange
            var dinghyBoat = new SmallSailboat("11111");
            var basicFee = 10;
            var hourlyFee = 15;

            var startDate1 = new DateTime(2021, 1, 1, 12, 5, 0); // Start date 2021-01-01 12:05
            var endDate1 = new DateTime(2021, 1, 1, 12, 45, 0); // End date 2021-01-01 12:45

            var startDate2 = new DateTime(2021, 1, 1, 12, 30, 0); // Start date 2021-01-01 12:30
            var endDate2 = new DateTime(2021, 1, 1, 15, 35, 0); // End date 2021-01-01 15:35


            //Act
            var price1 = dinghyBoat.CalculatePrice(startDate1, endDate1, hourlyFee, basicFee);
            var price2 = dinghyBoat.CalculatePrice(startDate2, endDate2, hourlyFee, basicFee);


            //Assert
            Assert.AreEqual((decimal)31.5, price1);
            Assert.AreEqual(90, price2);

        }

        [TestMethod]
        public void LargeSailboatCalculatePrice()
        {
            //Arrange
            var dinghyBoat = new LargeSailboat("11111");
            var basicFee = 10;
            var hourlyFee = 15;

            var startDate1 = new DateTime(2021, 1, 1, 12, 5, 0); // Start date 2021-01-01 12:05
            var endDate1 = new DateTime(2021, 1, 1, 12, 45, 0); // End date 2021-01-01 12:45

            var startDate2 = new DateTime(2021, 1, 1, 12, 30, 0); // Start date 2021-01-01 12:30
            var endDate2 = new DateTime(2021, 1, 1, 15, 35, 0); // End date 2021-01-01 15:35


            //Act
            var price1 = dinghyBoat.CalculatePrice(startDate1, endDate1, hourlyFee, basicFee);
            var price2 = dinghyBoat.CalculatePrice(startDate2, endDate2, hourlyFee, basicFee);


            //Assert
            Assert.AreEqual((decimal)36, price1);
            Assert.AreEqual((decimal)99, price2);

        }
    }
}
