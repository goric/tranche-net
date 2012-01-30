%namespace LexicalAnalysis
%using SyntaxAnalysis;
%using QUT.Gppg;
%using tc;
%visibility internal

NewLine     \r|\n|\r\n
WhiteSpace  {NewLine}|\t\f
Integer		0|[1-9][0-9,]*
Exponent	[eE]("+"|"-")?{Integer}
Real		{Integer}("."{Integer})?{Exponent}?
Comment		(\/\*)([^\*]*|\*+[^\/\*])*(\*+\/)
Identifier	[a-zA-Z][a-zA-Z0-9]*
String		\"(\\.|[^"])*\"
Character	'\\u[0-9a-fA-F][0-9a-fA-F][0-9a-fA-F][0-9a-fA-F]'

%%

"Settings"				{ yylval.Token = new Token(Tokens.SETTINGS, yytext, yyline, yycol); return (int) Tokens.SETTINGS; }
"Deal"					{ yylval.Token = new Token(Tokens.DEAL, yytext, yyline, yycol); return (int) Tokens.DEAL; }
"Collateral"			{ yylval.Token = new Token(Tokens.COLLATERAL, yytext, yyline, yycol); return (int) Tokens.COLLATERAL; }
"CollateralItem"		{ yylval.Token = new Token(Tokens.COLLATERALITEM, yytext, yyline, yycol); return (int) Tokens.COLLATERALITEM; }
"Securities"			{ yylval.Token = new Token(Tokens.SECURITIES, yytext, yyline, yycol); return (int) Tokens.SECURITIES; }
"Bond"					{ yylval.Token = new Token(Tokens.BOND, yytext, yyline, yycol); return (int) Tokens.BOND; }
"CreditPaymentRules"	{ yylval.Token = new Token(Tokens.CREDITPAYMENTRULES, yytext, yyline, yycol); return (int) Tokens.CREDITPAYMENTRULES; }
"Simulation"			{ yylval.Token = new Token(Tokens.SIMULATION, yytext, yyline, yycol); return (int) Tokens.SIMULATION; }
"Rules"					{ yylval.Token = new Token(Tokens.RULES, yytext, yyline, yycol); return (int) Tokens.RULES; }


"+"          	    { yylval.Token = new Token(Tokens.PLUS, yytext, yyline, yycol); return (int)Tokens.PLUS; }
"-"          	    { yylval.Token = new Token(Tokens.MINUS, yytext, yyline, yycol); return (int)Tokens.MINUS; }
"*"          	    { yylval.Token = new Token(Tokens.TIMES, yytext, yyline, yycol); return (int)Tokens.TIMES; }
"/"          	    { yylval.Token = new Token(Tokens.DIVIDE, yytext, yyline, yycol); return (int)Tokens.DIVIDE; }
"<"          	    { yylval.Token = new Token(Tokens.SMALLER, yytext, yyline, yycol); return (int)Tokens.SMALLER; }
">"          	    { yylval.Token = new Token(Tokens.GREATER, yytext, yyline, yycol); return (int)Tokens.GREATER; }
"<="        	    { yylval.Token = new Token(Tokens.SMEQ, yytext, yyline, yycol); return (int)Tokens.SMEQ; }
">="        	    { yylval.Token = new Token(Tokens.GTEQ, yytext, yyline, yycol); return (int)Tokens.GTEQ; }
"="               	{ yylval.Token = new Token(Tokens.ASSIGN, yytext, yyline, yycol); return (int)Tokens.EQ; }
"=="               	{ yylval.Token = new Token(Tokens.EQ, yytext, yyline, yycol); return (int)Tokens.EQ; }
"!="               	{ yylval.Token = new Token(Tokens.NEQ, yytext, yyline, yycol); return (int)Tokens.NEQ; }
":"                	{ yylval.Token = new Token(Tokens.ASSIGN, yytext, yyline, yycol); return (int)Tokens.ASSIGN; }
"and"              	{ yylval.Token = new Token(Tokens.AND, yytext, yyline, yycol); return (int)Tokens.AND; }
"or"               	{ yylval.Token = new Token(Tokens.OR, yytext, yyline, yycol); return (int)Tokens.OR; }
"!"                	{ yylval.Token = new Token(Tokens.NOT, yytext, yyline, yycol); return (int)Tokens.NOT; }
"mod"               { yylval.Token = new Token(Tokens.MOD, yytext, yyline, yycol); return (int)Tokens.MOD; }
"("                	{ yylval.Token = new Token(Tokens.LPAREN, yytext, yyline, yycol); return (int)Tokens.LPAREN; }
")"                	{ yylval.Token = new Token(Tokens.RPAREN, yytext, yyline, yycol); return (int)Tokens.RPAREN; }
"{"                	{ yylval.Token = new Token(Tokens.LBRACE, yytext, yyline, yycol); return (int)Tokens.LBRACE; }
"}"                	{ yylval.Token = new Token(Tokens.RBRACE, yytext, yyline, yycol); return (int)Tokens.RBRACE; }
"["                	{ yylval.Token = new Token(Tokens.LBRACKET, yytext, yyline, yycol); return (int)Tokens.LBRACKET; }
"]"                	{ yylval.Token = new Token(Tokens.RBRACKET, yytext, yyline, yycol); return (int)Tokens.RBRACKET; }
"[]"				{ yylval.Token = new Token(Tokens.PBRACKET, yytext, yyline, yycol); return (int)Tokens.PBRACKET; }
"++"				{ yylval.Token = new Token(Tokens.INCREMENT, yytext, yyline, yycol); return (int)Tokens.INCREMENT; }
"--"				{ yylval.Token = new Token(Tokens.DECREMENT, yytext, yyline, yycol); return (int)Tokens.DECREMENT; }
"^"					{ yylval.Token = new Token(Tokens.EXP, yytext, yyline, yycol); return (int)Tokens.EXP; }
"**"				{ yylval.Token = new Token(Tokens.EXP, yytext, yyline, yycol); return (int)Tokens.EXP; }
".'"				{ yylval.Token = new Token(Tokens.AUTOPROP, yytext, yyline, yycol); return (int)Tokens.AUTOPROP; }
"."					{ yylval.Token = new Token(Tokens.DOT, yytext, yyline, yycol); return (int)Tokens.DOT; }
".."				{ yylval.Token = new Token(Tokens.DOTDOT, yytext, yyline, yycol); return (int)Tokens.DOTDOT;}
","					{ yylval.Token = new Token(Tokens.COMMA, yytext, yyline, yycol); return (int)Tokens.COMMA; }

"int"              	{ yylval.Token = new Token(Tokens.TINT, yytext, yyline, yycol); return (int)Tokens.TINT; }
"real"              { yylval.Token = new Token(Tokens.TREAL, yytext, yyline, yycol); return (int)Tokens.TREAL; }
"string"			{ yylval.Token = new Token(Tokens.TSTRING, yytext, yyline, yycol); return (int)Tokens.TSTRING; }
"flag"             	{ yylval.Token = new Token(Tokens.TBOOL, yytext, yyline, yycol); return (int)Tokens.TBOOL; }
"char"             	{ yylval.Token = new Token(Tokens.TCHAR, yytext, yyline, yycol); return (int)Tokens.TCHAR; }
"set"				{ yylval.Token = new Token(Tokens.TSET, yytext, yyline, yycol); return (int)Tokens.TSET; }
"list"				{ yylval.Token = new Token(Tokens.TLIST, yytext, yyline, yycol); return (int)Tokens.TLIST; }
"file"				{ yylval.Token = new Token(Tokens.TFILE, yytext, yyline, yycol); return (int)Tokens.TFILE; }
"void"             	{ yylval.Token = new Token(Tokens.TVOID, yytext, yyline, yycol); return (int)Tokens.TVOID; }
"for"				{ yylval.Token = new Token(Tokens.FOR, yytext, yyline, yycol); return (int)Tokens.FOR; }
"in"				{ yylval.Token = new Token(Tokens.IN, yytext, yyline, yycol); return (int)Tokens.IN; }
"if"				{ yylval.Token = new Token(Tokens.IF, yytext, yyline, yycol); return (int)Tokens.IF; }   
"else"             	{ yylval.Token = new Token(Tokens.ELSE, yytext, yyline, yycol); return (int)Tokens.ELSE; }
"type"				{ yylval.Token = new Token(Tokens.CLASS, yytext, yyline, yycol); return (int)Tokens.CLASS; }
"new"				{ yylval.Token = new Token(Tokens.NEW, yytext, yyline, yycol); return (int)Tokens.NEW; }
"return"			{ yylval.Token = new Token(Tokens.RETURN, yytext, yyline, yycol); return (int)Tokens.RETURN; }
"yes"		   		{ yylval.Token = new Token(Tokens.TRUE, yytext, yyline, yycol); return (int)Tokens.TRUE; }
"no"		   		{ yylval.Token = new Token(Tokens.FALSE, yytext, yyline, yycol); return (int)Tokens.FALSE; }

{Integer}	 		{ yylval.Token = new Token(Tokens.LITERAL_INT, yytext, yyline, yycol); return (int)Tokens.LITERAL_INT; }
{Real}	 			{ yylval.Token = new Token(Tokens.LITERAL_REAL, yytext, yyline, yycol); return (int)Tokens.LITERAL_REAL; }
{String}			{ yylval.Token = new Token(Tokens.LITERAL_STRING, yytext, yyline, yycol); return (int)Tokens.LITERAL_STRING; }
{Character}			{ yylval.Token = new Token(Tokens.LITERAL_CHAR, yytext, yyline, yycol); return (int)Tokens.LITERAL_CHAR; }
{Identifier}		{ yylval.Token = new Token(Tokens.IDENTIFIER, yytext); return (int)Tokens.IDENTIFIER; }
{NewLine}			{ /* NewLine - Do Nothing */ }
{WhiteSpace}		{ /* Whitespace - Do Nothing */ }
{Comment}			{ /* Comment - Do Nothing */ }

	yylloc = new LexLocation(tokLin,tokCol,tokELin,tokECol);

%%

public override void yyerror(string format, params object[] args)
{
	Console.Write("Line: {0}", yyline);
	Console.WriteLine(format, args);
}
