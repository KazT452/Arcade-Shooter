using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public Texture2D customCursorTexture; // Assign your custom crosshair texture in the Inspector
    public Vector2 cursorHotspot; // Customize the hotspot if needed

    public int bullets = 12;
    public enum ShootState { Ready, Reload };
    public ShootState state;
    GunSounds gunSounds;
    public GameObject magazine;

    private void Awake()
    {
        gunSounds = GetComponent<GunSounds>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // Calculate the hotspot position based on the crosshair's dimensions.
        cursorHotspot = new Vector2(customCursorTexture.width / 2, customCursorTexture.height / 2);

        // Set the custom cursor texture and hotspot.
        Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if(state == ShootState.Ready)
        {
            //Shoot
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    if(hit.transform.gameObject.tag == "Enemy")
                    {
                        Enemy enemy = hit.transform.GetComponent<Enemy>();
                        enemy.TakeDamage();
                    }
                }
                gunSounds.Shoot();
                bullets--;
                magazine.transform.GetChild(bullets).gameObject.SetActive(false);
                
            }
            //Reload
            if (Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.R)||bullets==0)
            {
                StartCoroutine(Reload());
            }
        }

        // Get the current mouse position in screen coordinates.
        Vector3 mousePos = Input.mousePosition;
        // Convert the mouse position to world coordinates.
        mousePos.z = 10; // Adjust the z-coordinate as needed to match your desired depth.
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // Update the position of the custom cursor.
        transform.position = worldMousePos;
    }

    IEnumerator Reload()
    {
        state = ShootState.Reload;
        gunSounds.audioSource.PlayOneShot(gunSounds.reload);
        if (bullets != 0)
        {
            yield return new WaitForSeconds(2.5f);
        }
        else
        {
            yield return new WaitForSeconds(3f);
        }
        bullets = 12;
        for (int i = 0; i < magazine.transform.childCount; i++)
        {
            magazine.transform.GetChild(i).gameObject.SetActive(true);
        }
        state = ShootState.Ready;
    }

}
    

