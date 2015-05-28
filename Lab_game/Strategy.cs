using System;
using System.Collections.Generic;

namespace Lab_game
{
    public interface IStrategy
    {
        int CalcCount();
        int[] CalcIndex(out int Range);
        void ChangeIndex(IUnit u, ref int[] i);
        List<IUnit> GetUnits(ISpecialAction unit);
        bool CheckDeadHeat();
        void ChooseKnight(ref List<IUnit> list);
        void ShowArmies();
        void Battle();
    }

    public class Strategy_1x1 : IStrategy
    {
        public int CalcCount()
        {
            return 1;
        }

        public int[] CalcIndex(out int Range)
        {
            Range = Consts.MaxRange;
            return new[] {1, 1};
        }

        public void ChangeIndex(IUnit u, ref int[] i)
        {
            i[Math.Abs(u.PlayerId)]--;
        }

        public List<IUnit> GetUnits(ISpecialAction unit)
        {
            var index = ((IUnit) unit).UnitId;
            var Range = unit.Range;
            var id = ((IUnit) unit).PlayerId;
            var units = new List<IUnit>();
            // обратном порядке армия противника
            for (var i = Consts.Stacks[1 - id].Count - 1; i >= 0; i--)
            {
                units.Add(Consts.Stacks[1 - id][i]);
            }
            index += Consts.Stacks[1 - id].Count;
            //в нужном порядке армия этого юнита
            units.AddRange(Consts.Stacks[id]);
            if (units.Count - index - 1 - Range > 0)
                units.RemoveRange(index + 1 + Range, units.Count - index - 1 - Range);
            if (index - 1 - Range > 0)
                units.RemoveRange(0, index - 1 - Range);
            units.RemoveAll(u => u.HP <= 0);
            return units;
        }

        public bool CheckDeadHeat()
        {
            if (Consts.Stacks[0][0].Attack <= Consts.Stacks[1][0].Defence &&
                Consts.Stacks[1][0].Attack <= Consts.Stacks[0][0].Defence)
            {
                Consts.DeadHeat = true;
                Consts.Sw.WriteLine("Dead heat\n");
            }
            else
                Consts.DeadHeat = false;
            return Consts.DeadHeat;
        }

        public void ChooseKnight(ref List<IUnit> list)
        {
            list.RemoveAll(k => k.UnitId == 0);
        }

        public void ShowArmies()
        {
            for (var i1 = 0; i1 < 2; i1++)
            {
                Consts.Sw.WriteLine(Consts.PlayerName[i1] + " army: ");
                for (var j = 0; j < Consts.Stacks[i1].Count; j++)
                {
                    Consts.Sw.WriteLine("{0, -20} [{1, -4} HP: {2}",
                        Consts.Stacks[i1][i1 == 0 ? Consts.Stacks[0].Count - 1 - j : j].Name,
                        (i1 == 0 ? Consts.Stacks[0].Count - 1 - j : j) + "]",
                        Consts.Stacks[i1][i1 == 0 ? Consts.Stacks[0].Count - 1 - j : j].HP);
                }
                Consts.Sw.WriteLine();
            }
            Consts.Sw.WriteLine();
        }

        public void Battle()
        {
            var id = Consts.Rand.Next(2);
            Consts.Sw.WriteLine("rand = {0}, {1} started", id, Consts.PlayerName[id]);

            if (Consts.Stacks[id][0].Hit(Consts.Stacks[1 - id][0]))
            {
                Consts.Stacks[1 - id][0].Hit(Consts.Stacks[id][0]);
            }
        }
    }

    public class Strategy_3x3 : IStrategy
    {
        public int CalcCount()
        {
            if (Consts.Stacks[0].Count >= 3)
                if (Consts.Stacks[1].Count >= 3)
                    return 3;
                else
                    return Consts.Stacks[1].Count;
            return Consts.Stacks[0].Count < Consts.Stacks[1].Count
                ? Consts.Stacks[0].Count
                : Consts.Stacks[1].Count;
        }

        public int[] CalcIndex(out int Range)
        {
            Range = 9;
            int[] i = {6, 6};
            if (Consts.SomeoneDied)
            {
                for (var j = 0; j < 2; j++)
                    if (Consts.Stacks[j][0].HP <= 0)
                    {
                        Consts.Stacks[j].RemoveAt(0);
                    }
            }
            return i;
        }

        public void ChangeIndex(IUnit u, ref int[] i)
        {
            if (i[Math.Abs(u.PlayerId)] > 6)
                i[Math.Abs(u.PlayerId)]--;
        }

        public List<IUnit> GetUnits(ISpecialAction unit)
        {
            var id = ((IUnit) unit).PlayerId;
            var units = new List<IUnit>();

            var Range = unit.Range*3; //3

            if (unit.Range >= 2)
            {
                // обратном порядке армия противника
                for (var i = Consts.Stacks[1 - id].Count - 1; i >= 0; i--)
                {
                    units.Add(Consts.Stacks[1 - id][i]);
                }
                if (Consts.Stacks[1 - id].Count - Range + 6 > 0)
                    units.RemoveRange(0, Consts.Stacks[1 - id].Count - Range + 6);
            }
            //в нужном порядке армия этого юнита
            units.AddRange(Consts.Stacks[id]);
            if (units.Count - 9 - Range > 0) // -15
                units.RemoveRange(8 + Range, units.Count - 9 - Range);
            if (unit.Range == 1)
            {
                units.RemoveRange(0, 3);
                if (((IUnit) unit).UnitId == 6)
                {
                    if (units.Count > 2)
                        units.RemoveAt(2);
                    if (units.Count > 4)
                        units.RemoveAt(4);
                    if (units.Count > 6)
                        units.RemoveAt(6);
                }
                if (((IUnit) unit).UnitId == 8)
                {
                    if (units.Count > 0)
                        units.RemoveAt(0);
                    if (units.Count > 2)
                        units.RemoveAt(2);
                    if (units.Count > 4)
                        units.RemoveAt(4);
                }
            }
            units.RemoveAll(u => u.HP <= 0);
            return units;
        }

        public bool CheckDeadHeat()
        {
            Consts.DeadHeat = true;
            var count = Consts.Stacks[0].Count < Consts.Stacks[1].Count
                ? Consts.Stacks[0].Count
                : Consts.Stacks[1].Count;
            if (count > 3)
                count = 3;
            for (var i = 0; i < count; i++)
                if (Consts.Stacks[0][i].Attack > Consts.Stacks[1][i].Defence ||
                    Consts.Stacks[1][i].Attack > Consts.Stacks[0][i].Defence)
                {
                    Consts.DeadHeat = false;
                }
            if (Consts.DeadHeat)
                Consts.Sw.WriteLine("Dead heat\n");
            return Consts.DeadHeat;
        }

        public void ChooseKnight(ref List<IUnit> list)
        {
            list.RemoveAll(k => k.UnitId < 3);
        }

        public void ShowArmies()
        {
            for (var i1 = 0; i1 < 2; i1++)
            {
                Consts.Sw.WriteLine(Consts.PlayerName[i1] + " army: ");
                for (var j = 0; j < (Consts.Stacks[i1].Count + 2)/3; j++)
                {
                    Consts.Sw.Write("{0, -20} [{1, -4} HP: {2, -5}",
                        Consts.Stacks[i1][j*3] is Knight ? "Knight" : Consts.Stacks[i1][j*3].Name, j*3 + "]",
                        Consts.Stacks[i1][j].HP);
                    if (j*3 + 1 < Consts.Stacks[i1].Count)
                        Consts.Sw.Write("{0, -20} [{1, -4} HP: {2, -5}",
                            Consts.Stacks[i1][j*3 + 1] is Knight ? "Knight" : Consts.Stacks[i1][j*3 + 1].Name,
                            j*3 + 1 + "]",
                            Consts.Stacks[i1][j].HP);
                    if (j*3 + 2 < Consts.Stacks[i1].Count)
                        Consts.Sw.Write("{0, -20} [{1, -4} HP: {2, -5}",
                            Consts.Stacks[i1][j*3 + 2] is Knight ? "Knight" : Consts.Stacks[i1][j*3 + 2].Name,
                            j*3 + 2 + "]",
                            Consts.Stacks[i1][j].HP);
                    Consts.Sw.WriteLine();
                }
                Consts.Sw.WriteLine();
            }
            Consts.Sw.WriteLine();
        }

        public void Battle()
        {
            int id, count;
            if (!Consts.Strategy.CheckDeadHeat())
            {
                count = Consts.Strategy.CalcCount();
                for (var i = 0; i < count; i++)
                {
                    id = Consts.Rand.Next(2);
                    Consts.Sw.WriteLine("rand = {0}, {1} started", id, Consts.PlayerName[id]);

                    if (!Consts.Stacks[id][i].Hit(Consts.Stacks[1 - id][i]))
                    {
                        count = Consts.Strategy.CalcCount();
                    }
                    else
                    {
                        if (!Consts.Stacks[1 - id][i].Hit(Consts.Stacks[id][i]))
                            count = Consts.Strategy.CalcCount();
                    }
                }
            }
        }
    }

    public class Strategy_ALLxALL : IStrategy
    {
        public int CalcCount()
        {
            return Consts.Stacks[0].Count < Consts.Stacks[1].Count
                ? Consts.Stacks[0].Count
                : Consts.Stacks[1].Count;
        }

        public int[] CalcIndex(out int Range)
        {
            var count = CalcCount();
            Range = count + Consts.MaxRange;
            int[] i = {count, count};
            /*
            if (Consts.SomeoneDied)
            {
                for (var j = 0; j < 2; j++)
                    if (Consts.Stacks[j][0].HP <= 0)
                    {
                        Consts.Stacks[j].RemoveAt(0);
                        i[j]--;
                    }
            }
             */
            return i;
        }

        public void ChangeIndex(IUnit u, ref int[] i)
        {
            i[Math.Abs(u.PlayerId)]--;
        }

        public List<IUnit> GetUnits(ISpecialAction unit)
        {
            var index = ((IUnit) unit).UnitId;
            var id = ((IUnit) unit).PlayerId;
            var Range = unit.Range;
            Consts.SomeoneDied = false;
            var units = new List<IUnit>();
            //в нужном порядке армия этого юнита
            units.AddRange(Consts.Stacks[id]);
            if (units.Count - index - 1 - Range > 0)
                units.RemoveRange(index + 1, units.Count - index - 1 - Range);
            if (index - Range > 0)
            {
                units.RemoveRange(0, index - Range);
                if (Consts.Stacks[1 - id].Count - 1 > index - Range)
                {
                    units.AddRange(Consts.Stacks[1 - id].GetRange(index - Range,
                        Consts.Stacks[1 - id].Count - index + Range));
                }
            }
            else if (index - Range < 0)
                units.AddRange(Consts.Stacks[1 - id]);
            units.RemoveAll(u => u.HP <= 0);
            return units;
        }

        public bool CheckDeadHeat()
        {
            Consts.DeadHeat = true;
            for (var i = 0; i < CalcCount(); i++)
                if (Consts.Stacks[0][i].Attack > Consts.Stacks[1][i].Defence ||
                    Consts.Stacks[1][i].Attack > Consts.Stacks[0][i].Defence)
                {
                    Consts.DeadHeat = false;
                }
            if (Consts.DeadHeat)
                Consts.Sw.WriteLine("Dead heat\n");
            return Consts.DeadHeat;
        }

        public void ChooseKnight(ref List<IUnit> list)
        {
            var unit = list[0];
            list.RemoveAll(k => k.UnitId < Consts.Stacks[unit.PlayerId].Count);
        }

        public void ShowArmies()
        {
            Consts.Sw.Write("{0, -40}", Consts.PlayerName[0] + " army: ");
            Consts.Sw.WriteLine("{0, -40}", Consts.PlayerName[1] + " army: ");
            for (var j = 0;
                j < (Consts.Stacks[0].Count > Consts.Stacks[1].Count ? Consts.Stacks[0].Count : Consts.Stacks[1].Count);
                j++)
            {
                if (j < Consts.Stacks[0].Count)
                    Consts.Sw.Write("{0, -20} [{1, -4} HP: {2,-10}", Consts.Stacks[0][j].Name, j + "]",
                        Consts.Stacks[0][j].HP);
                if (j < Consts.Stacks[1].Count)
                    Consts.Sw.Write("{0, -20} [{1, -4} HP: {2,-10}", Consts.Stacks[1][j].Name, j + "]",
                        Consts.Stacks[1][j].HP);
                Consts.Sw.WriteLine();
            }
            Consts.Sw.WriteLine();
        }

        public void Battle()
        {
            int id, count;
            if (!Consts.Strategy.CheckDeadHeat())
            {
                count = Consts.Strategy.CalcCount();
                for (var i = 0; i < count; i++)
                {
                    id = Consts.Rand.Next(2);
                    Consts.Sw.WriteLine("rand = {0}, {1} started", id, Consts.PlayerName[id]);

                    if (!Consts.Stacks[id][i].Hit(Consts.Stacks[1 - id][i]))
                    {
                        count = Consts.Strategy.CalcCount();
                    }
                    else
                    {
                        if (!Consts.Stacks[1 - id][i].Hit(Consts.Stacks[id][i]))
                            count = Consts.Strategy.CalcCount();
                    }
                }
            }
        }
    }
}