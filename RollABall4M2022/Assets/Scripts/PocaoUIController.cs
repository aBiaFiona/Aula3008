using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PocaoUIController : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private TMP_Text pocaoText;
    
      private void OnEnable() 
      {
          PlayerObserverManager.OnPocaoChanged += UpdatePocaoText;
      }

      private void OnDisable()
      {
          PlayerObserverManager.OnPocaoChanged -= UpdatePocaoText;
      }

      private void UpdatePocaoText(int newPocaoValue)
      {
          pocaoText.text = newPocaoValue.ToString();
      }

      // Update is called once per frame
    
}
