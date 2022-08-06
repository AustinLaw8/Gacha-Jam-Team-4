using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateNextSectionTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    void OnTriggerEnter2D(Collider2D hit){
        Debug.Log(hit);
        gameManager.GenerateNextSection();
    }
}
