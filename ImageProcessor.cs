using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Drawing;
using System.Drawing.Imaging;

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
    }
}