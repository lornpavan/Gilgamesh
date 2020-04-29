using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private ArrayList inv;
	
	void Start(){
		inv = new ArrayList();
	}
	
	public ArrayList getInv(){
		return this.inv;
	}
	
	public void addItem(Item i){
		this.inv.Add(i);
	}
	//Needs to be tested
	public Item getItem(Item i){
		Item answer = null;
		for (int j = 0; j < inv.Count; j++){
			if (inv[j] == i){
				inv.Remove(i);
				answer = i;
				break;
			}
		}
		return answer;
	}
	
}
