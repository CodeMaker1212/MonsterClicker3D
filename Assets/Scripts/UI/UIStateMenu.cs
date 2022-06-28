using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIStateMenu : UI, IUIState
{
    public void Enter()
    {
        uiScreens["Menu Screen"].SetActive(true);
        GameMaster.instance.SetPause(true);
    }

    public void Exit() => uiScreens["Menu Screen"].SetActive(false);       
}
