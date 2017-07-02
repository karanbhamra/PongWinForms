using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pong
{
	public partial class Form1 : Form
	{
		Shape border;
		Shape ball;
		Shape userPaddle;
		Shape secondPaddle;
		Graphics g;
		Bitmap canvas;
		Pen p;
		Brush b;
		int formWidth;
		int formHeight;
		//bool playagain = false;
		Random rand = new Random();
		int player1Score;
		int player2Score;

		public int randomDirection(int speed)
		{
			int num = rand.Next(0, 2);
			//Console.WriteLine(num);
			if (num % 2 == 0)
			{
				return -speed;
			}
			else
			{
				return speed;
			}
		}

		public Form1()
		{
			InitializeComponent();

			formWidth = ClientSize.Width;
			formHeight = ClientSize.Height;
			player1Score = player2Score = 0;
			this.Text = $"Pong U1: {player1Score} U2: {player2Score}";
			// graphics init
			g = this.CreateGraphics();
			b = new SolidBrush(Color.Black);
			p = new Pen(b);
			canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
			g = Graphics.FromImage(canvas); // draw on picturebox because controls are double buffered (reduce flickering)

			// shapes init
			p.Width = 5;
			border = new Shape(0, 0, formWidth - 1, formHeight - 1, 0, 0);
			ball = new Shape(125, 125, 10, 10, randomDirection(1), randomDirection(1));
			userPaddle = new Shape(10, formHeight / 2, 3, 30, 10, 10);
			secondPaddle = new Shape(formWidth - 15, formHeight / 2, 3, 30, 10, 10);

		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{


		}
		public bool SecondPaddleXCollision()
		{
			return (ball.X + ball.Width >= secondPaddle.X && ball.X + ball.Width <= secondPaddle.X + secondPaddle.Width);

		}

		public bool SecondPaddleYCollision()
		{
			return (ball.Y >= secondPaddle.Y) && (ball.Y <= secondPaddle.Y + secondPaddle.Height);

		}

		public bool LeftPaddleXCollision()
		{
			return (ball.X >= userPaddle.X && ball.X <= userPaddle.X + userPaddle.Width);
		}

		public bool LeftPaddleYCollision()
		{
			return ((ball.Y > userPaddle.Y) && (ball.Y < userPaddle.Y + userPaddle.Height));

		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			g.Clear(Control.DefaultBackColor);


			// ball bouncing off wall and paddle
			// first check the x position of the ball with paddle
			if (LeftPaddleXCollision())
			{
				if (LeftPaddleYCollision())
				{
					Console.WriteLine("within y location");
					ball.XSpeed *= -1;
				}
			}

			if (SecondPaddleXCollision())
			{
				Console.WriteLine("within x");
				if (SecondPaddleYCollision())
				{
					Console.WriteLine("Collision");
					ball.XSpeed *= -1;
				}
			}

			// bounce off top and bottom
			if (ball.Y + ball.Height > formHeight - 3 || ball.Y < 2)
			{
				ball.YSpeed *= -1;
			}

			// ball goes out of the edges
			if (ball.X < 0 || ball.X + ball.Width > formWidth)
			{
				Shape.DrawText(g, "Game over", 14, Color.Red, formWidth / 2, formHeight / 2);
				if (ball.X < formWidth / 2)
				{
					player2Score++;
				}
				if (ball.X > formWidth / 2)
				{
					player1Score++;
				}
				ball.X = 125;
				ball.Y = 125;
				ball.XSpeed = 0;
				ball.YSpeed = 0;

				timer1.Stop();
			}

			// ball moving
			ball.X += ball.XSpeed;
			ball.Y += ball.YSpeed;

			//drawing
			border.DrawRectangle(g, p);
			ball.DrawEllipse(g, p, true);
			userPaddle.DrawRectangle(g, p);
			secondPaddle.DrawRectangle(g, p);
			this.Text = $"Pong U1: {player1Score} U2: {player2Score}";

			pictureBox1.Image = canvas;
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			// restart game when space is pressed
			if (e.KeyCode == Keys.Space)
			{
				timer1.Start();
				ball.X = 125;
				ball.Y = 125;
				ball.XSpeed = randomDirection(1);
				ball.YSpeed = randomDirection(1);
				secondPaddle.Y = formHeight / 2;
				userPaddle.Y = formHeight / 2;
			}
			if (e.KeyCode == Keys.Up)
			{
				if (secondPaddle.Y > 5)
				{
					secondPaddle.Y -= secondPaddle.YSpeed;

				}

			}
			if (e.KeyCode == Keys.Down)
			{
				if (secondPaddle.Y + secondPaddle.Height < formHeight - 10)
				{
					secondPaddle.Y += secondPaddle.YSpeed;
				}

			}
			if (e.KeyCode == Keys.W)
			{
				if (userPaddle.Y > 5)
				{
					userPaddle.Y -= userPaddle.YSpeed;

				}

			}
			if (e.KeyCode == Keys.S)
			{
				if (userPaddle.Y + userPaddle.Height < formHeight - 10)
				{
					userPaddle.Y += userPaddle.YSpeed;
				}

			}
		}
	}
}
