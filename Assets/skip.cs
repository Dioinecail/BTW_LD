using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skip : MonoBehaviour {
    public void Skip()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}
