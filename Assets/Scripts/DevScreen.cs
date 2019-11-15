using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class DevScreen : MonoBehaviour
{
    public Toggle showRouteTog, customTanksTog;
    public Toggle oneUp, levelUp, grenade, helmet, shovel, stopwatch;
    public InputField fastTankInput, bigTankInput, armoredTankInput;

    // Start is called before the first frame update
    void Start()
    {
        // Update toggles
        showRouteTog.isOn = GameManager.isShowRoute;
        customTanksTog.isOn = GameManager.isCustomTanks;

        fastTankInput.text = GameManager.customFastTanks.ToString();
        bigTankInput.text = GameManager.customBigTanks.ToString();
        armoredTankInput.text = GameManager.customArmoredTanks.ToString();

        oneUp.isOn = GameManager.itemOneUp;
        levelUp.isOn = GameManager.itemLevelUp;
        grenade.isOn = GameManager.itemGrenade;
        helmet.isOn = GameManager.itemHelmet;
        shovel.isOn = GameManager.itemShovel;
        stopwatch.isOn = GameManager.itemStopwatch;
    }

    public void ApplyGraphics()
    {
        GameManager.isShowRoute = showRouteTog.isOn;
        GameManager.isCustomTanks = customTanksTog.isOn;
        GameManager.itemOneUp = oneUp.isOn;
        GameManager.itemLevelUp = levelUp.isOn;
        GameManager.itemGrenade = grenade.isOn;
        GameManager.itemHelmet = helmet.isOn;
        GameManager.itemShovel = shovel.isOn;
        GameManager.itemStopwatch = stopwatch.isOn;

        int customFastTanks = 0;
        int customBigTanks = 0;
        int customArmoredTanks = 0;

        int.TryParse(fastTankInput.text, out customFastTanks);
        int.TryParse(bigTankInput.text, out customBigTanks);
        int.TryParse(armoredTankInput.text, out customArmoredTanks);

        if (customFastTanks < 0) customFastTanks = 0;
        if (customBigTanks < 0) customBigTanks = 0;
        if (customArmoredTanks < 0) customArmoredTanks = 0;

        GameManager.customFastTanks = customFastTanks;
        GameManager.customBigTanks = customBigTanks;
        GameManager.customArmoredTanks = customArmoredTanks;

        fastTankInput.text = GameManager.customFastTanks.ToString();
        bigTankInput.text = GameManager.customBigTanks.ToString();
        armoredTankInput.text = GameManager.customArmoredTanks.ToString();
    } 
}
