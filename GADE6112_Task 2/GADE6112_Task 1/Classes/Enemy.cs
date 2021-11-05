using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    abstract class Enemy : Character
    {
        protected Random rnd = new Random();

        public int IndexArr 
        { 
            get; 
            internal set; 
        }

        public Enemy(int hp, int damage, int x, int y, char symbol, int indexArr) : base(hp, damage, hp, x, y, symbol)
        {
            IndexArr = indexArr;
        }

        public override string ToString()
        {
            return "at [" + X + "," + Y + "] HP: " + HP + "/" + maxHp + " (" + damage + ")";
        }

        internal bool Attack(Enemy goblin, MapForm mainForm)
        {
            mainForm.Info("Mage attacks goblin");

            goblin.HP -= this.damage;

            mainForm.Info(goblin.ToString());

            if (goblin.HP < 1)
            {
                mainForm.Info("Goblin has died");
                return true;
            }

            return false;
        }
    }
}
