using UnityEngine;
using UnityEngine.UI;

public class UIStateGame : UI, IUIState
{
    private Text _monsterCounterText;
    private Text _scoreCounterText;


    private void Start()
    {
        _monsterCounterText = GameObject.Find("Monster Counter").GetComponent<Text>();
        _scoreCounterText = GameObject.Find("Score Counter").GetComponent<Text>();

        GameMaster.instance.MonsterCountChanged += UpdateMonsterCounterText;
        GameMaster.instance.ScoreUpdated += UpdateScoreCountText;
    }

    private void UpdateMonsterCounterText(int value) => _monsterCounterText.text = "Живых монстров : " + value.ToString();
    private void UpdateScoreCountText(int value) => _scoreCounterText.text = "Монстров убито: " + value.ToString();
    public void Enter()
    {
        uiScreens["Game Screen"].SetActive(true);
        GameMaster.instance.SetPause(false);
    }
    public void Exit() => uiScreens["Game Screen"].SetActive(false);   
}
