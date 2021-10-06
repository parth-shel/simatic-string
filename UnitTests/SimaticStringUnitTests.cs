using System;
using Xunit;

using SimaticLibrary;

namespace UnitTests
{
    public class SimaticStringTests
    {
        [Fact]
        public void SimpleTest()
        {
            SimaticString ss = new SimaticString();

            Xunit.assert(ss.Length == 0);
            Xunit.assert(ss.MaxLength == 254);
        }
    }
}
