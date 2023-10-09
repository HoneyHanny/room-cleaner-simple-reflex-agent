using System.Collections.Generic;
using System.Drawing;

namespace Cleaner
{
	internal class House
	{
		private readonly int _roomCount = 4;

		public List<Room> Rooms { get; set; }
		public Robot Robot { get; set; }

		public House(Form1 form1)
		{
			Rooms = new List<Room>();
			for (int i = 0; i < _roomCount; i++)
				Rooms.Add(new Room((Location)i, form1.ClientSize));

			Robot = new Robot(this, form1.ClientSize);
		}
		
		public void DrawRooms(Graphics g)
		{
			foreach (Room room in Rooms)
				room.Draw(g);
		}
	}
}
