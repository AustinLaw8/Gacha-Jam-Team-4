using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSectionTrigger : MonoBehaviour
{
   [SerializeField] private GameManager gameManager;

    void Start()
    {
        if (gameManager == null) gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D hit){
        if (hit.gameObject.tag == "Section") {
            gameManager.DestroyLastSection();
        }
    }
}
