using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        delegate void PersionChanger(int age);
        private void button1_Click(object sender, EventArgs e)
        {
            PersionChanger pc = new PersionChanger(Persion.AddAge);
            pc += new PersionChanger(Persion.AddHeigth);
            pc(1);
        }

    }
    class Persion
    {
        public static void AddAge(int age)
        {
            MessageBox.Show("person age is " + age );
        }

        public static void AddHeigth(int height)
        {
            MessageBox.Show("person height is " + height);
        }

        public static void AddWeight(int weight)
        {
            MessageBox.Show("person weight is " + weight);
        }
    }
}
