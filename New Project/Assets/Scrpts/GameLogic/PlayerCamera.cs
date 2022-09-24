using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Debugger;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

namespace GL
{
    public class PlayerCamera
    {
        private RectTransform _rect;

        private Camera _camera;

		private int _layerMask;

		private CancellationTokenSource _tokenSource;

		public PlayerCamera(RectTransform rect, Camera camera, int layer)
		{
			_rect = rect;
			_camera = camera;
			_layerMask = 1 << layer;
		}

		public void Init()
		{
			_tokenSource = new CancellationTokenSource();
			StartCheckHit();
		}

		#region Hit

		private void StartCheckHit()
		{
			CheckPlayerClick().ForEachAsync((pos) => 
			{
				TempDebug.Log("PLayerCamera: Player Have Clicked");

				Ray ray = _camera.ScreenPointToRay(pos);

				RaycastHit[] raycastHits = new RaycastHit[1];
				if(Physics.RaycastNonAlloc(ray, raycastHits, Mathf.Infinity, _layerMask) > 0)
				{
					// Raycast Successfully
					TempDebug.Log("PlayerCamera: Raycast Successfully");

					var hit = raycastHits[0].transform;

					var eventTrigger = hit.GetComponent<EventTrigger>();
					if(eventTrigger)
					{
						eventTrigger.Execute(this);
					}
				}

			}, _tokenSource.Token).Forget();
		}

		private void CancelCheckHit()
		{
			_tokenSource.Cancel();
			_tokenSource.Dispose();
			_tokenSource = new CancellationTokenSource();
		}

		private IUniTaskAsyncEnumerable<Vector3> CheckPlayerClick()
		{
			return UniTaskAsyncEnumerable.Create<Vector3>(async (writer, token) =>
			{
				await UniTask.Yield();
				while(!token.IsCancellationRequested)
				{
					if(Input.GetMouseButtonDown(0))
					{
						// Player Clicked
						await writer.YieldAsync(MainCameraMousePosToLevelCameraMousePos());
					}
					await UniTask.Yield();
				}
			});
		}

		public Vector3 MainCameraMousePosToLevelCameraMousePos()
		{
			// Screen Point To Local Point In Rectangle
			Vector2 localPoint;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(_rect, Input.mousePosition, null, out localPoint);

			// Local Point To Screen Point
			float x = localPoint.x / _rect.rect.width * 1024 + 1024 / 2f;
			float y = localPoint.y / _rect.rect.height * 1024 + 1024 / 2f;

			return new Vector3(x, y, 0);
		}

		#endregion

		#region Util

		public Ray GetMouseToRay()
		{
			var pos = MainCameraMousePosToLevelCameraMousePos();
			return _camera.ScreenPointToRay(pos);
		}

		public Vector3 GetCameraForward()
		{
			return _camera.transform.forward;
		}

		#endregion
	}
}
