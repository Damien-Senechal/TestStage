using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public static bool canJump;
    public static bool onWall;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.name.Equals("GroundTrigger"))
        {
            canJump = true;
        }
        if(this.gameObject.name.Equals("LeftTrigger") || this.gameObject.name.Equals("RightTrigger"))
        {
            onWall = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.gameObject.name.Equals("GroundTrigger"))
        {
            canJump = false;
        }
        if (this.gameObject.name.Equals("LeftTrigger") || this.gameObject.name.Equals("RightTrigger"))
        {
            onWall = false;
        }

    }
}
