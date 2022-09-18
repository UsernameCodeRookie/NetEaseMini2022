using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace GameLogic
{
	[Serializable]
	public struct ViewParams
	{
		[Header("ÊÓ½ÇÒÆ¶¯ºÄÊ±")] public float totalTime;
	}

	public class View
	{
		public Transform _camPos;

		private List<ViewHit> _viewHits;

		private ViewParams _viewParams;

		private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

		public View(Transform camPos, List<ViewHit> viewHits, ViewParams viewParams)
		{
			_camPos = camPos;
			_viewHits = viewHits;
			_viewParams = viewParams;
		}

		public void Start()
		{

		}

		#region Hit



		#endregion

		#region Move
		private static async UniTaskVoid Move(View fromView, View toView, PlayerCamera playerCamera)
		{
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			var token = cancellationTokenSource.Token;
			await MoveSliderAsync(fromView._camPos.position, toView._camPos.position, playerCamera, 1.0f, token);
		}

		public async UniTaskVoid MoveTo(View toView, PlayerCamera playerCamera)
		{
			var token = _cancellationTokenSource.Token;
			await MoveSliderAsync(this._camPos.position, toView._camPos.position, playerCamera, _viewParams.totalTime, token);
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
	}
}
