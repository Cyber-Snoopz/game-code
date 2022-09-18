using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerIdHolder : MonoBehaviour
{
    [SerializeField] private int answerId; //Serialized For Debugging
    
    public int getAnswerId()
    {
        return answerId;
    }
    public void setAnswerId(int id)
    {
        answerId = id;
    }
}
