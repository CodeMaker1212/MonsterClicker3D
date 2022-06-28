using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    private const int MAX_MONSTERS_IN_SCENE = 10;

    public static GameMaster instance;

    public delegate void GameplayEvent();
    public delegate void MonsterCountEvent(int value);

    public event GameplayEvent DifficultyChanged;
    public event GameplayEvent GameOver;

    public event MonsterCountEvent MonsterCountChanged;
    public event MonsterCountEvent ScoreUpdated;

    public enum Difficulty { EASY, MEDIUM, HARD, VERY_HARD}

    public Difficulty CurrentDifficulty { get; private set; } = Difficulty.EASY;

    private int _monstersAtScene;
    private int _currentScore;

    public int CurrentScore { get { return _currentScore; }}
    public int BestScore { get; private set; }

    private void Awake()
    {
        instance = this;

        BestScore = PlayerDataHandler.LoadBestScore();
    }

    private void OnApplicationQuit() => PlayerDataHandler.SaveBestScore(BestScore);

    private void ChangeDifficulty(Difficulty difficulty)
    {
        CurrentDifficulty = difficulty;
        DifficultyChanged();
    }
    
    public void UpdateMonsterCount(int value)
    {
        _monstersAtScene= Mathf.Clamp(_monstersAtScene + value, 0, MAX_MONSTERS_IN_SCENE);
        MonsterCountChanged?.Invoke(_monstersAtScene);

        if(_monstersAtScene >= 10)
            GameOver?.Invoke();

        if (value < 0) UpdateScore();

        switch (_currentScore)
        {
            case 20:
                ChangeDifficulty(Difficulty.MEDIUM);
                break;

            case 50:
                ChangeDifficulty(Difficulty.HARD);
                break;

            case 75:
                ChangeDifficulty(Difficulty.VERY_HARD);
                break;
        }
    }    
    private void UpdateScore()
    {
        _currentScore++;

        if(_currentScore >= BestScore) BestScore = _currentScore;

        ScoreUpdated?.Invoke(_currentScore);
    }

    public void StartNewGame()
    {
        PlayerDataHandler.SaveBestScore(BestScore);
        SceneManager.LoadScene("Game");
    }  
    public void ExitGame() => Application.Quit();
    
    public void SetPause(bool value)
    {
        if (value == true) Time.timeScale = 0;
        else Time.timeScale = 1;
    }         
}
