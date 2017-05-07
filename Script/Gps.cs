using UnityEngine;

//using GeoUtility;
//using GeoUtility.GeoSystem;
using System.Timers;

public class Gps : MonoBehaviour {
	
	bool gpsInit = false;
	LocationInfo currentGPSPosition;
	string gpsMensaje;
	public GUISkin Interfaz;
	public Texture2D busqueda;
	public Texture2D fondo;
	public float progreso = 0.0f;
	public string mensaje;

	private Rect windowRect;
	Rect rect;
	Vector2 pivot;

	public float angle = 0.0f;
	public Vector2 size = new Vector2 (128,128);
	Vector2 pos = new Vector2(0,0);

	public static float latitud;
	public static float longitud;

	void Start () {
		mensaje = "Detectando ubicación";
		actualizarConf ();
		//Starting the Location service before querying location
		Input.location.Start(0.5f); // precision de 0.5 m
		
		int espera = 1000; 
		
		// Verificando si el GPS esta habilitado
		if(Input.location.isEnabledByUser)
		{
			while(Input.location.status == LocationServiceStatus.Initializing  && espera>0)
			{
				espera--;
			}
			//verificando si no existio ningun error o si  el gps esta habillitado
			if (Input.location.status == LocationServiceStatus.Failed) {
				gpsMensaje = "El gps fallo... no esta habilitado";
			}
			else {
				gpsInit = true;
				// invocado cada 3 segundos
				InvokeRepeating("RetrieveGPSData", 0, 3);
			}
		}
		else
		{
			//GameObject.Find("gps_debug_text").guiText.text = "GPS not available";
			gpsMensaje = "El GPS no esta habilitado";
			gpsInit = false;
		}
	}
	void actualizarConf(){
		float tamanioBusqueda = Screen.height / 4;
		float popupX = Screen.width / 15;
		float altoPopup = Screen.height / 3.5f;
		float anchoPopup = Screen.width -Screen.width/7.5f;

		windowRect = new Rect(popupX,Screen.height/2-altoPopup/2,anchoPopup,altoPopup);
		pos = new Vector2 (transform.localPosition.x,transform.localPosition.y);
		rect = new Rect (Screen.width/2-tamanioBusqueda/2, Screen.height/2-tamanioBusqueda/2, tamanioBusqueda, tamanioBusqueda);
		pivot = new Vector2 (rect.xMin+rect.width*0.5f,rect.yMin+rect.height*0.5f);

	}
	void OnGUI(){
		if (Interfaz)
			GUI.skin = Interfaz;

		GUI.DrawTexture (new Rect(0,0,Screen.width,Screen.height),fondo,ScaleMode.StretchToFill);
		if (progreso >= 1.0f) {
						if (gpsInit) {
								Application.LoadLevel ("GeolocalizacionRA");
						} else {
								windowRect = GUI.Window (0, windowRect, ventanaEmergente, "");
								GUI.BringWindowToFront (0);
						}
		} else {
			Interfaz.label.fontSize = 25;
	
			float tamanioBusqueda = Screen.height / 4;
			// para que la imagen de busqueda gire
			Matrix4x4 matrixBackup = GUI.matrix;
			angle = angle + 3.0f;
			Debug.Log (angle);
			GUIUtility.RotateAroundPivot (angle,pivot);
			GUI.DrawTexture (rect,busqueda);
			GUI.matrix = matrixBackup;
			
			GUI.skin.label.alignment = TextAnchor.UpperCenter;
			GUI.skin.label.fontSize = Screen.height/20;
			if (angle % 100 == 0) {
				mensaje = mensaje +".";		
			}
			
			GUI.Label (new Rect(0,Screen.height/2+tamanioBusqueda/2,Screen.width,tamanioBusqueda),mensaje);		
		}
	
	}

	void Update () {
		progreso = progreso + Time.deltaTime * 0.2f;
		Debug.Log (progreso);
		if (Input.GetKeyDown(KeyCode.Escape)) 

			Application.LoadLevel("inicio");
	
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
		GUI.Box (new Rect(0,0,anchoPopup,altoPopup),"Por favor habilite el GPS \n de su dispositivo. ");
		Interfaz.button.fontSize = Screen.height / 18;
		if (GUI.Button (new Rect (botonX,botonY, anchoBoton, altoBoton), "Aceptar")) {
			Application.LoadLevel("inicio");
		}
		GUI.DragWindow ();

	}
	void RetrieveGPSData()
	{
		currentGPSPosition = Input.location.lastData;
		gpsMensaje = "::" + currentGPSPosition.latitude + "//" + currentGPSPosition.longitude;
		latitud = currentGPSPosition.latitude;
		longitud = currentGPSPosition.longitude;
		//latitud = -19.58891f;
		//longitud = -65.75843f;
		//latitud = -19.5886f;
		//longitud = -65.75339f;
		//latitud = -58.87978f;
		//longitud = -58.79898f;
	}
	}
