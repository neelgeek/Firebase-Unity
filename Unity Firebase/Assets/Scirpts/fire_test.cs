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
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("<your firebase database link>");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
        
    }
	
	//Taking Input on button click
	public void AddData(InputField name)
    {
        User newuser = new User(name.text.ToString()); //Initializing Class User
        string json = JsonUtility.ToJson(newuser); //converting User classs to Json
        reference.Child("Users").Push().SetValueAsync(json); //Pushing to Database
    }
        

    public void RefreshData()
    {
        //gettting all the children with name field equal to "neel"
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
                }
            }

            );
    }
}






//Class User, it will be used as a data container
public class User
{
    public string name;
    public int n = 2;

    public User(string newName)
    {
        this.name = newName;
    }
}
