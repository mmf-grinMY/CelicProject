using System;

namespace Celic
{
	/// <summary> Description of MineDevManager. </summary>
	public static class MineDevManager
	{
		public static string ToString(MineDev dev)
		{
			switch(dev)
			{
				default:
				case MineDev.undefine: return string.Empty;
				case MineDev.camera: return "камерная";
				case MineDev.lava: return "столбовая";
			}
		}
		public static MineDev ToMineDev(string str)
		{
			switch(str)
			{
				case "камерная": return MineDev.camera;
				case "столбовая": return MineDev.lava;
				default: return MineDev.undefine;
			}
		}
	}
}
