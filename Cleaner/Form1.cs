using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cleaner
{
	public partial class Form1 : Form
	{
		private Timer _timer;
		private Brush _brush;
		private House _house;

		public Form1()
		{
			InitializeComponent();

			MaximizeBox = false;
			FormBorderStyle = FormBorderStyle.FixedSingle;

			_brush = new SolidBrush(Color.Black);
			_house = new House(this);

			DoubleBuffered = true;

			CenterToScreen();
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			_timer = new Timer();
			_timer.Interval = 1000;
			_timer.Tick += Tick;
			_timer.Start();
		}

		public void Draw(Graphics g)
		{
			g.FillRectangle(_brush, 0, 0, Width, Height);

			_house.Robot.Draw(g);
			_house.DrawRooms(g);
		}

		public void Tick(object sender, EventArgs e)
		{
			_house.Robot.Act();
			Invalidate();
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;
			Draw(g);
		}

	}
}
