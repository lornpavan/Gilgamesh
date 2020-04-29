using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedEnemy : MonoBehaviour
{
    /* public Unit Enemy;
     // Start is called before the first frame update
          void OnMouseDown()
         {
             Enemy = this.GetComponent<Unit>();
         }

     public Unit SelectedUnit()
     {
         return Enemy;
     }*/
    bool newon = true;
    public Unit selectedEnemy;
    public void Start()
    {
        //if state == combat

    }
    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && newon == true)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                float distance = Vector3.Distance(hit.transform.position, transform.position);               
                
                    string hitTag = hit.transform.tag;
                    if (hitTag == "FlameElemental" && newon == true)
                    {
                        Debug.Log(hit.transform.gameObject.name);
                        print("TARGET");
                        // target = hit.transform.gameObject;
                        //   SelectTarget();
                    }
            }
        }


        //if (Input.GetMouseButtonDown (1)) {
        // DeselectTarget();
        // }
    }
}
