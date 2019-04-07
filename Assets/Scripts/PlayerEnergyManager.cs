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
    [SerializeField] private AudioClip chargeClip;

    private float _energy;
    private float energyPerSymbol;
    private AudioSource audioSource;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        energyPerSymbol = maxEnergy / energySymbols.Length;
        energy = maxEnergy;
    }

    private IEnumerator Charge()
    {
        float smootingTime = 0.5f;
        audioSource.clip = chargeClip;
        audioSource.loop = true;
        audioSource.Play();
        while (isCharging)
        {
            energy += chargePerSecond * smootingTime;
            yield return new WaitForSeconds(smootingTime);
            if (energy >= maxEnergy)
                audioSource.loop = false;
        }
        audioSource.loop = false;
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
