using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Collage
{
	public partial class Form1 : Form
	{
		string[] filenames = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog1 = new OpenFileDialog();

			openFileDialog1.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
					"|PNG Portable Network Graphics (*.png)|*.png" +
					"|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
					"|BMP Windows Bitmap (*.bmp)|*.bmp" +
					"|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
					"|GIF Graphics Interchange Format (*.gif)|*.gif";
			openFileDialog1.FilterIndex = 1;
			openFileDialog1.RestoreDirectory = true;
			openFileDialog1.Multiselect = true;

					
			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				filenames = openFileDialog1.FileNames;
				pictureBox1.Image = ImageCollage.GenerateCollage(openFileDialog1.FileNames, 600, 400, (CollageType)Enum.Parse(typeof(CollageType), comboBox1.Text));
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if (filenames != null)
			{
				FileInfo f = new FileInfo(filenames[0]);
				string collageFilename = f.Directory.FullName + "\\collage.png";

				pictureBox1.Image.Save(collageFilename, ImageFormat.Png);
				MessageBox.Show("Collage Saved As " + collageFilename);
			}
		}
	}
}
