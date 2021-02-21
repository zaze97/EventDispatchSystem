﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour
{
    
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {

            EventCenter.Broadcast<string>(EventType.ShowText,"开始");

        });
    }

}
