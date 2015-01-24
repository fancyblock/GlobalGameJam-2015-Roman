using UnityEngine;
using System;
using System.Collections;

public class LandDlg : MonoBehaviour 
{
    public Action m_callback;

    public UILabel m_txtName;
    public UILabel m_txtBigAttack;
    public UILabel m_txtMidAttack;
    public UILabel m_txtLittleAttack;

    protected Province m_province;

	// Use this for initialization
	void Start () 
	{
        gameObject.SetActive(false);
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

        m_province = province;

        m_txtName.text = "进攻" + m_province.m_name + "?";
        m_txtBigAttack.text = "大举进攻(必胜) " + m_province.GetBigAttackMoney() + "金币";
        m_txtMidAttack.text = "一般进攻(50%胜) " + m_province.GetMidAttackMoney() + "金币";
        m_txtLittleAttack.text = "小规模进攻(20%胜) " + m_province.GetLittleAttackMoney() + "金币";
    }

    /// <summary>
    /// abandon the attack 
    /// </summary>
    public void onAbandon()
    {
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
                Empire.SharedInstance.AddProvince(m_province);
            }

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }
}
