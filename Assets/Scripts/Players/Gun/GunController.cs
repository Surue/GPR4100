using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    [SerializeField] SO_Gun gun;

    float gunDelay;
    
    SpriteRenderer spriteRenderer;

    [SerializeField]PlayerController player;

    bool isFireing = false;

    bool lookingRight = true;
    
    [SerializeField] Transform bulletSpawnPoint;
    float currentDelay = 0.0f;
    
    [Header("Sounds")]
    [SerializeField] List<SO_Clip> gunShotClips_;

    AudioManager audioManager_;
    
	// Use this for initialization
	void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();

        SetNewGun(gun);
	}

    public void SetAudioManager(AudioManager audioManager) {
        audioManager_ = audioManager;
    }

    // Update is called once per frame
    void Update () {
        
        //Gets mouse position, you can define Z to be in the position you want the weapon to be in
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        Vector3 lookPos = Camera.main.ScreenToWorldPoint(mousePos);
        Vector3 lookDirection = lookPos - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        if(!lookingRight) {
            angle -= 180;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        } else {
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        
        

        if(currentDelay > 0) {
            currentDelay -= Time.deltaTime;
        }


        if(Input.GetButtonDown("Fire1")) {
            isFireing = true;
        }

        if(Input.GetButton("Fire1")) {
            if(currentDelay <= 0) {
                Fire(transform, bulletSpawnPoint);
                currentDelay = gun.Delay;
            }
        }

        if(Input.GetButtonUp("Fire1")) {
            isFireing = false;
        }
    }

    public void SetNewGun(SO_Gun newGun) {
        gunDelay = newGun.Delay;
        spriteRenderer.sprite = newGun.Sprite;

        gun = newGun;
    }

    public void FlipSprite(bool flip) {
        if(!flip) {
            lookingRight = true;
        } else {
            lookingRight = false;
        }
    }
    
    public void Fire(Transform transform, Transform bulletSpawnPoint) {
        for (int i = 0; i < gun.Numb; i++) {
            GameObject bullet = Instantiate(gun.PrefabBullet, bulletSpawnPoint);
            bullet.transform.parent = null;
            bullet.transform.localScale = Vector3.one;
            
            bullet.transform.position += (Vector3) Random.insideUnitCircle * gun.Dispertion;
            
            bullet.GetComponent<Rigidbody2D>().velocity = transform.right * gun.BulletSpeed;
            
            audioManager_.PlayWithRandomPitch(gunShotClips_[Random.Range(0, gunShotClips_.Count)]);
        }
    }
}
