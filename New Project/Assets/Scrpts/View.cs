using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Debugger;

namespace GameLogic
{
	[Serializable]
	public struct ViewParams
	{
		[Header("Slider Cost Time")] public float totalTime;
	}

	public class View : MonoBehaviour
	{
		[SerializeField]
		private Transform _camPos;

		[SerializeField]
		private List<ViewHit> _viewHits;

		[SerializeField]
		private ViewParams _viewParams;

		[SerializeField]
		private PlayerCamera _playerCamera;

		private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		private void Start()
		{
			StartCheckHit();
		}

		#region Hit

		private void StartCheckHit()
		{
			CheckPlayerClick().ForEachAsync((delta) => 
			{
				if(delta.Item1 == true)
				{
					TempDebug.Log("Get Mouse Clicked");

					// Have Clicked
					Ray ray = _playerCamera.cam.ScreenPointToRay(delta.Item2);
					TempDebug.Log(ray.ToString());

					// Raycast Check
					int layerMask = 1 << _playerCamera.layerMask;
					RaycastHit[] raycastHits = new RaycastHit[1];
					if(Physics.RaycastNonAlloc(ray, raycastHits, Mathf.Infinity, layerMask) > 0)
					{
						TempDebug.Log("Raycast Successfully");

						var hit = raycastHits[0].transform;
						/*_viewHits.ForEach((ViewHit v) => 
						{
							if(v.transform == hit)
							{
								TempDebug.Log("Hit Event Invoke");

								v.hitEvent.Invoke();
							}
						});*/
						var viewhit = hit.GetComponent<ViewHit>();
						if (viewhit)
						{
							viewhit.hitEvent.Invoke();
						}
					}
				}
			}, this.GetCancellationTokenOnDestroy()).Forget();
		}

		private IUniTaskAsyncEnumerable<(bool, Vector3)> CheckPlayerClick()
		{
			return UniTaskAsyncEnumerable.Create<(bool, Vector3)>(async (writer, token) =>
			{
				await UniTask.Yield();
				while(!token.IsCancellationRequested)
				{
					await writer.YieldAsync((GetClicked(), GetMousePosition()));
					await UniTask.Yield();
				}
			});
		}

		private bool GetClicked()
		{
			return Input.GetMouseButtonDown(0);
		}

		private Vector3 GetMousePosition()
		{
			var pos = _playerCamera.MainCameraMousePosToLevelCameraMousePos();
			return pos;
		}

		#endregion

		#region Move
		private static async UniTaskVoid Move(View fromView, View toView, PlayerCamera playerCamera)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			var token = cancellationTokenSource.Token;
			await MoveSliderAsync(fromView._camPos.position, toView._camPos.position, playerCamera, 1.0f, token);
		}

		public async UniTaskVoid MoveTo(View toView)
		{
			// old view activate
			this.gameObject.SetActive(false);

			// new view disactivate
			toView.gameObject.SetActive(true);
			_playerCamera.curView = toView;

			// async move slider
			var token = _cancellationTokenSource.Token;
			await MoveSliderAsync(this._camPos.position, toView._camPos.position, _playerCamera, _viewParams.totalTime, token);
		}

		private static async UniTask MoveSliderAsync
			(Vector3 fromPos, Vector3 toPos, PlayerCamera playerCamera, float totalTime, CancellationToken token)
		{
			float useTime = 0;
			while (useTime < totalTime)
			{
				useTime += Time.deltaTime;
				bool result = await UniTask.Yield(PlayerLoopTiming.Update, token).SuppressCancellationThrow();
				if (result)
				{
					return;
				}

				var newPos = fromPos + (toPos - fromPos) * useTime / totalTime;
				playerCamera.MovePosTo(newPos);
			}
		}
		#endregion

		#region Util

		public Vector3 GetCameraPosition()
		{
			return _camPos.position;
		}

		#endregion
	}
}
