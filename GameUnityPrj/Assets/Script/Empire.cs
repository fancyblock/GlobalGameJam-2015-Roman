using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Empire : MonoBehaviour 
{
    static public Empire m_instance;

    public int m_age;
    public int m_money;
    public List<Province> m_provinces;

    public float m_yearTime;

    // UI stuff 
    public UILabel m_txtAge;
    public UILabel m_txtMoney;
    public UILabel m_txtProvinceInfo;
    public UILabel m_txtNextMoney;
    public UIProgressBar m_yearProgressBar;
    public UIButton m_btnPayTribute;
    public UIButton m_btnNextYear;

    protected int m_status;
    protected float m_timer;

    void Awake()
    {
        m_instance = this;
    }

    /// <summary>
    /// return the singleton of this 
    /// </summary>
    static public Empire SharedInstance
    {
        get
        {
            return m_instance;
        }
    }

	// Use this for initialization
	void Start () 
	{
        m_timer = 0.0f;
        m_status = GameEnums.GAME_STATUS_READY;

        refreshUI();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            m_timer += Time.deltaTime;
            refreshProgress();

            if (m_timer >= m_yearTime)
            {
                m_status = GameEnums.GAME_STATUS_WAITTING;
                m_timer = 0.0f;
                refreshUI();
            }
        }
	}

    /// <summary>
    /// pay tribute 
    /// </summary>
    public void onPayTribute()
    {
        //TODO 
    }

    /// <summary>
    /// goto next turn
    /// </summary>
    public void onNextTurn()
    {
        int income = 0;
        foreach (Province p in m_provinces)
        {
            income += p.GetIncome();
        }

        m_money += income;

        m_status = GameEnums.GAME_STATUS_RUNNING;
        m_timer = 0.0f;
        m_age++;

        refreshUI();
        refreshProgress();
    }

    /// <summary>
    /// add province 
    /// </summary>
    /// <param name="province"></param>
    public void AddProvince(Province province)
    {
        province.m_conquered = true;
        province.Refresh();
        m_provinces.Add(province);
    }

    /// <summary>
    /// remove province 
    /// </summary>
    /// <param name="province"></param>
    public void RemoveProvince(Province province)
    {
        province.m_conquered = false;
        province.Refresh();
        m_provinces.Remove(province);
    }

    /// <summary>
    /// refresh UI
    /// </summary>
    protected void refreshUI()
    {
        int income = 0;
        foreach (Province p in m_provinces)
        {
            income += p.GetIncome();
        }

        m_txtAge.text = "公元" + m_age + "年";
        m_txtMoney.text = "国库：" + m_money + "金币";
        m_txtProvinceInfo.text = "行省数：" + m_provinces.Count;
        m_txtNextMoney.text = "明年收入：" + income;

        if (m_status == GameEnums.GAME_STATUS_READY)
        {
            //TODO 
        }
        else if (m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            //TODO 
        }
        else if (m_status == GameEnums.GAME_STATUS_WAITTING)
        {
            //TODO 
        }
    }

    /// <summary>
    /// refresh progress 
    /// </summary>
    protected void refreshProgress()
    {
        float progress = m_timer / m_yearTime;

        if (progress > 1.0f)
        {
            progress = 1.0f;
        }

        m_yearProgressBar.value = progress;
    }

}