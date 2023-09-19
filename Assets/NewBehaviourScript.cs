using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class TableData
{
    public string Name;
    public string Type;
    public string Speaker;
    public string Place;
    public string Description;
    public string TimeStart;
    public string TimeEnd;
}


public class TableDataManager: MonoBehaviour
{
    public List<TableData> data;
}
