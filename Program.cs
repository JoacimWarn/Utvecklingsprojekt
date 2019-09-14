using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using OpenCvSharp;
using OpenCvSharp.Extensions;


namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            bool isCameraRunning = false;

            Cv2.NamedWindow("hej");

            var bild = new VideoCapture(0);
            Mat frame = new Mat();

            //private Thread camera;

            bild.Open(0);
            bild.Read(frame);

            while (Cv2.WaitKey(16) != 27)
            {

                bild.Read(frame);

                Cv2.ImShow("hej", frame);
               //Cv2.ImShow("hej", frame);

            }
            
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            Console.WriteLine("Hello World!");

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
