using System;

namespace Lab_game
{
    public abstract class Unit : IUnit
    {
        public int UnitId { get; set; }
        public int PlayerId { get; set; }
        public string Name { get; set; }
        public virtual double Attack { get; set; }
        public virtual double Defence { get; set; }

        public virtual double HP { get; set; }

        public bool Hit(IUnit unit)
        {
            return Hit(unit, Attack);
        }

        protected virtual bool Hit(IUnit unit, double attack)
        {
            if (attack <= unit.Defence)
            {
                Consts.Sw.WriteLine("{0} {1} [{5}] {4} {2} {3} [{6}]", Consts.PlayerName[PlayerId],
                    Name, Consts.PlayerName[unit.PlayerId], unit.Name, "can't cause damage", UnitId, unit.UnitId);
                return true;
            }
            var random = Consts.Rand.Next(11)/10.0;
            var damage = attack*random;
            //Consts.Sw.WriteLine("random = {0}, attack = {1}, defence = {2}, HP = {3}", random, damage, unit.Defence, unit.HP);
            damage -= unit.Defence;
            damage = Math.Round(damage, 2);
            if (damage < 0)
                damage = 0;
            unit.HP -= damage;
            unit.HP = Math.Round(unit.HP, 2);

            Consts.Sw.WriteLine("{0} {1} [{7}] {6} {2} {3} [{8}], damage = {4}, HP of {2} {3} [{8}] = {5}",
                Consts.PlayerName[PlayerId],
                Name, Consts.PlayerName[unit.PlayerId], unit.Name, damage, unit.HP <= 0 ? 0 : unit.HP,
                "hit", UnitId, unit.UnitId);
            if (unit.HP <= 0)
            {
                Consts.Sw.WriteLine();
                Consts.Sw.WriteLine(Consts.PlayerName[PlayerId] + " " + Name + " [" + UnitId + "] killed " +
                                    Consts.PlayerName[unit.PlayerId] + " " + unit.Name + " [" + unit.UnitId + "]");
                for (var k = unit.UnitId + 1; k < Consts.Stacks[unit.PlayerId].Count; k++)
                {
                    Consts.Stacks[unit.PlayerId][k].UnitId--;
                }
                Consts.Sw.WriteLine();
                Consts.SomeoneDied = true;
                Consts.Stacks[unit.PlayerId].RemoveAll(u => u.HP <= 0);
                return false;
            }
            return true;
        }
    }

    public abstract class LightUnit : Unit
    {
    }


    public abstract class HeavyUnit : Unit
    {
    }
}