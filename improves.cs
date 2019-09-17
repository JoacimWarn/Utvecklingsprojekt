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
        int count = 0;

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

        private Rect[] DetectFaces(Mat image)
        {
            Rect[] faces = face_cascade.DetectMultiScale(image, 1.3, 5);
            return faces;
        }

        private void MarkFeatures(Mat image)
        {
            foreach (Rect face in Faces)
            {
                Cv2.Rectangle(image, face, new Scalar(0, 255, 0), thickness: 1);
                var face_region = image[face];
            }
        }

        public void DetectFeatures()
        {
            Mat image;
            while (true)
            {
                image = GrabFrame();
                if (image.Empty()) continue;
                if (count >= 15)
                {
                    Mat gray = ConvertGrayScale(image);
                    Rect[] faces = DetectFaces(gray);
                    Faces.Clear();
                    if (faces.Length > 0)
                    {
                        count = 0;
                        foreach (var item in faces)
                        {
                            Faces.Add(item);
                        }
                    }
                }
                MarkFeatures(image);
                Cv2.ImShow("frame", image);
                if (Cv2.WaitKey(13) == (int)ConsoleKey.Escape) break;
                count++;
            }
        }

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
            program.DetectFeatures();
            program.Release();
        }
    }
}
