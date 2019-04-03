using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEnergyManager : MonoBehaviour
{
    public bool isCharging { get; set; }

    [SerializeField] private float maxEnergy;
    [SerializeField] private float loseEnergyPerTile, loseEnergyOnLargeObject;
    [SerializeField] private GameObject[] energySymbols;
    [SerializeField] private float chargePerSecond;

    public float _energy;
    private float energyPerSymbol;

    private void Start()
    {
        energyPerSymbol = maxEnergy / energySymbols.Length;
        energy = maxEnergy;
    }

    private IEnumerator Charge()
    {
        float smootingTime = 1f;
        //audioSource.clip = chargeClip;
        //audioSource.loop = true;
        //audioSource.Play();
        while (isCharging)
        {
            energy += chargePerSecond * smootingTime;
            yield return new WaitForSeconds(smootingTime);
        }
        //audioSource.loop = false;
    }

    public void DeductTileEnergy()
    {
        energy -= loseEnergyPerTile;
    }
    public void DeductLargeObjectEnergy()
    {
        energy -= loseEnergyOnLargeObject;
    }

    public float energy
    {
        get
        {
            return _energy;
        }
        set
        {
            if (value >= 0 && value <= maxEnergy)
                _energy = value;
            else if (value < 0)
                _energy = 0;
            else if (value > maxEnergy)
                _energy = maxEnergy;

            for (int i = 0; i < energySymbols.Length; i++)
            {
                energySymbols[i].SetActive(_energy > (energyPerSymbol * i) ? true : false);
            }

        }
    }
}
