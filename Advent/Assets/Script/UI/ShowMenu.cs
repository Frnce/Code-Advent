using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMenu : MonoBehaviour
{
    [Header("Main Menu")]
    public GameObject menu;
    [Header("SubMenus")]
    public GameObject statMenu;
    public GameObject itemMenu;
    public GameObject settingMenu;
    [Space]
    public GameObject infoBar;
    private void Start()
    {
        menu.SetActive(false);

        statMenu.SetActive(false);
        itemMenu.SetActive(false);
        settingMenu.SetActive(false);

        infoBar.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !menu.activeSelf)
        {
            menu.SetActive(true);
            infoBar.SetActive(true);
        }
        else if(Input.GetKeyDown(KeyCode.E) && menu.activeSelf)
        {
            menu.SetActive(false);
            infoBar.SetActive(false);

            statMenu.SetActive(false);
            itemMenu.SetActive(false);
            settingMenu.SetActive(false);
        }
    }

    public void StatMenuEnable()
    {
        statMenu.SetActive(true);
        itemMenu.SetActive(false);
        settingMenu.SetActive(false);
    }
    public void ItemMenuEnable()
    {
        statMenu.SetActive(false);
        itemMenu.SetActive(true);
        settingMenu.SetActive(false);
    }
    public void SettingMenuEnable()
    {
        statMenu.SetActive(false);
        itemMenu.SetActive(false);
        settingMenu.SetActive(true);
    }
}
