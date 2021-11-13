using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabPositionCons : MonoBehaviour
{
	public GameObject target;
	public bool controlTarget;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(controlTarget){
			var p = transform.position;
			target.transform.position = p;
			transform.position = p;
		}else{
			var p = target.transform.position;
			transform.position = p;
			target.transform.position = p;
		}
    }
}
