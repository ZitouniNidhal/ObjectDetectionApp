using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ObjectDetectionApp
{
    public class ImageProcessor
    {
        // Méthode pour convertir un Mat en Bitmap
        public Bitmap MatToBitmap(Mat mat)
        {
            if (mat.IsEmpty)
                return null;

            int width = mat.Width;
            int height = mat.Height;
            Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            var rect = new Rectangle(0, 0, width, height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
            CvInvoke.cvCopy(mat, bmpData.Scan0, IntPtr.Zero);
            bitmap.UnlockBits(bmpData);
            return bitmap;
        }

        // Méthode pour appliquer un filtre sur l'image
        public Mat ApplyGaussianBlur(Mat image)
        {
            Mat result = new Mat();
            CvInvoke.GaussianBlur(image, result, new Size(5, 5), 0);
            return result;
        }

        // Méthode pour convertir une image en niveaux de gris
        public Mat ConvertToGray(Mat image)
        {
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);
            return grayImage;
        }

        // Méthode pour redimensionner une image
        public Mat ResizeImage(Mat image, int width, int height)
        {
            Mat resizedImage = new Mat();
            CvInvoke.Resize(image, resizedImage, new Size(width, height));
            return resizedImage;
        }

        // Méthode pour détecter les bords avec l'algorithme de Canny
        public Mat DetectEdges(Mat image, double threshold1, double threshold2)
        {
            Mat edges = new Mat();
            CvInvoke.Canny(image, edges, threshold1, threshold2);
            return edges;
        }

        // Méthode pour appliquer l'égalisation d'histogramme
        public Mat EqualizeHistogram(Mat image)
        {
            Mat grayImage = ConvertToGray(image);
            Mat equalizedImage = new Mat();
            CvInvoke.EqualizeHist(grayImage, equalizedImage);
            return equalizedImage;
        }
    }
}