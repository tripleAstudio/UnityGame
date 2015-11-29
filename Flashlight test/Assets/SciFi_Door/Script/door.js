#pragma strict

var open = true;  
//var close = false;  


function OnTriggerEnter (obj : Collider) 
{
		if (open)  
       {  
    		open = false;  
       		Open ();  
    	}  
}

/*function OnTriggerExit (obj : Collider) 
{
		if (close)  
    	{  
	        close = false;  
	        Close ();  
    	}  
}*/


function Open ()  
{  
		Debug.Log("input open anim");
      // GetComponent.<AudioSource>().clip = soundOpen; 
       // GetComponent.<AudioSource>().Play();  
        var thedoor = gameObject.FindWithTag("SF_Door");
		thedoor.GetComponent.<Animation>().Play("open");
        //yield WaitForSeconds (GetComponent.<Animation>().clip.length);  
      //  close = true;  
}  

/*function Close()
{
		Debug.Log("input anim close");
		//GetComponent.<AudioSource>().clip = soundClose;  
        //GetComponent.<AudioSource>().Play();  
        var thedoor = gameObject.FindWithTag("SF_Door");
		thedoor.GetComponent.<Animation>().Play("close");
       yield WaitForSeconds (GetComponent.<Animation>().clip.length);  
        open = true; 
}*/