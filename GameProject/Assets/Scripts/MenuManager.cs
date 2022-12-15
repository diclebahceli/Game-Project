using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;
    [SerializeField] Menu[] menus;

    void Awake()
    {
        Instance = this;
        //MenuManager.Instance.openMenu("LoadingMenu");
    }



    public void openMenu(string menuName)
    {

        for (int i = 0; i < menus.Length; i++)
        { //Open the menu that we want
            if (menus[i].menuName == menuName)
            {
                menus[i].open();
            }//Close the reamining menus
            else if (menus[i].isOpen)
            {
                closeMenu(menus[i]);
            }
        }
    }

    public void openMenu(Menu menu)
    {
        //Only one menu will be open at a time
        //So we close the other ones
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].isOpen)
            {
                closeMenu(menus[i]);
            }
        }
        //Open the menu that we want
        menu.open();
    }


    public void closeMenu(Menu menu)
    {
        menu.close();
    }
}
