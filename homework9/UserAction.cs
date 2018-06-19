using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UserAction{
	void priestGetOn ();
	void priestGetOff ();
	void devilGetOn ();
	void devilGetOff ();
	void moveBoat ();
	void restart ();
	int result ();
	int getResult ();

    int actNext();
    void clearBoat();
}