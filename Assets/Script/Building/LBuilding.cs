namespace Building
{
    using Base;
    using PathFinding;
    using System;
    using System.Collections.Generic;
    using UnityEngine;


    public class LBuilding : BaseBuilding
    {
        private bool draggingControl = true;
        private bool isRotate;
        [SerializeField] private GameObject objRotate;
        List<GridObject> objList = new List<GridObject>();
        float rotationDeger;
        public static event Action<LBuilding> HourseSoldierAction;
        List<float> objListDistance = new List<float>();
        private int hourseSoldierCount;
        private float minDistance;
        public int HourseSoldierCount
        {
            get { return hourseSoldierCount; }
            set
            {
                hourseSoldierCount = value;
                if (SoldierProduce() != null)
                {
                    HourseSoldier obj = Factory.Instance.CreateObject<HourseSoldier>(SoldierProduce().gameObject.transform);
                    SoldierProduce().IsEmpty = false;
                    obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2);
                }
                else
                {
                    hourseSoldierCount--;
                }

            }
        }
        private int hourseSoldierTwoCount;
        public int HourseSoldierTwoCount
        {
            get { return hourseSoldierTwoCount; }
            set
            {
                hourseSoldierTwoCount = value;
                GridObject grid = SoldierProduce();
                if (grid != null)
                {
                    HourseSoldierTwo obj = Factory.Instance.CreateObject<HourseSoldierTwo>(grid.gameObject.transform);
                    grid.IsEmpty = false;
                    obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2);
                }
                else
                {
                    hourseSoldierTwoCount--;
                }
            }
        }
        protected override void OnMouseDrag()
        {
            if (this.draggingControl)
            {
                base.OnMouseDrag();
            }
        }

        private async void OnMouseUp()
        {
            int count = 0;
            Vector3 pos = transform.position;
            if (this.draggingControl)
            {
                bool IsCompleted = false;
                foreach (GridObject obj in GameController.Instance.GridObjects)
                {
                    float mesafe = Vector3.Distance(pos, obj.transform.position);

                    if (mesafe < 0.97f && obj.IsEmpty && !IsCompleted)
                    {
                        count++;
                        objList.Add(obj);
                        if (count == 4)
                        {
                            this.draggingControl = false;
                            this.gameObject.transform.parent = objList[0].gameObject.transform;
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0.55f, -0.55f, 0);
                            transform.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);
                            transform.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
                            transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                            GamePanel.Instance.LCount = 0;
                            CameraController.Instance.camTransformChanged = true;
                            UserManager.Instance.UserLBuilding++;
                            foreach (GridObject obj2 in objList)
                            {
                                obj2.IsEmpty = false;
                                obj2.GetComponent<GridObject>().enabled = false;
                            }
                            isRotate = true;
                            IsCompleted = true;
                        }

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
                HourseSoldierAction?.Invoke(this);
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
            await System.Threading.Tasks.Task.Delay(1000);
            if (objRotate != null) objRotate.SetActive(false);
        }
        private GridObject SoldierProduce()
        {
            // objListDistance.OrderBy(x => x).First();
           
            int x = 0;
            foreach (GridObject obj in UserManager.Instance.GridEmptyList)
            {
                float mesafe = Vector3.Distance(transform.position, obj.transform.position);
                objListDistance.Add(mesafe);
            }
            minDistance = objListDistance[0];
            for (int i = 0; i < objListDistance.Count; i++)
            {
                if (minDistance > objListDistance[i])
                {
                        minDistance = objListDistance[i];
                        x = i;

                }
            }

            if (!UserManager.Instance.GridEmptyList[x].IsEmpty)
            {
                return null;

            }



            return UserManager.Instance.GridEmptyList[x];
        }
    }
}
