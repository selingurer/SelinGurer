namespace Building
{
    using Base;
    using PathFinding;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;
    using System.Linq;

    public class QuadrupleBuilding : BaseBuilding
    {
        private bool draggingControl = true;
        private bool isRotate;
        [SerializeField] private GameObject objRotate;
        List<GridObject> objList = new List<GridObject>();
        float rotationDeger;
        public static event Action<QuadrupleBuilding> FootSoldier;

        [SerializeField] private Transform doorSoldier;

        List<float> objListDistance = new List<float>();
        private int footSoldierCount;
        private float minDistance;
        Vector3 vec;
        public int FootSoldierCount
        {
            get { return footSoldierCount; }
            set
            {
                footSoldierCount = value;
                GridObject grid = SoldierProduce();
                if (grid != null)
                {
                    FootSoldier obj = Factory.Instance.CreateObject<FootSoldier>(grid.gameObject.transform);
                    grid.IsEmpty = false;
                    obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2);
                }
                else
                {
                    footSoldierCount--;
                }

            }
        }
        private int footSoldierTwoCount;
        public int FootSoldierTwoCount
        {
            get { return footSoldierTwoCount; }
            set
            {
                footSoldierTwoCount = value;
                GridObject grid = SoldierProduce();
                if (grid != null)
                {
                    FootSoldierTwo obj = Factory.Instance.CreateObject<FootSoldierTwo>(grid.gameObject.transform);
                    grid.IsEmpty = false;
                    obj.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2);
                }
                else
                {
                    footSoldierTwoCount--;

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

        private void OnMouseUp()
        {
            int count = 0;

            vec = transform.position;
            if (this.draggingControl)
            {
                bool IsCompleted = false;
                foreach (GridObject obj in GameController.Instance.GridObjects)
                {
                    float mesafe = Vector3.Distance(vec, obj.transform.position);
                
                    if (mesafe < 1f && obj.IsEmpty && !IsCompleted)
                    {
                        count++;
                        objList.Add(obj);
                        if (count == 4)
                        {

                            this.draggingControl = false;
                            this.gameObject.transform.parent = obj.gameObject.transform;
                            transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-0.55f, 0.55f, 0);
                            transform.GetComponent<RectTransform>().anchorMax = new Vector2(1, 0);
                            transform.GetComponent<RectTransform>().anchorMin = new Vector2(1, 0);
                            transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
                            GamePanel.Instance.QuadrupleCount = 0;
                            CameraController.Instance.camTransformChanged = true;
                            UserManager.Instance.UserQuadrupleBuilding++;
                            foreach (GridObject obj2 in objList)
                            {
                                obj2.IsEmpty = false;
                                obj2.GetComponent<GridObject>().enabled = false;
                            }
                            this.isRotate = true;
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
                FootSoldier?.Invoke(this);
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

        private GridObject SoldierProduce()
        {

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
