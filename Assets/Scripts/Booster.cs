using UnityEngine;



public class Booster : MonoBehaviour
{
    private const float FREEZE_TIME = 3f;

    private float _rotationSpeed = 70f;
    private float _timeToDestroy = 5f;

    private Transform _transform;
    [SerializeField] private ParticleSystem _explosionEffect;
    

    private void Start()
    {
        _transform = GetComponent<Transform>();

        Invoke("DestroyInstance", _timeToDestroy);
    }

    private void Update() => _transform.Rotate(new Vector3(0, _rotationSpeed * Time.deltaTime, 0));
    private void DestroyInstance() => Destroy(gameObject);


    private void OnMouseDown()
    {
        _explosionEffect.Play();
        _explosionEffect.transform.parent = null;

        switch (gameObject.name)
        {
            case "Freeze Booster(Clone)":

                FindObjectOfType<MonsterSpawner>().FreezeSpawn(FREEZE_TIME);
                SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.Freeze, 1f);
              break;

            case "Kill All Booster(Clone)":

                SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.KillAll, 1.5f);

                var livingMonsters  = FindObjectsOfType<Monster>();

                foreach (var monster in livingMonsters)               
                    monster.Die();
              break;
        }
       
        DestroyInstance();
    }
}
