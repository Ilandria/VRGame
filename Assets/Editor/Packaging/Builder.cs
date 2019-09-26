using System;
using System.Linq;
using UnityEditor;

namespace CCB.MechGame.Editor.Packaging
{
	public class Builder
	{
		public static void SetVersion()
		{
			CliArgs cliArgs = new CliArgs(Environment.GetCommandLineArgs());
			
			PlayerSettings.bundleVersion = cliArgs["semVer"];
			Console.WriteLine($"Set bundle version to {PlayerSettings.bundleVersion}.");
		}

		public static void Build()
		{
			CliArgs cliArgs = new CliArgs(Environment.GetCommandLineArgs());

			BuildPlayerOptions playerOptions = new BuildPlayerOptions
			{
				scenes = EditorBuildSettings.scenes.Where(scene => scene.enabled).Select(scene => scene.path).ToArray(),
				options = BuildOptions.StrictMode,
				locationPathName = $"{cliArgs["buildDropPath"]}/{PlayerSettings.bundleVersion}/{DateTime.Now.ToString("yy-MM-dd H.mm.ss")}/{PlayerSettings.productName}/{PlayerSettings.productName}.exe",
				target = BuildTarget.StandaloneWindows,
				targetGroup = BuildTargetGroup.Standalone
			};

			Console.WriteLine($"Building player bundle version {PlayerSettings.bundleVersion} to {playerOptions.locationPathName}.");

			// TODO: Hook up BuildReport.
			BuildPipeline.BuildPlayer(playerOptions);
		}
	}
}