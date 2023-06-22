using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UserFlock1 : MonoBehaviour
{

    public FlockAgent agentPrefab;
    private FlockAgent agent;
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
    PlayerControls controls;
    Vector2 PlayerMove;
    // void Awake() {
    //     controls = new PlayerControls();
        
    // }

    private void OnMovePlayer1(InputValue value) {
        PlayerMove = value.Get<Vector2>();
        Debug.Log(PlayerMove);
    }

    private void OnBoostPlayer1()
    {
        maxSpeed = 7f;
        Debug.Log("boo");
        move = new Vector2(move.x + Mathf.Sign(move.x) * 2f, move.y + Mathf.Sign(move.y) * 2f);
        if (move.sqrMagnitude > squareMaxSpeed)
        {
            move = move.normalized * maxSpeed;
        }
        agent.move(move);
    }

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        
        agent = Instantiate(
            agentPrefab,
            Random.insideUnitCircle * startingCount * agentDensity,
            Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)),
            transform
            );
        agent.name = "UserFlock";
        agent.Initialize(this);
        
    }

    static Vector2 move = new Vector2(0f, 5f);
    public Vector2 center;

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(horizontalRight);
        scoring(agent);

        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        if (Input.GetKey(KeyCode.Mouse0)){
            move.x = (mousePos.x - transform.position.x) * driveFactor;
            move.y = (mousePos.y - transform.position.y) * driveFactor;
        }

        if (PlayerMove != new Vector2(0, 0)) {
            move = PlayerMove * driveFactor;
        }


        Vector2 centerOffset = center - (Vector2)agent.transform.position;
        float t = centerOffset.magnitude / 25f;
        if (t >= 1.5f)
        {
            move = centerOffset*t*t;
        }



        bool key = false;
        if (Input.GetKey("i") && Input.GetKey("j")){
            move = new Vector2(-3.5f, 3.5f);
            key = true;
        } else if (Input.GetKey("i") && Input.GetKey("k")){
            move = new Vector2(3.5f, 3.5f);
            key = true;
        } else if (Input.GetKey("k") && Input.GetKey("j")) {
            move = new Vector2(-3.5f, -3.5f);
            key = true;
        } else if (Input.GetKey("k") && Input.GetKey("l")) {
            move = new Vector2(3.5f, -3.5f);
            key = true;
        } else {
            move = (Input.GetKey("i")) ? new Vector2(0f, 5f) : (Input.GetKey("k")) ? new Vector2(0f, -5f) : (Input.GetKey("j")) ? new Vector2(-5f, 0f) : (Input.GetKey("l")) ? new Vector2(5f , 0f) : move;
            key = true;
        }

        if (Input.GetKey("space")){
            maxSpeed = 7f;
            if (key){
                move = new Vector2(move.x+Mathf.Sign(move.x)*2f, move.y+Mathf.Sign(move.y)*2f);
            }
        } else {
            maxSpeed = 5f;
        }

        if (move.sqrMagnitude > squareMaxSpeed){
                    move = move.normalized * maxSpeed;
            }

        agent.move(move);
    }

    // void OnGUI() {
    //     GUI.skin.label.alignment = TextAnchor.UpperLeft;
    //     GUILayout.BeginArea(new Rect(10, Screen.height-100, Screen.width, Screen.height));
    //     GUILayout.Label("Target: " + Flock.total*0.75 + "\nPos. X:" + move.x + "\nPos. Y:"  + move.y + "\nScore: " + GameEnding.getScore() + "\nRemaining Time: " + (int) (GameEnding.playTime - Time.deltaTime));
    //     GUILayout.EndArea();
    // }

    void scoring(FlockAgent agent)
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
        GameEnding.setScore(context.Count);
    }
}
