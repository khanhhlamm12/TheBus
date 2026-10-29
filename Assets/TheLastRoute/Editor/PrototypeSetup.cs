using System.IO;
using TMPro;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TheLastRoute.Editor
{
    public static class PrototypeSetup
    {
        public const string ScenePath = "Assets/TheLastRoute/Scenes/BusPrototype.unity";

        [MenuItem("The Last Route/Create or Open Prototype")]
        public static void CreateScene()
        {
            if (File.Exists(ScenePath))
            {
                var existing = SceneManager.GetSceneByPath(ScenePath);
                if (!existing.IsValid() || !existing.isLoaded) existing = EditorSceneManager.OpenScene(ScenePath, OpenSceneMode.Additive);
                SceneManager.SetActiveScene(existing);
                return;
            }
            Directory.CreateDirectory("Assets/TheLastRoute/Scenes");
            Directory.CreateDirectory("Assets/TheLastRoute/Data");
            AssetDatabase.Refresh();
            var prototype = Object.FindAnyObjectByType<BusPrototype>();
            var scene = prototype ? prototype.gameObject.scene : EditorSceneManager.NewScene(NewSceneSetup.EmptyScene, NewSceneMode.Additive);
            SceneManager.SetActiveScene(scene);
            if (!prototype) prototype = new GameObject("The Last Route Prototype").AddComponent<BusPrototype>();
            var settings = AssetDatabase.LoadAssetAtPath<PrototypeSettings>("Assets/TheLastRoute/Data/PrototypeSettings.asset");
            if (!settings)
            {
                settings = ScriptableObject.CreateInstance<PrototypeSettings>();
                AssetDatabase.CreateAsset(settings, "Assets/TheLastRoute/Data/PrototypeSettings.asset");
            }
            prototype.settings = settings;

            // Use a project-local dynamic font; retain its source font for player builds.
            var source = AssetDatabase.LoadAssetAtPath<Font>("Assets/TheLastRoute/UI/PrototypeFont.ttf");
            if (!source) throw new System.InvalidOperationException("Missing prototype font source.");
            var font = TMP_FontAsset.CreateFontAsset(source);
            font.name = "PrototypeFont SDF";
            AssetDatabase.CreateAsset(font, "Assets/TheLastRoute/Data/PrototypeFont.asset");
            foreach (var atlas in font.atlasTextures) AssetDatabase.AddObjectToAsset(atlas, font);
            AssetDatabase.AddObjectToAsset(font.material, font);
            prototype.font = font;
            EditorSceneManager.SaveScene(scene, ScenePath);
            AssetDatabase.SaveAssets();
            Debug.Log("Prototype scene created: " + ScenePath);
        }

        public static void BuildWindows()
        {
            Directory.CreateDirectory("Builds/Prototype");
            var report = BuildPipeline.BuildPlayer(new BuildPlayerOptions {
                scenes = new[] { ScenePath }, locationPathName = "Builds/Prototype/TheLastRoute.exe",
                target = BuildTarget.StandaloneWindows64, options = BuildOptions.Development
            });
            if (report.summary.result != UnityEditor.Build.Reporting.BuildResult.Succeeded)
                throw new System.InvalidOperationException("Prototype build failed: " + report.summary.result);
        }
    }
}
