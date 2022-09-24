using Debugger;
using GL;
using UnityEngine;

namespace GL
{
	public class CollisionTrigger : MonoBehaviour
	{
		[SerializeField]
		private PlayerCameraSample _playerCameraSample;

		private void OnTriggerEnter(Collider other)
		{
			TempDebug.Log("CollisionTrigger: OnTriggerEnter");
			var eventTrigger = GetComponent<EventTrigger>();

			eventTrigger.Execute(_playerCameraSample.playerCamera);
		}
	}
}
