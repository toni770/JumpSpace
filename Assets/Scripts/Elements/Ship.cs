using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour ,IInteractable
{
    [SerializeField] private Animator shipAnim;
    [SerializeField] private float despegueDelay = 1;
    
    [SerializeField] private float transitionSpeed = 1;

    [SerializeField] private Transform parent;

    [SerializeField] private ParticleSystem _effect;

    [SerializeField] private AudioConfig _audio;

    private bool playerIn = false;
    private Transform playerTrans;
    private void Start() 
    {
        GameManager.Instance.SetShip(gameObject);
    //    gameObject.SetActive(false);
    }

    private void Update() {
        if(playerIn)
        {
           /* playerTrans.position = Vector3.Lerp(playerTrans.position, transform.position, Time.deltaTime * transitionSpeed);

            playerTrans.rotation = Quaternion.Lerp(playerTrans.rotation, transform.rotation, Time.deltaTime * transitionSpeed);*/
        }
    }
    public void Interact(GameObject player)
    {
        GameManager.Instance.CheckEnd();
        GetComponent<MeshRenderer>().enabled = false;
        playerIn = true;
        playerTrans = player.transform;

        playerTrans.position = transform.position;
        playerTrans.rotation = Quaternion.Euler(new Vector3(0,0,1));

        JuiceManager.Instance.PlayerJumpToPosition(parent.position);
        player.GetComponent<PlayerStats>().GoShip();
        StartCoroutine(Despegue());
    }

    private IEnumerator Despegue()
    {
        yield return new WaitForSeconds(despegueDelay);
        _effect.Play();
        shipAnim.SetTrigger("Go");
        yield return new WaitForSeconds(0.1f);
        SoundManager.Instance.MakeSound(_audio);
    }


}
