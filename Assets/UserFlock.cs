using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserFlock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(1, 1)]
    public int startingCount = 1;
    const float agentDensity = 1.5f;

    // move speed
    [Range(1f, 100f)]
    public float driveFactor = 10f;
    [Range(1f, 100f)]
    public float maxSpeed = 5f;
    [Range(1f, 10f)]
    public float neighborRadius = 1.5f;
    [Range(0f, 1f)]
    public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * agentDensity,
                Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
                transform
                );
            newAgent.name = "Agent " + i;
            newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    static Vector2 move = new Vector2(0f, 5f);
    public Vector2 center;

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            if (Input.GetKey(KeyCode.Mouse0)){
                move.x = (mousePos.x - transform.position.x) * driveFactor;
                move.y = (mousePos.y - transform.position.y) * driveFactor;
            }

        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / 15f;
        if (t >= 1.5f)
        {
            move = centerOffset*t*t;
        }

        if (move.sqrMagnitude > squareMaxSpeed){
                    move = move.normalized * maxSpeed;
                }

        if (Input.GetKey("w") && Input.GetKey("a")){
            move = new Vector2(-3.5f, 3.5f);
        } else if (Input.GetKey("w") && Input.GetKey("d")){
            move = new Vector2(3.5f, 3.5f);
        } else if (Input.GetKey("s") && Input.GetKey("a")) {
            move = new Vector2(-3.5f, -3.5f);
        } else if (Input.GetKey("s") && Input.GetKey("d")) {
            move = new Vector2(3.5f, -3.5f);
        } else {
            move = (Input.GetKey("w")) ? new Vector2(0f, 5f) : (Input.GetKey("s")) ? new Vector2(0f, -5f) : (Input.GetKey("a")) ? new Vector2(-5f, 0f) : (Input.GetKey("d")) ? new Vector2(5f , 0f) : move;
        }

        agent.move(move);
        }
    }

    void OnGUI() {
        GUILayout.BeginArea(new Rect(Screen.width - 400, 0, 400, Screen.height));
        GUILayout.Label(move.x + "\n" + move.y);
        GUILayout.EndArea();
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighborRadius);
        foreach (Collider2D c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }
}
