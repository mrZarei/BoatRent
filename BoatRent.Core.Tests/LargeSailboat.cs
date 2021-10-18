using System;

namespace BoatRent.Core.Tests
{
    internal class LargeSailboat : Boat
    {
        public LargeSailboat(string boatNumber) : base(boatNumber)
        {
        }

        public override decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee)
        {
            if (end < start)
            {
                return 0;
            }
            var rentTime = RentTimeInHour(start, end);
            return (basicFee * (decimal)1.5) + (rentTime * hourlyfee * (decimal)1.4);
        }
    }
}
