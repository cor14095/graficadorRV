using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using UnityEngine;
using IToken = Antlr4.Runtime.IToken;

class visitorMath : mathBaseVisitor<String>
{
	private string expression = "";

	public override String VisitProg([NotNull]mathParser.ProgContext context){
		
		return VisitChildren(context);
	}
	public override String VisitTerm([NotNull]mathParser.TermContext context){
		try
		{
			var b = (context.GetChild(0).GetChild(0).ChildCount >= 1);

			return VisitChildren(context);
			

			
		}
		catch (Exception e)
		{
			expression = expression + context.GetChild(0).GetText();
			return  expression;
		} 
	}

	public override String VisitFactor([NotNull] mathParser.FactorContext context){
		if (context.ChildCount <= 1) return VisitChildren(context);
		
		if (context.ChildCount != 3)
		{
			Visit(context.GetChild(0));
			expression = expression + "*";
			Visit(context.GetChild(1));
			
			
		}
		else
		{
			expression = expression + "System.Math.Pow (";
			Visit(context.GetChild(0));
			expression = expression + ",";
			Visit(context.GetChild(2));
			expression = expression + ")";
		}
		
		return expression;
	}

	public override String VisitExpr([NotNull] mathParser.ExprContext context){
		if (context.ChildCount <= 1) return VisitChildren(context);
		 Visit(context.GetChild(0));
	
		expression = expression + context.GetChild(1).GetText();
		 Visit(context.GetChild(2));
		
		//Debug.Log(expression);
		//VisitChildren(context)
		return expression;
	}

	public override String VisitEqu([NotNull] mathParser.EquContext context){
		
		if (context.ChildCount <= 1) return VisitChildren(context);
		 Visit(context.GetChild(0));
	
		expression = expression + context.GetChild(1).GetText();
		Visit(context.GetChild(2));
		//Debug.Log(expression);
		//VisitChildren(context)
		return expression;
	}

	public override String VisitPi([NotNull] mathParser.PiContext context)
	{
		expression = expression + "3.14159265358979323846";
		return expression;
	}

	public override String VisitE([NotNull] mathParser.EContext context)
	{
		expression = expression + "2.718281828459045235360";
		return expression;
	}

	public override String VisitConstant([NotNull] mathParser.ConstantContext context)
	{
		return VisitChildren(context);
	}

	public override String VisitLogarithm([NotNull] mathParser.LogarithmContext context)
	{
		return VisitChildren(context);
	}

	public override String VisitLog([NotNull] mathParser.LogContext context)
	{
		
		//Check if the children are:
			// logw NUM '(' expr ')' this would be logarithm with base
			// logw '(' expr ')' this would be log with base 10
			// logw '(' e ')' this would be log of e 

		if (context.ChildCount == 5)
		{
			expression = expression + "System.Math.Log(";
			Visit(context.GetChild(3));
			expression = expression + "," + context.GetChild(1).GetText() + ")";
			
			return expression;
		}
		else if (context.GetChild(2).GetText().Equals("e"))
		{
			expression = expression + "System.Math.Log(" + context.GetChild(2).GetText() + ")";
			
			return expression;
		}
		else
		{
			expression = expression + "System.Math.Log10(";
			Visit(context.GetChild(2));
			expression = expression + ")";
			
			return expression;
		}
		
	}

	public override String VisitLn([NotNull] mathParser.LnContext context)
	{
		expression = expression + "System.Math.Log(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitTrig([NotNull] mathParser.TrigContext context)
	{
		return VisitChildren(context);
	}

	public override String VisitSin([NotNull] mathParser.SinContext context)
	{
		expression = expression + "System.Math.Sin(";
		Visit(context.GetChild(2)); 
		expression = expression + ")";
		return expression;
	}

	public override String VisitCos([NotNull] mathParser.CosContext context)
	{
		expression = expression + "System.Math.Cos(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitTan([NotNull] mathParser.TanContext context)
	{
		expression = expression + "System.Math.Tan(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitSinh([NotNull] mathParser.SinhContext context)
	{
		expression = expression + "System.Math.Sinh(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitCosh([NotNull] mathParser.CoshContext context)
	{
		expression = expression + "System.Math.Cosh(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitTanh([NotNull] mathParser.TanhContext context)
	{
		expression = expression + "System.Math.Tanh(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitArcsin([NotNull] mathParser.ArcsinContext context)
	{
		expression = expression + "System.Math.Asin(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitArccos([NotNull] mathParser.ArccosContext context)
	{
		expression = expression + "System.Math.Acos(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitArctan([NotNull] mathParser.ArctanContext context)
	{
		expression = expression + "System.Math.Atan(";
		Visit(context.GetChild(2));
		expression = expression + ")";
		return expression;
	}

	public override String VisitArcsinh([NotNull] mathParser.ArcsinhContext context)
	{
		
		//ln(x + sqrt(x^2 + 1)) 
		expression = expression + "System.Math.Log(";
		Visit(context.GetChild(2));
		expression = expression + " + System.Math.Sqrt(System.Math.Pow(";
		Visit(context.GetChild(2));
		expression = expression + ",2) + 1))";
		return expression;
	}

	public override String VisitArccosh([NotNull] mathParser.ArccoshContext context)
	{
		//ln(x + sqrt(x^2 - 1))
		expression = expression + "System.Math.Log(";
		Visit(context.GetChild(2));
		expression = expression + " + System.Math.Sqrt(System.Math.Pow(";
		Visit(context.GetChild(2));
		expression = expression + ",2) - 1))";
		return expression;
	}

	public override String VisitArctanh([NotNull] mathParser.ArctanhContext context)
	{
		//(1/2)ln((1+x)/(1-x))
		expression = expression + "0.5 * System.Math.Log((1 + ";
		Visit(context.GetChild(2));
		expression = expression + ")/(1 - ";
		Visit(context.GetChild(2));
		expression = expression + "))";
		
		return expression;
	}
}

