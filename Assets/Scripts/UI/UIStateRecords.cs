using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStateRecords : UI, IUIState
{
    private Text _scoreRecordText;

    private void Start()
    {
        _scoreRecordText =GameObject.Find("Score Record Text").GetComponent<Text>();

        ShowRecord(GameMaster.instance.BestScore);
    }

    public void Enter() => uiScreens["Records Screen"].SetActive(true);   
    public void Exit() => uiScreens["Records Screen"].SetActive(false);
    private void ShowRecord(int value ) => _scoreRecordText.text = "Лучший счёт убитых монстров: " + value.ToString();
}
