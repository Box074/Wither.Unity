using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HKLabSoulAnim : MonoBehaviour
{
	public Sprite[] sprites;
	public SpriteRenderer render;
	public Material mat;
    void OnEnable()
    {
		gameObject.hideFlags = HideFlags.HideInHierarchy;
		gameObject.transform.parent = null;
		transform.localEulerAngles = Vector3.zero;
        StartCoroutine(Render());
    }
	
	IEnumerator Render(){
		render.enabled = true;
		foreach(var v in sprites){
			render.sprite = v;
			render.material = mat;
			yield return new WaitForSeconds(0.1f);
		}
		render.enabled = false;
		Destroy(gameObject);
	}

    // Update is called once per frame
    void Update()
    {
        
    }
}
