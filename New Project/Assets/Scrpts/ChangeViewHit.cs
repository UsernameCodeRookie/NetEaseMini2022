using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class ChangeViewHit : ViewHit
    {
        [SerializeField]
        private View fromView, toView;

        // Start is called before the first frame update
        void Start()
        {
            hitEvent.AddListener(()=> 
            {
                fromView.MoveTo(toView).Forget(); 
            });
        }

    }
}
