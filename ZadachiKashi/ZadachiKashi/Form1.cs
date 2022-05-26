using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace metodEiler
{
    public partial class Form1 : Form
    {
        double n;
        // условия Каши и задание отрезка
        const double x0=1, y0=1, minX = 1, maxX = 2;
        
        static double f1(double x, double y)//Исходное дифференциальное уравнение
        {
            return ((x * x) * y + 2*x) / (2 * x * y * y + y);
        }
        static double f2(double x)//Точное решение задачи Коши 
        {
           return ((Math.Pow(x,2)+Math.Sqrt(Math.Pow(x,4)+8*x))/4);
           
        }

        //точное решение задачи Каши


        private void Form1_Load(object sender, EventArgs e)
        {
      
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Clear(zedGraphControl1);
        }

        // метод очистки и настройки координатной плоскости
        private void Clear(ZedGraphControl Zed_GraphControl)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl1.GraphPane.GraphObjList.Clear();
            zedGraphControl1.GraphPane.XAxis.Type = AxisType.Linear;
            zedGraphControl1.GraphPane.XAxis.Scale.TextLabels = null;
            zedGraphControl1.GraphPane.XAxis.MajorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.MajorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.MinorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.XAxis.MinorGrid.IsVisible = false;
            zedGraphControl1.GraphPane.XAxis.Title.IsVisible = false;
            zedGraphControl1.GraphPane.YAxis.Title.IsVisible = false;
            zedGraphControl1.GraphPane.Title.IsVisible = false;
            zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }
        // построение графиков



        //public void build(ZedGraphControl Zed_GraphControl)
        //{

        //    PointPairList list = new PointPairList();
        //    PointPairList reallist = new PointPairList();

        //    double h, h1, x=x0, y=y0;
        //    double nowE, maxE = 0;
        //    h = (maxX - minX) / (n-1);
        //    h1 = (double)(maxX - minX) / 1000D;
        //    // нахождение точек точной функции
        //    while (x < x0+h1*1000D)
        //    {
        //        reallist.Add(x, f1(x));
        //        x += h1;
        //    }
        //    // нахождение точек приближённой функции
        //    x = x0;
        //    while (x < x0 + h * n)
        //    {
        //        list.Add(x, y);
        //        // вычисление максимальной невязки
        //        nowE = Math.Abs(y - f2(x));
        //        if (nowE > maxE)
        //            maxE = nowE;

        //        y += h * f1(x, y);
        //        x += h;
        //    }

        //    textBox3.Text = Convert.ToString(maxE);
        //    // отрисовка графиков функций
        //    GraphPane my_Pane = Zed_GraphControl.GraphPane;
        //    LineItem myCircle = my_Pane.AddCurve("Приближённое решение", list, Color.Red, SymbolType.None);
        //    LineItem myCircle2 = my_Pane.AddCurve("Точное решение", reallist, Color.Black, SymbolType.None);
        //    zedGraphControl1.AxisChange();
        //    zedGraphControl1.Invalidate();
        //}
        public Form1()
        {
            InitializeComponent();
            Clear(zedGraphControl1);
        }
        // при нажатии на кнопку расчёт
        private void button1_Click(object sender, EventArgs ee)
        {
            if (textBox6.Text != "")
                try
                { n = Convert.ToDouble(textBox6.Text); }
                catch { return; }
                Clear(zedGraphControl1);
                build(zedGraphControl1);      
        }

        private void build(ZedGraphControl Zed_GraphControl)
        {
            GraphPane my_Pane = Zed_GraphControl.GraphPane;
           //?????????????????
            double a = 1, b = 2;
            double h = (b - a) / n;
            List<double> EilerX = new List<double> { };
            List<double> EilerY = new List<double> { };
            PointPairList listEiler = new PointPairList();
            double xE=1; 
            double yE=1;
            string nameE = "N = " + n.ToString();
            for (int i = 0; i < n + 1; i++)
            {
                EilerX.Add(xE);
                EilerY.Add(yE);
                listEiler.Add(xE, yE);
                yE = yE + h * f1(xE, yE); //делаем шаг
                xE += h;
            }
          
            PointPairList listReal = new PointPairList();
            List<double> realX = new List<double>((int)n);
            List<double> realY = new List<double>((int)n);
          
          //  string nameR = "N = " + n.ToString();
           //good 
            for (int i = 10; i < n + 1; i++)
            {
                if(i*h >= a && i*h <= b)
                {
                    realX.Add(i * h);
                    realY.Add(f2(realX[i]));

                    listReal.Add(realX[i], realY[i]);
                }
                else
                {
                    realX.Add(0);
                    realY.Add(0);
                }
       
            }
          //sr 1-3 cht 1-4
            LineItem myCircle = my_Pane.AddCurve("Приближённое решение", listEiler, Color.Red, SymbolType.None);
            LineItem myCircle2 = my_Pane.AddCurve("Точное решение", listReal, Color.Black, SymbolType.None);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();

            double maxNevyaz = 0, currentNevyaz = 0;
            for (int i = 0; i < n; i++)
            {
                currentNevyaz = Math.Abs(EilerY[i] - realY[i]);
                if (currentNevyaz > maxNevyaz)
                    maxNevyaz = currentNevyaz;
            }
            textBox3.Text = Convert.ToString(maxNevyaz);
        }

    }
}
