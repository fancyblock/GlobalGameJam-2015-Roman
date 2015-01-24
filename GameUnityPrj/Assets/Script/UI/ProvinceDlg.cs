using UnityEngine;
using System.Collections;

public class ProvinceDlg : MonoBehaviour 
{
    public System.Action m_callback;

    public UILabel m_txtName;
    public UISlider m_slider;
    public UILabel m_txtRate;

    protected Province m_province;
	
	// Update is called once per frame
	void Update () 
	{
        m_txtRate.text = m_slider.value * 100 + "%";
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
        m_txtRate.text = m_province.m_taxRate * 100 + "%";
    }

    /// <summary>
    /// abandon
    /// </summary>
    public void onAbandon()
    {
        Debug.Log("[ProvinceDlg]: onAbandon => " + m_province.m_name );

        Empire.SharedInstance.RemoveProvince(m_province);

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
