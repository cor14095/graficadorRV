using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using UnityEngine;
using UnityEngine.UI;

public class TreeCreator : MonoBehaviour{

	GameObject parent;
	public InputField input;
	string math;

	private void Start(){
		parent = gameObject;
		input = parent.GetComponentInChildren<InputField>();
	}

	public string GetText(){
		
		return input.text;
	}

	public static string Creator(string mathInput){
		var input = new AntlrInputStream(mathInput);
		var lexer = new mathLexer(input);
		var tokens = new CommonTokenStream(lexer);
		var parser = new mathParser(tokens);
		IParseTree tree = parser.prog();

		var visitor = new visitorMath();
		return visitor.Visit(tree);

	}
}