using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class ViewSample : MonoBehaviour
    {
		[Header("视角参数")]
		[SerializeField]
		private ViewParams viewParams;

		[Header("视角摄像机位置")]
        [SerializeField]
        private Transform camPos;

        [Header("可交互物体")]
        [SerializeField]
        private List<ViewHit> viewHits;

        public View view;

        private void Start()
        {
            view = new View(camPos, viewHits, viewParams);
            view.Start();
        }

        public void MoveTo(ViewSample viewSample, PlayerCamera playerCamera)
        {
            view.MoveTo(viewSample.view, playerCamera).Forget();
        }    


    }
}
