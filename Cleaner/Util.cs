using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Cleaner
{
	internal static class Util
	{
		[ThreadStatic]
		private static Random _random;

		[DllImport("gdi32.dll")]
		private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
		private static PrivateFontCollection _pfc;
		private static uint _cFonts = 0;
		private static Font _font;

		private static void AddFont(byte[] fontdata)
		{
			System.IntPtr dataPointer;
			
			dataPointer = Marshal.AllocCoTaskMem(fontdata.Length);

			Marshal.Copy(fontdata, 0, dataPointer, (int)fontdata.Length);

			AddFontMemResourceEx(dataPointer, (uint)fontdata.Length, IntPtr.Zero, ref _cFonts);

			_cFonts++;

			_pfc.AddMemoryFont(dataPointer, (int)fontdata.Length);
		}

		public static Random Random()
		{
			if (_random == null)
				_random = new Random(Guid.NewGuid().GetHashCode());
			return _random;
		}

		public static Font Font()
		{
			if (_pfc == null)
			{
				_pfc = new PrivateFontCollection();
				AddFont(Properties.Resources.gamefont);
				_font = new Font(_pfc.Families[0], 14);
			}
			return _font;
		}
	}
}
