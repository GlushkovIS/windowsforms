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
        //Пикселей в одном делении оси
        const int PIX_IN_ONE = 15;
        //Длина стрелки
        const int ARR_LEN = 10;

        public mainForm()
        {
            InitializeComponent();
            panel1.Paint += panel1_Paint;
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

        //оси координат
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            int w = panel1.ClientSize.Width / 2;
            int h = panel1.ClientSize.Height / 2;
            //Смещение начала координат в центр PictureBox
            e.Graphics.TranslateTransform(w, h);
            DrawXAxis(new Point(-w, 0), new Point(w, 0), e.Graphics);
            DrawYAxis(new Point(0, h), new Point(0, -h), e.Graphics);
            //Центр координат
            e.Graphics.FillEllipse(Brushes.Red, -2, -2, 4, 4);
        }

        //Рисование оси X
        private void DrawXAxis(Point start, Point end, Graphics g)
        {
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }

        //Рисование оси Y
        private void DrawYAxis(Point start, Point end, Graphics g)
        {
            //Ось
            g.DrawLine(Pens.Black, start, end);
            //Стрелка
            g.DrawLines(Pens.Black, GetArrow(start.X, start.Y, end.X, end.Y, ARR_LEN));
        }

        //Рисование текста
        private void DrawText(Point point, string text, Graphics g, bool isYAxis = false)
        {
          
        }
       
        //Вычисление стрелки оси
        private static PointF[] GetArrow(float x1, float y1, float x2, float y2, float len = 10, float width = 4)
        {
             PointF[] result = new PointF[3];
            //направляющий вектор отрезка
            var n = new PointF(x2 - x1, y2 - y1);
            //Длина отрезка
            var l = (float)Math.Sqrt(n.X * n.X + n.Y * n.Y);
            //Единичный вектор
            var v1 = new PointF(n.X / l, n.Y / l);
            //Длина стрелки
            n.X = x2 - v1.X * len;
            n.Y = y2 - v1.Y * len;
            result[0] = new PointF(n.X + v1.Y * width, n.Y - v1.X * width);
            result[1] = new PointF(x2, y2);
            result[2] = new PointF(n.X - v1.Y * width, n.Y + v1.X * width);
            return result;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }
    }
}
