using System;
using System.Drawing;

namespace Cleaner
{
	internal class Robot
	{
		public Room CurrentRoom
		{
			get { return _currentRoom; }
			set
			{ 
				_currentRoom = value;
				if (_currentRoom.Location == Location.A)
				{
					X = (_size.Width / 4) - (Width / 2);
					Y = (_size.Height / 4) - (Height / 2);
				}
				else if (_currentRoom.Location == Location.B)
				{
					X = (_size.Width * (3F / 4F)) - (Width / 2);
					Y = (_size.Height / 4) - (Height / 2);
				}
				else if (_currentRoom.Location == Location.C)
				{
					X = (_size.Width / 4) - (Width / 2);
					Y = (_size.Height * (3F / 4F)) - (Height / 2);
				}
				else if (_currentRoom.Location == Location.D)
				{
					X = (_size.Width * (3F / 4F)) - (Width / 2);
					Y = (_size.Height * (3F / 4F)) - (Height / 2);
				}
			}
		}

		public float X { get; set; }
		public float Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }

		private Action _action;
		private Pen _pen;
		private Brush _brush;
		private Room _currentRoom;
		private House _house;
		private Size _size;

		public Robot(House house, Size size)
		{
			_pen = new Pen(Color.White);
			_house = house;
			_brush = new SolidBrush(Color.White);
			_size = size;

			Width = 100;
			Height = 100;

			switch ((Location)Util.Random().Next(4))
			{
				case Location.A:
					CurrentRoom = house.Rooms[(int)Location.A];
					break;

				case Location.B:
					CurrentRoom = house.Rooms[(int)Location.B];
					break;

				case Location.C:
					CurrentRoom = house.Rooms[(int)Location.C];
					break;

				case Location.D:
					CurrentRoom = house.Rooms[(int)Location.D];
					break;
			}
		}

		private SizeF GetTextSize(Graphics g, string text)
		{
			Font font = Util.Font();
			return g.MeasureString(text, font);
		}

		public void Draw(Graphics g)
		{
			g.DrawEllipse(_pen, X, Y, Width, Height);
			string str = _action.ToString().ToUpper();
			SizeF size = GetTextSize(g, str);
			g.DrawString(str, Util.Font(), _brush,
					X + (Width / 2) - (size.Width / 2), Y + (Height / 2) - (size.Height / 2));
			g.DrawString(CurrentRoom.Location.ToString(), Util.Font(), _brush, X, Y);
		}

		public void Act()
		{
			if (CurrentRoom.State == Status.Clean)
			{
				Move();
			}
			else
			{
				Clean();
			}
		}

		public void Idle()
		{
			Console.WriteLine("Idling");
			_action = Action.Nop;
		}

		public void Clean()
		{
			CurrentRoom.State = Status.Clean;
			Console.WriteLine("Cleaning");
			_action = Action.Clean;
		}

		public void Move()
		{
			int action = Util.Random().Next(25);
			if (CurrentRoom.Location == Location.A)
			{
				if (action >= 0 && action <= 9)
					Right();
				else if (action >= 10 && action <= 19)
					Down();
				else
					Idle();
			}
			else if (CurrentRoom.Location == Location.B)
			{
				if (action >= 0 && action <= 9)
					Left();
				else if (action >= 10 && action <= 19)
					Down();
				else
					Idle();
			}
			else if (CurrentRoom.Location == Location.C)
			{
				if (action >= 0 && action <= 9)
					Up();
				else if (action >= 10 && action <= 19)
					Right();
				else
					Idle();
			}
			else if (CurrentRoom.Location == Location.D)
			{
				if (action >= 0 && action <= 9)
					Up();
				else if (action >= 10 && action <= 19)
					Left();
				else
					Idle();
			}
		}

		public void Up()
		{
			
			if (CurrentRoom.Location == Location.C)
			{
				CurrentRoom = _house.Rooms[(int)Location.A];
				Console.WriteLine("Up");
				_action = Action.Up;
			}
			else if (_currentRoom.Location == Location.D)
			{
				CurrentRoom = _house.Rooms[(int)Location.B];
				Console.WriteLine("Up");
				_action = Action.Up;
			}
		}

		public void Down()
		{
				
			if (CurrentRoom.Location == Location.A)
			{
				CurrentRoom = _house.Rooms[(int)Location.C];
				Console.WriteLine("Down");
				_action = Action.Down;
			}
			if (CurrentRoom.Location == Location.B)
			{
				CurrentRoom = _house.Rooms[(int)Location.D];
				Console.WriteLine("Down");
				_action = Action.Down;
			}
		}

		public void Left()
		{
				
			if (CurrentRoom.Location == Location.B)
			{
				CurrentRoom = _house.Rooms[(int)Location.A];
				Console.WriteLine("Left");
				_action = Action.Left;
			}
			else if (CurrentRoom.Location == Location.D)
			{
				CurrentRoom = _house.Rooms[(int)Location.C];
				Console.WriteLine("Left");
				_action = Action.Left;
			}
		}

		public void Right()
		{
				
			if (CurrentRoom.Location == Location.A)
			{
				CurrentRoom = _house.Rooms[(int)Location.B];
				Console.WriteLine("Right");
				_action = Action.Right;
			}
			else if (CurrentRoom.Location == Location.C)
			{
				CurrentRoom = _house.Rooms[(int)Location.D];
				Console.WriteLine("Right");
				_action = Action.Right;
			}
		}

	}
}
