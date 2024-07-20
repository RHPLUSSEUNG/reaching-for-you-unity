using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionList : MonoBehaviour
{
    List<TempCompanion> companionList = new List<TempCompanion>();

    public void AddCompanion(TempCompanion companion)
    {
        if(!companionList.Contains(companion)) 
        {
            companionList.Add(companion);
        }
        else
        {
            Debug.Log("Already Exist Companion");
        }        
    }

    //public TempCompanion GetCompanion(string companionName)
    //{
    //    return companionList
    //}
}
