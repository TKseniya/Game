using System.Collections.Generic;

namespace Lab_game
{
    public interface IUnit
    {
        int UnitId { get; set; }
        int PlayerId { get; set; }
        string Name { get; }
        double Attack { get; }
        double Defence { get; }
        double HP { get; set; }
        bool Hit(IUnit unit);
    }

    public interface ICanBeHealed
    {
        int MaxHealth { get; set; }
        void Heal(int hp);
    }

    public interface ISpecialAction
    {
        int Range { get; set; }
        int SpecialActionPower { get; set; }
        IUnit SpecialAction(List<IUnit> units);
    }

    public interface IClonableUnit
    {
        IUnit Clone();
    }

    public interface IKnight
    {
        bool Horse { get; set; }
        bool Spear { get; set; }
        bool Plate { get; set; }
        bool Shield { get; set; }
        bool Helmet { get; set; }
        bool WearArmor();
        void RemoveArmor();
    }
}