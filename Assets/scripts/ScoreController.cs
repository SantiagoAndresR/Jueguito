using Firebase.Auth;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    DatabaseReference mDatabase;
    string UserId;

    int score = 0;
    // Start is called before the first frame update
    void Start()
    {
        mDatabase = FirebaseDatabase.DefaultInstance.RootReference;
        UserId = FirebaseAuth.DefaultInstance.CurrentUser.UserId;

        GetUserScore();
    }



    public void WriteNewScore(int score)
    {
        mDatabase.Child("users").Child(UserId).Child("score").SetValueAsync(score);
    }

    public void GetUserScore()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users/" + UserId + "/score")
            .GetValueAsync().ContinueWithOnMainThread(task => {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;
                    Debug.Log("Score: " + snapshot.Value);
                    score = (int)snapshot.Value;
                    GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;

                }
            });
    }
    public void GetUsersHighestScores()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("users").OrderByChild("score").LimitToLast(5)
            .GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log(task.Exception);
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot = task.Result;

                    foreach (var userDoc in (Dictionary<string, object>)snapshot.Value)
                    {
                        var userObject = (Dictionary<string, object>)userDoc.Value;
                        Debug.Log(userObject["username"] + " : " + userObject["score"]);
                    }



                }
            });
    }


    public void IncrementScore()
    {
        score += 1;
        GameObject.Find("LabelScore").GetComponent<TMPro.TMP_Text>().text = "Score: " + score;
        WriteNewScore(score);
    }
}

public class UserData
{
    public int score;
    public string username;
}
