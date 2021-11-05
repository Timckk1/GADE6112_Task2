using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    class Goblin : Enemy
    {
        private int purse;
        public int Purse
        {
            get { return purse; }
            set { purse = value; }
        }
        private Random rnd = new Random();
        public Goblin(int x, int y, int indexArr) : base(10, 1, x, y, 'G',indexArr) 
        {

        }

        public Movement GetRandomMovement()
        {
            int Rmove = rnd.Next(1, 4);
            return (Movement)Rmove;
        }

        public override Movement ReturnMove(Movement move = Movement.IDLE)
        {
            bool validMove = false;
            Movement _movement = GetRandomMovement();

            while (!validMove)
            {
                _movement = GetRandomMovement();
                Type tileType = CharacterVisonArr[(int)_movement].GetType();
                if (tileType != typeof(Obstacle))
                {
                    validMove = true;
                }
            }

            return _movement;
        }

        public override void Pickup(Item item, MapForm mainForm)
        {
            if (item.GetType() == typeof(Gold))
            {
                Purse += item.Amount;
                mainForm.Info("Goblin picked up " + item.Amount + "gold");
            }
        }
    }
}
