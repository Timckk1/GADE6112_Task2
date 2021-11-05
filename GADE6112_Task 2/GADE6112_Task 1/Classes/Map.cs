using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GADE6112_Task_1.Classes // Task 2
{
    class Map
    {
        private int xWidth, yHeight;
        private Tile[,] mapArr;
        private Enemy[] enemyArr;
        Random rnd = new Random();
        private Hero hero;
        private Item[] itemArr;

        public int XWidth
        {
            get { return xWidth; }
            set { xWidth = value; }
        }
        public int Yheight
        {
            get { return yHeight; }
            set { yHeight = value; }
        }

        public Tile[,] MapArr
        {
            get { return mapArr; }
            set { mapArr = value; }
        }
        public Enemy[] EnemyArr
        {
            get { return enemyArr; }
            set { enemyArr = value; }
        }
        public Item[] ItemArr
        {
            get { return itemArr; }
            set { itemArr = value; }
        }

        public Hero Hero
        {
            get { return hero; }
            set { hero = value; }
        }
        public Map(int xWidth, int yHeight)
        {
            XWidth = xWidth;
            Yheight = yHeight;

            MapArr = new Tile[xWidth, yHeight];
        }
        public Map(int minWidth, int maxWidth, int minHeight, int maxHeight, int numEnemies, int itemAmount)
        {
            XWidth = rnd.Next(minWidth, maxWidth);
            Yheight = rnd.Next(minHeight, maxHeight);

            mapArr = new Tile[XWidth, Yheight];
            enemyArr = new Enemy[numEnemies];
            itemArr = new Item[itemAmount];

            for (int i = 0; i < XWidth; i++)//Empty Tiles
            {
                for (int j = 0; j < Yheight; j++)
                {
                    mapArr[i, j] = new EmptyTile(i, j);
                }
            }

            for (int i = 0; i < XWidth; i++) //Obstacles around map/ top and bottom
            {
                Tile topRow = new Obstacle(i, 0);
                mapArr[0, i] = topRow;
                Tile bottomRow = new Obstacle(i, Yheight);
                mapArr[i, Yheight] = bottomRow;
            }

            for (int i = 0; i < Yheight; i++) //Obstacles around map/ Left and Right
            {
                Tile leftCol = new Obstacle(0, i);
                mapArr[0, i] = leftCol;
                Tile rightCol = new Obstacle(XWidth, i);
                MapArr[XWidth, i] = rightCol;
            }

            Hero = (Hero)Create(Tile.TileType.HERO, -1);
            mapArr[Hero.X, Hero.Y] = Hero;

            for (int i = 0; i < numEnemies; i++)
            {
                enemyArr[i] = (Enemy)Create(Tile.TileType.ENEMY, i);

                mapArr[enemyArr[i].X, enemyArr[i].Y] = enemyArr[i];

                int rSpawn = rnd.Next(1,3);
                int x = rnd.Next(1, xWidth - 1);
                int y = rnd.Next(1, yHeight - 1);
            }

            for (int i = 0; i < itemAmount; i++)
            {
                Gold gold = (Gold)Create(Tile.TileType.GOLD, i);
                itemArr[i] = gold;
                mapArr[gold.X, gold.Y] = gold;
            }

            UpdateVison();
        }
       
        public void UpdateVison()
        {
            Hero.CharacterVisonArr = new Tile[5];

            Hero.CharacterVisonArr[(int)Hero.Movement.IDLE] = null;
            Hero.CharacterVisonArr[(int)Hero.Movement.UP] = mapArr[Hero.X, Hero.Y -1];
            Hero.CharacterVisonArr[(int)Hero.Movement.DOWN] = mapArr[Hero.X, Hero.Y + 1];
            Hero.CharacterVisonArr[(int)Hero.Movement.LEFT] = mapArr[Hero.X -1, Hero.Y];
            Hero.CharacterVisonArr[(int)Hero.Movement.RIGHT] = mapArr[Hero.X +1, Hero.Y];

            for (int i = 0; i < enemyArr.Length; i++)
            {
                enemyArr[i].CharacterVisonArr = new Tile[5];

                enemyArr[i].CharacterVisonArr[(int)Hero.Movement.IDLE] = null;
                enemyArr[i].CharacterVisonArr[(int)Hero.Movement.UP] = mapArr[enemyArr[i].X, enemyArr[i].Y - 1];
                enemyArr[i].CharacterVisonArr[(int)Hero.Movement.DOWN] = mapArr[enemyArr[i].X, enemyArr[i].Y + 1];
                enemyArr[i].CharacterVisonArr[(int)Hero.Movement.LEFT] = mapArr[enemyArr[i].X - 1, enemyArr[i].Y];
                enemyArr[i].CharacterVisonArr[(int)Hero.Movement.RIGHT] = mapArr[enemyArr[i].X + 1, enemyArr[i].Y];
            }
        }
        
        private Tile Create(Tile.TileType type, int Index)
        {
            Random rndNum = new Random();


            if (Hero == null)
            {
                Hero = new Hero(0, 0, 10);//placeholder
            }

            int x = 0, y = 0;
            do
            {
                x = rndNum.Next(1, XWidth - 1);
                y = rndNum.Next(1, Yheight - 1);
            } while (MapContainsCharacterAt(x,y));

            if (type == Tile.TileType.HERO)
            {
                return new Hero(x, y, 10);
            }
            else if (type == Tile.TileType.ENEMY)
            {
                int eneRnd = rndNum.Next(1, 7);

                if (eneRnd == 1)
                {
                    return new Mage(x, y, Index);
                }
                else
                {
                    return new Goblin(x, y, Index);
                }
            }
            else if (type == Tile.TileType.GOLD)
            {
                return new Gold(x, y, rndNum.Next(1, 10), Index);
            }
            return null;
        }

        bool MapContainsCharacterAt(int x, int y)
        {
            if (Hero.X == x && Hero.Y == y)
            {
                return true;
            }

            for (int i = 0; i < enemyArr.Length; i++)
            {
                Tile Etile = enemyArr[i];
                if (Etile == null)
                {
                    return false;
                }
                if (Etile.X == x && Etile.Y == y)
                {
                    return true;
                }
            }

            return false;
        }
        internal void EnemyMove()
        {
            Random r = new Random();
            for (int i = 0; i < EnemyArr.Length; i++)
            {
                if (EnemyArr[i].GetType() == typeof(Mage)) 
                { 
                    continue; 
                }

                Character.Movement direction = (Character.Movement)r.Next(0, 5);
                if (direction != Character.Movement.IDLE)
                {
                    Enemy enemy = EnemyArr[i];
                    MapArr[enemy.X, enemy.Y] = new EmptyTile(enemy.X, enemy.Y);
                    switch (direction)
                    {
                        case Character.Movement.UP:
                            if (enemy.Y > 1 && enemy.CharacterVisonArr[(int)Character.Movement.UP].GetType() != typeof(Hero))
                            {
                                enemy.Y--;
                            }
                            break;
                        case Character.Movement.DOWN:
                            if (enemy.Y < (Yheight - 2) && enemy.CharacterVisonArr[(int)Character.Movement.DOWN].GetType() != typeof(Hero))
                            {
                                enemy.Y++;
                            }
                            break;
                        case Character.Movement.LEFT:
                            if (enemy.X > 2 && enemy.CharacterVisonArr[(int)Character.Movement.LEFT].GetType() != typeof(Hero))
                            {
                                enemy.X--;
                            }
                            break;
                        case Character.Movement.RIGHT:
                            if (enemy.X < (XWidth - 2) && enemy.CharacterVisonArr[(int)Character.Movement.RIGHT].GetType() != typeof(Hero))
                            {
                                enemy.X++;
                            }
                            break;
                    }
                    enemy.Button.Location = new System.Drawing.Point(enemy.X * 20, enemy.Y * 20);
                    MapArr[enemy.X, enemy.Y] = enemy;
                }
            }
        }

        public void DeleteItemFromItemArray(Item item)
        {
            Item[] items = new Item[ItemArr.Length - 1];
            int i = 0;
            foreach (Item itm in ItemArr)
            {
                if (itm.IndexArr != item.IndexArr)
                {
                    items[i++] = itm;
                }
            }
            ItemArr = items;
        }

        public Item GetItemAtPosition(int x, int y)
        {
            foreach (Item item in ItemArr)
            {
                if (item.X == x && item.Y == y)
                {
                    DeleteItemFromItemArray(item);
                    return item;
                }
            }

            return null;
        }
    }//Class
}//NS
