using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabBrustAnim : MonoBehaviour
{
	public GameObject soul;
	public HealthManager hm;
	public AudioControl ac;
	public Material[] brustMat;
	public Material[] defaultMat;
	public MeshRenderer[] renderers;
	float lhp = 0;

	
	void ChangeMat(Material[] mat){
		foreach(var v in renderers){
			v.materials = mat;
		}
	}
	
	void ChangeDefaultMat(){
		ChangeMat(defaultMat);
	}

    // Update is called once per frame
    void Update()
    {
		if(hm == null){
			Destroy(this);
		}
        if(hm.hp > lhp){
			lhp = hm.hp;
			return;
		}
		if(hm.hp < lhp){
			lhp = hm.hp;
			ac.PlayBrust();
			ChangeMat(brustMat);
			Invoke("ChangeDefaultMat", 0.35f);
			for(int i = 0;i< 5; i++){
				Instantiate(soul).transform.position = transform.position + new Vector3(Random.Range(-0.5f,0.5f), Random.Range(1,3), Random.Range(-1,1));
			}
		}
    }
}
