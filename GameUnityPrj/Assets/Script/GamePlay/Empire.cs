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

    public string[] ENEMYS = new string[5] { "汉朝", "匈奴", "帕提亚", "日耳曼", "玛雅" };

    public float m_goldRate;
    public float m_invadeRate;

    public Color m_conqueredColor;
    public Color m_unconqueredColor;

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
        m_goldRate = 0.0f;
        m_invadeRate = 0.0f;
        m_status = GameEnums.GAME_STATUS_RUNNING;

        UIMgr.SharedInstance.RefreshUI();
	}
	
	// Update is called once per frame
	void Update () 
	{
        if (m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            bool hasEvent = false;

            m_timer += Time.deltaTime;
            UIMgr.SharedInstance.RefreshProgress();

            float yearInterval = Time.deltaTime / m_yearTime;

            // province refresh 
            foreach( Province province in m_provinces )
            {
                m_money += province.Running(yearInterval);

                // if rebel the roman
                if( province.m_discontent > 0.15f && !hasEvent )
                {
                    if( UnityEngine.Random.value <= province.m_discontent )
                    {
                        hasEvent = true;

                        genProvinceRebel(province);
                    }
                }
            }

            // refresh event 
            if( hasEvent == false )
            {
                randomEvent(yearInterval);
            }

            if (m_timer >= m_yearTime)
            {
                m_timer -= m_yearTime;
                m_age++;
            }

            UIMgr.SharedInstance.RefreshUI();
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
    /// add province 
    /// </summary>
    /// <param name="province"></param>
    public void AddProvince(Province province)
    {
        province.m_conquered = true;
        province.m_taxRate = 0.1f;
        province.m_discontent = 0.0f;
        province.m_tax = 0.0f;
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


    /// <summary>
    /// generate a event about province rebel to roman 
    /// </summary>
    /// <param name="privince"></param>
    protected void genProvinceRebel( Province privince )
    {
        Debug.Log( privince.m_name + "叛乱");

        TheEvent evt = new TheEvent();

        evt.m_evtType = GameEnums.EVT_TYPE_REBELLION;
        evt.m_province = privince;

        UIMgr.SharedInstance.ShowEventDlg(evt);
    }

    /// <summary>
    /// random event 
    /// </summary>
    /// <param name="elapsed"></param>
    protected void randomEvent( float elapsed )
    {
        if (Empire.SharedInstance.m_provinces.Count <= 0) return;

        // find gold 
        m_goldRate += ( elapsed * GameEnums.GOLD_FACTOR );

        if( UnityEngine.Random.value <= m_goldRate )
        {
            // generate a gold event 
            TheEvent evt = new TheEvent();
            evt.m_evtType = GameEnums.EVT_TYPE_GOLD_ORE;
            evt.m_money = UnityEngine.Random.Range(1000, 5000);
            evt.m_title = "发现金矿";
            evt.m_info = "陛下，" + m_provinces[UnityEngine.Random.Range(0, m_provinces.Count-1)].m_name + "发现一个金矿，经开采将发掘出的金子铸成" + evt.m_money + "个金币。";

            m_goldRate = 0.0f;

            UIMgr.SharedInstance.ShowEventDlg(evt);

            return;
        }
        
        // invide 
        m_invadeRate += ( elapsed * GameEnums.INVADE_FACTOR);
        if( UnityEngine.Random.value <= m_invadeRate && m_provinces.Count > 2 )
        {
            // generate a invide  event 
            TheEvent evt = new TheEvent();
            evt.m_evtType = GameEnums.EVT_TYPE_INVADE;
            evt.m_province = m_provinces[UnityEngine.Random.Range(0, m_provinces.Count - 1)];

            m_invadeRate = 0.0f;

            UIMgr.SharedInstance.ShowEventDlg(evt);
        }
    }
}