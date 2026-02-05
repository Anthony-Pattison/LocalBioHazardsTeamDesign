using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
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
        if (CameraPos != null)
            transform.LookAt(CameraPos.position);
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
        print($"{Screen.width / 2} {MousePosition}");
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, clickableLayers))
        {
            agent.destination = hit.point;
            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }
    }

    public void PlaySound()
    {
        AudioClip ac = WalkSounds[Random.Range(0, WalkSounds.Count - 1)];
        if (audiomanager != null) {
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
