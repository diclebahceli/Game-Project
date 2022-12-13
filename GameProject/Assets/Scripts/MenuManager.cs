using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] menus; 

    public void openMenu(string menuName){
        for (int i = 0; i < menus.Length; i++)
        {
            if (menus[i].menuName == menuName)
            {
                openMenu(menus[i]);
            }
            else if(menuName[i].isOpen)
            {
                
            }
        }
    }

    public void openMenu(Menu menu){
        menu.open();
    }


    public void closeMenu(Menu menu){
        menu.close();
    }
}
