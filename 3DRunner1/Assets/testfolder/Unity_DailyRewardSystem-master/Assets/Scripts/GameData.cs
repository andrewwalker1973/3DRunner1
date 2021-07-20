using UnityEngine;

public static class GameData {
	private static int _metals = 0;
	private static int _TotalCoinScore = 0;
	private static int _Crystals = 0;

	//Static Constructor to load data from playerPrefs
	static GameData ( ) {
		_metals = PlayerPrefs.GetInt ( "Metals", 0 );
		_TotalCoinScore = PlayerPrefs.GetInt ("TotalCoinScore", 0 );
		_Crystals = PlayerPrefs.GetInt ("Crystals", 0 );
	}

	public static int Metals {
		get{ return _metals; }
		set{ PlayerPrefs.SetInt ( "Metals", (_metals = value) ); }
	}

	public static int Coins {
		get{ return _TotalCoinScore; }
		set{ PlayerPrefs.SetInt ("TotalCoinScore", (_TotalCoinScore = value) ); }
	}

	public static int Gems {
        get { return _Crystals; }
        
		set{ PlayerPrefs.SetInt ("Crystals", (_Crystals = value) ); }
	}

	/*---------------------------------------------------------
		this line:
		set{ PlayerPrefs.SetInt ( "Gems", (_gems = value) ); }

		is equivalent to:
		set{ 
			_gems = value;
			PlayerPrefs.SetInt ( "Gems", _gems ); 
		}
	------------------------------------------------------------*/
}
