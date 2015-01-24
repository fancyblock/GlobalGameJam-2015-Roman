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

    public int m_status;
    public float m_timer;

    protected int m_priorStatus;


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

        UIMgr.SharedInstance.RefreshUI();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            m_timer += Time.deltaTime;
            UIMgr.SharedInstance.RefreshProgress();

            if (m_timer >= m_yearTime)
            {
                m_status = GameEnums.GAME_STATUS_WAITTING;
                m_timer = 0.0f;
                UIMgr.SharedInstance.RefreshUI();
            }
        }
	}

    /// <summary>
    /// pay tribute 
    /// </summary>
    public void onPayTribute()
    {
        UIMgr.SharedInstance.ShowTributeDlg();
    }

    /// <summary>
    /// goto next turn
    /// </summary>
    public void onNextTurn()
    {
        // calculate the income 
        int income = 0;
        foreach (Province p in m_provinces)
        {
            income += p.GetIncome();
        }

        // add income to exchequer
        m_money += income;

        m_status = GameEnums.GAME_STATUS_RUNNING;
        m_timer = 0.0f;
        m_age++;

        UIMgr.SharedInstance.RefreshUI();
        UIMgr.SharedInstance.RefreshProgress();
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

        UIMgr.SharedInstance.RefreshUI();
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

        UIMgr.SharedInstance.RefreshUI();
    }

    /// <summary>
    /// pause the empire 
    /// </summary>
    public void Pause()
    {
        m_priorStatus = m_status;
        m_status = GameEnums.GAME_STATUS_PAUSE;
    }

    /// <summary>
    /// resume the empire 
    /// </summary>
    public void Resume()
    {
        m_status = m_priorStatus;
    }

}