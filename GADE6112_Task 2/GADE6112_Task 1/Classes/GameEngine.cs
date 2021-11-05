using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace GADE6112_Task_1.Classes // Task 2
{
    class GameEngine
    {
        private Map map;
        private MapForm mainForm;
        public Map CreateMap
        {
            get { return map; }
            set { map = value; }
        }
        public MapForm MainForm
        {
            get { return mainForm; }
            set { mainForm = value; }
        }

        public GameEngine (MapForm form, int minWidth, int maxWidth, int minHeight, int maxHeight, int enemyNum, int itemAmount)
        {
            mainForm = form;
            CreateMap = new Map(minWidth, maxWidth, minHeight, maxHeight, enemyNum, itemAmount);
        }

        private void DeleteEnemyFromEnemyArray(Enemy enemy)
        {
            Enemy[] enemies = new Enemy[CreateMap.EnemyArr.Length - 1];
            int i = 0;
            foreach (Enemy enm in CreateMap.EnemyArr)
            {
                if (enm.IndexArr != enemy.IndexArr)
                {
                    enemies[i++] = enm;
                }
            }
            CreateMap.EnemyArr = enemies;
        }

        public bool MovePlayer(Character.Movement direction)
        {
            int x = CreateMap.Hero.X;
            int y = CreateMap.Hero.Y;
            CreateMap.MapArr[x, y] = new EmptyTile(x, y);

            direction = CreateMap.Hero.ReturnMove(direction);

            switch(direction)
            {
                case Character.Movement.IDLE:
                    break;
                case Character.Movement.UP:
                    if (CreateMap.Hero.CharacterVisonArr[(int)Hero.Movement.UP].GetType() != typeof(EmptyTile)) 
                        break;
                    if (CreateMap.MapArr[x, y - 1].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)CreateMap.MapArr[x, y - 1];
                        CreateMap.Hero.Pickup((Item)CreateMap.MapArr[x, y - 1], mainForm);
                        CreateMap.DeleteItemFromItemArray(item);
                    }
                    CreateMap.Hero.Button.Location = new System.Drawing.Point(x * 20, (y - 1) * 20);
                    CreateMap.Hero.X = x;
                    CreateMap.Hero.Y = y - 1;
                    CreateMap.MapArr[x, y - 1] = CreateMap.Hero;
                    break;
                case Character.Movement.DOWN:
                    if (CreateMap.Hero.CharacterVisonArr[(int)Hero.Movement.DOWN].GetType() != typeof(EmptyTile)) 
                        break;
                    if (CreateMap.MapArr[x, y + 1].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)CreateMap.MapArr[x, y + 1];
                        CreateMap.Hero.Pickup((Item)CreateMap.MapArr[x, y + 1], mainForm);
                        CreateMap.DeleteItemFromItemArray(item);
                    }
                    CreateMap.Hero.Button.Location = new System.Drawing.Point(x * 20, (y + 1) * 20);
                    CreateMap.Hero.X = x;
                    CreateMap.Hero.Y = y + 1;
                    CreateMap.MapArr[x, y + 1] = CreateMap.Hero;
                    break;
                case Character.Movement.RIGHT:
                    if (CreateMap.Hero.CharacterVisonArr[(int)Hero.Movement.RIGHT].GetType() != typeof(EmptyTile)) 
                        break;
                    if (CreateMap.MapArr[x + 1, y].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)CreateMap.MapArr[x + 1, y];
                        CreateMap.Hero.Pickup((Item)CreateMap.MapArr[x + 1, y], mainForm);
                        CreateMap.DeleteItemFromItemArray(item);
                    }
                    CreateMap.Hero.Button.Location = new System.Drawing.Point((x + 1) * 20, y * 20);
                    CreateMap.Hero.X = x + 1;
                    CreateMap.Hero.Y = y;
                    CreateMap.MapArr[x + 1, y] = CreateMap.Hero;
                    break;
                case Character.Movement.LEFT:
                    if (CreateMap.Hero.CharacterVisonArr[(int)Hero.Movement.LEFT].GetType() != typeof(EmptyTile)) 
                        break;
                    if (CreateMap.MapArr[x - 1, y].GetType().BaseType == typeof(Item))
                    {
                        Item item = (Item)CreateMap.MapArr[x - 1, y];
                        CreateMap.Hero.Pickup((Item)CreateMap.MapArr[x - 1, y], mainForm);
                        CreateMap.DeleteItemFromItemArray(item);
                    }
                    CreateMap.Hero.Button.Location = new System.Drawing.Point((x - 1) * 20, y * 20);
                    CreateMap.Hero.X = x - 1;
                    CreateMap.Hero.Y = y;
                    CreateMap.MapArr[x - 1, y] = CreateMap.Hero;
                    break;
                default:
                    break;
            }
            CreateMap.UpdateVison();
            CreateMap.EnemyMove();
            CreateMap.UpdateVison();
            MageAttacks();
            CreateMap.UpdateVison();
            mainForm.Refresh();

            return false;
        }//MovePLayter

        public void Attack (Enemy enemy)
        {
            mainForm.Info("Hero attacks " + enemy.GetType().Name + " [" + enemy.X + "," + enemy.Y + "]");

            enemy.HP -= CreateMap.Hero.Damage;

            mainForm.Info("Enemy: " + enemy.HP + "/" + enemy.MaxHP + " [" + enemy.X + "," + enemy.Y + "]");

            if (enemy.HP < 1)
            {
                mainForm.Info(enemy.GetType().Name + " has died");
                CreateMap.MapArr[enemy.X, enemy.Y] = new EmptyTile(enemy.X, enemy.Y);
                DeleteEnemyFromEnemyArray(enemy);
                mainForm.DeleteTile(enemy);
            }
        }
        internal void AttackBack(Enemy enemy)
        {
            mainForm.Info(enemy.GetType().Name + " attacks Hero");
            CreateMap.Hero.HP -= enemy.Damage;
            mainForm.Info("Hero: " + CreateMap.Hero.HP);
            if (CreateMap.Hero.HP < 1)
            {
                mainForm.Info("Your hero has died");
                mainForm.EndGame();
            }
        }
        private void MageAttacks()
        {
            foreach (Enemy enemy in CreateMap.EnemyArr)
            {
                if (enemy.GetType() == typeof(Mage))
                {
                    MageAttack(enemy);
                }
            }
        }
        private void MageAttack(Enemy mage)
        {
            foreach (Enemy goblin in CreateMap.EnemyArr)
            {
                if (goblin.GetType() == typeof(Goblin))
                {
                    if (mage.CheckRange(goblin))
                    {
                        // Attacks goblin, return true if goblin died
                        if (mage.Attack(goblin, mainForm))
                        {
                            CreateMap.MapArr[goblin.X, goblin.Y] = new EmptyTile(goblin.X, goblin.Y);
                            DeleteEnemyFromEnemyArray(goblin);
                            mainForm.DeleteTile(goblin);
                        }
                    }
                }
            }
        }// MageAttack
        public bool Save()
        {
            BinaryWriter write;

            try
            {
                write = new BinaryWriter(new FileStream("GameFile.txt", FileMode.Create));

                write.Write(CreateMap.XWidth);
                write.Write(CreateMap.Yheight);

                for (int i = 0; i < CreateMap.XWidth; i++)
                {
                    for (int j = 0; j < CreateMap.Yheight; j++)
                    {
                        Tile t = CreateMap.MapArr[i, j];
                        write.Write(t.GetType().Name);
                        write.Write(t.X);
                        write.Write(t.Y);

                        switch (t.GetType().Name)
                        {
                            case "Wall": 
                                break;
                            case "EmptyTile": 
                                break;
                            case "Hero":
                                write.Write(((Hero)t).HP);
                                write.Write(((Hero)t).MaxHP);
                                write.Write(((Hero)t).Damage);
                                write.Write(((Hero)t).Purse);
                                break;
                            case "Goblin":
                                write.Write(((Goblin)t).HP);
                                write.Write(((Goblin)t).MaxHP);
                                write.Write(((Goblin)t).Damage);
                                write.Write(((Goblin)t).Purse);
                                write.Write(((Goblin)t).IndexArr);
                                break;
                            case "Mage":
                                write.Write(((Mage)t).HP);
                                write.Write(((Mage)t).MaxHP);
                                write.Write(((Mage)t).Damage);
                                write.Write(((Mage)t).Purse);
                                write.Write(((Mage)t).IndexArr);
                                break;
                            case "Gold":
                                write.Write(((Gold)t).Amount);
                                write.Write(((Gold)t).IndexArr);
                                break;
                        }
                    }
                }
                write.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed creating file for saving. Exception = " + ex.Message);
                return false;
            }

            return true;
        }// save
        public bool Load()
        {
            try
            {
                BinaryReader read;
                read = new BinaryReader(new FileStream("GameFile.txt", FileMode.Open));

                int xWidth = read.ReadInt32();
                int yHeight = read.ReadInt32();

                Map m = new Map(xWidth, yHeight);

                List<Enemy> enemyList = new List<Enemy>();
                List<Item> itemList = new List<Item>();

                for (int i = 0; i < xWidth; i++)
                {
                    for (int j = 0; j < yHeight; j++)
                    {
                        string tileType = read.ReadString();
                        int x = read.ReadInt32();
                        int y = read.ReadInt32();

                        switch (tileType)
                        {
                            case "Wall":
                                Wall wall = new Wall(x, y);
                                m.MapArr[i, j] = wall;
                                break;
                            case "EmptyTile":
                                EmptyTile e = new EmptyTile(x, y);
                                m.MapArr[i, j] = e;
                                break;
                            case "Hero":
                                int hpH = read.ReadInt32();
                                int maxHpH = read.ReadInt32();
                                int damageH = read.ReadInt32();
                                int purseH = read.ReadInt32();
                                Hero hero = new Hero(x, y, hpH);
                                hero.Damage = damageH;
                                hero.Purse = purseH;
                                m.MapArr[i, j] = hero;
                                m.Hero = hero;
                                break;
                            case "Goblin":
                                int hpG = read.ReadInt32();
                                int maxHpG = read.ReadInt32();
                                int damageG = read.ReadInt32();
                                int purseG = read.ReadInt32();
                                int arrayIndexG = read.ReadInt32();
                                Goblin gob = new Goblin(x, y, arrayIndexG);
                                gob.Damage = damageG;
                                gob.Purse = purseG;
                                m.MapArr[i, j] = gob;
                                enemyList.Add(gob);
                                break;
                            case "Mage":
                                int hpM = read.ReadInt32();
                                int maxHpM = read.ReadInt32();
                                int damageM = read.ReadInt32();
                                int purseM = read.ReadInt32();
                                int arrayIndexM = read.ReadInt32();
                                Mage mage = new Mage(x, y, arrayIndexM);
                                mage.Damage = damageM;
                                mage.Purse = purseM;
                                m.MapArr[i, j] = mage;
                                enemyList.Add(mage);
                                break;
                            case "Gold":
                                int quantity = read.ReadInt32();
                                int arrayIndexGld = read.ReadInt32();
                                Gold gld = new Gold(x, y, quantity, arrayIndexGld);
                                m.MapArr[i, j] = gld;
                                itemList.Add(gld);
                                break;
                        }
                    }
                }
                m.EnemyArr = enemyList.ToArray();
                m.ItemArr = itemList.ToArray();
                map = m;
                read.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed loading game. Exception = " + ex.Message);
            }
            return true;
        }//load
    }// class
}// Namespace
