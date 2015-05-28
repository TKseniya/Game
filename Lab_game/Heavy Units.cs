

using System.Linq;
using SpecialUnits;

namespace Lab_game
{
    public class GulyayGorodAdapter : HeavyUnit
    {
        private GulyayGorod GG;

        public GulyayGorodAdapter()
        {
            Name = "Gulyay gorod";
            Parameters p = Consts.Parameters.Find(s => s.Name == Name);
            GG = new GulyayGorod(p.HP, p.Defence, p.Price);
        }

        public override double HP
        {
            get { return GG.AreDeath ? GG.GetHealth() : 0; }
            set
            {
                if (GG.AreDeath && GG.GetHealth() - value > 0)
                    GG.TakeDamage((int) (GG.GetHealth() - value));
            }
        }

        public override double Attack
        {
            get { return GG.GetStrength(); }
        }

        public override double Defence
        {
            get { return GG.GetDefence(); }
        }
    }

    public class HeavyInfantryman : HeavyUnit, ICanBeHealed
    {
        public HeavyInfantryman()
        {
            Name = "Heavy infantryman";
            Parameters p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = MaxHealth = p.HP;
        }

        public int MaxHealth { get; set; }

        public void Heal(int hp)
        {
            HP += hp;
            if (HP > MaxHealth)
                HP = MaxHealth;
        }
    }

    public class Knight : HeavyUnit, IKnight
    {
        private double _hp;

        public Knight()
        {
            Name = "Knight"; 
            Parameters p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = p.HP;

            Horse = false;
            Spear = false;
            Plate = false;
            Shield = false;
            Helmet = false;
        }

        public override double HP
        {
            get { return _hp; }
            set
            {
                if (_hp != 0)
                    if (Consts.Rand.NextDouble()*100 <= 30)
                        RemoveArmor();
                _hp = value;
            }
        }

        public bool Horse { get; set; }
        public bool Spear { get; set; }
        public bool Plate { get; set; }
        public bool Shield { get; set; }
        public bool Helmet { get; set; }

        public virtual bool WearArmor()
        {
            Decorator decor;
            while (true)
            {
                if (!Horse)
                {
                    decor = new DecoratorHorse(this);
                    break;
                }
                if (!Spear)
                {
                    decor = new DecoratorSpear(this);
                    break;
                }
                if (!Plate)
                {
                    decor = new DecoratorPlate(this);
                    break;
                }
                if (!Shield)
                {
                    decor = new DecoratorShield(this);
                    break;
                }
                if (!Helmet)
                {
                    decor = new DecoratorHelmet(this);
                    break;
                }
                return false;
            }
            decor.WearArmor();
            return true;
        }

        public virtual void RemoveArmor()
        {
            Decorator decor;
            while (true)
            {
                if (Horse)
                {
                    decor = new DecoratorHorse(this);
                    break;
                }
                if (Spear)
                {
                    decor = new DecoratorSpear(this);
                    break;
                }
                if (Plate)
                {
                    decor = new DecoratorPlate(this);
                    break;
                }
                if (Shield)
                {
                    decor = new DecoratorShield(this);
                    break;
                }
                if (Helmet)
                {
                    decor = new DecoratorHelmet(this);
                    break;
                }
                return;
            }
            decor.RemoveArmor();
        }
    }
}