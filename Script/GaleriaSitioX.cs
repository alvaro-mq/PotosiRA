using UnityEngine;
using System.Collections;

public class GaleriaSitioX : MonoBehaviour {
	public GUISkin Interfaz;
	public GUISkin skinDescripcion;
	public Texture2D fondo;
	public Texture2D[] imagenes = new Texture2D[55];
	public Texture2D imagen;
	public Texture2D descripcion;
	public Texture2D cabecera;
	public int idiomaElegido;
	public int sitioElegido;
	public string descSitio;
	public string tituloSitio;

	public int indice;
	string nueva;
	//inside class
	Vector2 primerToque = Vector2.zero;
	Vector2 segundoToque = Vector2.zero;
	Vector2 actualRecorrido = Vector2.zero;
	Touch t;

	public Vector2 scrollPosition = Vector2.zero;
	 Touch touch;
	// Use this for initialization
	void Start () {
		sitioElegido = ListadoSitios.sitioElegido;
		descSitio = MenuSitioX.descSitio;
		tituloSitio = MenuSitioX.tituloSitio;
		indice = (sitioElegido - 1) * 5;
		imagen = imagenes[indice];
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) 
			Application.LoadLevel("MenuSitioX");
		// si el touch se movio hacia arriba, para simular el scroll
		if(Input.touchCount > 0 )
		{
			touch = Input.touches[0];
			if (touch.phase == TouchPhase.Moved)
			{
				scrollPosition.y += touch.deltaPosition.y;
			}
		}


		// si el touch se movio a izquierda o deracha para simular cambiar de imagen
		if(Input.touches.Length > 0)
		{
			t = Input.GetTouch(0);
			if(t.phase == TouchPhase.Began)
			{
				//guardando el primer toque
				primerToque = new Vector2(t.position.x,t.position.y);
			}
			if(t.phase == TouchPhase.Ended)
			{
				//guardando el segundo toque
				segundoToque = new Vector2(t.position.x,t.position.y);
				
				//creando vector segun los toques
				actualRecorrido = new Vector3(segundoToque.x - primerToque.x, segundoToque.y - primerToque.y);
				
				//normalize the 2d vector
				actualRecorrido.Normalize();
	
				if((actualRecorrido.x < 0  && actualRecorrido.y > -0.5f  && actualRecorrido.y < 0.5f) || Input.GetKeyDown(KeyCode.A))
				{
					Debug.Log("movimiento hacia la izquierda");
					indice--;
					if(indice < (sitioElegido-1)*5){
						indice = sitioElegido*5-1;
					}
					imagen = imagenes[indice];
				}

				if(actualRecorrido.x > 0  && actualRecorrido.y > -0.5f  && actualRecorrido.y < 0.5f)
				{
					indice++;
					Debug.Log("movimiento hacia la derecha");
					if(indice > sitioElegido*5-1){
						indice = (sitioElegido-1)*5;
					}
					imagen = imagenes[indice];
					
				}
			}
		}
	}
	void OnGUI(){
		if (Interfaz)
			GUI.skin = Interfaz;
		//variables para la interfaz
		float altoPantalla = Screen.height;
		float anchoPantalla = Screen.width;
		int tamanioLetra = Screen.width/23;
		//Botones
		float inicioBotones = altoPantalla / 2;
		float diferenciaAlto = altoPantalla / 12;
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fondo,ScaleMode.StretchToFill);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, diferenciaAlto), cabecera, ScaleMode.StretchToFill);
		GUI.skin.label.fontSize = Screen.width/12;
		GUI.Label (new Rect (anchoPantalla/2-Screen.width/2,0,Screen.width, Screen.height/2-diferenciaAlto),tituloSitio);
		float tamaniogaleria = Screen.height/2.3f;
		float x1 = Screen.width/2-tamaniogaleria/2;
		float y1 = diferenciaAlto*1.5f;
		GUI.DrawTexture(new Rect (x1,y1,tamaniogaleria,tamaniogaleria),imagen);
		GUI.DrawTexture (new Rect(Screen.width/15,Screen.height/2+Screen.width/7.5f,Screen.width-Screen.width/7.5f,Screen.height/2.6f),descripcion);

		GUI.skin = skinDescripcion;
		GUI.skin.label.fontSize =Screen.width/16;
		scrollPosition  = GUI.BeginScrollView (new Rect (0, Screen.height/2+Screen.width/7.5f,Screen.width-Screen.width/7.5f, Screen.height/2.6f), scrollPosition, new Rect (20, Screen.height/2+Screen.width/7.5f, Screen.width-Screen.width/7.5f, Screen.height), GUIStyle.none, GUIStyle.none);
		GUI.Label (new Rect (Screen.width/7, Screen.height/2+Screen.height/12f, Screen.width-Screen.width/4,Screen.height), descSitio);
		GUI.EndScrollView ();
	}
}
