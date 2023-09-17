using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonLogout : MonoBehaviour, IPointerClickHandler 
{

    [SerializeField]
    private string _SceneToLoad = "Home";

    public void OnPointerClick(PointerEventData eventData)
    {
        FirebaseAuth.DefaultInstance.SignOut();

        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            SceneManager.LoadScene(_SceneToLoad);
        }
    }
}
