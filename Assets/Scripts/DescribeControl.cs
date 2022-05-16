using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DescribeControl : MonoBehaviour
{
    public ArtWork[] artList;

    public Button button;

    public GameObject targetText;

    public float triggerDist;

    private bool isShowing = false;

    private int displayId = -1;

    void Start(){
        button.onClick.AddListener(toggleDescribe);
    }
    void Update()
    {
        for(int i=0;i<artList.Length;i++){ 
            Vector2 cameraXZ = new Vector2(this.transform.position.x,this.transform.position.z);
            Vector2 targetXZ = new Vector2(artList[i].artObject.position.x/artList[i].artObject.localScale.x,artList[i].artObject.position.z/artList[i].artObject.localScale.z);
            //Debug.Log(i.ToString()+":"+Vector2.Distance(cameraXZ,targetXZ));
            if(Vector2.Distance(cameraXZ,targetXZ) <= triggerDist) {
                if(!isShowing){
                    targetText.GetComponentInChildren<Text>().text = artList[i].info;
                    button.gameObject.SetActive(true);
                    isShowing = true;
                    displayId = i;
                }
            } else {
                if(displayId == i){
                    button.gameObject.SetActive(false);
                    targetText.gameObject.SetActive(false);
                    displayId = -1;
                    isShowing = false;
                }
            }
        }
    }

    void toggleDescribe(){
        Debug.Log(targetText.gameObject.active);
        if(targetText.gameObject.active){
            targetText.gameObject.SetActive(false);
        } else {
            targetText.gameObject.SetActive(true);
        }
    }
}
