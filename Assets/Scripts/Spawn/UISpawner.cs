using UnityEngine;

public class UISpawner : MonoBehaviour
{
    [SerializeField] private GameObject uiPrefab;

    private void Start() => Instantiate(uiPrefab);
}
