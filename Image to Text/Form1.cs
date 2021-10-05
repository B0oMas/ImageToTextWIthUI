using System;
using System.Windows.Forms;
using IronOcr;
using System.IO;

namespace Image_to_Text
{
    public partial class Form1 : Form
    {
        public static string logPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Tekstas.txt");

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            openFileDialog1.InitialDirectory = path;
            openFileDialog1.Filter = "Image files (*.png, *.jpg)|*.png;*.jpg";
            openFileDialog1.FilterIndex = 0;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = openFileDialog1.FileName.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var Ocr = new IronTesseract();
            Ocr.Language = OcrLanguage.Lithuanian;
            using (var Input = new OcrInput(textBox1.Text))

                if (checkBox1.Checked == true && checkBox2.Checked == false)
                {
                    Input.Deskew();  // use if image not straight
                    var Result = Ocr.Read(Input);
                    File.WriteAllText(logPath, Result.Text);
                }

                else if (checkBox1.Checked == false && checkBox2.Checked == true)
                {
                    Input.DeNoise(); // use if image contains digital noise
                    var Result = Ocr.Read(Input);
                    File.WriteAllText(logPath, Result.Text);
                }

                else if (checkBox1.Checked == true && checkBox2.Checked == true)
                {
                    Input.Deskew();  // use if image not straight
                    Input.DeNoise(); // use if image contains digital noise
                    var Result = Ocr.Read(Input);
                    File.WriteAllText(logPath, Result.Text);
                }

                else if (checkBox1.Checked == false && checkBox2.Checked == false)
                {
                    var Result = Ocr.Read(Input);
                    File.WriteAllText(logPath, Result.Text);
                }
            MessageBox.Show("Baigta konvertuoti");
        }
    }
}
