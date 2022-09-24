using UnityEngine;

namespace GL
{
	public class EventActivate : EventTrigger
	{
		[SerializeField]
		[Header("Activate Target")]
		private GameObject _gameObject;

		public override void Execute(PlayerCamera playerCamera)
		{
			_gameObject.SetActive(true);
		}
	}
}
