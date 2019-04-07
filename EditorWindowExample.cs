using UnityEditor;

namespace ListView {
	public class EditorWindowExample : EditorWindow {
		
		private static EditorWindowExample window;
		
		[MenuItem("Window/EditorWindowExample")]
		public static void OpenWindow(){
			if (window != null) window.Close();
			GetWindow<EditorWindowExample>("EditorWindowExample").Show();
		}
		
	}
}
