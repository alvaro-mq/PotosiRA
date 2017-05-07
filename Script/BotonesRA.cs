using UnityEngine;
using System.Collections;

public class BotonesRA : MonoBehaviour {
	public GUISkin Interfaz;
	public Texture2D botonCamara;
	public Texture2D botonDescripcion;
	public Texture2D botonHome;
	public Texture2D botonCerrar;
	public Texture2D imgDescripcion;
	public AudioClip captura;
	string[] datosSitio;
	string descripcion;
	public int idSitioElegido;
	private int idiomaElegido;
	bool mostrarDescripcion = false;
	private Vector2 scrollPosition = Vector2.zero;
	Touch touch;


	// Use this for initialization
	void Start () {
		idiomaElegido = Inicio1.idiomaElegido;

		BaseDatos db = new BaseDatos();
		db.AbrirBD("turismo.db");
		datosSitio = db.obtieneDatosSitio (idSitioElegido);
		descripcion = datosSitio[7+idiomaElegido];
		db.CerrarBD ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("MenuSitioX");
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
		if(Interfaz)
			GUI.skin = Interfaz;
		float tamanioBoton = Screen.width/5;
		if (GUI.Button (new Rect (0, 0, tamanioBoton, tamanioBoton),botonHome )) {
			Application.LoadLevel("inicio");
		}
		if (GUI.Button (new Rect (0,tamanioBoton, tamanioBoton, tamanioBoton),botonCamara )) {
			string nombre="Potosi"+System.DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss")+".jpg";
			Application.CaptureScreenshot(nombre);

			audio.clip = captura;
			audio.Play();	
		}
		if (GUI.Button (new Rect (0, tamanioBoton*2, tamanioBoton, tamanioBoton),botonDescripcion )) {
			mostrarDescripcion = true;
		}
		if (mostrarDescripcion) {
			if (GUI.Button (new Rect (Screen.width-tamanioBoton,Screen.height/2+Screen.width/6.5f, tamanioBoton/2, tamanioBoton/2),botonCerrar )) {
				mostrarDescripcion = false;
			}
			GUI.DrawTexture (new Rect (Screen.width / 15, Screen.height / 2 + Screen.width / 8f, Screen.width - Screen.width / 7.5f, Screen.height / 2.6f), imgDescripcion);
						//GUI.skin = skinDescripcion;
			GUI.skin.label.fontSize = Screen.width / 16;
			scrollPosition = GUI.BeginScrollView (new Rect (0, Screen.height / 2 + Screen.width / 7.5f, Screen.width - Screen.width / 7.5f, Screen.height / 2.6f), scrollPosition, new Rect (20, Screen.height / 2 + Screen.width / 7.5f, Screen.width - Screen.width / 7.5f, Screen.height), GUIStyle.none, GUIStyle.none);
			GUI.Label (new Rect (Screen.width / 7, Screen.height / 2 + Screen.height / 12f, Screen.width - Screen.width / 4, Screen.height), descripcion);
			GUI.EndScrollView ();
		}

	}
}
