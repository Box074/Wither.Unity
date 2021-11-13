using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabExpControl : MonoBehaviour
{
	public Sprite[] sprites;
	public SpriteRenderer render;
	public Material mat;
    void OnEnable()
    {
		gameObject.transform.parent = null;
		transform.localEulerAngles = Vector3.zero;
        StartCoroutine(Render());
    }
	
	IEnumerator Render(){
		foreach(var v in sprites){
			render.sprite = v;
			render.material = mat;
			yield return new WaitForSeconds(0.025f);
		}
		Destroy(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
