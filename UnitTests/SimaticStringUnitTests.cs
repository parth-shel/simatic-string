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

            Assert.Equal(0, ss.Length);
            Assert.Equal(254, ss.MaxLength);

            SimaticString sn = new SimaticString(10);

            Assert.Equal(0, sn.Length);
            Assert.Equal(10, sn.MaxLength);
        }

        [Fact]
        public void ConstructAndSetTest()
        {
            SimaticString ss = new SimaticString(5);

            Assert.Equal(5, ss.MaxLength);
            Assert.Equal(0, ss.Length);

            ss.Set("Hello");

            Assert.Equal(5, ss.Length);
            Assert.Equal(5, ss.MaxLength);

            byte[] arr = ss.GetBytes();
            Assert.Equal(Convert.ToByte(5) ,arr[0]);
            Assert.Equal(Convert.ToByte(5), arr[1]);
            Assert.Equal(Convert.ToByte('H'), arr[2]);
            Assert.Equal(Convert.ToByte('e'), arr[3]);
            Assert.Equal(Convert.ToByte('l'), arr[4]);
            Assert.Equal(Convert.ToByte('l'), arr[5]);
            Assert.Equal(Convert.ToByte('o'), arr[6]);
            Assert.Equal("Hello", ss.ToString());

            SimaticString sn = new SimaticString(ss);

            Assert.Equal(5, sn.MaxLength);
            Assert.Equal(5, sn.Length);

            byte[] byteArray = {Convert.ToByte('W'), Convert.ToByte('o'), Convert.ToByte('r'), Convert.ToByte('l'), Convert.ToByte('d')};
            sn.Set(byteArray);

            Assert.Equal(5, sn.MaxLength);
            Assert.Equal(5, sn.Length);

            arr = sn.GetBytes();
            Assert.Equal(Convert.ToByte(5) ,arr[0]);
            Assert.Equal(Convert.ToByte(5), arr[1]);
            Assert.Equal(Convert.ToByte('W'), arr[2]);
            Assert.Equal(Convert.ToByte('o'), arr[3]);
            Assert.Equal(Convert.ToByte('r'), arr[4]);
            Assert.Equal(Convert.ToByte('l'), arr[5]);
            Assert.Equal(Convert.ToByte('d'), arr[6]);

            sn.Set(ss);

            Assert.Equal(5, sn.MaxLength);
            Assert.Equal(5, sn.Length);

            arr = sn.GetBytes();
            Assert.Equal(Convert.ToByte(5) ,arr[0]);
            Assert.Equal(Convert.ToByte(5), arr[1]);
            Assert.Equal(Convert.ToByte('H'), arr[2]);
            Assert.Equal(Convert.ToByte('e'), arr[3]);
            Assert.Equal(Convert.ToByte('l'), arr[4]);
            Assert.Equal(Convert.ToByte('l'), arr[5]);
            Assert.Equal(Convert.ToByte('o'), arr[6]);
        }

        [Fact]
        public void ReverseTest()
        {
            SimaticString ss = new SimaticString();
            ss.Set("Siemens");

            Assert.Equal(9, ss.Length);

            byte[] arr = ss.GetBytes();
            Assert.Equal(Convert.ToByte(254) ,arr[0]);
            Assert.Equal(Convert.ToByte(9), arr[1]);

            Assert.Equal("Siemens", ss.ToString());
            ss.Reverse();

            arr = ss.GetBytes();
            Assert.Equal(Convert.ToByte(254) ,arr[0]);
            Assert.Equal(Convert.ToByte(9), arr[1]);

            Assert.Equal(9, ss.Length);

            Assert.Equal("snemeiS", ss.ToString());
        }

        [Fact]
        public void SpecialCharactersTest()
        {
            SimaticString ss = new SimaticString();
            
            ss.Set("$N");
            Assert.Equal(2, ss.Length);
            byte[] arr = ss.GetBytes();
            Assert.Equal(Convert.ToByte('\r'), arr[2]);
            Assert.Equal(Convert.ToByte('\n'), arr[3]);
            Assert.Equal("$R$L", ss.ToString());
        }
    }
}
