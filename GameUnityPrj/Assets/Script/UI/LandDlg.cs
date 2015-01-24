using UnityEngine;
using System;
using System.Collections;

public class LandDlg : MonoBehaviour 
{
    public Action m_callback;

    public UILabel m_txtTitle;
    public UILabel m_txtInfo;
    public UILabel m_txtBigAttack;
    public UILabel m_txtMidAttack;
    public UILabel m_txtLittleAttack;

    protected Province m_province;
    protected TheEvent m_event;

	// Use this for initialization
	void Start () 
	{
        //gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO 
	}

    /// <summary>
    /// show dialog 
    /// </summary>
    /// <param name="province"></param>
    /// <param name="callback"></param>
    public void Show( Province province )
    {
        gameObject.SetActive(true);

        m_event = null;
        m_province = province;

        m_txtTitle.text = "进攻" + m_province.m_name + "?";
        m_txtInfo.text = "是否要派出我们英勇的罗马军团占领这块土地。";
        m_txtBigAttack.text = "4个军团(必胜) " + m_province.GetBigAttackMoney() + "金币";
        m_txtMidAttack.text = "2个军团(50%胜) " + m_province.GetMidAttackMoney() + "金币";
        m_txtLittleAttack.text = "1个军团(20%胜) " + m_province.GetLittleAttackMoney() + "金币";
    }

    /// <summary>
    /// public show event 
    /// </summary>
    /// <param name="evt"></param>
    public void ShowEvent( TheEvent evt )
    {
        gameObject.SetActive(true);

        m_event = evt;
        m_province = evt.m_province;

        m_txtTitle.text = m_event.m_title;
        m_txtInfo.text = m_event.m_info;
        m_txtBigAttack.text = "4个军团(必胜) " + m_province.GetBigAttackMoney() + "金币";
        m_txtMidAttack.text = "2个军团(50%胜) " + m_province.GetMidAttackMoney() + "金币";
        m_txtLittleAttack.text = "1个军团(20%胜) " + m_province.GetLittleAttackMoney() + "金币";
    }


    /// <summary>
    /// abandon the attack 
    /// </summary>
    public void onAbandon()
    {
        if (m_event != null)
        {
            Empire.SharedInstance.RemoveProvince(m_province);
        }

        m_callback();
    }

    /// <summary>
    /// big attack 
    /// </summary>
    public void onBigAttack()
    {
        int cost = m_province.GetBigAttackMoney();
        attack(1.0f, cost);
    }

    /// <summary>
    /// mid attack 
    /// </summary>
    public void onMidAttack()
    {
        int cost = m_province.GetMidAttackMoney();
        attack(0.5f, cost);
    }

    /// <summary>
    /// little attack 
    /// </summary>
    public void onLittleAttack()
    {
        int cost = m_province.GetLittleAttackMoney();
        attack(0.2f, cost);
    }

    /// <summary>
    /// attack the land 
    /// </summary>
    /// <param name="winRate"></param>
    /// <param name="cost"></param>
    protected void attack( float winRate, int cost )
    {
        if (Empire.SharedInstance.m_money >= cost)
        {
            Empire.SharedInstance.m_money -= cost;

            float randomVal = UnityEngine.Random.value;

            Debug.Log("[LandDlg]: attack, randomVal => " + randomVal );

            if (randomVal <= winRate)
            {
                m_province.m_taxRate = 0.1f;

                if( Empire.SharedInstance.m_provinces.Contains( m_province ) == false )
                {
                    Empire.SharedInstance.AddProvince(m_province);
                }
                else
                {
                    m_province.m_discontent = 0.0f;
                    m_province.m_tax = 0.0f;
                    m_province.m_taxRate = 0.1f;
                    m_province.Refresh();
                }
            }
            else
            {
                if( m_event != null )
                {
                    Empire.SharedInstance.RemoveProvince(m_province);
                }
            }

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }

}
