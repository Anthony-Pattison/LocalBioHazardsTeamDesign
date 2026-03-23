using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerController : MonoBehaviour
{
    public List<AudioClip> WalkSounds;
    AudioManager audiomanager;
    EventCore eventCore;
    CustomAction input;
    NavMeshAgent agent;
    Animator KillerAnimator;
    SpriteRenderer KillerSpriteRenderer;
    Vector3 MousePosition;
    public LayerMask clickableLayers;
    public ParticleSystem clickEffect;
    public Transform CameraPos;
    public value seenValue;
    private void Awake()
    {
        audiomanager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        eventCore = GameObject.Find("EventCore").GetComponent<EventCore>();
        agent = GetComponent<NavMeshAgent>();
        input = new CustomAction();
        KillerAnimator = GetComponent<Animator>();
        KillerSpriteRenderer = GetComponent<SpriteRenderer>();
        assignInputs();

    }

    private void Update()
    {
        Vector3 CamPos = CameraPos.position;
        if (CameraPos != null) {
            CamPos.y = transform.position.y;
            transform.LookAt(CamPos);
            //print($" Player rotation {transform.eulerAngles}, wanted rotation {CamPos}");
        }


        GetPlayerInput();
        if (agent.velocity != Vector3.zero)
            KillerAnimator.speed = 1;
        else KillerAnimator.speed = 0;
        if (MousePosition.x > Screen.width / 2)
        {
            KillerSpriteRenderer.flipX = true;
        }
        else
        {
            KillerSpriteRenderer.flipX = false;
        }
    }
    void GetPlayerInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            eventCore.dropingTrapEV.Invoke();
        }
    }
    void assignInputs()
    {
        input.Main.Move.performed += ctx => ClickToMove();

    }

    void ClickToMove()
    {
        MousePosition = Input.mousePosition;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100, clickableLayers))
        {

            NavMeshHit navHit;

            if(NavMesh.SamplePosition(hit.point, out navHit, 1.0f, NavMesh.AllAreas))
            {
                agent.destination = navHit.position;
                TelemetryLogger.Log(this, $"Click position: {navHit.position}");

                if (clickEffect != null)
                {
                    ParticleSystem _partical = Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
                    StartCoroutine(DestroyWayPoint(_partical.gameObject, 1.5f));
                }
            }



        }
        else
        {
            agent.ResetPath();
            Debug.Log("blocked click");
        }
    }
    /// <summary>
    /// For destorying the partical effects after they are done waiting and <br/>
    /// shrink and can't be seen.
    /// </summary>
    /// <param name="Partical"></param>
    /// <param name="SecondsToWait"></param>
    /// <returns></returns>
    IEnumerator DestroyWayPoint(GameObject Partical, float SecondsToWait)
    {
        yield return new WaitForSeconds(SecondsToWait);
        Destroy(Partical);
        yield break;
    }
    public void PlaySound()
    {
        AudioClip ac = WalkSounds[Random.Range(0, WalkSounds.Count - 1)];
        if (audiomanager != null)
        {
            audiomanager.PlayOneShot(ac);
            return;
        }
        Debug.Log("There's no audio manager in the scene");
    }
    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }
}
