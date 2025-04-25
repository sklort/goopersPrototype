using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    private Camera _cam;
    [SerializeField] GameObject cameraObject;

    private int _z;

    private AudioSource _source;
    [SerializeField] private AudioClip shoot;

    [SerializeField] private GameBoss gameBoss;
    private bool canPlay;
    [SerializeField] GameObject pistol;

    public float pistolCooldown = 1.0f;
    private float cooldownTime;
    private float currentTime;
    private float pistolDamage = 1.0f;

    private LineRenderer line;
    private float lineWidthStart = 0.1f;
    private float lineWidthEnd = 0.05f;
    private float lineMaxLength = 100f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _cam = cameraObject.GetComponent<Camera>();
        _source = GetComponent<AudioSource>();
        line = GetComponent<LineRenderer>();

        Vector3[] initLinePositions = new Vector3[2] { Vector3.zero, Vector3.zero };
        line.SetPositions(initLinePositions);
        line.startWidth = lineWidthStart;
        line.endWidth = lineWidthEnd;

    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;

        canPlay = gameBoss.canPlay;

        if (!canPlay)
        {
            pistol.SetActive(false);
        }

        if (canPlay)
        {
            pistol.SetActive(true);

            if (Input.GetMouseButtonDown(0))
            {
                pistolCooldown = 1.0f / gameBoss.globalFireSpeed;
                if (currentTime >= cooldownTime)
                {
                    _source.clip = shoot;
                    _source.pitch = Random.Range(0.75f, 1.3f);
                    _source.volume = Random.Range(0.6f, 0.9f);
                    _source.PlayOneShot(shoot, 0.3f);
                    cooldownTime = currentTime + pistolCooldown;

                    StartCoroutine(LineOn());

                    // Create a point at the middle of the viewport
                    Vector3 point = new(_cam.pixelWidth * 0.5f, _cam.pixelHeight * 0.5f, 0);

                    // Create a ray to the created point
                    Ray ray = _cam.ScreenPointToRay(point);

                    // Data structure to record information about the ray collision
                    RaycastHit hit;

                    Vector3 endPosition = point + (lineMaxLength * Vector3.forward);

                    // Check if the created ray collided with any geometry
                    if (Physics.Raycast(ray, out hit))
                    {
                        // Retrieve GameObject ray collided with.
                        GameObject hitObj = hit.transform.gameObject;
                        Target target = hitObj.GetComponent<Target>();

                        if (target != null)
                            target.EnemyTakeDamage(pistolDamage);
                    }

                    if (Physics.Raycast(ray, out hit, lineMaxLength))
                    {
                        endPosition = hit.point;
                    }

                    line.SetPosition(0, pistol.transform.position);
                    line.SetPosition(1, endPosition);

                }
            }
        }
    }

    IEnumerator LineOn()
    {
        line.enabled = true;

        yield return new WaitForSeconds(0.3f);

        line.enabled = false;
    }

}
