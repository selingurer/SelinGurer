using Base;
using PathFinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootSoldierTwo : BaseSoldier
{
    private void OnMouseDown()
    {
        if (!isSoldierRun)
            return;
        foreach (GridObject obj in UserManager.Instance.GridEmptyList)
        {
            IsSoldier = this;
            obj.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            obj.ISSoldier = true;
        }
        GridObject.ClickedPath += OnClickedPathPos;
    }
    protected override void OnClickedPathPos(GridObject obj)
    {
        base.OnClickedPathPos(obj);
    }
}
