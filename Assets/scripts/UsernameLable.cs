using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using System;

public class UsernameLable : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _lable;

    private void Reset()
    {
        _lable = GetComponent<TMP_Text>();
    }

    void Start()
    {
        FirebaseAuth.DefaultInstance.StateChanged += HandleAuthChange;
    }

    private void HandleAuthChange(object sender, EventArgs e)
    {
        var currentUser = FirebaseAuth.DefaultInstance.CurrentUser;

        if (currentUser != null)
        {
            _lable.text = currentUser.Email;
        }
    }
}


