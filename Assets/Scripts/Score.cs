using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    [SerializeField]TextMeshProUGUI scoreText;
    // Use this for initialization
    int score = 5;
   
    
	void Start () {

        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () 
    {
        scoreText.text = score.ToString();
    }

    public void ReduceScore()
    {
        score--;
    }
}
