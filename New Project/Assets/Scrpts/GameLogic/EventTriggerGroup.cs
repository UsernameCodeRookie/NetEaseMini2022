using System.Collections.Generic;
using UnityEngine;

namespace GL
{
	public class EventTriggerGroup : EventTrigger
	{
		[SerializeField]
		private List<EventTrigger> _triggers;

		private void Start()
		{
			foreach(Transform child in transform)
			{
				var eventTrigger = child.GetComponent<EventTrigger>();
				if (eventTrigger && !_triggers.Contains(eventTrigger))
				{
					_triggers.Add(eventTrigger);
				}
			}
		}

		public override void Execute(PlayerCamera playerCamera)
		{
			foreach(var trigger in _triggers)
			{
				trigger.Execute(playerCamera);
			}
		}

	}
}
