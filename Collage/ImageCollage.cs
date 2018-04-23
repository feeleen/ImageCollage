using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Collage
{
	static class ImageCollage
	{
		public static Bitmap GenerateCollage(string[] fileNames, int width, int height, CollageType collageType)
		{
			if (fileNames == null)
				throw new Exception("You should specify file names!");

			switch (collageType)
			{
				case CollageType.Three:
					if (fileNames.Length >= 3)
					{
						/// collage map:
						/// 1   2
						/// 1   3

						var canvas = new Bitmap(width, height);

						// set background
						using (Graphics gfx = Graphics.FromImage(canvas))
						{
							using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
							{
								gfx.FillRectangle(brush, 0, 0, canvas.Width, canvas.Height);
							}
						}

						Size one = new Size(Convert.ToInt32(width / 2), height);
						Size two = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height / 2));
						Size three = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height / 2));

						canvas = DrawResizeImage(Image.FromFile(fileNames[0]), canvas, one, new Rectangle(0, 0, one.Width, one.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[1]), canvas, two, new Rectangle(one.Width, 0, two.Width, two.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[2]), canvas, three, new Rectangle(one.Width, two.Height, three.Width, three.Height));

						return canvas;
					}
					break;
				case CollageType.Four:
					if (fileNames.Length >= 4)
					{
						/// collage map:
						/// 1   2
						/// 4   3

						var canvas = new Bitmap(width, height);

						// set background
						using (Graphics gfx = Graphics.FromImage(canvas))
						{
							using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
							{
								gfx.FillRectangle(brush, 0, 0, canvas.Width, canvas.Height);
							}
						}

						Size one = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height/ 2));
						Size two = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height / 2));
						Size three = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height / 2));
						Size four = new Size(Convert.ToInt32(width / 2), Convert.ToInt32(height / 2));

						canvas = DrawResizeImage(Image.FromFile(fileNames[0]), canvas, one, new Rectangle(0, 0, one.Width, one.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[1]), canvas, two, new Rectangle(one.Width, 0, two.Width, two.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[2]), canvas, three, new Rectangle(one.Width, two.Height, three.Width, three.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[3]), canvas, four, new Rectangle(0, one.Height, four.Width, four.Height));

						return canvas;
					}
					break;
				case CollageType.Five:
					if (fileNames.Length >= 4)
					{
						/// collage map:
						/// 1  5  2
						/// 4  5  3

						var canvas = new Bitmap(width, height);

						// set background
						using (Graphics gfx = Graphics.FromImage(canvas))
						{
							using (SolidBrush brush = new SolidBrush(Color.FromArgb(0, 0, 0)))
							{
								gfx.FillRectangle(brush, 0, 0, canvas.Width, canvas.Height);
							}
						}

						Size one = new Size(Convert.ToInt32(width / 3), Convert.ToInt32(height / 2));
						Size two = new Size(Convert.ToInt32(width / 3), Convert.ToInt32(height / 2));
						Size three = new Size(Convert.ToInt32(width / 3), Convert.ToInt32(height / 2));
						Size four = new Size(Convert.ToInt32(width / 3), Convert.ToInt32(height / 2));
						Size five = new Size(Convert.ToInt32(width / 3), height);

						canvas = DrawResizeImage(Image.FromFile(fileNames[0]), canvas, one, new Rectangle(0, 0, one.Width, one.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[1]), canvas, two, new Rectangle(one.Width + five.Width, 0, two.Width, two.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[2]), canvas, three, new Rectangle(one.Width + five.Width, two.Height, three.Width, three.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[3]), canvas, four, new Rectangle(0, one.Height, four.Width, four.Height));
						canvas = DrawResizeImage(Image.FromFile(fileNames[4]), canvas, four, new Rectangle(one.Width, 0, five.Width, five.Height));

						return canvas;
					}
					break;
				default:
					return null;
			}
			return null;
		}

		public static Bitmap DrawResizeImage(Image sourceImage, Bitmap canvas, Size destinationSize, Rectangle destinationRectangle)
		{
			var originalWidth = sourceImage.Width;
			var originalHeight = sourceImage.Height;

			var hRatio = (float)originalHeight / destinationSize.Height;
			var wRatio = (float)originalWidth / destinationSize.Width;

			var ratio = Math.Min(hRatio, wRatio);

			var hScale = Convert.ToInt32(destinationSize.Height * ratio);
			var wScale = Convert.ToInt32(destinationSize.Width * ratio);

			var startX = (originalWidth - wScale) / 2;
			var startY = (originalHeight - hScale) / 2;

			var sourceRectangle = new Rectangle(startX, startY, wScale, hScale);

			using (var g = Graphics.FromImage(canvas))
			{
				g.SmoothingMode = SmoothingMode.HighQuality;
				g.InterpolationMode = InterpolationMode.HighQualityBicubic;
				g.DrawImage(sourceImage, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
			}

			return canvas;

		}

		public static byte[] GetBytesOfImage(Image img)
		{
			ImageConverter converter = new ImageConverter();
			return (byte[])converter.ConvertTo(img, typeof(byte[]));
		}
	}

	public enum CollageType
	{
		Three = 3,
		Four = 4,
		Five = 5
	}
}
