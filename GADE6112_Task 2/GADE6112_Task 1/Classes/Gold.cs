using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    class Gold : Item
    {
        protected int goldAmount;
        protected int GoldAmount
        {
            get { return goldAmount; }
            set { goldAmount = value; }
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }

        public Gold(int x, int y, int amount, int indexArr) : base(x, y, '$', indexArr)
        {
            Random r = new Random();
            goldAmount = r.Next(1, 6);
            Amount = goldAmount;
        }
    }
}
