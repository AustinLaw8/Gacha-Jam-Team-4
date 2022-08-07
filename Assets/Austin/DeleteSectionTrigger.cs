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
        switch (hit.gameObject.tag)
        {
            case "Section":
                gameManager.DestroyLastSection();
                break;
            case "EndMarker":
                Destroy(hit.transform.parent.gameObject);
                gameManager.ClearParts();
                break;
        }
    }
}
