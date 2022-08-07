using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionScene : MonoBehaviour
{
    public void transition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
