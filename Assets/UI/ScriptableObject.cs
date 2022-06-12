using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ClassInfoText", order = 1)]
public class ClassInfoText : ScriptableObject
{
    public string className;

    public string[] textToWrite;
}