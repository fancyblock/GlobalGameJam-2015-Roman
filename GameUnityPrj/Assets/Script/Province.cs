using UnityEngine;
using System.Collections;

public class Province : MonoBehaviour 
{
    public UISprite m_mapSprite;
    public UILabel m_txtName;

    public string m_name;
    public int m_conquered;
    public int m_productivity;              // 生产力
    public float m_taxRate;                 // 税率

	// Use this for initialization
	void Start () 
	{
        m_txtName.text = m_name;
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
        Debug.Log("[Province]: onClick");

        //TODO 
    }

    /// <summary>
    /// get province income
    /// </summary>
    /// <returns></returns>
    public int GetIncome()
    {
        return (int)( m_productivity * m_taxRate );
    }

}
