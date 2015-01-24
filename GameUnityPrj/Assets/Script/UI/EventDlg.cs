using UnityEngine;
using System.Collections;

public class EventDlg : MonoBehaviour 
{
    public System.Action m_callback;

    public UILabel m_title;
    public UILabel m_info;

    protected TheEvent m_event;

	// Use this for initialization
	void Start () 
	{
		//TODO 
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
    public void Show( TheEvent evt )
    {
        gameObject.SetActive(true);

        m_event = evt;
        
        //TODO 

    }

    public void onOk()
    {
        m_callback();
    }

}
