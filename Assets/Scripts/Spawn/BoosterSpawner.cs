using UnityEngine;

public class BoosterSpawner : Spawner
{
    [SerializeField] private GameObject[] _boosters;

    private float _delay = 10f;
    private float _yPos = 0.7f;

    private void Start() => StartCoroutine(Spawn(_boosters, _delay, _yPos));
}
