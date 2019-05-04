namespace translator
{
    partial class AddWord
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

        public string Label1Add
        {
            get { return label1Add.Text; }
            set { label1Add.Text = value; }
        }

        public string Label2Add
        {
            get { return label2Add.Text; }
            set { label2Add.Text = value; }
        }

        public string TextBox1
        {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        public string TextBox2
        {
            get { return textBox2.Text; }
            set { textBox2.Text = value; }
        }

        
        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.CancelButtonAdd = new System.Windows.Forms.Button();
            this.OkButtonAdd = new System.Windows.Forms.Button();
            this.label1Add = new System.Windows.Forms.Label();
            this.label2Add = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // CancelButtonAdd
            // 
            this.CancelButtonAdd.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelButtonAdd.Location = new System.Drawing.Point(64, 136);
            this.CancelButtonAdd.Name = "CancelButtonAdd";
            this.CancelButtonAdd.Size = new System.Drawing.Size(93, 36);
            this.CancelButtonAdd.TabIndex = 0;
            this.CancelButtonAdd.Text = "Cancel";
            this.CancelButtonAdd.UseVisualStyleBackColor = true;
            this.CancelButtonAdd.Click += new System.EventHandler(this.CancelButtonAdd_Click);
            // 
            // OkButtonAdd
            // 
            this.OkButtonAdd.Font = new System.Drawing.Font("Calibri", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OkButtonAdd.Location = new System.Drawing.Point(304, 136);
            this.OkButtonAdd.Name = "OkButtonAdd";
            this.OkButtonAdd.Size = new System.Drawing.Size(93, 36);
            this.OkButtonAdd.TabIndex = 1;
            this.OkButtonAdd.Text = "OK";
            this.OkButtonAdd.UseVisualStyleBackColor = true;
            this.OkButtonAdd.Click += new System.EventHandler(this.OkButtonAdd_Click);
            // 
            // label1Add
            // 
            this.label1Add.AutoSize = true;
            this.label1Add.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1Add.Location = new System.Drawing.Point(78, 31);
            this.label1Add.Name = "label1Add";
            this.label1Add.Size = new System.Drawing.Size(69, 24);
            this.label1Add.TabIndex = 2;
            this.label1Add.Text = "English";
            // 
            // label2Add
            // 
            this.label2Add.AutoSize = true;
            this.label2Add.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2Add.Location = new System.Drawing.Point(78, 84);
            this.label2Add.Name = "label2Add";
            this.label2Add.Size = new System.Drawing.Size(60, 24);
            this.label2Add.TabIndex = 3;
            this.label2Add.Text = "Polish";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(273, 34);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(160, 22);
            this.textBox1.TabIndex = 4;
            this.textBox1.Validating += new System.ComponentModel.CancelEventHandler(this.textBox1_Validating);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(273, 87);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(160, 22);
            this.textBox2.TabIndex = 5;
            this.textBox2.Validating += new System.ComponentModel.CancelEventHandler(this.textBox2_Validating);
            // 
            // errorProvider1
            // 
            this.errorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider1.ContainerControl = this;
            // 
            // AddWord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 211);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2Add);
            this.Controls.Add(this.label1Add);
            this.Controls.Add(this.OkButtonAdd);
            this.Controls.Add(this.CancelButtonAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddWord";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "AddWord";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButtonAdd;
        private System.Windows.Forms.Button OkButtonAdd;
        private System.Windows.Forms.Label label1Add;
        private System.Windows.Forms.Label label2Add;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}