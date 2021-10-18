using System;

namespace BoatRent.Core.Tests
{
    public class SmallSailboat : Boat
    {
        public SmallSailboat(string boatNumber) : base(boatNumber)
        {
        }

        public override decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee)
        {
            if (end < start)
            {
                return 0;
            }
            var rentTime = RentTimeInHour(start, end);
            return (basicFee * (decimal)1.2) + (rentTime * hourlyfee * (decimal)1.3);
        }
    }
}
