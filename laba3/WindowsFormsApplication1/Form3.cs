using System;

using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Api call = (Api)this.Owner;
            call.callback(textBox1.Text);
            this.Close();
        }
    }
}
