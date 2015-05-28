namespace Forms
{
    partial class BattleForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Battle = new System.Windows.Forms.Button();
            this.button_Undo = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button_Battle.Location = new System.Drawing.Point(24, 12);
            this.button_Battle.Name = "button1";
            this.button_Battle.Size = new System.Drawing.Size(763, 181);
            this.button_Battle.TabIndex = 1;
            this.button_Battle.Text = "Battle";
            this.button_Battle.UseVisualStyleBackColor = true;
            this.button_Battle.Click += new System.EventHandler(this.Battle_Click);
            // 
            // button2
            // 
            this.button_Undo.Location = new System.Drawing.Point(24, 199);
            this.button_Undo.Name = "button2";
            this.button_Undo.Size = new System.Drawing.Size(763, 195);
            this.button_Undo.TabIndex = 2;
            this.button_Undo.Text = "Undo";
            this.button_Undo.UseVisualStyleBackColor = true;
            this.button_Undo.Click += new System.EventHandler(this.Undo_Click);
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 406);
            this.Controls.Add(this.button_Undo);
            this.Controls.Add(this.button_Battle);
            this.Name = "BattleForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_Battle;
        private System.Windows.Forms.Button button_Undo;
    }
}

