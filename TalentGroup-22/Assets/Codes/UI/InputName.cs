using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InputName : MonoBehaviour
{
    private string input;

    public void ReadStringInput(string enter)
    {
        input = enter;
        Debug.Log(input);
    }
    
    
}
