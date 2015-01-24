using UnityEngine;
using System.Collections;

public class UIMgr : MonoBehaviour 
{
    static protected UIMgr m_instance;

    /// <summary>
    /// initial 
    /// </summary>
	void Awake() 
	{
        m_instance = this;
    }

    /// <summary>
    /// return singleton of this class
    /// </summary>
    static public UIMgr SharedInstance
    {
        get
        {
            return m_instance;
        }
    }

    /// <summary>
    /// function part 
    /// </summary>

    public Empire m_empire;

    // UI stuff 
    public UILabel m_txtAge;
    public UILabel m_txtMoney;
    public UILabel m_txtProvinceInfo;
    public UILabel m_txtNextMoney;
    public UIProgressBar m_yearProgressBar;
    public UIButton m_btnPayTribute;
    public UIButton m_btnNextYear;

    public GameObject m_dialogMask;

    /// <summary>
    /// refresh UI
    /// </summary>
    public void RefreshUI()
    {
        int income = 0;
        foreach (Province p in m_empire.m_provinces)
        {
            income += p.GetIncome();
        }

        m_txtAge.text = "公元" + m_empire.m_age + "年";
        m_txtMoney.text = "国库：" + m_empire.m_money + "金币";
        m_txtProvinceInfo.text = "行省数：" + m_empire.m_provinces.Count;
        m_txtNextMoney.text = "明年收入：" + income;

        if (m_empire.m_status == GameEnums.GAME_STATUS_READY)
        {
            //TODO 
        }
        else if (m_empire.m_status == GameEnums.GAME_STATUS_RUNNING)
        {
            //TODO 
        }
        else if (m_empire.m_status == GameEnums.GAME_STATUS_WAITTING)
        {
            //TODO 
        }
    }

    /// <summary>
    /// refresh progress 
    /// </summary>
    public void RefreshProgress()
    {
        float progress = m_empire.m_timer / m_empire.m_yearTime;

        if (progress > 1.0f)
        {
            progress = 1.0f;
        }

        m_yearProgressBar.value = progress;
    }

    /// <summary>
    /// show province dlg 
    /// </summary>
    /// <param name="province"></param>
    public void ShowProvinceDlg( Province province )
    {
        if( province.m_conquered )
        {
            //TODO 
        }
        else
        {
            //TODO 
        }
    }

    /// <summary>
    /// show event dialog 
    /// </summary>
    /// <param name="evt"></param>
    public void ShowEventDlg( TheEvent evt )
    {
        //TODO 
    }

    /// <summary>
    /// show tribute dialog 
    /// </summary>
    public void ShowTributeDlg()
    {
        Debug.Log("[UIMgr]: PayTribute");

        //TODO 
    }

}
