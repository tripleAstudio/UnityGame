using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Gun : MonoBehaviour
{
	public enum weaponType {Shotgun};
	public weaponType typeOfGun;
	
    public enum BulletType { Physical, Raycast }; // physical bullets of raycasts
    public BulletType typeOfBullet;
    

    // basic weapon variables all guns have in common
    // Objects, effects and tracers
    public GameObject bullet = null;        // the weapons bullet object
	public AudioClip Fire = null;
	private Animator ShotgunAnim;
    public Renderer muzzleFlash = null;     // the muzzle flash for this weapon
    public Light lightFlash = null;         // the light flash for this weapon
    public Transform muzzlePoint = null;    // the muzzle point of this weapon
    public Transform ejectPoint = null;     // the ejection point
    public Transform mountPoint = null;     // the mount point.... more for weapon swapping then anything
    public Rigidbody shell = null;          // the weapons empty shell object
    public GameObject gunOwner = null;      // the gun owner
    public GameObject mainCamera = null;    // the player's main camera
	public GameObject WorldmainCamera = null;
    public GameObject WorldweaponCamera = null;  // this weapon's camera
	public GameObject weaponCamera = null;
    public GameObject impactEffect = null;  // impact effect, used for raycast bullet types
    public GameObject bulletHole = null;    // bullet hole for raycast bullet types
	public GameObject enemyBulletHole = null;
   
    //Shotgun Specific Vars
    public int pelletsPerShot = 10;         // number of pellets per round fired for the shotgun
	
    //Launcher Specific Vars    

    // basic stats
    public int range = 300;                 // range for raycast bullets... bulletType = Ray
    public float damage = 20.0f;            // bullet damage
    public float maxPenetration = 3.0f;     // how many impacts the bullet can survive
    public float fireRate = 0.5f;           // how fast the gun shoots... time between shots can be fired
    public int impactForce = 50;            // how much force applied to a rigid body
    public float bulletSpeed = 200.0f;      // how fast are your bullets

    public int bulletsPerClip = 50;         // number of bullets in each clip
    public int numberOfClips = 5;           // number of clips you start with
    public int maxNumberOfClips = 10;       // maximum number of clips you can hold
    private int bulletsLeft;                // bullets in the gun-- current clip
        
    public float baseSpread = 1.0f;         // how accurate the weapon starts out... smaller the number the more accurate
    public float maxSpread = 4.0f;          // maximum inaccuracy for the weapon
    public float spreadPerSecond = 0.2f;    // if trigger held down, increase the spread of bullets
    public float spread = 0.0f;             // current spread of the gun
    public float decreaseSpreadPerSec = 0.5f;// amount of accuracy regained per frame when the gun isn't being fired 
    
    public float reloadTime = 1.0f;         // time it takes to reload the weapon
    private bool isReloading = false;       // am I in the process of reloading
    // used for tracer rendering
    public int shotsFired = 0;              // shots fired since last tracer round
    public int roundsPerTracer = 1;         // number of rounds per tracer

    private int m_LastFrameShot = -1;       // last frame a shot was fired
    private float nextFireTime = 0.0f;      // able to fire again on this frame

    private float[] bulletInfo = new float[6];// all of the info sent to a fired bullet

    void Start()
    {
        bulletsLeft = bulletsPerClip;
		ShotgunAnim = GetComponent<Animator>();
    }

    bool Update()
    {
        switch (typeOfGun)
        {
            case weaponType.Shotgun:
                if (Input.GetButtonDown("Fire1"))
                {
                    ShotGun_Fire();
                }
                break;
        }

        if (Input.GetButton("Fire2"))
        {
            if (weaponCamera)
            {
                weaponCamera.GetComponent<Camera>().enabled = true;
				WorldweaponCamera.GetComponent<Camera>().enabled = true;
                mainCamera.GetComponent<Camera>().enabled = false;
				WorldmainCamera.GetComponent<Camera>().enabled = false;
            }
        }
        else
        {
            weaponCamera.GetComponent<Camera>().enabled = false;
			WorldweaponCamera.GetComponent<Camera>().enabled = false;
			WorldmainCamera.GetComponent<Camera>().enabled = true;
            mainCamera.GetComponent<Camera>().enabled = true;
        }
        return true;
    }
    void LateUpdate()
    {
        if (muzzleFlash || lightFlash)  // need to have a muzzle or light flash in order to enable or disable them
        {
            // We shot this frame, enable the muzzle flash
            if (m_LastFrameShot == Time.frameCount)
            {
                muzzleFlash.transform.localRotation = Quaternion.AngleAxis(Random.value * 57.3f, Vector3.forward);
                muzzleFlash.enabled = true;// enable the muzzle and light flashes
                lightFlash.enabled = true;
            }
            else
            {
                muzzleFlash.enabled = false; // disable the light and muzzle flashes
                lightFlash.enabled = false;
            }
        }

        if (spread >= maxSpread)
        {
            spread = maxSpread;  //if current spread is greater then max... set to max
        }
        else
        {
            if (spread <= baseSpread)
            {
                spread = baseSpread; //if current spread is less then base, set to base
            }
        }
    }
    void ShotGun_Fire()
    {
        int pelletCounter = 0;

        if (bulletsLeft == 0)
        {
            StartCoroutine("reload");
            return;
        }

        if (Time.time - fireRate > nextFireTime)
            nextFireTime = Time.time - Time.deltaTime;

        while (nextFireTime < Time.time)
        {
            do
            {
                switch (typeOfBullet)
                {
                    case BulletType.Physical:
                        StartCoroutine("FireOneShot");
                        break;
                    default:
                        Debug.Log("error in bullet type");
                        break;
                }
                pelletCounter++; 
                shotsFired++;               
            } while (pelletCounter < pelletsPerShot); 
            EjectShell(); 
            nextFireTime += fireRate;  
            bulletsLeft--; 
        }
    }
    IEnumerator FireOneShot()
    {
        Vector3 position = muzzlePoint.position;   

        bulletInfo[0] = damage;
        bulletInfo[1] = impactForce;
        bulletInfo[2] = maxPenetration;
        bulletInfo[3] = maxSpread;
        bulletInfo[4] = spread;
        bulletInfo[5] = bulletSpeed;

        GameObject newBullet = Instantiate(bullet, position, transform.parent.rotation) as GameObject; 
        newBullet.SendMessageUpwards("SetUp", bulletInfo); 
        newBullet.GetComponent<Bullet>().Owner = gunOwner; 

        if ((bulletsLeft == 0))
        {
            StartCoroutine("reload"); 
            yield break;
        }
        
        m_LastFrameShot = Time.frameCount;
    }
	
    void EjectShell()
    {
        Vector3 position = ejectPoint.position; // ejectile spawn point at gun's ejection point
        
        if (shell)
        {
            Rigidbody newShell = Instantiate(shell, position, transform.parent.rotation) as Rigidbody; // create empty shell
            //give ejectile a slightly random ejection velocity and direction
            newShell.velocity = transform.TransformDirection(Random.Range(-2, 2) - 3.0f, Random.Range(-1, 2) + 3.0f, -Random.Range(-2, 2) + 1.0f);
        }
    }

    //effects for raycast bullets
    void ShowHits(RaycastHit hit)
    {
        switch (hit.transform.tag)
        {
            case "bullet":
                // do nothing if 2 bullets collide
                break;
            case "Zombie":
                // add blood effect
                break;
            case "wood":
                // add wood impact effects
                break;
            case "stone":
                // add stone impact effect
                break;
            case "ground":
                // add dirt or ground  impact effect
                break;
            default: // default impact effect and bullet hole
//                Instantiate(impactEffect, hit.point + 0.1f * hit.normal, Quaternion.FromToRotation(Vector3.up, hit.normal));
//               	GameObject newBulletHole = Instantiate(bulletHole, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal)) as GameObject;
//                newBulletHole.transform.parent = hit.transform;
                break;
        }
    }
    // reload your weapon
    IEnumerator reload()
    {
        if (isReloading)
        {
            yield break; // if already reloading... exit and wait till reload is finished
        }

        if (numberOfClips > 0)
        {
            isReloading = true; // we are now reloading
            numberOfClips--; // take away a clip
            yield return new WaitForSeconds(reloadTime); // wait for set reload time
            bulletsLeft = bulletsPerClip; // fill up the gun
        }

        isReloading = false; // done reloading
    }
}