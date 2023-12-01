using Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace PathFinding
{
    public class GridObject : MyObject
    {
        public static event Action<GridObject> ClickedPath;
        [SerializeField] private bool isEmpty = true;
        private bool isSoldier;
        public bool ISSoldier
        {
            get => isSoldier;
            set
            {
                isSoldier = value;
            }
        }
        public bool IsEmpty
        {
            get { return isEmpty; }
            set
            {
                isEmpty = value;
                if (!isEmpty)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                    if (UserManager.Instance.GridEmptyList.Contains(this))
                    {
                        UserManager.Instance.GridNotEmptyCount++;
                        UserManager.Instance.GridEmptyList.Remove(this);
                    }
                }
                else
                {
                    if (!UserManager.Instance.GridEmptyList.Contains(this))
                    {
                        UserManager.Instance.GridNotEmptyCount--;
                        UserManager.Instance.GridEmptyList.Add(this);
                    }
                }

            }
        }

        protected override void Initialize()
        {
            base.Initialize();
            isEmpty = true;
            UserManager.Instance.GridEmptyList.Add(this);
            gameObject.GetComponent<SpriteRenderer>().color = Color.grey;

        }
        protected override void Registration()
        {
            base.Registration();
            BaseSoldier.ActionEmptyListSoldier += OnBaseSoldierIsEmty;
        }
        protected override void UnRegistration()
        {
            base.UnRegistration();
            BaseSoldier.ActionEmptyListSoldier -= OnBaseSoldierIsEmty;
        }

        private void OnBaseSoldierIsEmty(List<string> obj)
        {
            foreach (string s in obj)
            {
                if (this.gameObject.name == s)
                {
                    if (this.transform.childCount == 0)
                        IsEmpty = true;
                }
            }
        }

        private void Start()
        {
            Vector2 vec = new Vector2(transform.position.x, transform.position.y);
            this.gameObject.name = vec.ToString();
        }
        private void OnMouseDown()
        {
            if (isSoldier)
            {
                //AStar.Instance.vecEnd = this.transform.position;
                foreach (GridObject obj in UserManager.Instance.GridEmptyList)
                {
                    obj.gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                }
                ClickedPath?.Invoke(this);
            }
        }

    }
}