using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Node 
{
    public int g,h,f;
    public int x,y;
    public GameObject visual;
    TMP_Text fCost,gCost,hCost;
    public bool isClosed = false;
    public bool isObstacle = false;
    public Node connectionNode;
    GameObject internalGo;
    public Node (int _x,int _y,bool _isObstacle){
        g = int.MaxValue;
        h = int.MaxValue;
        f = int.MaxValue;
        x = _x;
        y = _y;
        isObstacle = _isObstacle;
        /*visual = _visual;
        fCost = visual.transform.Find("fcost").GetComponent<TMP_Text>();
        gCost = visual.transform.Find("gcost").GetComponent<TMP_Text>();
        hCost = visual.transform.Find("hcost").GetComponent<TMP_Text>();
        internalGo = visual.transform.Find("internal").gameObject;
        UpdateVisual();*/
        /*
        if(isObstacle){
            UpdateMaterial(Color.red);
            fCost.gameObject.SetActive(false);
            gCost.gameObject.SetActive(false);
            hCost.gameObject.SetActive(false);
        }*/
    }
    public Vector2 GetPosition(){
        return new Vector2(x,y);
    }

    public void UpdateVisual(){
        gCost.text = "g = "+g;
        hCost.text = "h = "+h;
        fCost.text = ""+f;

    }
    public void UpdateCost(int _g,int _h){
        g = _g;
        h = _h;
        f=g+h;
        //UpdateVisual();
    }
    public void UpdateMaterial(Color _color){
       internalGo.GetComponent<MeshRenderer>().material.color = _color;

    }
}
