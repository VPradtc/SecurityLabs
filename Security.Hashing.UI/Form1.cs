using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Security.Hashing.UI.Facade;

namespace Security.Hashing.UI
{
    public partial class Form1 : Form
    {
        private HashingFacade _facade;

        public Form1()
        {
            _facade = new HashingFacade();

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var input = this.textBox1.Text;

            var hash = _facade.GetHash(input);

            this.textBox2.Text = hash;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var hash = this.textBox2.Text;

            this.saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            _facade.SaveToFile(this.saveFileDialog1.FileName, hash);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
            {
                return;
            }

            var hash = _facade.GetHashForFile(openFileDialog1.FileName);

            this.textBox3.Text = hash;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var hash = this.textBox3.Text;

            this.saveFileDialog1.ShowDialog();

            if (saveFileDialog1.FileName == "")
            {
                return;
            }

            _facade.SaveToFile(this.saveFileDialog1.FileName, hash);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
            {
                return;
            }

            this.textBox4.Text = openFileDialog1.FileName;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.ShowDialog();

            if (openFileDialog1.FileName == "")
            {
                return;
            }

            this.textBox5.Text = openFileDialog1.FileName;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var inputFilePath = this.textBox4.Text;
            var hashFilePath = this.textBox5.Text;

            var integrityStatus = _facade.VerifyIntegrity(inputFilePath, hashFilePath);

            var labelText = integrityStatus ? "OK" : "HASH MISMATCH";

            this.label7.Text = labelText;
        }
    }
}
