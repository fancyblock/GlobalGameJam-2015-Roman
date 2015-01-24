using UnityEngine;
using System.Collections;

public class Province : MonoBehaviour 
{
    public UISprite m_mapSprite;
    public UILabel m_txtName;

    public string m_name;
    public bool m_conquered;
    public int m_productivity;              // 生产力
    public float m_taxRate;                 // 税率

    public Color m_conqueredColor;
    public Color m_unconqueredColor;

	// Use this for initialization
	void Start () 
	{
        m_txtName.text = m_name;

        Refresh();
	}
	
	// Update is called once per frame
	void Update () 
	{
		//TODO 
	}

    /// <summary>
    /// this province be clicked 
    /// </summary>
    public void onClick()
    {
        Debug.Log("[Province]: onClick => " + m_name);

        UIMgr.SharedInstance.ShowProvinceDlg(this);
    }

    /// <summary>
    /// get province income
    /// </summary>
    /// <returns></returns>
    public int GetIncome()
    {
        return (int)( m_productivity * m_taxRate );
    }

    /// <summary>
    /// refresh 
    /// </summary>
    public void Refresh()
    {
        if (m_conquered)
        {
            m_mapSprite.color = m_conqueredColor;
        }
        else
        {
            m_mapSprite.color = m_unconqueredColor;
        }
    }

    /// <summary>
    /// get big attack money
    /// </summary>
    /// <returns></returns>
    public int GetBigAttackMoney()
    {
        return m_productivity * 3;
    }

    /// <summary>
    /// get middle attack money
    /// </summary>
    /// <returns></returns>
    public int GetMidAttackMoney()
    {
        return m_productivity * 2;
    }

    /// <summary>
    /// get little attack money
    /// </summary>
    /// <returns></returns>
    public int GetLittleAttackMoney()
    {
        return m_productivity;
    }

}
