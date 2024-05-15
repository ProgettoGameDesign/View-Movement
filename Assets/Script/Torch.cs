using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private Transform _torch;
    private Light _color;

    // Start is called before the first frame update
    void Start()
    {
        _color = _torch.GetComponent<Light>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) 
        {
            _color.color = Color.blue;  
        }
        if (Input.GetKey(KeyCode.Alpha2)) 
        {
            _color.color = Color.green;  
        }
        if (Input.GetKey(KeyCode.Alpha3)) 
        {
            _color.color = Color.red;  
        }
    }
}
