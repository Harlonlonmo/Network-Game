using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Network_Game.Client
{
    public delegate void ConnectToServer(string ip);

    public partial class IPRequestForm : Form
    {
        public event ConnectToServer connect;

        public IPRequestForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (connect != null)
            {
                connect(textBox1.Text);
            }
            Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void IPRequestForm_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
