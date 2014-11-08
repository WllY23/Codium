﻿//-----------------------------------------------------------------
// 1. Implements the SyntaxHighlighter by supplying keywords from txt files
// & the code to format. 
// 2. Implements the CodeCompiler to evaluate C# code at runtime
//-----------------------------------------------------------------

using UnityEngine;
using System.Collections;

public class CodeManager : MonoBehaviour {

	public bool setupSyntaxHighlighter = true;	// Setup a SyntaxHighlighter at startup?
	// An array of keywords (TextAsset).
	// The first line in the txt file is the color.
	public TextAsset[] keywordTxtFiles;

	public bool setupCompiler = true;	// Setup a CodeCompiler at startup?
	public string extraNamespaces = "";

	private static SyntaxHighlighter syntaxHighlighter;
	private static CodeCompiler codeCompiler;

	void Awake () {
		if (setupSyntaxHighlighter)
			SetupSyntaxHighlighter (keywordTxtFiles);

		if (setupCompiler)
			SetupCompiler (extraNamespaces);

		Intellisense codeComplete = new Intellisense ();
		codeComplete.GetFieldsInClass ("Debug", "UnityEngine");
	}

	// STATIC SETUP METHODS

	// Static method for creating a SyntaxHighlighter class
	public static void SetupSyntaxHighlighter (TextAsset[] keywordTxtFiles) {
		// Init an instance of the SyntaxHighlighter class
		syntaxHighlighter = new SyntaxHighlighter();

		// Loop through the keywords array and add them to the SyntaxHighlighter
		for (int i = 0; i < keywordTxtFiles.Length; i++) {
			syntaxHighlighter.AddKeywords (keywordTxtFiles[i]);
		}
	}

	// Static method for creating a SyntaxHighlighter class
	public static void SetupSyntaxHighlighter (TextAsset keywordTxtFile) {
		// Init an instance of the SyntaxHighlighter class
		syntaxHighlighter = new SyntaxHighlighter();

		//Add the keywords in keywordTxtFile to the SyntaxHighlighter
		syntaxHighlighter.AddKeywords (keywordTxtFile);
	}

	// Static method for creating an instance of CodeCompiler & initializing it
	public static void SetupCompiler (string namespaces) {
		// Init an instance of the CodeCompiler class
		codeCompiler = new CodeCompiler();

		// Initialize the evaluator to make it ready for compiling
		codeCompiler.InitEvaluator ("using UnityEngine;\n" + 
									"using System;\n" + namespaces);
	}

	// STATIC USAGE METHODS

	// Static method for evaluating C# code
	public static void RunCode (string code) {
		if (codeCompiler == null) {
			Debug.LogError("Couldn't run code because no CodeCompiler was set up in CodeManager.");
			return;
		}

		if (!codeCompiler.IsInitialized) {
			Debug.LogError ("Code couldn't be run. Evaluator not initialized.");
			return;
		}

		codeCompiler.RunCode (code);
	}

	// Static method for formatting code by applying syntax highlighting
	public static string FormatCode (string code) {
		if (syntaxHighlighter == null) {
			Debug.LogError("Couldn't format code because no SyntaxHighlighter was set up in CodeManager.");
			return null;
		}

		return syntaxHighlighter.Highlight(code);
	}
}