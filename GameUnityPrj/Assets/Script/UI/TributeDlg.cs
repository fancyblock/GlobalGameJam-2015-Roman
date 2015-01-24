using UnityEngine;
using System.Collections;

public class TributeDlg : MonoBehaviour 
{
    public System.Action m_callback;

    public int m_cost;
    public UILabel m_txtTitle;

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
    public void Show()
    {
        gameObject.SetActive(true);

        m_txtTitle.text = "进贡金币" + m_cost;

        //TODO 
    }

    /// <summary>
    /// abandon the attack 
    /// </summary>
    public void onAbandon()
    {
        m_callback();
    }

    public void onHan()
    {
        if( Empire.SharedInstance.m_money >= m_cost )
        {
            Empire.SharedInstance.m_money -= m_cost;
            //TODO 

            UIMgr.SharedInstance.RefreshUI();
            m_callback();
        }
    }

    public void onHuns()
    {
        if (Empire.SharedInstance.m_money >= m_cost)
        {
            Empire.SharedInstance.m_money -= m_cost;
            //TODO 

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }

    public void onMaya()
    {
        if (Empire.SharedInstance.m_money >= m_cost)
        {
            Empire.SharedInstance.m_money -= m_cost;
            //TODO 

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }

    public void onGermanic()
    {
        if (Empire.SharedInstance.m_money >= m_cost)
        {
            Empire.SharedInstance.m_money -= m_cost;
            //TODO 

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }

    public void onParthian()
    {
        if (Empire.SharedInstance.m_money >= m_cost)
        {
            Empire.SharedInstance.m_money -= m_cost;
            //TODO 

            UIMgr.SharedInstance.RefreshUI();

            m_callback();
        }
    }

}
