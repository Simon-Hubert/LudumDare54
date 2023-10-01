using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Danger")
        {
            //Placer code pour crever
            Debug.Log("AIE");
        }
    }
}
