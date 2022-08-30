using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainManuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if (Keyboard.current.anyKey.inPressed)
        {
            GameManager.Instance.StarGame();
        }
    }
    
}
