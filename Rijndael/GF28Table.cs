﻿using System;

namespace encryption
{
    /// <summary>
    /// A class containing a GF(2^8) multiplication table.
    /// </summary>
    class GF28Table
    {
        /// <summary>
        /// A ushort representation of the irreducible polynomial used in GF(2^8) arithmetic.
        /// </summary>
        public static readonly ushort div = 0x11b;

        /// <summary>
        /// The multiplication table.
        /// </summary>
        private byte[][] multTable; //64 KILObytes of memory stored here. 64 kilobytes is chump change.
        
        /// <summary>
        /// Constructs a new multiplication table.
        /// </summary>
        public GF28Table()
        {
            multTable = new byte[256][]; //could potentially half the table
            for (int ii=0; ii<multTable.Length; ii++)
            {
                multTable[ii] = new byte[multTable.Length];
            }
            for (int ii=0; ii<multTable.Length; ii++)
            {
                for (int jj=0; jj<multTable[ii].Length; jj++)
                {
                    multTable[ii][jj] = multiplication((byte)ii, (byte) jj);
                }
            }
        }

        /// <summary>
        /// Gets a product given two bytes to be multiplied.
        /// </summary>
        /// <param name="a"> One byte being multiplied. </param>
        /// <param name="b"> The other byte being multiplied. </param>
        /// <returns> The product. </returns>
        public byte multiply(byte a, byte b) //this is the public call to the object. Multiplication is
        {                                    //the underlying arithmetic, but it is only used to created
            return multTable[a][b];          //the multiplication table, because it is expensive.
        }

        /// <summary>
        /// The underlying multiplication algorithm.
        /// </summary>
        /// <param name="a1"> One byte being multiplied. </param>
        /// <param name="b2"> The other byte being multiplied. </param>
        /// <returns> THe product. </returns>
        private static byte multiplication(byte a1, byte b2) //this may not be optimized, but only happens once so it's not awful
        {
            ushort r = 0; //result of the multiplication
            ushort a = a1;
            ushort b = b2;
            int number = 0; //number of right shifts I have performed.
            while (b != 0) //multiplication step
            {
                while (b % 2 != 1)//if the LSB is a 0.  
                {
                    b>>=1; //bring the next byte into the LSB.
                    number++; //adds to the right shift count
                }
                b--; //if the LSB is a 1, make it a 0, then do the following.
                r = (ushort)(r ^ (a << number)); //adds(XORs) A shifted left by however much were right-shifting B to the result.
            }
            while (r > 255) //division step, go until the value can fit into a byte.
            {
                ushort d = div; //convert the class constant irreducible polynomial into something modifiable
                int n = 7; //could be 0, but there is no reason to--we know its 256 or more.
                while (r >= Math.Pow(2, (n + 1)))
                {
                    n++; //find the position of the most significant '1' bit.
                }
                d = (ushort)(d << (n - 8)); //shift the division polynomial to the left until the two top bytes align.
                r = (ushort)(d ^ r); //Gets rid of the most significant '1'. Repeat until the result is a byte.
            }
            return (byte)r;
        }
    }
}
