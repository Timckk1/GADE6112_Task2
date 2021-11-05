using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    class Mage : Enemy
    {
        private int purse;
        public int Purse
        {
            get { return purse; }
            set { purse = value; }
        }

        public Mage(int x, int y, int indexArr) : base(5, 5, x, y, 'M', indexArr)
        {
            this.hp = 5;
            this.damage = 5;
        }

        public override void Pickup(Item item, MapForm mainForm)
        {
            
        }

        public override Movement ReturnMove(Movement move = 0)
        {
            return move;
        }
        public override bool CheckRange(Character target)
        {
            if (target.X == X + 1 && target.Y == Y)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y)
            {
                return true;
            }
            else if (target.X == X && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X == X && target.Y == Y + 1)
            {
                return true;
            }
            else if (target.X == X + 1 && target.Y == Y + 1)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X == X + 1 && target.Y == Y - 1)
            {
                return true;
            }
            else if (target.X == X - 1 && target.Y == Y + 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
