using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveControl : MonoBehaviour
{
	public HKLabColliderControl cc;
	public GameObject model;
	public bool canTurn = true;
    IEnumerator Test(){
		while(true){
			yield return new WaitForSeconds(0.001f);
			if(canTurn) rig.velocity = Vector2.zero;
			if(canTurn && isIdle){
				var tp = target.transform.position;
				//Face Target
				FaceTarget();
                if (facingRight)
                {
					if (cc.HitRight) continue;
                }
                else
                {
					if (cc.HitLeft) continue;
                }
				if(Vector2.Distance(tp, transform.position) > 5 || Mathf.Abs(tp.x - transform.position.x) < 3){
					var np = transform.position;
					var r = Random.Range(0,0.1f);
					if(facingRight){
						if(cc.HitRight) continue;
					}else{
						if(cc.HitLeft) continue;
					}
					np.x += facingRight ? r : -r;
					//np.y += Random.Range(-0.1f,0.1f);
					transform.position = np;
					
				}
			}
		}
	}
	
	public IEnumerator FaceTarget(){
		var tp = target.transform.position;
		//Face Target
		if(tp.x > transform.position.x){
			yield return TurnRight();
		}else{
			yield return TurnLeft();
		}
	}
	IEnumerator TurnLeft(){
		if(!canTurn) yield break;
		//Vector3 ry = model.transform.localEulerAngles;
		if(!facingRight) yield break;
		facingRight = false;
		/*
		for(float f = 0; f < 1 ; f += 0.08f){
			ry.y = Mathf.Lerp(rightRot,leftRot,f);
			model.transform.localEulerAngles = ry;
			yield return new WaitForSeconds(0.01f);
		}
		ry.y = leftRot;
		model.transform.localEulerAngles = ry;*/
		transform.localScale = new Vector3(-1 , 1, 1);
	}
	IEnumerator TurnRight(){
		if(!canTurn) yield break;
		//Vector3 ry = model.transform.localEulerAngles;
		if(facingRight) yield break;
		facingRight = true;
		/*
		for(float f = 0; f < 1 ; f += 0.08f){
			ry.y = Mathf.Lerp(leftRot,rightRot,f);
			model.transform.localEulerAngles = ry;
			yield return new WaitForSeconds(0.01f);
		}
		ry.y = rightRot;
		model.transform.localEulerAngles = ry;*/
		transform.localScale = new Vector3(1 , 1, 1);
	}
	
	void OnEnable(){
		StartCoroutine(Test());
	}
	public Rigidbody2D rig;
	public GameObject target;
	public bool facingRight;
	public bool isIdle;
}
