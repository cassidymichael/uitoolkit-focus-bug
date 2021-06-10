using UnityEngine;
using UnityEngine.UIElements;


namespace UI
{
	public class DevToolsHandler : MonoBehaviour
	{
		[SerializeField] UIDocument UIDocument; // set in Inspector
		[SerializeField] GameObject playerObject; // set in Inspector
		PlayerController playerController;
		public TextElement xInput;
		public TextElement yInput;
		
		public static DevToolsHandler Instance { get; private set; }

		private void Awake()
		{
			if (Instance != null && Instance != this)
				Destroy(gameObject);
			Instance = this;
		}

		private void Start()
		{
	
			VisualElement root = UIDocument.rootVisualElement;
			playerController = playerObject.GetComponent<PlayerController>();

			xInput = root.Q<TextElement>("xInput");
			yInput = root.Q<TextElement>("yInput");
			
			//root.Q<TextField>("speedModifier").value = playerController.accelerationMultiplier.ToString();
			//root.Q<TextField>("changeDirMultiplier").value = playerController.changeDirMultiplier.ToString();
			// original player controller removed

			root.Q<VisualElement>("container").RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation(), TrickleDown.TrickleDown);
			root.panel.visualTree.RegisterCallback<KeyDownEvent>(e => e.StopImmediatePropagation(), TrickleDown.TrickleDown);

		}

	}
}

