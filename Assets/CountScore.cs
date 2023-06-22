using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CountScore : MonoBehaviour
{
    private int score;
    private List<GameObject> flocks;
    public TextMeshProUGUI scoreText;

    public string nama;

    // Start is called before the first frame update
    void Start()
    {
        flocks = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = nama + ": " + score;
    }

    void OnTriggerEnter2D(Collider2D other) {
        // Debug.Log("kena");
        if (other.gameObject.tag == "Flock") {
            
            score++;
            flocks.Add(other.gameObject);
        }
    }

}
