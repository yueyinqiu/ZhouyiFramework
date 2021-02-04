using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 周易
{
    /// <summary>
    /// Represents a painting made up by the yin and yang lines.
    /// While generally it can stand for a hexagram or a trigram, you can also use <seealso cref="卦画(阴阳[])"/> , <seealso cref="FromByte(byte)"/> and <seealso cref="Parse(string)"/> to make your own painting but a painting including more than 7 lines is not allowed.
    /// When you use this class as <see cref="IEnumerable"/> or <see cref="IEnumerable{T}"/> , you will get the lower lines first.
    /// </summary>
    public sealed class 卦画 : IEnumerable<阴阳>, IComparable<卦画>, IEquatable<卦画>
    {
        private 阴阳[] 各爻阴阳 { get; set; }
        /// <summary>
        /// Get the attribute (yin or yang) of a line in the painting.
        /// </summary>
        /// <param name="index">
        /// The index of the line you want to get.
        /// The higher the value is, the upper the line you will get.
        /// The minimum value is 0.
        /// </param>
        /// <returns>The attribute of the line.</returns>
        /// <exception cref="IndexOutOfRangeException"> <paramref name="index"/> is out of range.</exception>
        public 阴阳 this[int index]
            => this.各爻阴阳[index];
        IEnumerator IEnumerable.GetEnumerator()
            => this.各爻阴阳.GetEnumerator();
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// When you use this enumerator, you will get the lower lines first.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public IEnumerator<阴阳> GetEnumerator()
            => ((IEnumerable<阴阳>)this.各爻阴阳).GetEnumerator();
        /// <summary>
        /// The count of the lines in the painting.
        /// </summary>
        public int 爻数 => this.各爻阴阳.Length;

        private 卦画() { }
        /// <summary>
        /// Initializes a new instance of <seealso cref="卦画"/>.
        /// </summary>
        /// <param name="各爻阴阳"></param>
        public 卦画(params 阴阳[] 各爻阴阳)
        {
            if (各爻阴阳 == null)
                this.各爻阴阳 = new 阴阳[0];
            this.各爻阴阳 = new 阴阳[各爻阴阳.Length];
            各爻阴阳.CopyTo(this.各爻阴阳, 0);
        }
        internal 卦画(bool _, params 阴阳[] 各爻阴阳)
        {
            this.各爻阴阳 = 各爻阴阳;
        }
        internal 卦画(params 爻[] 各爻)
        {
            this.各爻阴阳 =
                (from 爻 in 各爻
                 select 爻.爻题.爻阴阳).ToArray();
        }
        /// <summary>
        /// Convert to a <see cref="byte"/> .
        /// You can convert it back by using <seealso cref="FromByte(byte)"/> .
        /// A painting with more than 6 lines can't be correctly converted, and a <see cref="OverflowException"/> will be thrown.
        /// </summary>
        /// <returns>The byte that can represent this painting.</returns>
        /// <exception cref="OverflowException">There are more than 6 lines.</exception>
        public byte ToByte()
        {
            int result = 1;
            foreach (var 阴阳 in this.各爻阴阳)
            {
                result <<= 1;
                if (阴阳 == 阴阳.阳)
                    result++;
            }
            return checked((byte)result);
        }
        /// <summary>
        /// Convert from a <see cref="byte"/> .
        /// </summary>
        /// <param name="b">The byte that represents this painting.</param>
        /// <returns>The painting.</returns>
        public static 卦画 FromByte(byte b)
        {
            BitArray bitArray = new BitArray(new byte[] { b });
            List<阴阳> r = new List<阴阳>(6);
            bool notStarted = true;
            for (int i = 7; i >= 0; i--)
            {
                var bit = bitArray[i];
                if (notStarted)
                {
                    if (bit)
                        notStarted = false;
                    continue;
                }
                r.Add(bit ? 阴阳.阳 : 阴阳.阴);
            }
            return new 卦画() {
                各爻阴阳 = r.ToArray()
            };
        }
        /// <summary>
        /// Returns a string that represents the painting.
        /// You can use <seealso cref="Parse(string)"/> to convert it back.
        /// </summary>
        /// <returns>The string.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder(6);
            foreach (var 阴阳 in this.各爻阴阳)
                stringBuilder.Append(阴阳 == 阴阳.阳 ? 1 : 0);
            return stringBuilder.ToString();
        }
        /// <summary>
        /// Convert from a <see cref="string"/> .
        /// </summary>
        /// <param name="s">
        /// The string represents the painting.
        /// </param>
        /// <returns>The painting.</returns>
        /// <exception cref="ArgumentNullException"> <paramref name="s"/> is null.</exception>
        /// <exception cref="FormatException"> <paramref name="s"/> is not in the correct format.</exception>
        public static 卦画 Parse(string s)
        {
            if (s == null)
                throw new ArgumentNullException(nameof(s));
            List<阴阳> r = new List<阴阳>(6);
            foreach (var c in s)
            {
                if (c < '0' || c > '9')
                    throw new FormatException($"{nameof(s)}的格式不正确。只允许出现数字字符。");
                r.Add(c % 2 == 0 ? 阴阳.阴 : 阴阳.阳);
            }
            return new 卦画() {
                各爻阴阳 = r.ToArray()
            };
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// It will actually compare their hash codes.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(卦画 other)
        {
            if (other == null)
                return 1;
            return this.GetHashCode().CompareTo(other.GetHashCode());
        }
        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance, or null.</param>
        /// <returns>true if obj is an instance of System.Byte and equals the value of this instance; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is 卦画 画)
                return this.各爻阴阳.SequenceEqual(画.各爻阴阳);
            return false;
        }
        /// <summary>
        /// Returns the hash code for this instance.
        /// It is actually an <see cref="int"/> equivalent of the painting when there are less then 32 lines.
        /// </summary>
        /// <returns>A hash code for the current <see cref="卦画"/> .</returns>
        public override int GetHashCode()
        {
            int result = 1;
            foreach (var 阴阳 in this.各爻阴阳)
            {
                result <<= 1;
                if (阴阳 == 阴阳.阳)
                    result++;
            }
            return result;
        }
        /// <summary>
        /// Returns a value indicating whether this instance and a specified <see cref="卦画"/> object represent the same value.
        /// </summary>
        /// <param name="other">An object to compare to this instance.</param>
        /// <returns>true if obj is equal to this instance; otherwise, false.</returns>
        public bool Equals(卦画 other)
        {
            if (other == null)
                return false;
            return this.各爻阴阳.SequenceEqual(other.各爻阴阳);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator ==(卦画 left, 卦画 right)
        {
            if (left == null)
                return right == null;
            if (right == null)
                return false;
            return left.SequenceEqual(right);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool operator !=(卦画 left, 卦画 right)
        {
            if (left == null)
                return right != null;
            if (right == null)
                return true;
            return !left.SequenceEqual(right);
        }
    }
}
