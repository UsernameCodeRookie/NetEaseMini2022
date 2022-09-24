using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Debugger;
using GL;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace GL
{
	public class EventDragItem : EventTrigger
	{
		private Transform _transform;

		private PlayerCamera _playerCamera;

		public override void Execute(PlayerCamera playerCamera)
		{
			_playerCamera = playerCamera;
			_transform = this.transform;
			if (Input.GetMouseButton(0))
			{
				OnDrag().Forget();
			}
		}

		private async UniTaskVoid OnDrag()
		{
			TempDebug.Log("EventDragItem: OnDrag");

			bool _isDrag = true;
			var token = new CancellationTokenSource();

			CheckMouseButtonUp().ForEachAsync((b) =>
			{
				if (b)
				{
					_isDrag = false;
				}
			}, token.Token).Forget();

			await UniTask.WaitUntil(() => !_isDrag);
			// Mouse Button Up
			TempDebug.Log("EventDragItem: OffDrag");
			token.Cancel();
		}

		private IUniTaskAsyncEnumerable<bool> CheckMouseButtonUp()
		{
			return UniTaskAsyncEnumerable.Create<bool>(async (writer, token) =>
			{
				await UniTask.Yield();
				while (!token.IsCancellationRequested)
				{
					FollowMouse();
					await writer.YieldAsync(Input.GetMouseButtonUp(0));
					await UniTask.Yield();
				}
			});
		}

		private void FollowMouse()
		{
			//TempDebug.Log("EventDragItem: Follow Mouse");
			_transform.position = GetNewTransformPos();
		}

		private Vector3 GetNewTransformPos()
		{
			Ray ray = _playerCamera.GetMouseToRay();

			// (direction * t).dot(camera.forward) = (transform - origin).dot(camera.forward)

			Vector3 distance = _transform.position - ray.origin;
			Vector3 forward = _playerCamera.GetCameraForward();

			float dis_dot_for = Vector3.Dot(distance, forward);
			float dir_dot_for = Vector3.Dot(ray.direction, forward);

			float mul = dis_dot_for / dir_dot_for;

			Vector3 newPos = ray.origin + ray.direction * mul;

			return newPos;
		}
	}
}