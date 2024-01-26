using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int _maximumHealthPoints = 5;

    [Tooltip("Adds amount to max hitpoints when enemy dies.")]
    [SerializeField] private int _difficultyRamp = 1;

    private int _currentHealthPoint;
    private Enemy _enemy;

    private void Start()
    {
        _enemy = GetComponent<Enemy>();
    }

    private void OnEnable()
    {
        _currentHealthPoint = _maximumHealthPoints;
    }

    private void OnParticleCollision(GameObject other)
    {
        Hit();
    }

    private void Hit()
    {
        _currentHealthPoint--;

        if (_currentHealthPoint <= 0)
        {
            gameObject.SetActive(false);
            _maximumHealthPoints += _difficultyRamp;
            _enemy.RewardGold();
        }
    }
}
