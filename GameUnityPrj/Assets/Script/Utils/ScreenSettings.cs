using UnityEngine;
using System.Collections;

public class ScreenSettings : MonoBehaviour 
{
    public int m_width;
    public int m_height;

	// Use this for initialization
	void Start () 
	{
        Screen.SetResolution(m_width, m_height, false);
	}
	
}
