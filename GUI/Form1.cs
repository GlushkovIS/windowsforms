using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //считываем значение а
            //проверка на корретность
            int res, a;
            if (Int32.TryParse(textBox1.Text, out res)&& res > 0)
            {
                a = res;
            }
            else
            {
                MessageBox.Show("Неверное значение для параметра а." + " Введите, пожалуйста, целое число, больше 0.");
                return;
            }

            Graphics g = Graphics.FromHwnd(panel1.Handle);
            Pen p = new Pen(Color.Red, 3);
            for (int x = -300; x <= 300; x += 1)
            {
                int y = (a * a - x * x) / (600 + (int)Math.Sqrt((a * a - x * x)));
                g.DrawRectangle(p, x + 300, 300 - y, 1, 1);
                y = (a * a - x * x) / (600 - (int)Math.Sqrt((a * a - x * x)));
                g.DrawRectangle(p, x + 300, 300 - y, 1, 1);
                System.Threading.Thread.Sleep(5);
            }
        }
               
            

        private void panel1_Paint(object sender, EventArgs e)
        {
            
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.Red;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)
        {
            CloseButton.ForeColor = Color.White;
        }

        Point lastPoint;

        private void mainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }

        private void mainForm_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X, e.Y);
        }
    }
}
