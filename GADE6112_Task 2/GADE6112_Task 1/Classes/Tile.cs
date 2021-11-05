using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace GADE6112_Task_1.Classes // Task 2
{

    public abstract class Tile
    {
        public enum TileType
        {
            HERO,
            ENEMY,
            GOLD,
            WEAPON

        }

        protected int x;
        protected int y;
        protected char symbol;
        public Button button;
        public int X
        {
            get { return x; }
            set { x = value; }

        }
        public int Y
        {
            get { return y; }
            set { y = value; }

        }
        public char Symbol
        {
            get { return symbol; }
            set { symbol = value; }

        }

        public Tile(int x, int y, char sym)
        {
            X = x;
            Y = y;
            symbol = sym;
        }
        public Button Button
        {
            get { return button; }
            set { button = value; }

        }
    }
}//NameSpace
