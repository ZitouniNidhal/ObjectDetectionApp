using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Net.Http;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ObjectDetectionApp
{
    public partial class Form1 : Form
    {
        private VideoCapture capture;
        private Mat frame;
        private static readonly HttpClient client = new HttpClient();
        private PictureBox pictureBox;
        private Label labelPrediction;

        public Form1()
        {
            InitializeComponent(); // Appel du constructeur généré par le designer

            // Initialisation des composants du formulaire
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.StretchImage
            };
            labelPrediction = new Label
            {
                Dock = DockStyle.Bottom,
                AutoSize = true
            };

            Controls.Add(pictureBox);
            Controls.Add(labelPrediction);

            capture = new VideoCapture(0); // Capture vidéo depuis la webcam
            frame = new Mat();
        }

        // Méthode de chargement du formulaire
        private void Form1_Load(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 30; // Intervalle en millisecondes (par exemple, 30 ms pour ~33 FPS)
            timer.Tick += Timer_Tick;
            timer.Start();
        }
public static Bitmap MatToBitmap(Mat mat)
{
    if (mat.IsEmpty)
        return null;

    // Créer un Bitmap à partir des données du Mat
    int width = mat.Width;
    int height = mat.Height;
    Bitmap bitmap = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);

    // Copier les données de l'image Mat vers le Bitmap
    var rect = new Rectangle(0, 0, width, height);
    BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
    CvInvoke.cvCopy(mat, bmpData.Scan0, IntPtr.Zero);  // Copier les données de l'image dans le Bitmap

    bitmap.UnlockBits(bmpData);
    return bitmap;
}
        // Capture et affichage des frames à chaque "tick" du timer
        private void Timer_Tick(object sender, EventArgs e)
        {
            capture.Read(frame); // Capture une frame de la vidéo
            if (!frame.IsEmpty)
            {
                pictureBox.Image = MatToBitmap(frame); // Afficher l'image dans PictureBox
                Task.Run(() => PredictObjectAsync(frame)); // Lancer la prédiction dans un thread séparé
            }
        }

        // Envoi de l'image à l'API Python pour prédiction d'objets
        private async Task PredictObjectAsync(Mat frame)
        {
            var imageBytes = frame.ToImage<Bgr, byte>().ToJpegData();
            var imageContent = new ByteArrayContent(imageBytes);
            var formData = new MultipartFormDataContent();
            formData.Add(imageContent, "image", "frame.jpg");

            var response = await client.PostAsync("http://localhost:5000/predict", formData);
            var responseString = await response.Content.ReadAsStringAsync();

            // Mise à jour de l'interface utilisateur avec les résultats de la détection
            Invoke(new Action(() =>
            {
                labelPrediction.Text = responseString; // Affichage des résultats dans le Label
            }));
        }

        // Libération des ressources à la fermeture du formulaire
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            capture.Release(); // Libérer la caméra
            CvInvoke.DestroyAllWindows(); // Fermer toutes les fenêtres OpenCV
        }
    }
}
    