using Base;
using Building;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    private static GamePanel _instance;
    public static GamePanel Instance { get => _instance; }
    [SerializeField] private Button btnSingleBuilding;
    [SerializeField] private Button btnQuadrupleBuilding;
    [SerializeField] private Button btnLBuilding;
    [SerializeField] private TextMeshProUGUI txtCoin;
    [SerializeField] private Image imgAddSingleBuilding;
    [SerializeField] private Image imgAddQuadrupleBuilding;
    [SerializeField] private Image imgAddLBuilding;
    [SerializeField] private Button btnFootSoldier;
    [SerializeField] private Button btnFootSoldierTwo;
    [SerializeField] private Button btnHourseSoldier;
    [SerializeField] private Button btnHourseSoldierTwo;
    [SerializeField] private Button btnGridAdded;


    QuadrupleBuilding quadrupBuilding = new QuadrupleBuilding();
    LBuilding lBuilding = new LBuilding();
    private int singleCount;
    public int SingleCount
    {
        get => singleCount; set
        {
            singleCount = value;
            if (singleCount > 0)
                imgAddSingleBuilding.gameObject.SetActive(false);
        }
    }

    private int quadrupleCount;
    public int QuadrupleCount
    {
        get => quadrupleCount; set
        {
            quadrupleCount = value;
            if (quadrupleCount > 0)
                imgAddQuadrupleBuilding.gameObject.SetActive(false);

        }
    }

    private int lCount;
    public int LCount
    {
        get => lCount; set
        {
            lCount = value;
            if (lCount > 0)
                imgAddLBuilding.gameObject.SetActive(false);
        }
    }

    public static event Action posSinglePos;
    public static event Action posQuadruplePos;
    public static event Action posLPos;

    protected override void Initialize()
    {
        base.Initialize();
        _instance = this;
        txtCoin.text = Convert.ToString(UserManager.Instance.UserCoin);
        imgAddSingleBuilding.gameObject.SetActive(true);
    }
    protected override void Registration()
    {
        base.Registration();
        btnLBuilding.onClick.AddListener(OnBtnLBuildingClicked);
        btnSingleBuilding.onClick.AddListener(OnBtnSingleBuildingClicked);
        btnQuadrupleBuilding.onClick.AddListener(OnBtnQuadrupleBuildingClicked);
        btnFootSoldier.onClick.AddListener(OnBtnFootSoldierAdded);
        btnFootSoldierTwo.onClick.AddListener(OnBtnFootSoldierTwoAdded);
        btnHourseSoldier.onClick.AddListener(OnBtnHourseSoldierAdded);
        btnHourseSoldierTwo.onClick.AddListener(OnBtnHourseSoldierTwoAdded);
        btnGridAdded.onClick.AddListener(OnBtnGridAddedClicked);
        UserManager.UserCoinChanged += OnChangeUserCoin;
        QuadrupleBuilding.FootSoldier += OnFootSoldier;
        LBuilding.HourseSoldierAction += OnHourseSoldier;
    }
    protected override void UnRegistration()
    {
        base.UnRegistration();
        btnLBuilding.onClick.RemoveListener(OnBtnLBuildingClicked);
        btnSingleBuilding.onClick.RemoveListener(OnBtnSingleBuildingClicked);
        btnQuadrupleBuilding.onClick.RemoveListener(OnBtnQuadrupleBuildingClicked);
        btnFootSoldier.onClick.RemoveListener(OnBtnFootSoldierAdded);
        btnFootSoldierTwo.onClick.RemoveListener(OnBtnFootSoldierTwoAdded);
        btnHourseSoldier.onClick.RemoveListener(OnBtnHourseSoldierAdded);
        btnHourseSoldierTwo.onClick.RemoveListener(OnBtnHourseSoldierTwoAdded);
        btnGridAdded.onClick.RemoveListener(OnBtnGridAddedClicked);
        UserManager.UserCoinChanged -= OnChangeUserCoin;
        QuadrupleBuilding.FootSoldier -= OnFootSoldier;
        LBuilding.HourseSoldierAction -= OnHourseSoldier;

    }

    private void OnBtnGridAddedClicked()
    {

        PanelHome.Instance.IsBtnContinueActive = true;
        PanelHome.Instance.IsBtnStartActive = false;
        PanelHome.Instance.Description = "Alanýný ne kadar büyütmek istersin ?";
        PanelHome.Instance.gameObject.SetActive(true);
       
    }

    private void OnHourseSoldier(LBuilding cs)
    {
        lBuilding = cs;
        ButtonControl(btnFootSoldier, btnFootSoldierTwo, false);
        btnHourseSoldier.gameObject.SetActive(cs.HourseSoldierCount < 1);
        btnHourseSoldierTwo.gameObject.SetActive(cs.HourseSoldierTwoCount < 1);

    }

    private void OnBtnHourseSoldierTwoAdded()
    {
        ButtonControl(btnHourseSoldier, btnHourseSoldierTwo, false);
        UserManager.Instance.HourseSoldierTwoCount++;
        lBuilding.HourseSoldierTwoCount = 1;
    }

    private void OnBtnHourseSoldierAdded()
    {
        ButtonControl(btnHourseSoldier, btnHourseSoldierTwo, false);
        btnHourseSoldier.gameObject.SetActive(false);
        UserManager.Instance.HourseSoldierCount++;
        lBuilding.HourseSoldierCount = 1;
    }

    private void OnBtnFootSoldierTwoAdded()
    {
        ButtonControl(btnFootSoldier, btnFootSoldierTwo, false);
        UserManager.Instance.FootSoldierTwoCount++;
        quadrupBuilding.FootSoldierTwoCount = 1;
    }

    private void OnBtnFootSoldierAdded()
    {
        btnFootSoldier.gameObject.SetActive(false);
        btnFootSoldierTwo.gameObject.SetActive(false);
        UserManager.Instance.FootSoldierCount++;
        quadrupBuilding.FootSoldierCount = 1;
    }

    private void OnFootSoldier(QuadrupleBuilding building)
    {
        quadrupBuilding = building;
        ButtonControl(btnHourseSoldier, btnHourseSoldierTwo, false);
        btnFootSoldier.gameObject.SetActive(building.FootSoldierCount < 1);
        btnFootSoldierTwo.gameObject.SetActive(building.FootSoldierTwoCount < 1);

    }
    private void ButtonControl(Button btn,Button btnTwo, bool isActive)
    {
       if(btn!=null) btn.gameObject.SetActive(isActive);
        if (btnTwo != null) btnTwo.gameObject.SetActive(isActive);
    }
    private void OnChangeUserCoin(int coin)
    {
        txtCoin.text = coin.ToString();
        imgAddSingleBuilding.gameObject.SetActive(coin >= UserManager.Instance.SinglePrice);
        imgAddLBuilding.gameObject.SetActive(coin >= UserManager.Instance.LPrice);
        imgAddQuadrupleBuilding.gameObject.SetActive(coin >= UserManager.Instance.QuadruplePrice);
    }

    private void OnBtnLBuildingClicked()
    {
        if (LCount == 0)
        {
            if (UserManager.Instance.UserCoin >= UserManager.Instance.LPrice)
            {
                lCount += 1;
                UserManager.Instance.UserCoin -= UserManager.Instance.LPrice;

                posLPos?.Invoke();

            }
        }
    }

    private void OnBtnSingleBuildingClicked()
    {
        if (SingleCount == 0)
        {
            if (UserManager.Instance.UserCoin >= UserManager.Instance.SinglePrice)
            {
                singleCount += 1;
                UserManager.Instance.UserCoin -= UserManager.Instance.SinglePrice;

                posSinglePos?.Invoke();

            }
        }

    }

    private void OnBtnQuadrupleBuildingClicked()
    {
        if (QuadrupleCount == 0)
        {
            if (UserManager.Instance.UserCoin >= UserManager.Instance.QuadruplePrice)
            {
                quadrupleCount += 1;
                UserManager.Instance.UserCoin -= UserManager.Instance.QuadruplePrice;

                posQuadruplePos?.Invoke();

            }

        }
    }
}
