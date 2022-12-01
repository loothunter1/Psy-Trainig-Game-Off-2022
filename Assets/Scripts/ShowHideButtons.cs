using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideButtons : MonoBehaviour
{
    public GameObject[] toHide;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        Debug.Log("Interface");
        if (Input.GetMouseButton(2))
        {
            for (int i = 0; i < toHide.Length; i++)
            {
                bool isActive = toHide[i].activeSelf;
                toHide[i].SetActive(!isActive);
            }
        }
    }
}
