using System;

namespace BoatRent.Core.Domain
{
    public class DinghyBoat : Boat
    {
        public DinghyBoat(string boatNumber) : base(boatNumber)
        {
        }

        public override decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee)
        {
            if (end < start)
            {
                return 0;
            }
            var rentTime = RentTimeInHour(start, end);
            return basicFee + (rentTime * hourlyfee);
        }
    }
}
