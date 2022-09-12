
namespace ScreenSleep
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingForm));
            this.apply = new System.Windows.Forms.Button();
            this.yes = new System.Windows.Forms.Button();
            this.cancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.hotkey_textbox = new System.Windows.Forms.TextBox();
            this.delay_textbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // apply
            // 
            this.apply.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.apply.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.apply.Location = new System.Drawing.Point(12, 119);
            this.apply.Name = "apply";
            this.apply.Size = new System.Drawing.Size(58, 30);
            this.apply.TabIndex = 0;
            this.apply.Text = "应用";
            this.apply.UseVisualStyleBackColor = true;
            // 
            // yes
            // 
            this.yes.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.yes.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.yes.Location = new System.Drawing.Point(89, 119);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(58, 30);
            this.yes.TabIndex = 1;
            this.yes.Text = "确认";
            this.yes.UseVisualStyleBackColor = true;
            // 
            // cancel
            // 
            this.cancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.cancel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cancel.Location = new System.Drawing.Point(164, 119);
            this.cancel.Name = "cancel";
            this.cancel.Size = new System.Drawing.Size(58, 30);
            this.cancel.TabIndex = 2;
            this.cancel.Text = "取消";
            this.cancel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 21);
            this.label1.TabIndex = 3;
            this.label1.Text = "热键：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(12, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 21);
            this.label2.TabIndex = 4;
            this.label2.Text = "热键延迟：";
            // 
            // textBox1
            // 
            this.hotkey_textbox.BackColor = System.Drawing.Color.White;
            this.hotkey_textbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.hotkey_textbox.Location = new System.Drawing.Point(76, 19);
            this.hotkey_textbox.Name = "textBox1";
            this.hotkey_textbox.ReadOnly = true;
            this.hotkey_textbox.Size = new System.Drawing.Size(146, 23);
            this.hotkey_textbox.TabIndex = 5;
            this.hotkey_textbox.Text = "Ctrl+Shift+P";
            // 
            // textBox2
            // 
            this.delay_textbox.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.delay_textbox.Location = new System.Drawing.Point(102, 65);
            this.delay_textbox.Name = "textBox2";
            this.delay_textbox.Size = new System.Drawing.Size(92, 23);
            this.delay_textbox.TabIndex = 6;
            this.delay_textbox.Text = "300";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(190, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "ms";
            // 
            // SettingForm
            // 
            this.ClientSize = new System.Drawing.Size(234, 161);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.delay_textbox);
            this.Controls.Add(this.hotkey_textbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancel);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.apply);
            this.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "设置";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void TextBox1_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button apply;
        public System.Windows.Forms.Button yes;
        public System.Windows.Forms.Button cancel;
        public System.Windows.Forms.TextBox hotkey_textbox;
        public System.Windows.Forms.TextBox delay_textbox;
        private System.Windows.Forms.Label label3;
    }
}