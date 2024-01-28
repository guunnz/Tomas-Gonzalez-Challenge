using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "UserDataBase", menuName = "Database/UserData", order = 1)]
public class UserDataBase : ScriptableObject
{
    public UserData[] users;

    public UserData GetUserByID(string id)
    {
        foreach (UserData user in users)
        {
            if (user.Id == id)
            {
                return user;
            }
        }

        return null;
    }

    public UserData GetRandomUser()
    {
        if (users.Length == 0)
            return null;

        return users[Random.Range(0, users.Length)];
    }
}