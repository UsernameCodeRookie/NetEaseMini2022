using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameLogic
{
    public class ViewHit : MonoBehaviour
    {
        [SerializeField]
        public UnityEvent hitEvent;

        public virtual void hitFunc()
        {

        }

    }
}
