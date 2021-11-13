using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private BoxCollider2D boxCollider;

	// Token: 0x04000A3D RID: 2621
	private DamageHero damageHero;

	// Token: 0x04000A3E RID: 2622
	[Header("Assets")]
	[SerializeField]
	private AudioSource audioPlayerPrefab;

	// Token: 0x04000A40 RID: 2624
	[SerializeField]
	private GameObject blockHitPrefab;

	// Token: 0x04000A41 RID: 2625
	[SerializeField]
	private GameObject strikeNailPrefab;

	// Token: 0x04000A42 RID: 2626
	[SerializeField]
	private GameObject slashImpactPrefab;

	// Token: 0x04000A43 RID: 2627
	[SerializeField]
	private GameObject fireballHitPrefab;

	// Token: 0x04000A44 RID: 2628
	[SerializeField]
	private GameObject sharpShadowImpactPrefab;

	// Token: 0x04000A45 RID: 2629
	[SerializeField]
	private GameObject corpseSplatPrefab;


	// Token: 0x04000A48 RID: 2632
	[SerializeField]
	private GameObject smallGeoPrefab;

	// Token: 0x04000A49 RID: 2633
	[SerializeField]
	private GameObject mediumGeoPrefab;

	// Token: 0x04000A4A RID: 2634
	[SerializeField]
	private GameObject largeGeoPrefab;

	// Token: 0x04000A4B RID: 2635
	[Header("Body")]
	[SerializeField]
	public int hp;

	// Token: 0x04000A4C RID: 2636
	[SerializeField]
	private int enemyType;

	// Token: 0x04000A4D RID: 2637
	[SerializeField]
	private Vector3 effectOrigin;

	// Token: 0x04000A4E RID: 2638
	[SerializeField]
	private bool ignoreKillAll;

	// Token: 0x04000A4F RID: 2639
	[SerializeField]
	[Space]
	[UnityEngine.Tooltip("HP is scaled if in a GG boss scene (These are absolute values, not a multiplier. Leave 0 for no scaling).")]
	private HealthManager.HPScaleGG hpScale;

	// Token: 0x04000A50 RID: 2640
	[Header("Scene")]
	[SerializeField]
	private GameObject battleScene;

	// Token: 0x04000A51 RID: 2641
	[SerializeField]
	private GameObject sendHitTo;

	// Token: 0x04000A52 RID: 2642
	[SerializeField]
	private GameObject sendKilledToObject;

	// Token: 0x04000A53 RID: 2643
	[SerializeField]
	private string sendKilledToName;

	// Token: 0x04000A54 RID: 2644
	[Header("Geo")]
	[SerializeField]
	private int smallGeoDrops;

	// Token: 0x04000A55 RID: 2645
	[SerializeField]
	private int mediumGeoDrops;

	// Token: 0x04000A56 RID: 2646
	[SerializeField]
	private int largeGeoDrops;

	// Token: 0x04000A57 RID: 2647
	[SerializeField]
	private bool megaFlingGeo;

	// Token: 0x04000A58 RID: 2648
	[Header("Hit")]
	[SerializeField]
	private bool hasAlternateHitAnimation;

	// Token: 0x04000A59 RID: 2649
	[SerializeField]
	private string alternateHitAnimation;

	// Token: 0x04000A5A RID: 2650
	[Header("Invincible")]
	[SerializeField]
	private bool invincible;

	// Token: 0x04000A5B RID: 2651
	[SerializeField]
	private int invincibleFromDirection;

	// Token: 0x04000A5C RID: 2652
	[SerializeField]
	private bool preventInvincibleEffect;

	// Token: 0x04000A5D RID: 2653
	[SerializeField]
	private bool hasAlternateInvincibleSound;

	// Token: 0x04000A5E RID: 2654
	[SerializeField]
	private AudioClip alternateInvincibleSound;

	// Token: 0x04000A5F RID: 2655
	[Header("Death")]

	// Token: 0x04000A60 RID: 2656
	[SerializeField]
	public bool hasSpecialDeath;

	// Token: 0x04000A61 RID: 2657
	[SerializeField]
	public bool deathReset;

	// Token: 0x04000A62 RID: 2658
	[SerializeField]
	public bool damageOverride;

	// Token: 0x04000A63 RID: 2659
	[SerializeField]
	private bool ignoreAcid;

	// Token: 0x04000A64 RID: 2660
	[Space]
	[SerializeField]
	private bool showGodfinderIcon;

	// Token: 0x04000A65 RID: 2661
	[SerializeField]
	private float showGodFinderDelay;

	// Token: 0x04000A66 RID: 2662
	//[SerializeField]
	//private BossScene unlockBossScene;

	// Token: 0x04000A67 RID: 2663

	// Token: 0x04000A68 RID: 2664
	[Header("Deprecated/Unusued Variables")]
	[SerializeField]
	private bool ignoreHazards;

	// Token: 0x04000A69 RID: 2665
	[SerializeField]
	private bool ignoreWater;

	// Token: 0x04000A6A RID: 2666
	[SerializeField]
	private float invulnerableTime;

	// Token: 0x04000A6B RID: 2667
	[SerializeField]
	private bool semiPersistent;

	// Token: 0x04000A6C RID: 2668
	public bool isDead;

	// Token: 0x04000A6D RID: 2669
	private GameObject sendKilledTo;

	// Token: 0x04000A6E RID: 2670
	private float evasionByHitRemaining;

	// Token: 0x04000A6F RID: 2671
	private int directionOfLastAttack;

	// Token: 0x04000A70 RID: 2672
	//private PlayMakerFSM stunControlFSM;

	// Token: 0x04000A71 RID: 2673
	private bool notifiedBattleScene;

	// Token: 0x04000A72 RID: 2674
	private const string CheckPersistenceKey = "CheckPersistence";
	
	[Serializable]
private struct HPScaleGG
{

	// Token: 0x04000A73 RID: 2675
	public int level1;

	// Token: 0x04000A74 RID: 2676
	public int level2;

	// Token: 0x04000A75 RID: 2677
	public int level3;
}
}
