using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    UserFlock1 uFlock1;
    public UserFlock1 userFlock1 { get { return uFlock1; } }

    UserFlock2 uFlock2;
    public UserFlock2 userFlock2 { get { return uFlock2; } }

    Collider2D agentCollider;
    public Collider2D AgentCollider { get {return agentCollider;} }
    // Start is called before the first frame update
    void Start()
    {
       agentCollider = GetComponent<Collider2D>(); 
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    public void Initialize(UserFlock1 flock)
    {
        uFlock1 = flock;
    }

    public void Initialize(UserFlock2 flock)
    {
        uFlock2 = flock;
    }

    public void move(Vector2 velocity) {
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;

    }
}
