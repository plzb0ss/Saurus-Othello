﻿using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace SaurusConsole.OthelloAI
{
    /// <summary>
    /// Represents an Othello Move
    /// </summary>
    public class Move
    {
        ulong move;

        /// <summary>
        /// Initializes an instance 
        /// </summary>
        /// <param name="move"></param>
        public Move(ulong move)
        {
            // Input not validated to prevent performance hit
            //if ((move == 0) || ((move & (move - 1)) != 0))
            //{
            //    throw new ArgumentException();
            //}
            this.move = move;
            move = 0;
        }
        public Move(string notation)
        {
            // input not validated to prevent performance hit
            int col = Convert.ToInt32(notation[0] - 65);
            int row = Convert.ToInt32(notation[1] - 49);
            this.move = (ulong)1 << (row * 8 - col + 7);
        }

        /// <summary>
        /// Gets the move in co-ordinate notation Example: A5
        /// </summary>
        /// <returns>The move</returns>
        override public string ToString()
        {
            int x;
            int y;
            ulong rowMask = 0xff;
            ulong colMask = 0x8080808080808080;
            for (y = 0; y < 8; y++)
            {
                if ((move & rowMask) != 0)
                {
                    break;
                }
                rowMask <<= 8;
            }
            for (x = 0; x < 8; x++)
            {
                if ((move & colMask) != 0)
                {
                    break;
                }
                colMask >>= 1;
            }
            string col = ((char)(x + 65)).ToString();
            int row = y + 1;
            return $"{col}{row}";
        }

        /// <summary>
        /// Gets the bitmask that represents the move on the othello board
        /// </summary>
        /// <returns>A ulong with 1 set bit denoting the square on the board</returns>
        public ulong GetBitMask()
        {
            return move;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null) || !(obj is Move))
            {
                return false;
            }

            Move pos = (Move)obj;
            return move == pos.move;
        }

        public override int GetHashCode()
        {
            return move.GetHashCode();
        }

        public static bool operator ==(Move left, Move right)
        {
            if (!ReferenceEquals(left, null) && !ReferenceEquals(right, null))
            {
                return left.Equals(right);
            }
            return ReferenceEquals(left, null) && ReferenceEquals(right, null);
        }

        public static bool operator !=(Move left, Move right)
        {
            return !(left == right);
        }
    }
}
