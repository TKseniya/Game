namespace Lab_game
{
    public class Decorator : Knight
    {
        public Knight Knight;
        public string Name;
    }

    public class DecoratorHorse : Decorator
    {
        public DecoratorHorse(Knight knight)
        {
            Knight = knight;
            Name = "Horse";
        }

        public override bool WearArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] weared {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name += "[" + Name + "]";
            Knight.Defence += 1;
            Knight.Attack += 1;
            Knight.Horse = true;
            return true;
        }

        public override void RemoveArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] removed {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name = Knight.Name.Replace("[" + Name + "]", "");
            Knight.Defence -= 1;
            Knight.Attack -= 1;
            Knight.Horse = false;
        }
    }

    public class DecoratorSpear : Decorator
    {
        public DecoratorSpear(Knight knight)
        {
            Knight = knight;
            Name = "Spear";
        }

        public override bool WearArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] weared {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name += "[" + Name + "]";
            Knight.Attack += 8;
            Knight.Spear = true;
            return true;
        }

        public override void RemoveArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] removed {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name = Knight.Name.Replace("[" + Name + "]", "");
            Knight.Attack -= 8;
            Knight.Spear = false;
        }
    }

    public class DecoratorPlate : Decorator
    {
        public DecoratorPlate(Knight knight)
        {
            Knight = knight;
            Name = "Plate";
        }

        public override bool WearArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] weared {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name += "[" + Name + "]";
            Knight.Defence += 4;
            Knight.Plate = true;
            return true;
        }

        public override void RemoveArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] removed {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name = Knight.Name.Replace("[" + Name + "]", "");
            Knight.Defence -= 4;
            Knight.Plate = false;
        }
    }

    public class DecoratorShield : Decorator
    {
        public DecoratorShield(Knight knight)
        {
            Knight = knight;
            Name = "Shield";
        }

        public override bool WearArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] weared {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name += "[" + Name + "]";
            Knight.Defence += 2;
            Knight.Attack -= 1;
            Knight.Shield = true;
            return true;
        }

        public override void RemoveArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] removed {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name = Knight.Name.Replace("[" + Name + "]", "");
            Knight.Defence -= 2;
            Knight.Attack += 1;
            Knight.Shield = false;
        }
    }

    public class DecoratorHelmet : Decorator
    {
        public DecoratorHelmet(Knight knight)
        {
            Knight = knight;
            Name = "Helmet";
        }

        public override bool WearArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] weared {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name += "[" + Name + "]";
            Knight.Defence += 2;
            Knight.Helmet = true;
            return true;
        }

        public override void RemoveArmor()
        {
            Consts.Sw.WriteLine("{0} {1} [{3}] removed {2}", Consts.PlayerName[Knight.PlayerId], Knight.Name, Name, Knight.UnitId);
            Knight.Name = Knight.Name.Replace("[" + Name + "]", "");
            Knight.Defence -= 2;
            Knight.Helmet = false;
        }
    }
}