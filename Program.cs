using System;
using OpenTK.Input;
using SharpCanvas;
using System.Drawing;
using System.Drawing.Imaging;

namespace Diktator{
    class Program{
        static void Main(string[] args){
            Diktator dik = new Diktator();
            dik.Start();
            dik.LoadQuestion(new Question("7! =", new string[] {"120", "720", "49", "5040"}));
        }
    }
}
