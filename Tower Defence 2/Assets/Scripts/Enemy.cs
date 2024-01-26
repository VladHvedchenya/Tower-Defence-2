using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _goldReward = 25;
    [SerializeField] private int _goldPenalty = 25;

    private Bank _bank;

    private void Start()
    {
        _bank = FindObjectOfType<Bank>();
    }

    public void RewardGold()
    {
        if (_bank != null)
            _bank.Deposit(_goldReward);
    }

    public void StealGold()
    {
        if (_bank != null)
            _bank.Withdraw(_goldPenalty);
    }
}
