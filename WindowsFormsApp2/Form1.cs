using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {

        public double inToM(double inc)
        {
            return inc * 0.0254;
        }
        public double mToIn(double inc)
        {
            return inc / 0.0254;
        }
        public Form1()
        {
            InitializeComponent();
            chart1.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart2.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart3.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart4.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart5.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            // 0 is plunger
            // 1 is air pressure
            // 2 is dart
            double pi = 3.1415926;
            double bLen = inToM(9);
            double gasC = inToM(3);
            double dartx = inToM(3);
            double dartxo = inToM(3);
            double dartv = 0;
            double dartvo = 0;
            double plungerx = inToM(0.5);
            double plungerxo = inToM(0.5);
            double gasR = inToM(0.75);
            double barrelR = inToM(0.25);
            double plungerA = pi * gasR * gasR;
            double dartA = pi * barrelR * barrelR;
            double k = 35;
            double dMass = 0.001;
            double pMass = 1.9;
            double plungerv = 0;
            double timescale = 100000;
            double plungervo = 0;
            double aVol = plungerA * (gasC - plungerx) + (dartx - inToM(3)) * dartA;
            double air = 101325; //1 ATM
            double PV = 101325 * inToM(2.5) * plungerA;
            double airo = 101325; //1 ATM
            double stp = 101325;
            Console.WriteLine(100000 / timescale);
            Console.WriteLine("TankVolume: " + (inToM(2.5) * plungerA) + " tube volume: " + (inToM(9) * dartA));
            for (long i = 0; i < 200000; i++)
            {
                //Plunger velocity     pretension, spring force     air force pushign back devide all by mass then 1000 by timescale
                    plungerv += ((k * (gasC+inToM(1) - plungerx) - (air - stp) * plungerA) / pMass) / timescale;
                // Plunger position is just velocity / 1000 timescale
                    plungerx += plungervo / timescale;
                    //if (plungerx > 0.07)
                        //Console.WriteLine("hi");

                if (plungerx >= inToM(3))
                {
                    plungerx = inToM(3);
                }
                air = PV / (plungerA * (gasC - plungerxo) + (dartxo - inToM(3)) * dartA);
                dartv += ((airo - stp) * dartA / dMass)/ timescale;
                dartx += dartvo / timescale;

                dartvo = dartv;
                dartxo = dartx;
                airo = air;
                plungervo = plungerv;
                plungerxo = plungerx;

                //Console.WriteLine(plungerx);
                //chart1.Series[0].Points.AddXY( plungerx ,i/1000);
                chart1.Series[0].Points.AddXY(i/timescale, mToIn(plungerx) - 0.5);
                chart2.Series[0].Points.AddXY(i / timescale, air/stp );
                chart3.Series[0].Points.AddXY(i / timescale, mToIn(dartx) - 3 );
                chart4.Series[0].Points.AddXY(i / timescale, dartv);
                chart5.Series[0].Points.AddXY(i / timescale, (airo - stp) * dartA / dMass);
                if (dartx  - inToM(3) > bLen) break;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }
    }
}
