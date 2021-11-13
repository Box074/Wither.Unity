using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorControl : MonoBehaviour
{
	public GameObject sender;
	public Rigidbody2D rig;
	public GameObject exp;
	int w = 0;
	bool c = false;
    void OnTriggerEnter2D(Collider2D col){
		if(col.gameObject.tag != "Nail Attack"){
			if(col.gameObject.layer == 8){
				exp?.SetActive(true);
				Destroy(gameObject);
				return;
			}
			var hm = col.gameObject.GetComponentInParent<HealthManager>();
			if(hm!=null && (c || hm.gameObject != sender)){
				exp?.SetActive(true);
				hm.hp -= 20;
				Destroy(gameObject);
			}
			return;
		}
			c = true;
			transform.localScale = new Vector3(rig.velocity.x > 0 ? 1 : -1, 1, 1);
			rig.velocity = new Vector2(-rig.velocity.x, -rig.velocity.y);
	}
	void FixedUpdate(){
		var angle = Mathf.Atan2(rig.velocity.y , rig.velocity.x) * Mathf.Rad2Deg;
		rig.rotation = angle;
	}
	void Update(){
		w++;
		if(w>=1000){
			exp?.SetActive(true);
			Destroy(gameObject);
		}
	}
}
