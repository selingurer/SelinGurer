namespace Base
{

    using Base;
    using PathFinding;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using UnityEngine;

    public class BaseSoldier : MyObject
    {
        public static bool isSoldierRun { get; set; }

        public int i { get; set; } = 0;
        private static BaseSoldier soldier;
        public static BaseSoldier IsSoldier
        {
            set
            {
                isSoldierRun = false;
                soldier = value;

            }
        }
        AStar pathSoldier = new AStar();

        private GridObject objGrid;
        private int attackPoint;
        private int health;
        private int defencePower;

        public List<string> NotEmptyGridList = new List<string>();
        public static event Action<List<string>> ActionEmptyListSoldier;
        public int AttackPoint
        {
            get { return attackPoint; }
            set { attackPoint = value; }
        }
        public int Health
        {
            get { return health; }
            set { health = value; }
        }
        public int DefencePower
        {
            get { return defencePower; }
            set { defencePower = value; }
        }

        protected override void Initialize()
        {
            base.Initialize();
            isSoldierRun = true;
        }
        protected virtual void OnClickedPathPos(GridObject obj)
        {
            if (i > 0)
                return;
            pathSoldier.vecStart = soldier.transform.position;
            pathSoldier.vecEnd = obj.transform.position;
            pathSoldier.IsEmptyList.Clear();
            foreach (GridObject obj2 in UserManager.Instance.GridEmptyList)
            {
                pathSoldier.IsEmptyList.Add(obj2.transform.position);

            }
            objGrid = obj;
            i++;
            Path();
        }
        public async void Path()
        {
           
            List<Vector2> pathList = pathSoldier.FindPath();
            if (pathList == null)
            {
                isSoldierRun = true;
                i = 0;
                return;
            }

            foreach (Vector2 vecTarget in pathList)
            {

                await Task.Delay(50);
                var obj = GameObject.Find(vecTarget.ToString());
                soldier.gameObject.transform.parent = obj.transform;
                NotEmptyGridList.Add(vecTarget.ToString());
                soldier.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, -2);
                soldier.transform.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
                soldier.transform.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                soldier.transform.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);

            }
            if (objGrid != null) objGrid.IsEmpty = false;
            pathSoldier.IsEmptyList.Clear();
            ActionEmptyListSoldier?.Invoke(NotEmptyGridList);
            isSoldierRun = true;
            i= 0;
        }
    }
}
