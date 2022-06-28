using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image _healthBarForeground;
    private Monster _monster;

    private void Start()
    {
        _monster = GetComponentInParent<Monster>();

        _monster.LivesChanged += OnLivesChanged; 
    }
    private void OnDestroy() => _monster.LivesChanged -= OnLivesChanged;
              
    private void OnLivesChanged(float percantageLives, bool isDead)
    {
        if (!isDead)
        _healthBarForeground.fillAmount = percantageLives;
        else Destroy(gameObject);
    }
}

