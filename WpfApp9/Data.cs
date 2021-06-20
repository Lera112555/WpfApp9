using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp9
{
    class Data
    {
        public enum Object
        {
            Empty,
            Snake,
            Food
        }

        public enum Direction //направление 
        {
            Right,
            Down,
            Left,
            Up
        }

        public struct Point // координаты 
        {
            public int X { get; }
            public int Y { get; }

            public Point(int x, int y)
            {
                X = x;
                Y = y;
            }
        }
    }
}
