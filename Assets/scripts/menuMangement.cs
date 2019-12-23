using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class menuMangement : MonoBehaviour
{
    // Start is called before the first frame update
    public void go(int id) 
    {
        SceneManager.LoadScene(id);
    }
}
