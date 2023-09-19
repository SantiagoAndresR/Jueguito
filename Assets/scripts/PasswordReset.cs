using Firebase.Auth;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Database;

public class PasswordReset : MonoBehaviour
{
    [SerializeField] private Button _resetPasswordButton;
    [SerializeField] private TMP_InputField _emailInputField;

    void Reset()
    {
        _resetPasswordButton = GetComponent<Button>();
        _emailInputField = GameObject.Find("InputEmail").GetComponent<TMP_InputField>();
    }

    void Start()
    {
        _resetPasswordButton.onClick.AddListener(HandleResetPasswordButtonClicked);
    }

    private void HandleResetPasswordButtonClicked()
    {
        string emailAddress = _emailInputField.text;

        var auth = FirebaseAuth.DefaultInstance;
        auth.SendPasswordResetEmailAsync(emailAddress).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SendPasswordResetEmailAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SendPasswordResetEmailAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }
}
