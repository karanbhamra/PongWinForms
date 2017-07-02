using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pong
{
	class Shape
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int XSpeed { get; set; }
		public int YSpeed { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		public Shape(int x, int y, int width, int height, int xspeed, int yspeed)
		{
			X = x;
			Y = y;
			Width = width;
			Height = height;
			XSpeed = xspeed;
			YSpeed = yspeed;

		}

		public void DrawRectangle(Graphics g, Pen p)
		{
			g.DrawRectangle(p, X, Y, Width, Height);
		}

		public void DrawEllipse(Graphics g, Pen p, bool fill = false)
		{
			if (!fill)
			{
				g.DrawEllipse(p, X, Y, Width, Height);
			}
			else
			{
				g.FillEllipse(new SolidBrush(Color.Black), X, Y, Width, Height);
			}
		}

		public static void DrawText(Graphics g, String s, int size, Color c, int X, int Y)
		{
			g.DrawString(s, new Font("Helvetica", size), new SolidBrush(c), X, Y);
		}

	}
}
