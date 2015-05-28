using System;
using System.Collections.Generic;
using System.IO;

namespace Lab_game
{
    
    public class Parameters 
    {
        public string Name;
        public int Attack;
        public int Defence;
        public int HP;
        public int SpecialActionPower;
        public int Range;
        public int Price;
    }

    public static class Consts
    {
        public static readonly List<Parameters> Parameters = new List<Parameters>
        {
            new Parameters
            {
                Name = "Archer",
                Attack = 8,
                Defence = 4,
                HP = 10,
                SpecialActionPower = 14,
                Range = 3,
                Price = 100
            },
            new Parameters
            {
                Name = "Light infantryman",
                Attack = 12,
                Defence = 6,
                HP = 10,
                SpecialActionPower = 0,
                Range = 1,
                Price = 100
            },
            new Parameters
            {
                Name = "Heavy infantryman",
                Attack = 14,
                Defence = 8,
                HP = 12,
                Price = 200
            },
            new Parameters
            {
                Name = "Healer",
                Attack = 5,
                Defence = 3,
                HP = 8,
                SpecialActionPower = 1,
                Range = 4,
                Price = 200
            },
            new Parameters
            {
                Name = "Wizard",
                Attack = 5,
                Defence = 3,
                HP = 6,
                SpecialActionPower = 20,
                Range = 5,
                Price = 250
            },
            new Parameters
            {
                Name = "Knight",
                Attack = 10,
                Defence = 6,
                HP = 10,
                Price = 250
            },
            new Parameters
            {
                Name = "Gulyay gorod",
                Defence = 8,
                HP = 10,
                Price = 150
            }

        };

        public static int N = 0;
        public static Condition Condition;
        public const int TotalPrice = 1000;
        public static IStrategy Strategy = new Strategy_3x3();
        public static bool SomeoneDied = false;
        public static bool SomeoneCloned = false;
        public static bool DeadHeat = false;
        public static readonly Random Rand = new Random(DateTime.Now.Millisecond);
        public static string[] PlayerName = {"First", "Second"};
        public static StreamWriter Sw;
        public static List<IUnit>[] Stacks = new List<IUnit>[2];
        public static int MaxRange = 0;
    }
}