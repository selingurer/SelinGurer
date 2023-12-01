using Newtonsoft.Json;
using PathFinding;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class UserManager
{
    public static event Action<int> UserCoinChanged;
    public int GridCount { get; set; }

    private int gridNotEmptyCount;
    public int GridNotEmptyCount
    {
        get => gridNotEmptyCount;
        set
        {
            gridNotEmptyCount = value;
            if (GridCount == gridNotEmptyCount)
            {
                PanelHome.Instance.IsBtnContinueActive = true;
                PanelHome.Instance.IsBtnStartActive = false;
                PanelHome.Instance.gameObject.SetActive(true);
            }
        }
    }
    private List<GridObject> gridEmptyList = new List<GridObject>();

    public List<GridObject> GridEmptyList
    {
        get { return gridEmptyList; }
        set
        {
            gridEmptyList = value;
        }
    }
    private int userCoin = 10;
    public int UserCoin
    {
        get => userCoin;
        set
        {
            userCoin = value;
            UserCoinChanged?.Invoke(userCoin);
        }
    }
    public int UserSingleBuilding { get; set; }
    public int UserQuadrupleBuilding { get; set; }
    public int UserLBuilding { get; set; }

    private int singlePrice = 10;
    public int SinglePrice { get => singlePrice; set => singlePrice = value; }

    private int quadruplePrice = 40;


    public int FootSoldierCount { get; set; }

    public int FootSoldierTwoCount { get; set; }
    public int HourseSoldierCount { get; set; }

    public int HourseSoldierTwoCount { get; set; }
    public int QuadruplePrice { get => quadruplePrice; set => quadruplePrice = value; }

    private int lPrice = 30;
    public int LPrice { get => lPrice; set => lPrice = value; }
    public static UserManager Instance { get; private set; } = new UserManager();

    static string jsonKonum;
    public static JsonData SoldierDatas = new JsonData();

    public static void Init()
    {
        jsonKonum = Path.Combine(Application.persistentDataPath, "Data.json");

        if (File.Exists(jsonKonum))
        {
          
            string readJson = File.ReadAllText(jsonKonum);
            SoldierDatas = JsonUtility.FromJson<JsonData>(readJson);
          //  JsonUtility.FromJsonOverwrite(readJson, SoldierDatas);

        }

        string json = JsonUtility.ToJson(new JsonData(), true);

        Application.quitting += OnQuit;
    }
    private static void OnQuit()
    {
        JsonData Data = new JsonData();
       
        string DataJson = JsonUtility.ToJson(Data, true);
        File.WriteAllText(jsonKonum, DataJson);

    }

}
[System.Serializable]
public class JsonData
{
    public int HourseSoldierAttackPoint = 10;
    public int HourseSoldierTwoAttackPoint = 10;
    public int HourseSoldierHealth = 100;
    public int HourseSoldierTwoHealth = 100;
    public int HourseSoldierDefencePower = 10;
    public int HourseSoldierTwoDefencePower = 10;
    public int FootSoldierAttackPoint = 10;
    public int FootSoldierTwoAttackPoint = 10;
    public int FootSoldierHealth = 100;
    public int FootSoldierTwoHealth = 100;
    public int FootSoldierDefencePower = 10;
    public int FootSoldierTwoDefencePower = 10;

}


