
public class UIStateTitle : UI, IUIState
{
    public void Enter() => uiScreens["Title Screen"].SetActive(true);   
    public void Exit() => uiScreens["Title Screen"].SetActive(false);
}
