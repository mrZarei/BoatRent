using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoatRent.Core.Tests
{
    internal class MockedRepository : IBoatRentalRepository
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
