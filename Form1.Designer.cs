using Emgu.CV;

namespace ObjectDetectionApp
{
    partial class Form1
    {
        /// <summary>
        /// Variable requise par le concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyer toutes les ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }

            // Libérer les ressources non managées
            if (capture != null)
            {
                capture.Release();
            }
            CvInvoke.DestroyAllWindows();
            base.Dispose(disposing);
        }

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas le contenu de cette méthode avec l'éditeur de code.
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
    }
}
