using System.Collections.Generic;
using UnityEngine;
using System;

public class UI : MonoBehaviour
{
    protected static Dictionary<string, GameObject> uiScreens;

    private Dictionary<Type, IUIState> _statesMap;
    private IUIState _currentState;
   
    private void Awake()
    {
        InitializeScreens();
        InitializeStates();       
    }
    private void Start()
    {
        uiScreens["Game Screen"].SetActive(false);
        uiScreens["Menu Screen"].SetActive(false);
        uiScreens["Game Over Screen"].SetActive(false);
        uiScreens["Records Screen"].SetActive(false);
        uiScreens["Title Screen"].SetActive(false);

        GameMaster.instance.GameOver += SetGameOverState;

        SetDefaultState();      
    }
    private void InitializeScreens()
    {
        uiScreens = new Dictionary<string, GameObject>();

        uiScreens.Add("Game Screen", GameObject.Find("Game Screen"));
        uiScreens.Add("Menu Screen", GameObject.Find("Menu Screen"));
        uiScreens.Add("Game Over Screen", GameObject.Find("Game Over Screen"));
        uiScreens.Add("Records Screen", GameObject.Find("Records Screen"));
        uiScreens.Add("Title Screen", GameObject.Find("Title Screen"));       
    }
    private void InitializeStates()
    {
        _statesMap = new Dictionary<Type, IUIState>();

        _statesMap[typeof(UIStateGame)] = new UIStateGame();
        _statesMap[typeof(UIStateMenu)] = new UIStateMenu();
        _statesMap[typeof(UIStateRecords)] = new UIStateRecords();
        _statesMap[typeof(UIStateTitle)] = new UIStateTitle();
        _statesMap[typeof(UIStateGameOver)] = new UIStateGameOver();

    }
    private void SetDefaultState() => SetGameState(); 
    private void SetState(IUIState newState)
    {
        if(_currentState != null) _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }
    private IUIState GetState<T>() where T : IUIState
    {
        var type = typeof(T);
        return _statesMap[type];
    }
    public void SetGameState()
    {
        var state = GetState<UIStateGame>();
        SetState(state);
    }   
    public void SetMenuState()
    {
        var state = GetState<UIStateMenu>();
        SetState(state);
    }
    public void SetRecordsState()
    {
        var state = GetState<UIStateRecords>();
        SetState(state);
    }
    public void SetTitleState()
    {
        var state = GetState<UIStateTitle>();
        SetState(state);
    }
    public void SetGameOverState()
    {
        var state = GetState<UIStateGameOver>();
        SetState(state);
    }

    public void HandleStartNewGameButton() => GameMaster.instance.StartNewGame();
    public void HandleExitButton() => GameMaster.instance.ExitGame();
}
