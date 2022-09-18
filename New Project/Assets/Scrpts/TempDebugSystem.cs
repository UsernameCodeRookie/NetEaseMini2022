using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Debugger
{
    public class TempDebugSystem : MonoBehaviour
    {
        [SerializeField]
        private GameLogic.View view1, view2;

        [SerializeField]
        GameLogic.PlayerCamera playerCamera;

        public void MoveView1ToView2()
        {
            view1.MoveTo(view2).Forget();
        }
    }
}
