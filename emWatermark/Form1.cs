using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace emWatermark
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string imgPath= textBox1.Text;
            string watermark= textBox2.Text;
            if (imgPath !=""&& watermark!="")
            {
                try
                {
                    DirectoryInfo folder = new DirectoryInfo(imgPath);
                    foreach (FileInfo file in folder.GetFiles("*.*", SearchOption.AllDirectories))
                    {
                        // 加水印
                        using (var img = Image.FromStream(File.OpenRead($"{imgPath}\\{file}")))
                        {
                            using (var graphic = Graphics.FromImage(img))
                            {
                                var font = new Font("微软雅黑", 20, FontStyle.Bold, GraphicsUnit.Pixel);
                                var color = Color.FromArgb(128, 255, 255, 255);
                                var brush = new SolidBrush(color);
                                var point = new Point(img.Width - 130, img.Height - 50);
                                graphic.DrawString(watermark, font, brush, point);
                                if (!Directory.Exists($"{imgPath}\\emWatermark\\"))
                                    Directory.CreateDirectory($"{imgPath}\\emWatermark\\");
                                var sdk = img.RawFormat;
                                img.Save($"{imgPath}\\emWatermark\\{file}", img.RawFormat);
                            }

                        }


                    }

                }
                catch (Exception ex)
                {

                   MessageBox.Show($"异常信息={ex.Message}");
                }
               


            }
            else
            {
                MessageBox.Show("目录和水印不可为空！");
            }
           


        }
    }
}
