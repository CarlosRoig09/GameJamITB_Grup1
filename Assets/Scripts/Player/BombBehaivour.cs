using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombBehaivour : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<Enemy>().ChangeState(collision.gameObject.GetComponent<Enemy>().leaving);
    }
}
