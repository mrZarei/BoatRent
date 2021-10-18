using System;
using System.Collections.Generic;
using System.Text;

namespace BoatRent.Core.Tests
{
    public abstract class Boat
    {
        private string _boatNumber;
        public string BoatNumber { get { return _boatNumber; } }
        public BoatType Type { get; }

        public Boat(string boatNumber)
        {
            _boatNumber = boatNumber;
        }


        public abstract decimal CalculatePrice(DateTime start, DateTime end, decimal hourlyfee, decimal basicFee);

        public enum BoatType
        {
            DinghyBoat,
            SmallSailboat,
            LargeSailboat,
        }

        public static Boat Build(BoatType boatType, string boatNumber)
        {
            throw new NotImplementedException();
        }
    }
}
