namespace Building
{
    using Base;
    using PathFinding;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class SingleBuilding : BaseBuilding
    {
        private bool draggingControl = true;
        public bool isCoin;
        private float timer;
        private bool isRotate;
        [SerializeField] private GameObject objRotate;

        List<float> objListDistance = new List<float>();
        float rotationDeger;
        private void Update()
        {
            if (isCoin)
                timer += Time.deltaTime;
            if (isCoin && timer > 0.99)
            {
                UserManager.Instance.UserCoin += 10;
                timer = 0;
            }
        }

        protected override void OnMouseDrag()
        {
            if (this.draggingControl)
            {
                base.OnMouseDrag();
            }
        }

        private void OnMouseUp()
        {
            Vector3 pos = transform.position;
            if (this.draggingControl)
            {
                bool IsCompleted = false;
                foreach (GridObject obj in GameController.Instance.GridObjects)
                {
                    float mesafe = Vector3.Distance(pos, obj.transform.position);
                    objListDistance.Add(mesafe);
                    if (mesafe < 0.7f && obj.IsEmpty && !IsCompleted)
                    {
                        this.draggingControl = false;
                        this.gameObject.transform.parent = obj.gameObject.transform;
                        transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);
                        transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                        transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                        transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                        isCoin = true;
                        GamePanel.Instance.SingleCount = 0;
                        CameraController.Instance.camTransformChanged = true;
                        obj.IsEmpty = false;
                        UserManager.Instance.UserSingleBuilding++;
                        this.isRotate = true;
                        obj.GetComponent<GridObject>().enabled = false;
                        IsCompleted = true;
                    }
                    else if (draggingControl)
                    {
                        transform.position = FirstPos;
                    }
                }
            }
            if (this.isRotate && !objRotate.activeSelf)
            {
                objRotate.SetActive(true);
                TimerReturn();
                return;
            }
            else if (this.isRotate)
            {
                rotationDeger += 90;
                GetComponent<RectTransform>().rotation = Quaternion.Euler(GetComponent<RectTransform>().rotation.x, GetComponent<RectTransform>().rotation.y, rotationDeger * -1);
            }
        }
        private async void TimerReturn()
        {
            await Task.Delay(1000);
            if (objRotate != null) objRotate.SetActive(false);
        }
    }
}

