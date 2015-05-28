using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab_game
{
    public static class Core
    {
        static Core()
        {
            Factory.CreateRandomArmy(0);
            Factory.CreateRandomArmy(1);
        }
        public static void Start()
        {
            Factory.CreateRandomArmy(0);
            Factory.CreateRandomArmy(1);
            //Consts.Condition = new Condition();
            var comparer = new UnitComparer();

            using (Consts.Sw = new StreamWriter("Battle.txt", false))
            {
                for (var i = 0; i < 2; i++)
                {
                    Consts.Sw.WriteLine("Армия {0} :", Consts.PlayerName[i]);
                    foreach (var unit in Consts.Stacks[i].Distinct(comparer))
                        Consts.Sw.WriteLine("{0}: {1}", unit.Name,
                            Consts.Stacks[i].FindAll(p => p.Name == unit.Name).Count);
                }
                Consts.Sw.WriteLine();
                Consts.Strategy.ShowArmies();
                do
                {
                 Battle();   
                } while (Consts.Stacks[0].Count != 0 && Consts.Stacks[1].Count != 0 && !Consts.DeadHeat);
                End();
                Consts.Sw.Close();
            }
        }

        private static void End()
        {
                if (!Consts.DeadHeat)
                {
                    Consts.Sw.WriteLine("{0} won",
                        (Consts.Stacks[0].Count() > 0 ? Consts.PlayerName[0] : Consts.PlayerName[1]));
                   
                }
            
        }
        private static void Battle()
        {
                Consts.N ++;
                Consts.Sw.WriteLine("\nState: {0}\n", Consts.N);
                Consts.Strategy.Battle();
                Consts.Sw.WriteLine();
                if (Consts.Stacks[0].Count == 0 || Consts.Stacks[1].Count == 0)
                    return;

            
                IUnit unit;
                int Range;
                var i = Consts.Strategy.CalcIndex(out Range);
                
                if (Consts.SomeoneDied)
                {
                    Consts.Strategy.ShowArmies();
                    Consts.SomeoneDied = false;
                    Consts.SomeoneCloned = false;
                    if (Consts.Stacks[0].Count == 0 || Consts.Stacks[1].Count == 0)
                        return;
                }
                Consts.Sw.WriteLine("\nTry SpecialAction\n");
                if (Consts.Stacks[0].Count > i[0] || Consts.Stacks[1].Count > i[1])
                    do
                    {
                        //выбираем первого
                        int id = Consts.Rand.Next(2);
                        //поочереди для 2 юнитов с одинаковыми индексами вызывается спешл экшен
                        for (var j = 0; j < 2; j++)
                        {
                            //если юнит с нужным индексом существует
                            if (Consts.Stacks[Math.Abs(id - j)].Count > i[Math.Abs(id - j)])
                            {
                                //выбранный юнит
                                unit = Consts.Stacks[Math.Abs(id - j)][i[Math.Abs(id - j)]];

                                if (unit is ISpecialAction)
                                {
                                    //список юнитов, на которых данный юнит может воздействовать
                                    var list = new List<IUnit>(Consts.Strategy.GetUnits((ISpecialAction) unit));
                                    if (list.Count > 0)
                                    {
                                        var u = ((ISpecialAction) unit).SpecialAction(list);
                                        if (u != null)
                                        {
                                            if (u.HP <= 0)
                                            {
                                                Consts.Strategy.ChangeIndex(u, ref i);
                                                if (Consts.SomeoneDied)
                                                {
                                                    Consts.Stacks[u.PlayerId].RemoveAll(dead => dead.HP <= 0);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            i[Math.Abs(id - j)]++;
                        }
                    } while (i[0] < Range && i[1] < Range);
                else
                {
                    Consts.Sw.WriteLine("No special actions\n");
                }

               // Consts.Condition.RecordCondition();

                if (Consts.DeadHeat)
                {
                    Consts.Sw.WriteLine("Friendship won!");
                    return;
                }

                Consts.Sw.WriteLine();
                if (Consts.SomeoneDied || Consts.SomeoneCloned)
                {
                    Consts.Strategy.ShowArmies();
                    Consts.SomeoneDied = false;
                    Consts.SomeoneCloned = false;
                }
            
        }
    }


    public class UnitComparer : IEqualityComparer<IUnit>
    {
        public bool Equals(IUnit x, IUnit y)
        {
            return String.CompareOrdinal(x.Name, y.Name) == 0;
        }

        public int GetHashCode(IUnit obj)
        {
            if (ReferenceEquals(obj, null)) return 0;
            var hashName = obj.Name == null ? 0 : obj.Name.GetHashCode();
            return hashName;
        }
    }
}