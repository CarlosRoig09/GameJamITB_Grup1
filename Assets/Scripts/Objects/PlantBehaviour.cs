using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : ObjectBehaviour
{
    private bool anim;
    private int _countGrow;
    private void Awake()
    {
        var waterController = GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>();
        if ((objectSO as PlantSO).Price <= waterController.WaterValue)
        {
            waterController.ModWater((objectSO as PlantSO).Price * -1);
        }
        else
        {
            Destroy(gameObject);
        }

        anim = false;
        GetComponent<Animator>().SetBool("Anim", anim);
        GameManager.Instance.OnChangeDay += Sold;
        _countGrow = 0;
    }

    //private IEnumerator Buyable(float time)
    //{
    //   
    //}


    public void Sold(DayCicle dayCicle)
    {
        if (dayCicle == DayCicle.Day && anim)
        {
            GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>().ModWater(objectSO.SoldPrice);
            Destroy(gameObject);
        }

        else if(dayCicle == DayCicle.Day)
        {
                _countGrow += 1;
            if (_countGrow >= objectSO.TimeGrow)
            {
                anim = true;
                GetComponent<Animator>().SetBool("Anim", anim);
            }
        }
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnChangeDay -= Sold;
    }
}
