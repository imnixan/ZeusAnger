using System.Collections;
using UnityEngine;


    public class BgLay : MonoBehaviour
    {

        void Start()
        {
            GetComponent<RectTransform>().sizeDelta = BgResizer.BgSize;
        }

        
    }
