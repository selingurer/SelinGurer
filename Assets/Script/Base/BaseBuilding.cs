namespace Base
{
    using UnityEngine;

    public class BaseBuilding : MyObject
    {
        private Vector3 firstPos;
        public Vector3 FirstPos { get { return firstPos; } set => firstPos = value; }
 
        private Camera cam;
      
        private void Start()
        {
            cam = CameraController.Instance.gameObject.GetComponent<Camera>();
            firstPos = transform.position;
        }
        protected virtual void OnMouseDrag()
        {
            CameraController.Instance.camTransformChanged = false;
            Vector3 pos = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            pos.z = 0;
            this.transform.position += pos;
        }
    }
}
