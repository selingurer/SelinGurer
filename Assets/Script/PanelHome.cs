using Base;
using PathFinding;
using System;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PanelHome : BasePanel
{
    [SerializeField] private InputField iFGridCount;
    [SerializeField] private Button btnStart;
    [SerializeField] private Button btnContinue;
    [SerializeField] private Button btnClose;
    [SerializeField] private TextMeshProUGUI textGridTitle;
    [SerializeField] private TextMeshProUGUI textGrid;
    private string desc;
    int z;
   public int line { get; set; }
    int w = 0;
    public string Description
    {
        get => desc;
        set
        {
            desc = value;
            textGrid.text = desc;
        }
    }
    Vector3 vecEndPos;
    private bool isBtnStartActive;
    private bool isBtnContinueActive;
    public bool IsBtnStartActive
    {
        get { return isBtnStartActive; }
        set
        {
            isBtnStartActive = value;
            btnStart.gameObject.SetActive(value);
        }
    }
    public bool IsBtnContinueActive
    {
        get { return isBtnContinueActive; }
        set
        {
            textGridTitle.text = "Grid Sayýsý Giriniz";
            isBtnContinueActive = value;
            btnContinue.gameObject.SetActive(value);
            if (value)
                textGrid.gameObject.SetActive(true);
        }
    }

    private static PanelHome _instance;
    public static PanelHome Instance { get => _instance; }
    protected override void Initialize()
    {
        base.Initialize();
        _instance = this;
        btnContinue.gameObject.SetActive(false);
    }
    protected override void Registration()
    {
        base.Registration();
        btnStart.onClick.AddListener(OnBtnStartClicked);
        btnContinue.onClick.AddListener(OnBtnContinueClicked);
        btnClose.onClick.AddListener(OnBtnCloseClicked);
    }
    protected override void UnRegistration()
    {
        base.UnRegistration();
        btnStart.onClick.RemoveListener(OnBtnStartClicked);
        btnContinue.onClick.RemoveListener(OnBtnContinueClicked);
        btnClose.onClick.RemoveListener(OnBtnCloseClicked);
    }

    protected override void AfterInitialize()
    {
        base.AfterInitialize();
        UserManager.Init();
    }
    private void OnBtnCloseClicked()
    {
        Application.Quit();
    }

    private void OnBtnContinueClicked()
    {
        if ((UserManager.Instance.GridCount + Convert.ToInt32(iFGridCount.text)) > 450)
        {
            int x = 450 - UserManager.Instance.GridCount;
            textGrid.gameObject.SetActive(true);
            textGrid.text = "En fazla" + x + "adet oluþturabilirsin tekrar dene..";
            iFGridCount.text = "";
            return;
        }
        textGrid.gameObject.SetActive(false);
        float deger = Convert.ToInt32(iFGridCount.text);
        int y = 0;
        int z = 0;
        Vector3 vecFor = new Vector3(vecEndPos.x + 1, 0, vecEndPos.z);
        for (int i = 0; i < deger; i++)
        {
            y++;
            GridObject obj = Factory.Instance.CreateObject<GridObject>(GameController.Instance.transform);
            GameController.Instance.GridObjects.Add(obj);
            if (w == line || (i > line && i % line == 0) || vecEndPos.y == -line + 1)
            {
                obj.transform.position = new Vector3(vecFor.x + z / 2, 0, 0f);
                vecEndPos = obj.transform.position;
                y = -1;
                z++;
                w = 0;
            }
            else
            {
                obj.transform.position = new Vector3(0, -1, 0f) + vecEndPos;
                vecEndPos = obj.transform.position;
                w++;

            }
        }
        iFGridCount.text = "";
        gameObject.SetActive(false);
        UserManager.Instance.GridCount = UserManager.Instance.GridCount = GameController.Instance.GridObjects.Count;
    }

    private void OnBtnStartClicked()
    {
        if (Convert.ToInt32(iFGridCount.text) > 25)
        {
            textGrid.gameObject.SetActive(true);
            textGrid.text = "En fazla 250 adet oluþturabilirsin tekrar dene..";
            iFGridCount.text = "";
            return;
        }
        textGrid.gameObject.SetActive(false);
        int deger = Convert.ToInt32(iFGridCount.text);
        line = deger;
        int x = 0;
        for (int i = 0; i < deger; i++)
        {
            for (int y = 0; y < deger; y++)
            {
                GridObject obj = Factory.Instance.CreateObject<GridObject>(GameController.Instance.transform);
                GameController.Instance.GridObjects.Add(obj);
                obj.transform.position = new Vector3(i - 1, -y, 0f);
                obj.transform.position = new Vector3(i - 1, -y, 0f);
                vecEndPos = obj.transform.position;

            }
        }
        UserManager.Instance.GridCount = GameController.Instance.GridObjects.Count;
        iFGridCount.text = "";
        gameObject.SetActive(false);
    }

}
