using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathControl : MonoBehaviour
{
	public GameObject soul;
	public HealthManager hm;
	public AttackControl akc;
	public AudioControl ac;
	public MoveControl mc;
	public Collider2D[] col;
	public GameObject heads;
	
	public Material[] deathMat;
	public Material[] defaultMat;
	public MeshRenderer[] renderers;

	
	void ChangeMat(Material[] mat){
		foreach(var v in renderers){
			v.materials = mat;
		}
	}
	
	bool die = false;

    // Update is called once per frame
    void Update()
    {
        if(hm.hp < 1){
			if(!die){
				die = true;
				StartCoroutine(Die());
			}
		}
    }
	
	IEnumerator Die(){
		var p = ac.player;
		var model = mc.model;
		foreach(var v in col) v.enabled = false;
		Destroy(mc.rig);
		Destroy(akc);
		Destroy(mc);
		ac.PlayDeath();
		Destroy(ac);
		
		float sp = 0.25f;
		bool d = true;
		for(; sp > 0; sp -= 0.0064f){
			if(d){
				ChangeMat(deathMat);
			}else{
				ChangeMat(defaultMat);
			}
			d = !d;
			yield return new WaitForSeconds(sp);
		}
		transform.localScale = new Vector3(1,1,1);
		heads.SetActive(true);
		//heads.transform.localEulerAngles = new Vector3(0,80,0);
		heads.transform.parent = null;
		Destroy(gameObject);
	}
}
