using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Province : MonoBehaviour 
{
    public UISprite m_mapSprite;
    public List<UISprite> m_subSprite;
    public UISprite m_spotSprite;
    public UILabel m_txtName;

    public string m_name;
    public bool m_conquered;
    public int m_productivity;              // 生产力
    public float m_taxRate;                 // 税率
    public bool m_dontDisplayName;

    public Color m_conqueredColor;
    public Color m_unconqueredColor;

    public float m_discontent;

    public float m_tax;

	// Use this for initialization
	void Start () 
	{
        m_txtName.text = m_name;
        m_discontent = 0.0f;
        m_discontent = 0.0f;
        m_tax = 0.0f;

        if( m_dontDisplayName )
        {
            m_txtName.gameObject.SetActive(false);
            m_spotSprite.gameObject.SetActive(true);
        }
        else
        {
            m_txtName.gameObject.SetActive(true);
            if( m_spotSprite != null )
                m_spotSprite.gameObject.SetActive(false);
        }

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
    /// running 
    /// </summary>
    /// <param name="time"></param>
    public int Running( float time )
    {
        if( m_taxRate > 0.1f )
        {
            m_discontent += ( m_taxRate * m_taxRate * time * GameEnums.REBEL_FACTOR );

            if( m_discontent > 1.0f )
            {
                m_discontent = 1.0f;
            }
        }
        else if( m_taxRate < 0.1f )
        {
            m_discontent -= time * 3;   //<- 
        }

        m_tax += m_productivity * (m_taxRate * time);

        if( m_tax >= 1.0f )
        {
            int rest = (int)(m_tax);
            m_tax -= rest;

            return rest;
        }

        return 0;
    }

    /// <summary>
    /// refresh 
    /// </summary>
    public void Refresh()
    {
        if (m_conquered)
        {
            m_mapSprite.color = m_conqueredColor;

            if( m_subSprite != null )
            {
                for( int i = 0; i < m_subSprite.Count ;i++)
                {
                    m_subSprite[i].color = m_conqueredColor;
                }
            }
        }
        else
        {
            m_mapSprite.color = m_unconqueredColor;

            if (m_subSprite != null)
            {
                for (int i = 0; i < m_subSprite.Count; i++)
                {
                    m_subSprite[i].color = m_unconqueredColor;
                }
            }
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
