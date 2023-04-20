using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoint = 5;
    [SerializeField] private int difficultyRamp = 1;

    private int _currentHitPoints;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _currentHitPoints = maxHitPoint;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        _currentHitPoints--;
        if (_currentHitPoints < 1)
        {
            _enemy.RewardGold();
            maxHitPoint += difficultyRamp;
            gameObject.SetActive(false);
        }

    }
}
