using UnityEngine;
using System.Collections;

public class ProvinceDlg : MonoBehaviour 
{
    public System.Action m_callback;

    public UILabel m_txtName;
    public UISlider m_slider;
    public UILabel m_txtGDP;
    public UILabel m_txtTax;

    protected Province m_province;
	
	// Update is called once per frame
	void Update () 
	{
        m_txtTax.text = (int)(m_slider.value * m_province.m_productivity) + "金币/年";
	}

    /// <summary>
    /// show dialog 
    /// </summary>
    /// <param name="province"></param>
    /// <param name="callback"></param>
    public void Show(Province province)
    {
        gameObject.SetActive(true);

        m_province = province;

        m_txtName.text = m_province.m_name;
        m_slider.value = m_province.m_taxRate;
        m_txtGDP.text = m_province.m_productivity + "金币/年";
        m_txtTax.text = (int)(m_province.m_taxRate * m_province.m_productivity) + "金币/年";
    }

    /// <summary>
    /// abandon
    /// </summary>
    public void onAbandon()
    {
        m_callback();
    }

    /// <summary>
    /// on adjust tax rate 
    /// </summary>
    public void onAdjustTaxRate()
    {
        m_province.m_taxRate = m_slider.value;
        UIMgr.SharedInstance.RefreshUI();

        m_callback();
    }

}
