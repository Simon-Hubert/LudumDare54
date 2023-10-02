using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifebar : MonoBehaviour
{
    private GameObject[] mesCoeurs;
    int vie = 3;

    void Awake()
    {
        mesCoeurs = new GameObject[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            mesCoeurs[i] = transform.GetChild(i).gameObject;
        }        
    }

    public void updateVie()
    {
        vie--;
        for (int i = 0; i < transform.childCount; i++)
        {
            if(vie > i)
            {
                mesCoeurs[i].SetActive(true);
            } else {
                mesCoeurs[i].SetActive(false);
            }
        }
    }
}
