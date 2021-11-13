using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackControl : MonoBehaviour
{
	public GameObject dashSoul;
	public GameObject model;
	public MoveControl mc;
	public AudioControl ac;
	public GameObject armorPrefab;
	public GameObject[] heads;
	public HKLabColliderControl cc;
	IEnumerator Shoot()
	{
		mc.isIdle = false;
		if (mc.target == null) yield break;

		yield return mc.FaceTarget();
		ac.PlayShoot();
		int c = Random.Range(1, 3);
		for (int i = 0; i < c; i++)
		{
			var head = heads[Random.Range(0, heads.Length - 1)];
			var ar = Instantiate(armorPrefab);
			ar.GetComponent<ArmorControl>().sender = gameObject;
			var ap = ar.transform.position = head.transform.position;
			ar.transform.localScale = new Vector3(mc.facingRight ? -1 : 1, 1, 1);
			var tp = mc.target.transform.position;
			var ag = tp - ap;
			var angle = Mathf.Atan2(ag.y, ag.x) * Mathf.Rad2Deg;
			//head.transform.parent.localEulerAngles = new Vector3(0,0,angle + 90);
			if (ag.x > ag.y)
			{
				ag *= Mathf.Abs(30f / ag.x);
			}
			else
			{
				ag *= Mathf.Abs(30f / ag.y);
			}
			ar.GetComponent<Rigidbody2D>().velocity = new Vector2(ag.x, ag.y + Random.Range(-5f, 5f));
		}
		yield return new WaitForSeconds(0.5f);
		mc.isIdle = true;
	}

	IEnumerator Dash()
	{
		//Try to fly target Y
		mc.isIdle = false;
		yield return mc.FaceTarget();
		

		mc.canTurn = false;
		var t = mc.target;
		int count = 0;
		//Debug.Log(transform.position.ToString() + "-" + t.transform.position.ToString());

		while (Mathf.Abs(transform.position.y - t.transform.position.y) > 0.15f && count < 150)
		{
			count++;
			yield return new WaitForSeconds(0.001f);
			var p = transform.position;
			if (p.y > t.transform.position.y)
			{
				if (cc.HitDown) break;
				p.y -= 0.1f;
			}
			else
			{
				if (cc.HitTop) break;
				p.y += 0.1f;
			}
			transform.position = p;
		}
		if (Mathf.Abs(transform.position.y - transform.transform.position.y) > 3) yield break;
		float ta = mc.facingRight ? -2 : 2;
		var q = transform.localEulerAngles;
		q.z = 0;
		transform.localEulerAngles = q;
		for (int i = 0; i < 45; i++)
		{
			
			q = transform.localEulerAngles;
			q.z += ta;
			transform.localEulerAngles = q;
			yield return new WaitForSeconds(0.001f);
		}
		ac.PlayShoot();
		mc.rig.velocity = new Vector2(mc.facingRight ? 30 : -30, 0);
		dashSoul?.SetActive(true);
		float st = Time.time;
		yield return new WaitForSeconds(0.25f);
		while(Time.time - st < 1.25f)
        {
			if (mc.rig.velocity.x < 1) break;
			yield return new WaitForSeconds(0.001f);
        }
		dashSoul?.SetActive(false);
		mc.rig.velocity = Vector2.zero;
		for (int i = 0; i < 45; i++)
		{
			mc.rig.velocity = Vector2.zero;
			q = transform.localEulerAngles;
			q.z -= ta;
			transform.localEulerAngles = q;
			yield return new WaitForSeconds(0.001f);
		}
		q.z = 0;
		transform.localEulerAngles = q;
		mc.rig.velocity = Vector2.zero;
		mc.canTurn = true;
		mc.isIdle = true;
		yield return new WaitForSeconds(0.75f);
	}

	IEnumerator Test()
	{
		while (true)
		{
			yield return null;
			if (mc.target == null) continue;
			yield return mc.FaceTarget();
			int count = 0;
			while (Vector2.Distance(mc.target.transform.position, transform.position) < 10 && count < 100)
			{
				count++;
				yield return new WaitForSeconds(0.001f);
				if (mc.facingRight)
				{
					if (cc.HitRight) break;
					transform.position = transform.position + new Vector3(-0.1f, 0, 0);
				}
				else
				{
					if (cc.HitLeft) break;
					transform.position = transform.position + new Vector3(0.1f, 0, 0);
				}
			}
			yield return null;
			if (Random.Range(0, 8) < 3)
			{
				yield return Dash();
			}
			else
			{
				yield return Shoot();
			}
			yield return new WaitForSeconds(Random.Range(0, 0.5f));
		}
	}

	void OnEnable()
	{
		StartCoroutine(Test());
	}
}
