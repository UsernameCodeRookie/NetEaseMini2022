using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class ChangeViewHit : ViewHit
    {
        [SerializeField]
        private ViewSample fromView;

        [SerializeField]
        private ViewSample toView;

        private void Start()
        {
            hitEvent.AddListener(() => 
            {
                fromView.view.MoveTo(toView).Forget();
            });
        }

        public override void hitFunc()
        {

        }

    }
}
