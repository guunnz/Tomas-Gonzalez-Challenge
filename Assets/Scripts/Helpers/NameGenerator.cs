using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public static class NameGenerator
{
    private static readonly string[] FirstNameList =
        { "Sofía", "Martina", "Lucía", "Juan", "Matías", "Santiago", "Nicolás", "Agustín", "Facundo", "Camila" };

    private static readonly string[] LastNameList =
        { "González", "Rodríguez", "Pérez", "Fernández", "López", "García", "Martínez", "Silva", "Gómez", "Ruiz" };

    public static string GetRandomFirstName()
    {
        string firstName = FirstNameList[UnityEngine.Random.Range(0, FirstNameList.Length)];

        return firstName;
    }

    public static string GetRandomLastName()
    {
        string lastName = LastNameList[UnityEngine.Random.Range(0, FirstNameList.Length)];

        return lastName;
    }

    public static string GetRandomUserName(string firstName, string lastName)
    {
        int lastNameStartIndex = Random.Range(0, lastName.Length);
        int firstNameStartIndex = Random.Range(0, firstName.Length);

        return $"{lastName.Substring(lastNameStartIndex, lastName.Length - lastNameStartIndex)}{firstName.Substring(firstNameStartIndex, firstName.Length - firstNameStartIndex)}".ToLower().FirstCharacterToUpper();
    }
}