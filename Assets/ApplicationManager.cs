using UnityEngine;
using UnityEngine.Events;

public class ApplicationManager : MonoBehaviour {


	#region Singleton pattern (Awake)

	private static ApplicationManager _ins;
	public static ApplicationManager ins
	{
		get
		{
			if (_ins == null)
			{
				_ins = GameObject.FindObjectOfType<ApplicationManager>();

				DontDestroyOnLoad(_ins.gameObject);
			}

			return _ins;
		}
		set
		{
			_ins = value;
		}
	}

	void Awake()
	{
		if (_ins == null)
		{
			// Populate with first instance
			_ins = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			// Another instance exists, destroy
			if (this != _ins)
				Destroy(this.gameObject);
		}
	}

	#endregion

	private UserDataManager userDataManager;

	private ModalPanel modalPanel;
	private UnityAction yesQuitAction;
	private UnityAction noQuitAction;

	void Start()
	{
		userDataManager = UserDataManager.ins;

		modalPanel = ModalPanel.ins;
		yesQuitAction = new UnityAction(_QuitAndSave);
		noQuitAction = new UnityAction(_DoNothing);
	}


	public void QuitAndSave()
	{
		modalPanel.Choice("Are you sure you want to quit?\nThere is so much to do!", yesQuitAction, noQuitAction);
	}

	// method for exiting the application
	private void _QuitAndSave()
	{
		userDataManager.SaveUserData();
		Debug.Log("TODO: Display quit animation.");
		Application.Quit();
	}

	private void _DoNothing()
	{
		// Maybe eat some chips?
	}
}
