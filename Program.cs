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
        VideoCapture videoCapture;
        CascadeClassifier face_cascade;
        CascadeClassifier eyes_cascade;
        List<FaceFeature> features = new List<FaceFeature>();

        class FaceFeature
        {
            public Rect Face { get; set; }
            public Rect[] Eyes { get; set; }
        }

        public void Init()
        {
            videoCapture = new VideoCapture(0);
            face_cascade = new CascadeClassifier("C:/Users/Laptop/Desktop/facedetection/haarcascade_frontalface_default.xml");
            eyes_cascade = new CascadeClassifier("C:/Users/Laptop/Desktop/facedetection/haarcascade_eye.xml");
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

        private Rect[] DetectEyes(Mat image)
        {
            Rect[] eyes = eyes_cascade.DetectMultiScale(image);
            return eyes;
        }

        private void MarkFeatures(Mat image)
        {
            foreach (FaceFeature feature in features)
            {
                Cv2.Rectangle(image, feature.Face, new Scalar(0, 255, 0), thickness: 1);
                var face_region = image[feature.Face];
                foreach (var eye in feature.Eyes)
                {
                    Cv2.Rectangle(face_region, eye, new Scalar(255, 0, 0), thickness: 1);
                }
            }
        }

        public void DetectFeatures()
        {
            Mat image;
            while (true)
            {
                image = GrabFrame();
                Mat gray = ConvertGrayScale(image);
                Rect[] faces = DetectFaces(gray);
                if (image.Empty())
                    continue;
                features.Clear();
                foreach (var item in faces)
                {
                    Mat face_roi = gray[item];

                    Rect[] eyes = DetectEyes(face_roi);

                    features.Add(new FaceFeature()
                    {
                        Face = item,
                        Eyes = eyes
                    });
                }
                MarkFeatures(image);
                Cv2.ImShow("frame", image);
                if (Cv2.WaitKey(1) == (int)ConsoleKey.Escape)
                    break;
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