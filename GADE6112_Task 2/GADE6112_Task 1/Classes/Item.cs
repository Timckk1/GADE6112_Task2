using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    public abstract class Item : Tile
    {
        private int amount;
        private int indexArr;

        public int Amount
        {
            get { return amount; }
            set { amount = value; }
        }
        public int IndexArr
        {
            get { return indexArr; }
            set { indexArr = value; }
        }

        public Item(int x, int y, char symbol, int indexArr) : base(x, y, symbol)
        {
            IndexArr = indexArr;
        }

        public abstract override string ToString();
    }
}
