using System;
using UnityEngine;

public class MeunuCofig : MonoBehaviour
{
    public GameObject mMenu;
    public GameObject cMenu;

    private void Awake()
    {
        //mMenu = GameObject.FindGameObjectWithTag("MainMenu");
        //cMenu = GameObject.FindGameObjectWithTag("ConfigMenu");
        
        if (mMenu != null && cMenu != null)
        {
            mMenu.SetActive(true);
            cMenu.SetActive(false);
        }
    }

    public void ConfigMenuOn()
    {
        if (mMenu != null && cMenu != null)
        {
            mMenu.SetActive(false);
            cMenu.SetActive(true);
        }
    }
    
    public void ConfigMenuOff()
    {
        if (mMenu != null && cMenu != null)
        {
            mMenu.SetActive(true);
            cMenu.SetActive(false);
        }
    }
}
