using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace translator
{
    public partial class AddWord : Form
    {
        bool validAbove;
        bool validBelow;

        public AddWord()
        {
            validAbove = true;
            validBelow = true;
            InitializeComponent();
        }

        private void OkButtonAdd_Click(object sender, EventArgs e)
        {
            ValidateChildren();

            if(!validBelow || !validAbove)
            {
                ValidationError validationError = new ValidationError();
                validationError.ShowDialog();
                return;
            }

            this.DialogResult = DialogResult.OK;
        }

        private void CancelButtonAdd_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        public void DisableTextBox1()
        {
            textBox1.Enabled = false;
        }

        private void textBox1_Validating(object sender, CancelEventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                validAbove = false;
                errorProvider1.SetError(textBox1, "Field cannot be empty");
                return;
            }

            if (textBox1.Text.All(char.IsLetter))
            {
                validAbove = true;
                errorProvider1.SetError(textBox1, "");
            }
            else
            {
                validAbove = false;
                errorProvider1.SetError(textBox1, "Only letters are allowed");
            }
        }

        private void textBox2_Validating(object sender, CancelEventArgs e)
        {
            if (textBox2.Text == string.Empty)
            {
                validBelow = false;
                errorProvider1.SetError(textBox2, "Field cannot be empty");
                return;
            }
            if (textBox2.Text.All(char.IsLetter))
            {
                validBelow = true;
                errorProvider1.SetError(textBox2, "");
            }
            else
            {
                validBelow = false;
                errorProvider1.SetError(textBox2, "Only letters are allowed");
            }
        }
    }
}
