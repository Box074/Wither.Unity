using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabDeadhead : MonoBehaviour
{
	public GameObject soul;
	public GameObject spawnPoint;
	float st = 0;
    // Start is called before the first frame update
    void OnEnable()
    {
        rig.velocity = new Vector2(Random.Range(-25,25),Random.Range(1,25));
		st = 4.5f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
		if(st <= 0) return;
		st -= Time.fixedDeltaTime;
		if(Random.Range(0,10)<5f) return;
        Instantiate(soul).transform.position = spawnPoint.transform.position + 
		new Vector3(Random.Range(-0.5f,0.5f), Random.Range(-0.5f,0.5f), Random.Range(-1,1));
    }
	
	public Rigidbody2D rig;
}
