using Base;
using Building;
using PathFinding;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class GameController : MyObject
{
    private static GameController _instance;
    public static GameController Instance { get { return _instance; } }

    private List<GridObject> gridObjects = new List<GridObject>();
    public List<GridObject> GridObjects
    {
        get { return gridObjects; }
        set { gridObjects = value; }
    }

    protected override void Initialize()
    {
        base.Initialize();
        _instance = this;
    }

    protected override void Registration()
    {
        base.Registration();
        GamePanel.posSinglePos += OnSingleTouchClicked;
        GamePanel.posQuadruplePos += OnQuadrupleClicked;
        GamePanel.posLPos += OnLTouchClicked;
    }
    protected override void UnRegistration()
    {
        base.UnRegistration();
        GamePanel.posSinglePos -= OnSingleTouchClicked;
        GamePanel.posQuadruplePos -= OnQuadrupleClicked;
        GamePanel.posLPos -= OnLTouchClicked;

    }

    private void OnLTouchClicked( )
    {
        LBuilding obj = Factory.Instance.CreateObject<LBuilding>(Camera.main.transform);
        obj.gameObject.transform.SetAsLastSibling();
        obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7.5f,-2,20);
    }

    private void OnQuadrupleClicked( )
    {
        QuadrupleBuilding obj = Factory.Instance.CreateObject<QuadrupleBuilding>(Camera.main.transform);
        obj.gameObject.transform.SetAsLastSibling();
        obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7.5f, -0.5f, 20);
    }

    private void OnSingleTouchClicked( )
    {
        SingleBuilding obj = Factory.Instance.CreateObject<SingleBuilding>(Camera.main.transform);
        obj.gameObject.transform.SetAsLastSibling();
        obj.transform.GetComponent<RectTransform>().anchoredPosition = new Vector3(-7.5f, 2, 20);

    }
    public void GenerateNavMesh()
    {
        // Use this if you want to clear existing
        NavMesh.RemoveAllNavMeshData();  
        var settings = NavMesh.CreateSettings();
        var buildSources = new List<NavMeshBuildSource>();
        // create floor as passable area
        var floor = new NavMeshBuildSource
        {
            transform = Matrix4x4.TRS(Vector3.zero, quaternion.identity, Vector3.one),
            shape = NavMeshBuildSourceShape.Box,
            size = new Vector3(10, 1, 10)
        };
        buildSources.Add(floor);

        // Create obstacle 
        const int OBSTACLE = 1 << 0;
        var obstacle = new NavMeshBuildSource
        {
            transform = Matrix4x4.TRS(new Vector3(3, 0, 3), quaternion.identity, Vector3.one),
            shape = NavMeshBuildSourceShape.Box,
            size = new Vector3(1, 1, 1),
            area = OBSTACLE
        };
        buildSources.Add(obstacle);

        // build navmesh
        NavMeshData built = NavMeshBuilder.BuildNavMeshData(
            settings, buildSources, new Bounds(Vector3.zero, new Vector3(10, 10, 10)),
            new Vector3(0, 0, 0), quaternion.identity);
        NavMesh.AddNavMeshData(built);
    }
}
