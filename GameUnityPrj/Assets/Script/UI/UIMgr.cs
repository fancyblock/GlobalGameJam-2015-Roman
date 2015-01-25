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
    public UIProgressBar m_yearProgressBar;
    public UIButton m_btnPayTribute;

    public GameObject m_dialogMask;
    // action dlgs 
    public ProvinceDlg m_provinceDlg;
    public LandDlg m_landDlg;
    public TributeDlg m_tributeDlg;
    public EventDlg m_eventDlg;

    /// <summary>
    /// start 
    /// </summary>
    void Start()
    {
        m_landDlg.m_callback = onDlgClosed;
        m_provinceDlg.m_callback = onDlgClosed;
        m_tributeDlg.m_callback = onDlgClosed;
        m_eventDlg .m_callback = onDlgClosed;
    }

    /// <summary>
    /// refresh UI
    /// </summary>
    public void RefreshUI()
    {
        if( m_empire.m_age < 0 )
        {
            m_txtAge.text = "公元前" + -m_empire.m_age + "年";
        }
        else if( m_empire.m_age == 0 )
        {
            m_txtAge.text = "公元元年";
        }
        else
        {
            m_txtAge.text = "公元" + m_empire.m_age + 2 + "年";
        }
        
        m_txtMoney.text = "国库：" + m_empire.m_money + "金币";
        m_txtProvinceInfo.text = "行省数：" + m_empire.m_provinces.Count;

        if (m_empire.m_status == GameEnums.GAME_STATUS_READY)
        {
            //TODO 
        }
        else if (m_empire.m_status == GameEnums.GAME_STATUS_RUNNING)
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
        m_dialogMask.SetActive(true);
        m_empire.Pause();

        if( province.m_conquered )
        {
            m_provinceDlg.Show(province);
        }
        else
        {
            m_landDlg.Show(province);
        }
    }

    /// <summary>
    /// show event dialog 
    /// </summary>
    /// <param name="evt"></param>
    public void ShowEventDlg( TheEvent evt )
    {
        m_dialogMask.SetActive(true);
        m_empire.Pause();

        if( evt.m_evtType == GameEnums.EVT_TYPE_GOLD_ORE ||
            evt.m_evtType == GameEnums.EVT_TYPE_TRIBUTE )
        {
            m_eventDlg.Show(evt);
        }
        else if( evt.m_evtType == GameEnums.EVT_TYPE_BLACKMAIL )
        {
            //TODO 
        }
        else if( evt.m_evtType == GameEnums.EVT_TYPE_INVADE ||
                evt.m_evtType == GameEnums.EVT_TYPE_REBELLION )
        {
            if( evt.m_evtType == GameEnums.EVT_TYPE_REBELLION )
            {
                evt.m_title = evt.m_province.m_name + "发生叛乱";
                evt.m_info = evt.m_province.m_name + "已无法忍受帝国沉重的赋税，决心推翻你的统治。";
            }
            else
            {
                evt.m_title = evt.m_province.m_name + "遭到入侵";
                evt.m_info = "邪恶的" + Empire.SharedInstance.ENEMYS[UnityEngine.Random.Range(0, 4)] +"向我们发起了进攻，意图使" + evt.m_province.m_name + "脱离罗马。";

                // 敌人恶感度清零 
                //TODO 
            }

            m_landDlg.ShowEvent(evt);
        }
        
    }

    /// <summary>
    /// show tribute dialog 
    /// </summary>
    public void ShowTributeDlg()
    {
        Debug.Log("[UIMgr]: PayTribute");

        m_dialogMask.SetActive(true);
        m_empire.Pause();

        m_tributeDlg.Show();
    }

    /// <summary>
    /// callback when dialog closed 
    /// </summary>
    protected void onDlgClosed()
    {
        Debug.Log("[UIMgr]: onDlgClosed");

        m_eventDlg.gameObject.SetActive(false);
        m_landDlg.gameObject.SetActive(false);
        m_provinceDlg.gameObject.SetActive(false);
        m_tributeDlg.gameObject.SetActive(false);

        m_dialogMask.SetActive(false);
        m_empire.Resume();
    }

}
