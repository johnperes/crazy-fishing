using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLine : MonoBehaviour
{
    [SerializeField]
    GameObject boat;

    [SerializeField]
    GameObject hook;

    [SerializeField]
    LineRenderer line;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, boat.transform.position);
        line.SetPosition(1, hook.transform.position);
    }
}
