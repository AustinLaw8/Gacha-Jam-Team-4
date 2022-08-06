using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    [SerializeField] private string sceneToTransition;

    public void transition()
    {
        SceneManager.LoadScene(sceneToTransition);
    }
}
