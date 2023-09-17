using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System;

public class LoadSceneWhenUserAuthenticated : MonoBehaviour
{

    [SerializeField]
    private string _SceneToLoad = "GameScene";

    // Start is called before the first frame update
    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthStateChange;
    }

    private void HandleAuthStateChange(object sender, EventArgs e)
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            SceneManager.LoadScene(_SceneToLoad);
        }
    }

    void OnDestroy()
    {
       FirebaseAuth.DefaultInstance.StateChanged -= HandleAuthStateChange;
    }
}
