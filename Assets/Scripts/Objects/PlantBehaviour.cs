using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : ObjectBehaviour
{
    private bool anim;
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
        StartCoroutine(Buyable(objectSO.TimeGrow));
        GameManager.Instance.OnChangeDay += Sold;
    }

    private IEnumerator Buyable(float time)
    {
        yield return new WaitForSeconds(time);
        anim = true;
        GetComponent<Animator>().SetBool("Anim", anim);
    }


    public void Sold(DayCicle dayCicle)
    {
        if (dayCicle == DayCicle.Day&&anim)
        {
            GameObjectLibrary.Instance.WaterController.GetComponent<WaterController>().ModWater(objectSO.SoldPrice);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        StopAllCoroutines();
        GameManager.Instance.OnChangeDay -= Sold;
    }
}
