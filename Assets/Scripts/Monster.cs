using UnityEngine;



[RequireComponent(typeof(Animator), typeof(Rigidbody), typeof(BoxCollider))]
public class Monster : MonoBehaviour
{
    public delegate void LiveChangedEvent(float value, bool isDead);

    public event LiveChangedEvent LivesChanged;

    [SerializeField] private float _obstacleRange = 1.0f;
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationAngle = 150.0f;
    [SerializeField] private float _rayRadius = 0.75f;
    [SerializeField] private float _timeToDestroy = 1.0f;

    private int _maxLives;
    private int _lives;

    private Rigidbody _rb;
    private Animator _animator;
    private HealthBar _healthBar;
    private Transform _healthBarTransform;
    private Camera _camera;

    private bool _isDead;


    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBarTransform = _healthBar.GetComponent<Transform>();
        _camera = FindObjectOfType<Camera>();

        SetStats(GameMaster.instance.CurrentDifficulty);

        RotateInRandomDirection(Random.Range(-_rotationAngle, _rotationAngle));

        _lives = _maxLives;
    }
    private void FixedUpdate()
    {
        if (!_isDead) Move();
    }
    private void LateUpdate()
    {
        if (_healthBarTransform != null)
        {
            _healthBarTransform.LookAt(new Vector3(transform.position.x,
                                  _camera.transform.position.y,
                                  _camera.transform.position.z));
            _healthBarTransform.Rotate(0, 180, 0);
        }
    }

    private void OnMouseDown()
    {
        SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.Damage, 0.5f);

        if (!_isDead)
            ReduceLives(5);
    }

    private void Move()
    {
        _rb.transform.Translate(Vector3.forward * _speed);

        Ray ray = new Ray(_rb.transform.position, _rb.transform.forward);
        RaycastHit hit;

        if (Physics.SphereCast(ray, _rayRadius, out hit))
        {
            if (hit.distance < _obstacleRange && !hit.collider.isTrigger)
                RotateInRandomDirection(Random.Range(-_rotationAngle, _rotationAngle));
        }
    } 
    private void RotateInRandomDirection(float angle) => _rb.transform.Rotate(0, angle, 0);
    private void DestroyInstance() => Destroy(gameObject);   
    private void ReduceLives(int value)
    {
        _lives = Mathf.Clamp(_lives - value, 0, _maxLives);

        if(_lives == 0)
        {
            Die();
        }
        else
        {
            float percantageLives = (float)_lives / _maxLives;
            LivesChanged?.Invoke(percantageLives, false);
        }
    }
    public void Die()
    {
        LivesChanged?.Invoke(0, true);
        GameMaster.instance.UpdateMonsterCount(-1);

        _animator.SetTrigger("Death");
        _isDead = true;
        Invoke("DestroyInstance", _timeToDestroy);

        switch (gameObject.name)
        {
            case "Goblin(Clone)":
                SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.GoblinDie, 0.7f);
                break;

            case "Monster(Clone)":
                SoundMaster.instance.PlaySoundEffect(SoundMaster.Sounds.MonsterDie, 0.7f);
                break;
        }
    }
    private void SetStats(GameMaster.Difficulty difficulty)
    {
        switch (difficulty)
        {
            case GameMaster.Difficulty.EASY:
                _speed = 0.05f;
                _maxLives = 10;
                break;

            case GameMaster.Difficulty.MEDIUM:
                _speed = 0.07f;
                _maxLives = 15;
                break;

            case GameMaster.Difficulty.HARD:
                _speed = 0.08f;
                _maxLives = 20;
                break;

            case GameMaster.Difficulty.VERY_HARD:
                _speed = 0.1f;
                _maxLives = 30;
                break;
        }
    }          
}
