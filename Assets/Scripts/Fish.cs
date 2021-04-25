using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField]
    GameObject effect;

    [SerializeField]
    TMP_Text scoreLabel;

    public int score = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Catch(int currentScore)
    {
        scoreLabel.gameObject.SetActive(true);
        scoreLabel.text = currentScore.ToString();
        effect.SetActive(true);
        GetComponent<SpriteRenderer>().enabled = false;
        Invoke("Clean", 1.1f);
    }

    void Clean()
    {
        Destroy(gameObject);
    }
}
