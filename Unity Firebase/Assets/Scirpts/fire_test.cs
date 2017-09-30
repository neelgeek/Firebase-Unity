using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class fire_test : MonoBehaviour {

    DatabaseReference reference;
	// Use this for initialization
	void Start () {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-firebase-5a873.firebaseio.com/");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
    }
	
	public void AddData(InputField name)
    {
        User newuser = new User(name.text.ToString());
        string json = JsonUtility.ToJson(newuser);
        reference.Child("Users").Push().SetValueAsync(json);
    }
        

    public void RefreshData()
    {
        
        FirebaseDatabase.DefaultInstance.GetReference("Users").OrderByChild("name").EqualTo("neel").GetValueAsync().ContinueWith(
            task =>
            {
                DataSnapshot snapshot = task.Result;
                var result = snapshot.Value as Dictionary<string, object>;
                foreach (var item in result)
                {
                    Debug.Log(item.Key); // Kdq6...
                    var values = JsonUtility.FromJson<User>(item.Value.ToString());
                    Debug.Log(values.name + " " + values.n);

                    //foreach (var v in values)
                    //{
                    //    Debug.Log(v.Key + ":" + v.Value); // category:livingroom, code:126 ...
                    //}
                }
            }

            );

      //  var newval = FirebaseDatabase.DefaultInstance.GetReference("Users").OrderByChild("name").EqualTo("Neel");
      
    }
}







public class User
{
    public string name;
    public int n = 2;

    public User(string newName)
    {
        this.name = newName;
    }
}
