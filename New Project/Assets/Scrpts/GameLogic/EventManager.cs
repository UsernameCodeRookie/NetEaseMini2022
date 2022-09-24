

using System;
namespace GL
{
	public static class EventManager
	{
		public static void PlayEventAnimation(EventAnimation eventAnimation, Action callback)
		{
			callback();
		}
	}
}
