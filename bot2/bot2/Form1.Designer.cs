using Awesomium.Core;
namespace bot2
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.testButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.JStextBox = new System.Windows.Forms.TextBox();
            this.jsButton = new System.Windows.Forms.Button();
            this.searchButton = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.AutoButton = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // testButton
            // 
            this.testButton.Location = new System.Drawing.Point(827, 12);
            this.testButton.Name = "testButton";
            this.testButton.Size = new System.Drawing.Size(75, 23);
            this.testButton.TabIndex = 1;
            this.testButton.Text = "Test";
            this.testButton.UseVisualStyleBackColor = true;
            this.testButton.Click += new System.EventHandler(this.testButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(827, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Region";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(827, 81);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "50 results";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // JStextBox
            // 
            this.JStextBox.Location = new System.Drawing.Point(827, 199);
            this.JStextBox.Multiline = true;
            this.JStextBox.Name = "JStextBox";
            this.JStextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.JStextBox.Size = new System.Drawing.Size(234, 162);
            this.JStextBox.TabIndex = 4;
            // 
            // jsButton
            // 
            this.jsButton.Location = new System.Drawing.Point(986, 367);
            this.jsButton.Name = "jsButton";
            this.jsButton.Size = new System.Drawing.Size(75, 23);
            this.jsButton.TabIndex = 5;
            this.jsButton.Text = "Run javascript";
            this.jsButton.UseVisualStyleBackColor = true;
            this.jsButton.Click += new System.EventHandler(this.jsButton_Click);
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(827, 110);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 6;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // AutoButton
            // 
            this.AutoButton.Location = new System.Drawing.Point(827, 159);
            this.AutoButton.Name = "AutoButton";
            this.AutoButton.Size = new System.Drawing.Size(75, 23);
            this.AutoButton.TabIndex = 7;
            this.AutoButton.Text = "AutoStart";
            this.AutoButton.UseVisualStyleBackColor = true;
            this.AutoButton.Click += new System.EventHandler(this.AutoButton_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(923, 12);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "Register";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 762);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.AutoButton);
            this.Controls.Add(this.searchButton);
            this.Controls.Add(this.jsButton);
            this.Controls.Add(this.JStextBox);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.testButton);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button testButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox JStextBox;
        private System.Windows.Forms.Button jsButton;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button AutoButton;
        private System.Windows.Forms.Button button3;
    }
}

