using System;
using System.Collections.Generic;

namespace Lab_game
{
    /*
    public interface ICommand
    {
        void Execute();
        void Unexecute();
    }

    public class Invoker
    {
        private List<ICommand> list = new List<ICommand>();
        private ISPOLNITEL ispolnitel = new ISPOLNITEL();
        private int last_index = 0;

        public void Do(ICommand command)
        {
            list.RemoveRange(last_index, list.Count - last_index - 1);
            list.Add(command);
            last_index ++;
            command.Execute();
        }

        public void Undo()
        {
            if (list.Count > 0)
            {
                list[last_index].Unexecute();
                last_index--;
            }
        }

        public void Redo()
        {
            if (last_index != list.Count - 1)
            {
                last_index ++;
                list[last_index].Execute();
            }
        }
    }
    public class ISPOLNITEL
    {
        public void Operation(string Command_Name)
        {
            
        }
    }
    
    public class Hit : ICommand
    {
        private string Name;
        public void Execute()
        {
            ISPOLNITEL.Operation(Name );
        }

        public void Unexecute()
        {
            ISPOLNITEL.Operation(Undo_Name());
        }

        private string Undo_Name()
        {
            string name = "";
            return name;
        }
    }
     */
    public class Condition
    {
        private Condition previosCondition;
        private List<IUnit>[] army;
        public Condition()
        {
            army = new[] { new List<IUnit>(Consts.Stacks[0]), new List<IUnit>(Consts.Stacks[1]) };
            previosCondition = null;
        }
        public void RecordCondition()
        {
            for (int i = 0; i < 2; i++)
            {
                List<IUnit> newUnits = new List<IUnit>(Consts.Stacks[i]);
                
                for (int j = 0; j < newUnits.Count; j++)
                {
                    IUnit new_unit = newUnits.Find(u => u.UnitId == j);
                    IUnit old_unit = army[i].Find(u => u.UnitId == j);
                    if (new_unit == null)
                    {
                        continue;
                    }
                    if (old_unit == null)
                    {
                        army[i].Add(new Archer { HP = 0, UnitId = j });
                        continue;
                    }
                    if (Compare(new_unit, old_unit))
                    {
                        army[i].Remove(old_unit);
                    }
                }
                Condition new_condition = new Condition();

                
                for (int j = army[i].Count; j < newUnits.Count; j++)
                {
                    army[i].Add(new Archer { HP = 0, UnitId = j });
                }

                if (army[i].Count > newUnits.Count)
                    previosCondition.army[i].AddRange(army[i].GetRange(newUnits.Count,
                        army[i].Count - newUnits.Count));
                 
            }

        }
        public bool GoToPreviosCondition()
        {
            if (previosCondition == null)
                return false; ;
            for (int i = 0; i < 2; i++)
            {
                List<IUnit> old_army = new List<IUnit>(previosCondition.army[i]);
                foreach (var unit in old_army)
                {
                    Consts.Stacks[i][Consts.Stacks[i].FindIndex(u => u.UnitId == unit.UnitId)] = unit;
                }
            }
            previosCondition = previosCondition.previosCondition;
            return true;
        }
        private bool Compare(IUnit new_unit, IUnit old_unit)
        {
            if (old_unit.Name != new_unit.Name)
                return false;
            if (old_unit.Attack != new_unit.Attack)
                return false;
            if (old_unit.Defence != new_unit.Defence)
                return false;
            if (old_unit.HP != new_unit.HP)
                return false;
            return true;
        }
        public bool HavePreviosCondition()
        {
            return previosCondition != null;
        }
    }


}