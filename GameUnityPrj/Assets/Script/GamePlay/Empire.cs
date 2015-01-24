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
        int income = 0;
        foreach (Province p in m_provinces)
        {
            income += p.GetIncome();
        }

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

}