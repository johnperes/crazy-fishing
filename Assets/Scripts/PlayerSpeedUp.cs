using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpeedUp : MonoBehaviour
{
    Rigidbody2D rb;
    PlayerControls player;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerControls>();
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("SpeedUp", 0f, 1f);
    }

    // Update is called once per frame
    void SpeedUp()
    {
        if (player.started)
        {
            rb.drag -= 0.009f;
        }
    }
}
