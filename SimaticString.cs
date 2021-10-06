using System;
using System.Text;

namespace SimaticLibrary
{
    public class SimaticString
    {
        public SimaticString(int maxLength = 254)
        {
            if (maxLength > 254)
            {
                throw new Exception("simatic string length overflow");
            }

            this.array = new byte[maxLength + 2];
            this.array[(int)Index.MaxLength] = Convert.ToByte(maxLength);
            this.array[(int)Index.Length] = Convert.ToByte(0);

            this.MaxLength = maxLength;
            this.Length = 0;
        }

        public SimaticString(SimaticString s)
        {
            this.array = new byte[s.MaxLength + 2];
            s.array.CopyTo(this.array, 0);

            this.MaxLength = s.MaxLength;
            this.Length = s.Length;
        }

        public void Set(byte[] s)
        {
            if (s.Length > this.MaxLength)
            {
                throw new Exception("simatic string length overflow");
            }

            this.array[(int)Index.Length] = Convert.ToByte(s.Length);
            s.CopyTo(this.array, (int)Index.Offset);            

            this.Length = s.Length;
        }

        public void Set(string s)
        {
            this.Set(Encoding.ASCII.GetBytes(s));
        }

        public void Set(SimaticString s)
        {
            this.array[(int)Index.Length] = Convert.ToByte(s.Length);
            s.array.CopyTo(this.array, (int)Index.Offset);

            this.Length = s.Length;
        }

        public int Length { get; private set; }
        public int MaxLength { get; private set; }

        public void Reverse()
        {
            Array.Reverse(this.array, (int)Index.Offset, this.Length);
        }

        public override string ToString()
        {
            return Encoding.ASCII.GetString(this.array, (int)Index.Offset, this.Length);
        }

        public byte[] GetBytes()
        {
            return this.array;
        }

        private byte[] array;

        private enum Index
        {
            MaxLength,
            Length,
            Offset
        }
    }
}