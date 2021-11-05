using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    abstract class Character : Tile
    {
        protected Tile[] characterVisonArr;

        protected int hp, maxHp, damage;

        public int range = 1;

        public Tile[] CharacterVisonArr
        {
            get { return characterVisonArr; }
            set { characterVisonArr = value; }
        }

        public int HP
        {
            get { return hp; }
            set { hp = value; }
        }
        public int MaxHP
        {
            get { return maxHp; }
            set { maxHp = value; }
        }
        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }

        public enum Movement
        {
            IDLE,
            UP,
            DOWN,
            LEFT,
            RIGHT
        }

        public Character(int hp, int damage, int maxHp, int x, int y, char symbol) : base(x, y, symbol)
        {
            this.hp = hp;
            this.damage = damage;
            this.maxHp = maxHp;
        }

        public virtual void Attack(Character target)
        {
            target.hp -= damage;
        }

        public bool IsDead()
        {
            if (hp <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private int DistanceTo(Character target)
        {
            return Math.Abs(X - target.X) + Math.Abs(Y - target.Y);
        }

        public virtual bool CheckRange(Character target)
        {
            if (DistanceTo(target) <= range)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Move(Movement move)
        {
            switch (move)
            {
                case Movement.IDLE:
                    break;

                case Movement.DOWN:
                    Y += 1;
                    break;

                case Movement.UP:
                    Y -= 1;
                    break;

                case Movement.LEFT:
                    X -= 1;
                    break;

                case Movement.RIGHT:
                    X += 1;
                    break;

            }
        }

        public abstract Movement ReturnMove(Movement move = 0);
        public abstract override string ToString();
        public abstract void Pickup(Item item, MapForm mainForm);

    }
}
