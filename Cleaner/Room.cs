using System;
using System.Drawing;
using System.Windows.Forms;

namespace Cleaner
{
	internal class Room
	{
		public int X { get; set; }
		public int Y { get; set; }
		public Status State
		{
			get { return _state; }
			set
			{
				_state = value;
				if (_state == Status.Clean)
				{
					_time = Util.Random().Next(100, 200);
					_timer.Start();
				}
				else if (_state == Status.Dirty)
				{
					_timer.Stop();
				}
			}
		}

		public Location Location
		{
			get { return _location; }
			set
			{
				_location = value;
				switch (_location)
				{
					case Location.A:
						X = Y = 0;
						break;

					case Location.B:
						X = (_size.Width / 2) - 1;
						Y = 0;
						break;

					case Location.C:
						X = 0;
						Y = (_size.Height / 2) - 1;
						break;

					case Location.D:
						X = (_size.Width / 2) - 1;
						Y = (_size.Height / 2) - 1;
						break;
				}
			}
		}

		private Status _state;
		private Location _location;
		private Pen _pen;
		private Brush _brush;
		private Size _size;
		private Font _font;
		private Timer _timer;
		private int _time;

		public Room(Location location, Size size)
		{
			_size = size;
			Location = location;

			_timer = new Timer();
			_timer.Interval = 60;
			_timer.Tick += AccumulateDirt;

			State = (Status) Util.Random().Next(2);

			_brush = new SolidBrush(Color.White);
			_pen = new Pen(Color.White);
			_font = Util.Font();
		}

		public void Draw(Graphics g)
		{
			g.DrawRectangle(_pen, X, Y, _size.Width / 2, _size.Height / 2);
			g.DrawString("ROOM: " + Location.ToString(), _font, _brush, new Point(X, Y));
			g.DrawString("STATUS: " + State.ToString().ToUpper(), _font, _brush, new Point(X, Y + 20));
		}

		private void AccumulateDirt(object sender, EventArgs e)
		{
			_time--;
			if (_time <= 0)
				State = Status.Dirty;
		}
	}
}
