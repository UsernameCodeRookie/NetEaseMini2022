using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GL
{
    public class PlayerCameraSample : MonoBehaviour
    {
        [SerializeField] private RectTransform _rect;

        [SerializeField] private int _layer;

        private Camera _camera;

        public PlayerCamera playerCamera;


        private void Start()
        {
            _camera = GetComponent<Camera>();
            playerCamera = new PlayerCamera(_rect, _camera, _layer);
            playerCamera.Init();
        }
    }
}
