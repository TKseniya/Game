using System.Collections.Generic;

namespace Lab_game
{
    public sealed class LightInfantryman : LightUnit, IClonableUnit, ICanBeHealed, ISpecialAction
    {
        public LightInfantryman()
        {
            Name = "Light infantryman";
            var p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = MaxHealth = p.HP;
            SpecialActionPower = p.SpecialActionPower;
            Range = p.Range;
        }

        public int MaxHealth { get; set; }

        public void Heal(int hp)
        {
            HP += hp;
            if (HP > MaxHealth)
                HP = MaxHealth;
        }

        public IUnit Clone()
        {
            Consts.Sw.WriteLine("{0} {1} [{2}] was cloned", Consts.PlayerName[PlayerId], Name, UnitId);
            return (LightInfantryman) MemberwiseClone();
        }

        public int Range { get; set; }
        public int SpecialActionPower { get; set; }

        public IUnit SpecialAction(List<IUnit> units)
        {
            units.RemoveAll(u => PlayerId != u.PlayerId);
            units.RemoveAll(u => !(u is IKnight));
            if (units.Count > 0)
            {
                Consts.Strategy.ChooseKnight(ref units);
                foreach (var unit in units)
                {
                    Consts.Sw.WriteLine("{0} {1} [{2}] trying to wear {0} Knight [{3}]", Consts.PlayerName[PlayerId],
                        Name, UnitId, unit.UnitId);
                    if (((IKnight) unit).WearArmor())
                    {
                        return unit;
                    }
                }
            }
            return null;
        }
    }

    public sealed class Archer : LightUnit, ISpecialAction, ICanBeHealed
    {
        public Archer()
        {
            Name = "Archer";
            var p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = MaxHealth = p.HP;
            SpecialActionPower = p.SpecialActionPower;
            Range = p.Range;
        }

        public int MaxHealth { get; set; }

        public void Heal(int hp)
        {
            HP += hp;
            if (HP > MaxHealth)
                HP = MaxHealth;
        }

        public int Range { get; set; }

        public IUnit SpecialAction(List<IUnit> units)
        {
            units.RemoveAll(u => u.PlayerId == PlayerId);

            if (units.Count > 0)
            {
                if (units.FindAll(un => un.Defence < SpecialActionPower).Count != 0)
                {
                    Consts.DeadHeat = false;

                    var unit = units[Consts.Rand.Next(units.Count)];
                    Hit(unit, SpecialActionPower);
                    return unit;
                }
            }
            return null;
        }

        public int SpecialActionPower { get; set; }
    }

    public sealed class Healer : LightUnit, ISpecialAction, ICanBeHealed
    {
        public Healer()
        {
            Name = "Healer";
            var p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = MaxHealth = p.HP;
            SpecialActionPower = p.SpecialActionPower;
            Range = p.Range;
        }

        public int MaxHealth { get; set; }

        public void Heal(int hp)
        {
            HP += hp;
            if (HP > MaxHealth)
                HP = MaxHealth;
        }

        public int Range { get; set; }
        public int SpecialActionPower { get; set; }

        public IUnit SpecialAction(List<IUnit> units)
        {
            units.Add(this);
            units.RemoveAll(u => u.PlayerId != PlayerId || !(u is ICanBeHealed));
            units.RemoveAll(u => ((ICanBeHealed) u).MaxHealth == u.HP);

            Consts.Sw.WriteLine("{0} {1} [{2}] trying to heal", Consts.PlayerName[PlayerId],
                Name, UnitId);
            if (units.Count == 0)
                return null;

            var unit = units[Consts.Rand.Next(units.Count)];
            ((ICanBeHealed)unit).Heal(SpecialActionPower);
            Consts.Sw.WriteLine("{0} {1} [{4}] was healed on {2}, HP = {3}", Consts.PlayerName[PlayerId], unit.Name, SpecialActionPower, unit.HP,
                unit.UnitId);
            return unit;
        }
    }

    public sealed class Wizard : LightUnit, ISpecialAction, ICanBeHealed
    {
        public Wizard()
        {
            Name = "Wizard";
            var p = Consts.Parameters.Find(s => s.Name == Name);
            Attack = p.Attack;
            Defence = p.Defence;
            HP = MaxHealth = p.HP;
            SpecialActionPower = p.SpecialActionPower;
            Range = p.Range;
        }

        public int MaxHealth { get; set; }

        public void Heal(int hp)
        {
            HP += hp;
            if (HP > MaxHealth)
                HP = MaxHealth;
        }

        public int Range { get; set; }
        public int SpecialActionPower { get; set; }

        public IUnit SpecialAction(List<IUnit> units)
        {
            Consts.DeadHeat = false;
            Consts.Sw.WriteLine("{0} {1} [{2}] trying to clone", Consts.PlayerName[PlayerId],
                Name, UnitId);
            units.RemoveAll(u => u.PlayerId != PlayerId || !(u is IClonableUnit));
            if (Consts.Rand.Next(100) >= SpecialActionPower || units.Count <= 0)
                return null;
            var unit = units[Consts.Rand.Next(units.Count)];
            List<IUnit> part = null;
            if (Consts.Stacks[PlayerId].Count > unit.UnitId + 1)
            {
                part = new List<IUnit>(Consts.Stacks[PlayerId].GetRange(unit.UnitId + 1,
                    Consts.Stacks[PlayerId].Count - unit.UnitId - 1));
                Consts.Stacks[PlayerId].RemoveRange(unit.UnitId + 1, Consts.Stacks[PlayerId].Count - unit.UnitId - 1);
            }
            Consts.Stacks[PlayerId].Add(((IClonableUnit) unit).Clone());
            Consts.Sw.WriteLine("New unit in {0} army : {1} [{2}]", Consts.PlayerName[PlayerId], unit.Name,
                unit.UnitId + 1);
            if (part != null)
                Consts.Stacks[PlayerId].AddRange(part);
            for (var index = 0; index < Consts.Stacks[PlayerId].Count; index++)
            {
                var unit1 = Consts.Stacks[PlayerId][index];
                unit1.UnitId = index;
            }

            Consts.SomeoneCloned = true;
            return unit;
        }
    }
}