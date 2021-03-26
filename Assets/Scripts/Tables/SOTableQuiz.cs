using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SOTableItemQuiz : SOTableItem
{
    public string question;
    public string options;
}

[System.Serializable]
public class SOTableQuiz : SOTable<SOTableItemQuiz>
{

}
