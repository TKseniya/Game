using System.Collections.Generic;
using System.Linq;

namespace Lab_game
{
    public interface IFactory
    {
        IUnit CreateUnit();
    }

    public struct Elem
    {
        public IFactory Factory;
        public int Price;

        public Elem(IFactory factory, int price)
        {
            Factory = factory;
            Price = price;
        }
    }

    public class ElemComparer : IEqualityComparer<Elem>
    {
        public bool Equals(Elem x, Elem y)
        {
            return Equals(x.Price, y.Price);
        }

        public int GetHashCode(Elem obj)
        {
            if (ReferenceEquals(obj, null)) return 0;
            var hashName = obj.Price == null ? 0 : obj.Price.GetHashCode();
            return hashName;
        }
    }

    public static class Factory
    {
        // Отсортировано по возрастанию цены юнитов
        private static readonly List<Elem> UnitFactory;

        static Factory()
        {
            UnitFactory = new List<Elem>
            {
                new Elem(new LightInfantrymanFactory(), Consts.Parameters.Find(s => s.Name == "Light infantryman").Price),
                new Elem(new ArcherFactory(), Consts.Parameters.Find(s => s.Name == "Archer").Price),
                new Elem(new HealerFactory(), Consts.Parameters.Find(s => s.Name == "Healer").Price),
                new Elem(new HeavyInfantrymanFactory(), Consts.Parameters.Find(s => s.Name == "Heavy infantryman").Price),
                new Elem(new WizardFactory(), Consts.Parameters.Find(s => s.Name == "Wizard").Price),
                new Elem(new GulyayGorodFactory(), Consts.Parameters.Find(s => s.Name == "Gulyay gorod").Price),
                new Elem(new KnightFactory(), Consts.Parameters.Find(s => s.Name == "Knight").Price)
            };
        }

        public static void CreateRandomArmy(int playerId, int totalPrice = Consts.TotalPrice)
        {
            var units = new List<IUnit>();
            //все цены без повторений
            var priceList = new List<int>();
            for (var i = 0; i < UnitFactory.Count(); i++)
            {
                if (priceList.FindAll(price => price == UnitFactory[i].Price).Count == 0)
                    priceList.Add(UnitFactory[i].Price);
            }
            priceList.Sort();
            //массив с вероятностями
            var mas = new List<int>(priceList);
            for (var i = 0; i < mas.Count; i++)
            {
                mas[i] = 1000/priceList[i];
            }

            int rand;
            //пока есть хотя бы 1 юнит, которого мы можем создать
            while (totalPrice >= priceList.Min())
            {
                int a;
                //находим индекс максимально возможной цены
                for (a = 0; a < priceList.Count && priceList[a] <= totalPrice; a++) ;
                if (a < priceList.Count)
                {
                    priceList.RemoveRange(a, priceList.Count - a);
                    mas.RemoveRange(a, mas.Count - a);
                }
                var maxPrice = priceList.Max();
                //наши 100%
                var sum = mas.Sum();
                //пока мы можем покупать всех из прайс листа
                while (totalPrice >= maxPrice)
                {
                    rand = Consts.Rand.Next(sum);
                    var j = -1;
                    //когда rand < 0 нашли нужную вероятность
                    do
                    {
                        j++;
                        rand -= mas[j];
                    } while (rand > 0);
                    //индекс фабрики, цена юнита которой соответствует вероятности (если несколько то случайная)
                    var listElem = new List<Elem>(UnitFactory.FindAll(el => el.Price == priceList[j]));
                    var index = Consts.Rand.Next(listElem.Count);

                    var unit = listElem[index].Factory.CreateUnit();
                    if (unit is ISpecialAction)
                        if (((ISpecialAction) unit).Range > Consts.MaxRange)
                            Consts.MaxRange = ((ISpecialAction) unit).Range;

                    unit.PlayerId = playerId;
                    unit.UnitId = units.Count;
                    units.Add(unit);
                    totalPrice -= listElem[index].Price;
                }
            }
            Consts.Stacks[playerId] = new List<IUnit>(units);
        }
    }

    public class GulyayGorodFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new GulyayGorodAdapter();
        }
    }

    public class LightInfantrymanFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new LightInfantryman();
        }
    }

    public class HeavyInfantrymanFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new HeavyInfantryman();
        }
    }

    public class ArcherFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new Archer();
        }
    }

    public class HealerFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new Healer();
        }
    }

    public class WizardFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new Wizard();
        }
    }

    public class KnightFactory : IFactory
    {
        public IUnit CreateUnit()
        {
            return new Knight();
        }
    }
}