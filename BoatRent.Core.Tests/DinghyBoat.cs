using System;
using System.Collections.Generic;
using System.Text;

namespace BoatRent.Core.Tests
{
    public class DinghyBoat : Boat
    {
        public DinghyBoat(string boatNumber) : base(boatNumber)
        {
        }

        public override decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee)
        {
            throw new NotImplementedException();
        }
    }
}
