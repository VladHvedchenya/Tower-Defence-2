using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Bank : MonoBehaviour
{
    [SerializeField] private int _startBalance = 150;

    [SerializeField] private int _currentBalance;

    [SerializeField] private TextMeshPro _displayBalance;

    public int CurrentBanalce => _currentBalance;

    private void Awake()
    {
        _currentBalance = _startBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        _currentBalance += Mathf.Abs(amount);
        UpdateDisplay();
    }

    public void Withdraw(int amount)
    {
        _currentBalance -= Mathf.Abs(amount);
        UpdateDisplay();

        if (_currentBalance < 0)
            ReloadScene();
    }

    private void UpdateDisplay()
    {
        _displayBalance.text = "Gold: " + _currentBalance;
    }

    private void ReloadScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }
}
