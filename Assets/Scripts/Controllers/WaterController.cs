using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterController : MonoBehaviour, IOnStartGame
{
    private float _waterValue = 0;
    private float _limitWater;
    public float WaterValue {  get { return _waterValue; } set {} }
    public void OnStartGame()
    {
        ModLimitWater(GameManager.Instance.LimitWater);
        ModWater(GameManager.Instance.WaterInitValue);
    }

    //If you want to increase the cuantity value in (+), if you want to decrease it, change value to (-)
    public void ModWater(float value)
    {
        _waterValue += value;
        if (_limitWater < _waterValue)
            _waterValue = _limitWater;
        else ComproveIfThereIsWater();
        UIManager.Instance.ModifyWaterHUD(_waterValue);
    }

    public void ModLimitWater(float value)
    {
        _limitWater += value;
        if (_waterValue > _limitWater)
            ModWater((_waterValue-_limitWater)*-1);
        UIManager.Instance.ModifyLimitWaterHUD(_limitWater);
    }

    private void ComproveIfThereIsWater()
    {
        if (_waterValue <= 0)
        {
            _waterValue = 0;
            UIManager.Instance.ModifyWaterHUD(_waterValue);
            UIManager.Instance.ModifyLimitWaterHUD(_limitWater);
            GameManager.Instance.GameOver();
        }
    }
    //Use it for shopping, plants.
    public bool UseWater(float quantity)
    {
        if(quantity>_waterValue)
            return false;
        ModWater(quantity * -1);
        return true;
    }
}
