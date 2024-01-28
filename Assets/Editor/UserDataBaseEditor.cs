using UnityEngine;
using UnityEditor;
using System;
using Random = UnityEngine.Random;

[CustomEditor(typeof(UserDataBase))]
public class UserDataBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UserDataBase userDataBase = (UserDataBase)target;

        if (GUILayout.Button("Generate Users"))
        {
            foreach (var user in userDataBase.users)
            {
                user.Id = Guid.NewGuid().ToString();
                user.FirstName = NameGenerator.GetRandomFirstName();
                user.LastName = NameGenerator.GetRandomLastName();
                user.Username = NameGenerator.GetRandomUserName(user.FirstName, user.LastName);
                user.Age = Random.Range(0, 90);
            }

            EditorUtility
                .SetDirty(userDataBase); // Marcar el ScriptableObject como 'sucio' para asegurar que los cambios se guarden
        }
    }
}