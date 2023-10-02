using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Chargement : MonoBehaviour
{
    [SerializeField] string nomDeLaScene;
    public void loadScene() {
        SceneManager.LoadScene(nomDeLaScene);
    }
}
