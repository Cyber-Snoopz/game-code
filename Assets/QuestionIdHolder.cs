using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionIdHolder : MonoBehaviour
{
    [SerializeField] int questionId; //Serialized For Debugging

    public int getQuestionId()
    {
        return questionId;
    }

    public void setQuestionId(int id)
    {
        questionId = id;
    }
}
