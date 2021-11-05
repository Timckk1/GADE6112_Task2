using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    class Hero : Character
    {
        private int purse;
        public int Purse
        {
            get { return purse; }
            set { purse = value; }
        }
        public Hero(int x, int y, int hp) : base(hp, 2, 10, x, y, 'H')
        {

        }

        public override Movement ReturnMove(Movement move = Movement.IDLE)
        {
            if (move == Movement.IDLE) 
            { 
                return move; 
            }

            Type visionType = CharacterVisonArr[(int)move].GetType();

            if (visionType == typeof(EmptyTile) || visionType.BaseType == typeof(Item))
            {
                return move;
            }
            else
            {
                return Movement.IDLE;
            }
        }

        public override void Pickup(Item item, MapForm mainForm)
        {
            if (item.GetType() == typeof(Gold))
            {
                Purse += item.Amount;
                mainForm.Info("Hero picked up " + item.Amount + " gold");
                mainForm.Controls.Remove(item.Button);
            }
        }

        public override string ToString()
        {
            return "Player Stats: " + Environment.NewLine + 
                   "HP: " + HP + "/" + MaxHP + Environment.NewLine + 
                   "Damage: " + Damage + Environment.NewLine + 
                   "[" + X + "," + Y + "]";
        }
    }
}
