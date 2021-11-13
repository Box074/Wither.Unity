using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAnim : MonoBehaviour
{
	public GameObject wither;
	public GameObject gate;
	float st = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
		st = Time.time;
		gate.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float f = Time.time - st;
		if(f < 5f){
			gate.transform.localScale = new Vector3(1, f * 0.2f, -1);
		}else if(f < 8){
			if(!wither.activeSelf){
				wither.SetActive(true);
				wither.transform.parent = null;
			}
		}else if(f <= 13f){
			gate.transform.localScale = new Vector3(1, 1 - (f - 8) * 0.2f, -1);
		}
    }
}
