using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Lab_game;

namespace Forms
{
    public partial class BattleForm : Form
    {
        public BattleForm()
        {
            InitializeComponent();
        }

        private void Battle_Click(object sender, EventArgs e)
        {
            button_Undo.IsAccessible = Consts.Condition.HavePreviosCondition();
            Core.Battle();
        }

        private void Undo_Click(object sender, EventArgs e)
        {
            Consts.Condition.GoToPreviosCondition();
            button_Undo.IsAccessible = Consts.Condition.HavePreviosCondition();
        }
    }
}
