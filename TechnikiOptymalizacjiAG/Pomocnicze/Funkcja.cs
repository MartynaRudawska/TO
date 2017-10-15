using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Math;

namespace TechnikiOptymalizacjiAG.Pomocnicze
{
    class Funkcja
    {
        double[] Dziedzina;
        int a, b, c;
        double y,x, yMin;



        ///<summary>
        ///Banalny sprawdzający
        ///</summary>
        ///<returns></returns>
        public Tuple<double, double> Banalny()
        {
            //y=2*x^2+x-2

            y = Math.Pow(x, 2) * 2 + x - 2;   
            return new Tuple<double, double>(yMin, y);
        }

        /// <summary>
        /// przykład funkcji kwadratowej Sin Cos
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> FKwadSinCos()
        {
            //f(x) = x^2+sin(3 cos(5 x))
            y = Math.Pow(x, 2) + Math.Sin(3 * Math.Cos(5 * x));
            Dziedzina = new double[] { -1, 1 } ;
            return new Tuple<double, double>(yMin, y);
            // return yMin;
        }

        /// <summary>
        /// Przykład funkcji wielomianowej
        /// </summary>
        /// <returns>Tuple of xMin, gMin</returns>
        public Tuple<double, double> FWielom()
        {
            //g(x)= x^4+x^3-7x^2-5x+10
            y = Math.Pow(x, 4) + Math.Pow(x, 3) - 4 * Math.Pow(x, 2) - 3 * x + 5;
            return new Tuple<double, double>(yMin, y);
            //return gMin;
        }

        /// <summary>
        /// Przykład funkcji sin
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> FSin()
        {
            //h(x) = sin(2 x)+ln(x^2)
            y = Math.Sin(2 * x) + Math.Log(Math.Pow(x, 2));
            return new Tuple<double, double>(yMin, y);
        }

        /// <summary>
        /// Przykład funkcji Logarytmicznej
        /// </summary>
        /// <returns></returns>
        public Tuple<double, double> FLogarytmiczne()
        {
            //p(x) = abs(log_{10}(x^2))
            y = Math.Abs(Math.Log10(Math.Pow(x, 2))) + 0.03;
            return new Tuple<double, double>(yMin, y);
            //return yMin;
        }
    }
    class Przystosowanie
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public double fitness(int[] t, int n) // funkcja przystosowania
        {
            double s = 0;
            for (int i = 0; i < n; i++)
            {
                s += t[i];
            }
            return s;
        }
    }

    class Funkcje
    {
        //miejsce zerowe
        //f(x)=ax^2+bx+c gdzie a->nie 0      del=b^2-4ac

        double dell, a, b, c;
        double x0, x1, x2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dell"></param>
        /// <returns></returns>
        public double Del(double dell)
        {
            dell = Math.Pow(b, 2) - 4 * a * c;
            return dell;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dell">'delta' - discriminant</param>
        /// <returns>Tuple of x1,x2</returns>
        public Tuple<double, double> MiejsceZer(double dell)
        {
            if (dell < 0)
            {
                return new Tuple<double, double>(-1, -1); // nie ma miejsca zerowego w R
            }
            else if (dell == 0)
            {
                x0 = -(b / (2 * a));
                return new Tuple<double, double>(x0, x0);
            }
            else
            {
                x1 = (-b + Math.Sqrt(dell)) / 2 * a;
                x2 = (-b - Math.Sqrt(dell)) / 2 * a;
                return new Tuple<double, double>(x1, x2);
            }
        }
    }
}
