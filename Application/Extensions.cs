using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Nubs {
	public static class Extensions {

		public static Rectangle ToRectangle(this RECT r) {
			return new Rectangle(r.Left, r.Top, r.Right - r.Left, r.Bottom - r.Top);
		}

	}
}
