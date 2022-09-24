using Debugger;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameLogic
{
    public class ViewSample : MonoBehaviour
    {
        [SerializeField]
        private Transform _camPos;

        [SerializeField]
        private PlayerCamera _playerCamera;

        [SerializeField]
        private ViewParams _viewParams;

        public View view;


        public void Start()
        {
            //TempDebug.Log("ViewSample Start");
            Init();
        }

        public void Init()
        {
			view = new View(_camPos, _viewParams, _playerCamera, this.gameObject);
			view.Start();
		}

    }
}
