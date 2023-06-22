using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Stay In Cage")]
public class StayInCage : FlockBehavior
{
    public GameObject cage;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
      float t;
        
        Vector2 cageOffset = cage.transform.position - agent.transform.position;
        t = cageOffset.magnitude / (cage.transform.localScale.x / 2);
        // Debug.Log(t);
        if (t < 0.999 && t > 0.9) {
            
            agent.GetComponentInChildren<SpriteRenderer>().color = Color.green;
            return cageOffset * t * t;
        }  

        return Vector2.zero;
    }
}
