using System;
using System.Text;

namespace SimaticLibrary
{
    public class SimaticString
    {
        private byte[] array;

        private enum Index
        {
            MaxLength,
            Length,
            Offset
        }

        // MAX_LENGTH + PADDING = 2^8
        private const int MAX_LENGTH = 254;
        private const int PADDING = 2;

        public SimaticString(int maxLength = MAX_LENGTH)
        {
            if (maxLength > MAX_LENGTH || maxLength < 0)
            {
                throw new Exception("simatic string length overflow");
            }

            this.array = new byte[maxLength + PADDING];
            this.array[(int)Index.MaxLength] = Convert.ToByte(maxLength);
            this.array[(int)Index.Length] = Convert.ToByte(0);

            this.MaxLength = maxLength;
            this.Length = 0;
        }

        public SimaticString(SimaticString s)
        {
            this.array = new byte[s.MaxLength + PADDING];
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
            this.Set(Encoding.ASCII.GetBytes(EncodeSpecialCharacters(s)));
        }

        public void Set(SimaticString s)
        {
            this.array[(int)Index.Length] = Convert.ToByte(s.Length);
            Array.Copy(s.array, (int)Index.Offset, this.array, (int)Index.Offset, s.Length);

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
            return DecodeSpecialCharacters(Encoding.ASCII.GetString(this.array, (int)Index.Offset, this.Length));
        }

        public byte[] GetBytes()
        {
            return this.array;
        }

        private string EncodeSpecialCharacters(string s)
        {
            // special character handling
            StringBuilder result = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                char ch = s[i];

                if (ch == '$')
                {
                    if (i+1 < s.Length)
                    {
                        char sc = s[i++];
                        if (Char.IsDigit(sc))
                        {
                            // hex special character
                            if (i+1 < s.Length)
                            {
                                string hex = sc.ToString() + s[i++].ToString();
                                
                                switch (hex)
                                {
                                    case "0A":
                                        result.Append('\n');
                                        break;
                                    case "0C":
                                        result.Append('\f');
                                        break;
                                    case "0D":
                                        result.Append('\r');
                                        break;
                                    case "09":
                                        result.Append('\t');
                                        break;
                                    case "24":
                                        result.Append('$');
                                        break;
                                    case "27":
                                        result.Append('\'');
                                        break;
                                    default:
                                        result.Append(hex);
                                        break;
                                }
                            }
                            else
                            {
                                result.Append(sc);
                            }
                        }
                        else
                        {
                            // letter special character
                            switch (sc)
                            {
                                case 'L':
                                case 'l':
                                    result.Append('\n');
                                    break;
                                case 'N':
                                    result.Append("\r\n");
                                    break;
                                case 'P':
                                case 'p':
                                    result.Append('\f');
                                    break;
                                case 'R':
                                case 'r':
                                    result.Append('\r');
                                    break;
                                case 'T':
                                case 't':
                                    result.Append('\t');
                                    break;
                                case '$':
                                    result.Append('$');
                                    break;
                                case '\'':
                                    result.Append('\'');
                                    break;
                                default:
                                    result.Append(sc);
                                    break;
                            }
                        }
                    }
                }
                else
                {
                    result.Append(ch);
                }
            }
            return result.ToString();
        }

        private string DecodeSpecialCharacters(string s)
        {
            // special character handling
            StringBuilder result = new StringBuilder(s.Length);
            foreach (char ch in s)
            {
                switch (ch)
                {
                    case '\n':
                        result.Append("$L");
                        break;
                    case '\f':
                        result.Append("$P");
                        break;
                    case '\r':
                        result.Append("$R");
                        break;
                    case '\t':
                        result.Append("$T");
                        break;
                    case '$':
                        result.Append("$$");
                        break;
                    case '\'':
                        result.Append("$'");
                        break;
                    default:
                        result.Append(ch);
                        break;
                }
            }
            return result.ToString();
        }
    }
}