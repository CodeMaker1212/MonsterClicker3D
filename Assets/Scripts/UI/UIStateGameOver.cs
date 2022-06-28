using UnityEngine;
using UnityEngine.UI;

public class UIStateGameOver : UI, IUIState
{
    private Text _pointsScoredText;

    private void ShowPointsScored(int value) => _pointsScoredText.text = "Монстров убито: " + value.ToString();

    public void Enter()
    {
        uiScreens["Game Over Screen"].SetActive(true);

        GameMaster.instance.SetPause(true);

        _pointsScoredText = GameObject.Find("Points Scored Text").GetComponent<Text>();

        ShowPointsScored(GameMaster.instance.CurrentScore);
    }
    public void Exit() => uiScreens["Game Over Screen"].SetActive(false);
}
