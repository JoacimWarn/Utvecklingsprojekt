using System;
using System.Collections.Generic;
using OpenCvSharp;


namespace ConsoleApp3
{
    class Program
    {
        VideoCapture videoCapture;
        CascadeClassifier face_cascade;
        List<Rect> Faces = new List<Rect>();

        public void Init()
        {
            videoCapture = new VideoCapture(0);
            face_cascade = new CascadeClassifier("C:/Users/Laptop/Desktop/facedetection/haarcascade_frontalface_default.xml");
            //face_cascade = new CascadeClassifier("C:/Users/marcu/source/repos/notvisualbasic/notvisualbasic/haarcascade_frontalface_default.xml");
        }

        public void Release()
        {
            videoCapture.Release();
            Cv2.DestroyAllWindows();
        }

        private Mat GrabFrame()
        {
            Mat image = new Mat();
            videoCapture.Read(image);
            return image;
        }
        private Mat ConvertGrayScale(Mat image)
        {
            Mat gray = new Mat();
            Cv2.CvtColor(image, gray, ColorConversionCodes.BGR2GRAY);
            return gray;
        }

        private Mat ConvertRedScale(Mat image)
        {

            Mat red = new Mat();
            //Cv2.CvtColor(image, red, Cv2.ExtractChannel.ConvertRedScale);
            //Cv2.PSNR
            return red;

        }

        static void Main(string[] args)
        {

            Program program = new Program();

            Mat src = Cv2.ImRead("F:/UtevcklingsProjekt_HT2019/cabins.jpg");

            Mat[] dest = new Mat[3];

            Cv2.Split(src, out dest);

            Cv2.ImWrite("F:/UtevcklingsProjekt_HT2019/Bluecabins.jpg", dest[0]);
            Cv2.ImWrite("F:/UtevcklingsProjekt_HT2019/Greencabins.jpg", dest[1]);
            Cv2.ImWrite("F:/UtevcklingsProjekt_HT2019/Redcabins.jpg", dest[2]);

            //Cv2.ResizeWindow("red");
            
            while (true)
            {

                //Cv2.ImShow("blue", dest[0]);
                //Cv2.ImShow("green", dest[1]);
                Cv2.ImShow("red", dest[2]);
                Cv2.ResizeWindow("red", 1024, 768);

                Cv2.WaitKey(15);

                if (Cv2.WaitKey(13) == (int)ConsoleKey.Escape) break;

                //Cv2.DestroyAllWindows();

                //src = Cv2.ImRead("F:/UtevcklingsProjekt_HT2019/cabins.jpg");

                //dest = program.ConvertGrayScale(src);

                //Cv2.ImShow("frame", dest[2]);
                //Cv2.ImShow("red", rgbchannel[2]);

            }
            

            //Mat[] dest = new Mat[3];
            //Cv2.Split(src, dest);

            //Program program = new Program();
            /*
            while(counter < 500)
            {

                if (Cv2.WaitKey(13) == (int)ConsoleKey.Escape) break;

                counter++;

            }
            */

        }
    }
}
