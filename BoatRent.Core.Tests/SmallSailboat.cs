using System;
using System.Collections.Generic;
using System.Text;

namespace BoatRent.Core.Tests
{
    public class SmallSailboat : Boat
    {
        public SmallSailboat(string boatNumber) : base(boatNumber)
        {
        }

        public override decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee)
        {
            throw new NotImplementedException();
        }
    }
}
