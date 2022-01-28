using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour ,IInteractable
{
    [SerializeField] private Animator shipAnim;
    [SerializeField] private float despegueDelay = 1;
    
    [SerializeField] private float transitionSpeed = 1;
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
            playerTrans.position = Vector3.Lerp(playerTrans.position, transform.position, Time.deltaTime * transitionSpeed);

            playerTrans.rotation = Quaternion.Lerp(playerTrans.rotation, transform.rotation, Time.deltaTime * transitionSpeed);
        }
    }
    public void Interact(GameObject player)
    {
        GameManager.Instance.CheckEnd();
        GetComponent<MeshRenderer>().enabled = false;
        playerIn = true;
        playerTrans = player.transform;

        player.GetComponent<PlayerStats>().GoShip();
        StartCoroutine(Despegue());
    }

    private IEnumerator Despegue()
    {
        yield return new WaitForSeconds(despegueDelay);
        shipAnim.SetTrigger("Go");
    }
}
