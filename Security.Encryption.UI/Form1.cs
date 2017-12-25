using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Security.Encryption.UI.Facade;

namespace Security.Encryption.UI
{
    public partial class Form1 : Form
    {
        private EnryptionFacade _facade;

        public Form1()
        {
            _facade = new EnryptionFacade();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();

            if (this.openFileDialog1.FileName == string.Empty)
            {
                return;
            }

            this.textBox1.Text = this.openFileDialog1.FileName;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.saveFileDialog1.ShowDialog();

            if (this.saveFileDialog1.FileName == string.Empty)
            {
                return;
            }

            this.textBox2.Text = this.saveFileDialog1.FileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var inputFilePath = this.openFileDialog1.FileName;
            var outputFilePath = this.saveFileDialog1.FileName;

            var password = this.textBox3.Text;

            _facade.Encrypt(password, inputFilePath, outputFilePath);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.openFileDialog2.ShowDialog();

            if (this.openFileDialog2.FileName == string.Empty)
            {
                return;
            }

            this.textBox6.Text = this.openFileDialog2.FileName;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.saveFileDialog2.ShowDialog();

            if (this.saveFileDialog2.FileName == string.Empty)
            {
                return;
            }

            this.textBox5.Text = this.saveFileDialog2.FileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var inputFilePath = this.openFileDialog2.FileName;
            var outputFilePath = this.saveFileDialog2.FileName;

            var password = this.textBox4.Text;

            _facade.Decrypt(password, inputFilePath, outputFilePath);
        }
    }
}
