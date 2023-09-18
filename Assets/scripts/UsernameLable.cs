using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using TMPro;
using System;

public class UsernameLable : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _label;

    private void Reset()
    {
        _label = GetComponent<TMP_Text>();
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
            SetLabelUsername(currentUser.UserId);
        }
    }

    private void SetLabelUsername(string userId)
    {
        FirebaseDatabase.DefaultInstance
     .GetReference("users/"+ userId+"/username")
     .GetValueAsync().ContinueWithOnMainThread(task => 
     {
         if (task.IsFaulted)
         {
            Debug.Log(task.Exception);
         }
         else if (task.IsCompleted)
         {
             DataSnapshot snapshot = task.Result;
             Debug.Log(snapshot.Value);
             _label.text = (string)snapshot.Value;
         }
     });
    }
}


