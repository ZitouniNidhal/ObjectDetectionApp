using Emgu.CV;

namespace ObjectDetectionApp;

partial class Form1 : System.Windows.Forms.Form
{
    private void Form1_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
    {
        // Add any cleanup code here if needed
    }
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    private Emgu.CV.VideoCapture capture;
    private System.Windows.Forms.PictureBox pictureBox;
    private System.Windows.Forms.Label labelPrediction;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
{
    if (disposing)
    {
        // Libération des ressources managées
        if (components != null)
        {
            components.Dispose();
        }
    }

    // Libération des ressources non managées
    if (capture != null)
    {
        capture.Release(); // Libérer la caméra si elle est utilisée
    }
    CvInvoke.DestroyAllWindows(); // Fermer les fenêtres OpenCV si elles sont ouvertes

    // Appeler la méthode Dispose de la classe de base
    base.Dispose(disposing);
}


    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>

   private void InitializeComponent()
{
    this.pictureBox = new System.Windows.Forms.PictureBox();
    this.labelPrediction = new System.Windows.Forms.Label();
    ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
    this.SuspendLayout();

    // 
    // pictureBox
    // 
    this.pictureBox.Location = new System.Drawing.Point(12, 12);
    this.pictureBox.Name = "pictureBox";
    this.pictureBox.Size = new System.Drawing.Size(640, 480);
    this.pictureBox.TabIndex = 0;
    this.pictureBox.TabStop = false;

    // 
    // labelPrediction
    // 
    this.labelPrediction.Location = new System.Drawing.Point(12, 500);
    this.labelPrediction.Name = "labelPrediction";
    this.labelPrediction.Size = new System.Drawing.Size(640, 50);
    this.labelPrediction.TabIndex = 1;
    this.labelPrediction.Text = "Résultats de détection";

    // 
    // Form1
    // 
    this.ClientSize = new System.Drawing.Size(664, 561);
    this.Controls.Add(this.labelPrediction);
    this.Controls.Add(this.pictureBox);
    this.Name = "Form1";
    this.Text = "Application de Détection d'Objets";
    this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
    this.Load += new System.EventHandler(this.Form1_Load);
    ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
    this.ResumeLayout(false);
}


    #endregion
}
