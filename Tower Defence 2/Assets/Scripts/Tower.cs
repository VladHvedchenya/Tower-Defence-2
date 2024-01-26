using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int _cost = 75;
    [SerializeField] private float _buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null)
            return false;

        if (bank.CurrentBanalce >= _cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(_cost);
            return true;
        }

        return false;
    }

    private IEnumerator Build()
    {
        var buildTime = new WaitForSeconds(_buildDelay);

        foreach (Transform children in transform)
        {
            children.gameObject.SetActive(false);

            foreach (Transform grandChild in children)
            {
                grandChild.gameObject.SetActive(false);
            }
        }

        while (enabled)
        {
            foreach (Transform children in transform)
            {
                children.gameObject.SetActive(true);
                yield return buildTime;

                foreach (Transform grandChild in children)
                {
                    grandChild.gameObject.SetActive(true);
                }
            }
        }
    }
}
