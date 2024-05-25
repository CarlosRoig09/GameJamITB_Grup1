using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject plantObjective;
    public Rigidbody2D rb;

    public EnemyBehaviour state =null;
    public EnemyFollowing following = new();
    public EnemyLeaving leaving = new();

    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeState(following);
    }

    // Update is called once per frame
    void Update()
    {
        state.StateUpdate(this);
        if(rb.velocity.x > 0) transform.eulerAngles = new Vector3(0, 180, 0);
        else transform.eulerAngles = new();
    }
    public void ChangeState(EnemyBehaviour newState)
    {
        state?.StateExit(this);
        state = newState;
        state.StateEnter(this);
    }
    public bool SearchNewPlant()
    {
        plantObjective = GameObject.FindGameObjectWithTag("Plant");
        return plantObjective != null;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("MainCamera")) state.StateExit(this);
    }
}
