using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class PlayerCamera : MonoBehaviour
    {
        public void MovePosTo(Vector3 pos)
        {
            transform.position = pos;
        }
    }
}
