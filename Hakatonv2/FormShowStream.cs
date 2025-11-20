using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Hakatonv2
{
    public partial class FormShowStream : Form
    {
        private ImageReceiver imageReceiver;
        private System.Windows.Forms.Timer imageUpdateTimer;

        public FormShowStream()
        {
            InitializeComponent();
            InitializeImageReceiver();
            //ToggleImageReceiver();
        }

        private void InitializeImageReceiver()
        {
            NetworkManager.SendMessage("get_stream_start");
            // Создаем PictureBox для отображения изображений

            btnStartImageReceiver.Click += (s, e) => ToggleImageReceiver();

            imageUpdateTimer = new System.Windows.Forms.Timer();
            imageUpdateTimer.Interval = 100; // 10 FPS
            imageUpdateTimer.Tick += (s, e) => UpdateImageDisplay();
            imageUpdateTimer.Start();

            imageReceiver = new ImageReceiver();
            imageReceiver.OnImageReceived += OnNewImageReceived;
        }

        private void ToggleImageReceiver()
        {
            if (imageReceiver != null)
            {
                imageReceiver.StartImageReceiver();
                btnStartImageReceiver.Text = "Остановить прием";
                btnStartImageReceiver.BackColor = Color.LightGreen;
                pictureBoxPreview.Visible = true;

                Debug.WriteLine("Image receiver started");
            }
        }

        private void OnNewImageReceived(Image image)
        {
            //label1.Text = "получили";
            // Это событие вызывается из другого потока
            if (pictureBoxPreview.InvokeRequired)
            {
                pictureBoxPreview.Invoke(new Action<Image>(OnNewImageReceived), image);
                return;
            }

            // Обновляем PictureBox
            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
            }
            pictureBoxPreview.Image = (Image)image.Clone();
            //btnSaveImage.Enabled = true;
        }

        private void UpdateImageDisplay()
        {
            // Проверяем очередь изображений и обновляем UI
            if (imageReceiver != null && imageReceiver.HasImages())
            {
                Image image = imageReceiver.GetNextImage();
                if (image != null)
                {
                    if (pictureBoxPreview.Image != null)
                    {
                        pictureBoxPreview.Image.Dispose();
                    }
                    pictureBoxPreview.Image = image;
                    //btnSaveImage.Enabled = true;
                }
            }
        }

        private void SaveCurrentImage()
        {
            if (pictureBoxPreview.Image != null)
            {
                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
                saveDialog.FileName = $"quest_screenshot_{DateTime.Now:yyyyMMdd_HHmmss}";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    ImageFormat format = ImageFormat.Jpeg;
                    string ext = Path.GetExtension(saveDialog.FileName).ToLower();

                    if (ext == ".png") format = ImageFormat.Png;
                    else if (ext == ".bmp") format = ImageFormat.Bmp;

                    pictureBoxPreview.Image.Save(saveDialog.FileName, format);
                    MessageBox.Show("Изображение сохранено!", "Успех",
                                  MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            imageReceiver?.StopImageReceiver();
            imageUpdateTimer?.Stop();

            if (pictureBoxPreview.Image != null)
            {
                pictureBoxPreview.Image.Dispose();
            }

            base.OnFormClosing(e);
        }

    }
}
