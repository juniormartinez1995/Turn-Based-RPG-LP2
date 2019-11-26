using RPGlib.Itens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RPGlib.Mobs
{
    abstract public class Mob
    {
        public IFightStrategy CurrentStrategy { get; set; }
        public String name { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int currentArmor { get; set; }
        public int evasionRate { get; set; }
        public int Damage { get; set; }
        public BitmapImage GifBattle { get; set; }
        public BitmapImage castBattle { get; set; }
        public BitmapImage RightMoviment { get; set; }
        public BitmapImage LeftMoviment { get; set; }
        public BitmapImage UpMoviment { get; set; }
        public BitmapImage DownMoviment { get; set; }

        public delegate void MobDeadEventHandler(object sender, EventArgs args);
        public event MobDeadEventHandler MobDead;

        protected virtual void OnMobDead()
        {
            MobDead?.Invoke(this, EventArgs.Empty);
        }

        public bool IsDead()
        {
            if (this.HP <= 0)
            {

                OnMobDead();
                return true;
            }
            else return false;
        }

        public int Skills()
        {

            return Damage; // NENHUMA CHANCE DE TER DANO DOBRADO

        }

        public int Skill2()
        {

            return RandomElement.Limiter(0, 5) > 3 ? Damage : 2 * Damage; // MEDIA CHANCE DE TER DANO DOBRADO
        }

        public int Skill3()
        {

            return RandomElement.Limiter(0, 10) > 8 ? Damage : 3 * Damage; // GRANDE CHANCE DE TER DANO TRIPLICADO
        }

        public int Fight()
        {
            return CurrentStrategy.Fight(this);
        }

    }
}
