using System.Collections;
using UnityEngine;

public class MonsterSpawner : Spawner 
{
    [SerializeField] private GameObject[] _monsterPrefab;

    private float _minDelayBetweenSpawns;
    private float _maxDelayBetweenSpawns;
    private float _yPos = 0;

    private Coroutine _coroutine;

    public void FreezeSpawn(float time)
    {
        StopCoroutine(_coroutine);
        _coroutine = null;
        Invoke("StartSpawn", time);
    }

    private void StartSpawn()
    {
        SetSpawnParams(GameMaster.instance.CurrentDifficulty);
       
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
        
        _coroutine = StartCoroutine(Spawn(_monsterPrefab,
            CalculateSpawnDelay(_minDelayBetweenSpawns, _maxDelayBetweenSpawns), _yPos));
    }

    protected override IEnumerator Spawn(GameObject[] spawnObjects, float delay, float yPos)
    {
        while (true)
        {
            delay = CalculateSpawnDelay(_minDelayBetweenSpawns, _maxDelayBetweenSpawns);

            yield return new WaitForSeconds(delay);

            var selectedObject = Random.Range(0, spawnObjects.Length);

            Instantiate(spawnObjects[selectedObject], 
                ChooseRandomHorizontalPosition(yPos), spawnObjects[selectedObject].transform.rotation);

            GameMaster.instance.UpdateMonsterCount(1);
            SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.Spawn, 0.7f);
        }
    }

    private void SetSpawnParams(GameMaster.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case GameMaster.Difficulty.EASY:
                _minDelayBetweenSpawns = 1.0f;
                _maxDelayBetweenSpawns = 1.3f;
                break;

            case GameMaster.Difficulty.MEDIUM:
                _minDelayBetweenSpawns = 0.9f;
                _maxDelayBetweenSpawns = 1.2f;
                break;

            case GameMaster.Difficulty.HARD:
                _minDelayBetweenSpawns = 0.9f;
                _maxDelayBetweenSpawns = 1.1f;
                break;

            case GameMaster.Difficulty.VERY_HARD:
                _minDelayBetweenSpawns = 0.7f;
                _maxDelayBetweenSpawns = 1f;
                break;
        }        
    }

    private void Start()
    {
        GameMaster.instance.DifficultyChanged += StartSpawn;

        StartSpawn();     
    }
}
