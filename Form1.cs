using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Drawing;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private VideoCapture capture;
    private Mat frame;
    private static readonly HttpClient client = new HttpClient();
    
    public Form1()
    {
        InitializeComponent();
        capture = new VideoCapture(0); // Capture vidéo à partir de la webcam
        frame = new Mat();
    }

    // Initialisation au chargement du formulaire
    private void Form1_Load(object sender, EventArgs e)
    {
        Timer timer = new Timer();
        timer.Interval = 30; // Mise à jour toutes les 30ms (30 FPS)
        timer.Tick += Timer_Tick;
        timer.Start();
    }

    // Méthode pour capturer et afficher l'image
    private void Timer_Tick(object sender, EventArgs e)
    {
        capture.Read(frame); // Lire une frame de la vidéo
        if (!frame.IsEmpty)
        {
            pictureBox.Image = frame.ToImage<Bgr, byte>().ToBitmap(); // Afficher dans un PictureBox
            Task.Run(() => PredictObjectAsync(frame)); // Lancer la prédiction dans un thread séparé
        }
    }

    // Méthode pour envoyer l'image à l'API Python pour la détection d'objets
    private async Task PredictObjectAsync(Mat frame)
    {
        var imageBytes = frame.ToImage<Bgr, byte>().ToJpegData();
        var imageContent = new ByteArrayContent(imageBytes);
        var formData = new MultipartFormDataContent();
        formData.Add(imageContent, "image", "frame.jpg");

        var response = await client.PostAsync("http://localhost:5000/predict", formData);
        var responseString = await response.Content.ReadAsStringAsync();

        // Traiter les résultats de la détection (par exemple, afficher les objets détectés)
        Invoke(new Action(() =>
        {
            labelPrediction.Text = responseString; // Afficher les résultats dans un label
        }));
    }

    // Libération des ressources à la fermeture
  private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    capture.Release(); // Libérer la caméra
    CvInvoke.DestroyAllWindows(); // Fermer les fenêtres OpenCV
}

}
