using GameLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class ViewSample : MonoBehaviour
    {
		[Header("�ӽǲ���")]
		[SerializeField]
		private ViewParams viewParams;

		[Header("�ӽ������λ��")]
        [SerializeField]
        private Transform camPos;

        [Header("�ɽ�������")]
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
