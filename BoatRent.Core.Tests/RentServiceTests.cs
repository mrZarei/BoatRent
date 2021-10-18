using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BoatRent.Core.Tests
{
    [TestClass]
    public class RentServiceTests
    {
        [TestMethod]
        public void RentNotAvailableBoat()
        {
            //Arrange
            var mockedRepository = new MockedRepository();
            var service = new RentService(mockedRepository, 10, 15);

            //Act
            var result1 = service.RentBoat("12322", Domain.Boat.BoatType.DinghyBoat, "0000", "198809212222", DateTime.Now).Result;
            var result2 = service.RentBoat("12322", Domain.Boat.BoatType.DinghyBoat, "0000", "198809212222", DateTime.Now).Result;

            //Assert
            Assert.IsTrue(result1.IsSucceed);
            Assert.IsFalse(result2.IsSucceed);
            Assert.AreEqual("Boat 12322 is not availble", result2.Message);
        }

        [TestMethod]
        public void CalculatePrice()
        {
            //Arrange
            var mockedRepository = new MockedRepository();
            var boatNumber = "1234567";
            var hourlyFee = 10;
            var basicFee = 15;
            var service = new RentService(mockedRepository, hourlyFee, basicFee);

            //Act
            var result = service.RentBoat(boatNumber, Domain.Boat.BoatType.DinghyBoat, "0000", "198809212222", new DateTime(2021, 1, 1, 10, 0, 0)).Result;
            var receipt = service.ReturnBoat(boatNumber, new DateTime(2021, 1, 1, 14, 20, 0)).Result;

            //Assert
            Assert.AreEqual(65, receipt.Price);
            Assert.AreEqual("0000", receipt.BookingNumber);
            Assert.AreEqual(boatNumber, receipt.BoatNumber);
        }

        [TestMethod]
        public void ReturnBoat()
        {
            //Arrange
            var mockedRepository = new MockedRepository();
            var boatNumber = "1234567";
            var hourlyFee = 10;
            var basicFee = 15;
            var service = new RentService(mockedRepository, hourlyFee, basicFee);

            //Act
            var receipt = service.ReturnBoat(boatNumber, new DateTime(2021, 1, 1, 14, 20, 0)).Result;

            //Assert
            Assert.IsNull(receipt);
        }
    }
}
