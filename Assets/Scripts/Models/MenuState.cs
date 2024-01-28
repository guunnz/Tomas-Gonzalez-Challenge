using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Views;
public enum MenuViewType
{
    MainMenu,
    UserMenu,
}
public class MenuState
{
    public MenuViewType currentMenuScreen;
}