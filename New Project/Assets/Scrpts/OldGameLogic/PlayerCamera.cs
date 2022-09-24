using Debugger;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace GameLogic
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField]
        private RectTransform rect;

        [DoNotSerialize]
        public Camera cam;

        [SerializeField]
        private ViewSample curViewSample;

        [SerializeField]
        public int layerMask;

		public View curView;

		//public Ray ray;

		private void Start()
        {
            curView = View.SetActiveAndReturnView(curViewSample, this);

            cam = GetComponent<Camera>();
            if(curView != null)
            {
                // Start First View
                cam.transform.position = curView.GetCameraPosition();
            }
        }

        private void Update()
        {
			//Debug.DrawRay(ray.origin, ray.direction * 100, Color.red, 0, true);
		}

        public void MovePosTo(Vector3 pos)
        {
            transform.position = pos;
        }

        public Vector3 MainCameraMousePosToLevelCameraMousePos()
        {
            // Screen Point To Local Point In Rectangle
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, Input.mousePosition, null, out localPoint);
            
            // Local Point To Screen Point
            float x = localPoint.x /rect.rect.width * 1024 + 1024 / 2f;
            float y = localPoint.y /rect.rect.height * 1024 + 1024 / 2f;

			return new Vector3(x, y, 0);
        }
    }
}
