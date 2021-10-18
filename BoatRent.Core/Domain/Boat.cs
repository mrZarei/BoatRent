using System;
using System.Linq;
using System.Reflection;

namespace BoatRent.Core.Domain
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

        protected int RentTimeInHour(DateTime startDate, DateTime endDate)
        {
            var rentTimeInMinute = (endDate - startDate).TotalSeconds;
            var rentTimeInHour = rentTimeInMinute / (3600);
            return (int)Math.Ceiling(rentTimeInHour);
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
            // Create instances of classes which has implemented the Boat abstract class and Class name contains the boat type
            var types = Assembly.GetAssembly(typeof(Boat))
                    .GetTypes()
                    .Where(t => typeof(Boat).IsAssignableFrom(t));
            var instanceType = types.Where(x => x.Name.ToLowerInvariant().Contains(boatType.ToString().ToLowerInvariant())).FirstOrDefault();
            var instance = (Boat)Activator.CreateInstance(instanceType, boatNumber);
            return instance;
        }
    }
}
