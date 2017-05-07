using UnityEngine;
using System.Collections;

public class GeolocalizacionRA : MonoBehaviour {
	public GUISkin skinDescripcion;
	private float latitud;
	private float longitud;
	public Texture2D descripcion;
	private GameObject _modelo3D;
	private float rotacion;
	private string[,] localizaciones;
	private string[] sitio;
	private string descSitio;
	private int idiomaElegido;
	private int existeSitioCerca;
	private Rect windowRect;

	public Vector2 scrollPosition = Vector2.zero;
	public Touch touch;


	void Start () {
		idiomaElegido = Inicio1.idiomaElegido;
		//ventana emergente
		float popupX = Screen.width / 15;
		float altoPopup = Screen.height / 3.5f;
		float anchoPopup = Screen.width -Screen.width/7.5f;
		windowRect = new Rect(popupX,Screen.height/2-altoPopup/2,anchoPopup,altoPopup);
		// verificacion de sitio cercano
		latitud = -1f*Gps.latitud;
		longitud = -1f*Gps.longitud;

		BaseDatos db = new BaseDatos();
		db.AbrirBD("turismo.db");
		localizaciones = db.obtieneLocalizaciones();

		float rangoError = 0.000920f;
		float maximaLatitud;
		float minimaLatitud;
		float maximaLongitud;
		float minimaLongitud;
		 existeSitioCerca = -1;
		//verificacion de todas las rutas
		Debug.Log ("latitud: "+latitud);
		Debug.Log ("longitud: "+longitud);
		for(int i=0;i < 11;i++ ){
			//Debug.Log("       "+float.Parse(localizaciones[i,0]));
			maximaLatitud =-1f* float.Parse(localizaciones[i,1]) + rangoError;
			minimaLatitud = -1f*float.Parse(localizaciones[i,1]) - rangoError;
			maximaLongitud = -1f*float.Parse(localizaciones[i,2]) + rangoError;
			minimaLongitud =-1f* float.Parse(localizaciones[i,2]) - rangoError;
			Debug.Log (float.Parse(localizaciones[i,1])+" "+maximaLatitud+" "+minimaLatitud);
			Debug.Log (float.Parse(localizaciones[i,2])+" "+maximaLongitud+" "+minimaLongitud);
			if(latitud >= minimaLatitud && latitud <= maximaLatitud){
				if(longitud >= minimaLongitud && longitud <= maximaLongitud){
					existeSitioCerca = i+1;
					Debug.Log("Sitio cercano: "+existeSitioCerca);
						break;
				}
				else{
					Debug.Log("No existe longitud");
				}
			}
			else{
				Debug.Log ("No existe latitud");
			}
		}
		//cargado de datos si se encontro un sitio cercano
		if(existeSitioCerca >= 0){
			sitio = db.obtieneDatosSitio (existeSitioCerca);
			descSitio = sitio[7+idiomaElegido].Replace ("Ã¡", "á").Replace ("Ã©","é").Replace ("Ã­","í").Replace ("Ã³","ó").Replace ("Ãº","ú").Replace ("Ã±","ñ");
		}
		db.CerrarBD();
		//modelos 3D

		Debug.Log (sitio[3]);
		_modelo3D = transform.FindChild(sitio[3]).gameObject;
		rotacion = _modelo3D.transform.rotation.y;
		float escala = 0.4f;
		_modelo3D.transform.localScale = new Vector3 (escala, escala, escala);


		


		//Debug.Log ("time:"+Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("inicio");

		rotacion = 40.5f *Time.deltaTime;
		_modelo3D.transform.Rotate(0,rotacion,0,Space.World);


		if(Input.touchCount > 0 )
		{
			touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
			{
				scrollPosition.y += touch.deltaPosition.y;
			}
		}

	}
	void OnGUI(){
		if (skinDescripcion)
			GUI.skin = skinDescripcion;
		int tamanioLetra = Screen.width/20;
		if (existeSitioCerca >= 0) {
			skinDescripcion.label.fontSize = tamanioLetra;
			skinDescripcion.label.fontSize = Screen.width / 20;
			GUI.Label (new Rect (0, 0, 300, 100), "latitud: " + latitud);
			GUI.Label (new Rect (0, Screen.height / 10f, 300, 100), "longitud: " + longitud);
			
			GUI.DrawTexture (new Rect (Screen.width / 15, Screen.height / 1.5f, Screen.width - Screen.width / 7.5f, Screen.height / 3.5f), descripcion, ScaleMode.StretchToFill);
			GUI.skin = skinDescripcion;
			GUI.skin.label.fontSize = Screen.width / 17;
						
		
		
			GUI.skin.label.fontSize =Screen.width/16;
			scrollPosition  = GUI.BeginScrollView (new Rect (0, Screen.height/1.5f,Screen.width-Screen.width/7.5f, Screen.height / 3.5f), scrollPosition, new Rect (20, Screen.height/1.5f, Screen.width-Screen.width/7.5f, Screen.height), GUIStyle.none, GUIStyle.none);
			GUI.Label (new Rect (Screen.width / 7, Screen.height / 1.5f, Screen.width - Screen.width / 3.75f, Screen.height),descSitio);
			GUI.EndScrollView ();
		
		} else {
			windowRect = GUI.Window (0, windowRect, ventanaEmergente, "");
			GUI.BringWindowToFront (0);		
		}



	}

	void ventanaEmergente(int windowID){
		float anchoBoton = Screen.width /4;
		float altoBoton = Screen.height/14;
		float botonX = Screen.width/2-anchoBoton/2-Screen.width/15;
		float botonY = Screen.height / 5;
		float popupX = Screen.width / 15;
		float altoPopup = Screen.height / 3.5f;
		float anchoPopup = Screen.width -Screen.width/7.5f;
		
		GUI.skin.box.fontSize = Screen.height/16;
		GUI.Box (new Rect(0,0,anchoPopup,altoPopup),"No se encontro ningun \nsitio cercano. ");
		skinDescripcion.button.fontSize = Screen.height / 18;
		if (GUI.Button (new Rect (botonX,botonY, anchoBoton, altoBoton), "Aceptar")) {
			Application.LoadLevel("inicio");
		}
		GUI.DragWindow ();
		//GUI.DragWindow (new Rect(0,0,Screen.width-Screen.width*2/15,Screen.height-Screen.width*2/15));
	}
}
