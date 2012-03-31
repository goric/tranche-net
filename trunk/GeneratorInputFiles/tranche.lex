%namespace LexicalAnalysis
%using SyntaxAnalysis;
%using QUT.Gppg;
%using tc;
%visibility internal

NewLine     \r|\n|\r\n
WhiteSpace  {NewLine}|\t\f
Integer		0|[1-9][0-9,]*
Exponent	[eE]("+"|"-")?{Integer}
Real		{Integer}("."0*{Integer})?{Exponent}?
Comment		(\/\*)([^\*]*|\*+[^\/\*])*(\*+\/)
Identifier	[a-zA-Z][a-zA-Z0-9]*
String		\"(\\.|[^"])*\"

%%

/* section names */

".Settings"				{ yylval.Token = new Token(Tokens.SETTINGS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.SETTINGS); return (int) Tokens.SETTINGS; }
".Deal"					{ yylval.Token = new Token(Tokens.DEAL, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.DEAL); return (int) Tokens.DEAL; }
".Collateral"			{ yylval.Token = new Token(Tokens.COLLATERAL, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.COLLATERAL); return (int) Tokens.COLLATERAL; }
".CollateralItem"		{ yylval.Token = new Token(Tokens.COLLATERALITEM, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.COLLATERALITEM); return (int) Tokens.COLLATERALITEM; }
".Securities"			{ yylval.Token = new Token(Tokens.SECURITIES, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.SECURITIES); return (int) Tokens.SECURITIES; }
".Bond"					{ yylval.Token = new Token(Tokens.BOND, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.BOND); return (int) Tokens.BOND; }
".CreditPaymentRules"	{ yylval.Token = new Token(Tokens.CREDITPAYMENTRULES, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.CREDITPAYMENTRULES); return (int) Tokens.CREDITPAYMENTRULES; }
".Interest"				{ yylval.Token = new Token(Tokens.INTEREST, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.INTEREST); return (int) Tokens.INTEREST; }
".Principal"			{ yylval.Token = new Token(Tokens.PRINCIPAL, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PRINCIPAL); return (int) Tokens.PRINCIPAL; }
".Simulation"			{ yylval.Token = new Token(Tokens.SIMULATION, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.SIMULATION); return (int) Tokens.SIMULATION; }
".Rules"				{ yylval.Token = new Token(Tokens.RULES, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.RULES); return (int) Tokens.RULES; }

/* other keywords */

"upto"				{ yylval.Token = new Token(Tokens.UPTO, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.UPTO); return (int) Tokens.UPTO; }
"downto"			{ yylval.Token = new Token(Tokens.DOWNTO, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.DOWNTO); return (int) Tokens.DOWNTO; }
"yes"		   		{ yylval.Token = new Token(Tokens.TRUE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.TRUE); return (int) Tokens.TRUE; }
"true"				{ yylval.Token = new Token(Tokens.TRUE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.TRUE); return (int) Tokens.TRUE; }
"y"					{ yylval.Token = new Token(Tokens.TRUE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.TRUE); return (int) Tokens.TRUE; }
"no"		   		{ yylval.Token = new Token(Tokens.FALSE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.FALSE); return (int) Tokens.FALSE; }
"false"				{ yylval.Token = new Token(Tokens.FALSE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.FALSE); return (int) Tokens.FALSE; }
"n"					{ yylval.Token = new Token(Tokens.FALSE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.FALSE); return (int) Tokens.FALSE; }
"and"				{ yylval.Token = new Token(Tokens.AND, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.AND); return (int) Tokens.AND; }
"or"				{ yylval.Token = new Token(Tokens.OR, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.OR); return (int) Tokens.OR; }
"where"				{ yylval.Token = new Token(Tokens.WHERE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.WHERE); return (int) Tokens.WHERE; }
"aggregate"			{ yylval.Token = new Token(Tokens.AGGREGATE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.AGGREGATE); return (int) Tokens.AGGREGATE; }
"filter"			{ yylval.Token = new Token(Tokens.FILTER, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.FILTER); return (int) Tokens.FILTER; }
"first"				{ yylval.Token = new Token(Tokens.FIRST, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.FIRST); return (int) Tokens.FIRST; }
"last"				{ yylval.Token = new Token(Tokens.LAST, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LAST); return (int) Tokens.LAST; }
"with"				{ yylval.Token = new Token(Tokens.WITH, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.WITH); return (int) Tokens.WITH; }
"+Day"				{ yylval.Token = new Token(Tokens.PLUSDAY, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PLUSDAY); return (int) Tokens.PLUSDAY; }
"-Day"				{ yylval.Token = new Token(Tokens.MINUSDAY, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.MINUSDAY); return (int) Tokens.MINUSDAY; }
"+Month"			{ yylval.Token = new Token(Tokens.PLUSMONTH, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PLUSMONTH); return (int) Tokens.PLUSMONTH; }
"-Month"			{ yylval.Token = new Token(Tokens.MINUSMONTH, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.MINUSMONTH); return (int) Tokens.MINUSMONTH; }
"+Year"				{ yylval.Token = new Token(Tokens.PLUSYEAR, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PLUSYEAR); return (int) Tokens.PLUSYEAR; }
"-Year"				{ yylval.Token = new Token(Tokens.MINUSYEAR, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.MINUSYEAR); return (int) Tokens.MINUSYEAR; }

/* operators */

"&&"				{ yylval.Token = new Token(Tokens.AND, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.AND); return (int) Tokens.AND; }
"||"				{ yylval.Token = new Token(Tokens.OR, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.OR); return (int) Tokens.OR; }
"->"          	    { yylval.Token = new Token(Tokens.RULE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.RULE); return (int) Tokens.RULE; }
"+"          	    { yylval.Token = new Token(Tokens.PLUS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PLUS); return (int) Tokens.PLUS; }
"-"          	    { yylval.Token = new Token(Tokens.MINUS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.MINUS); return (int) Tokens.MINUS; }
"*"          	    { yylval.Token = new Token(Tokens.TIMES, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.TIMES); return (int) Tokens.TIMES; }
"/"          	    { yylval.Token = new Token(Tokens.DIVIDE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.DIVIDE); return (int) Tokens.DIVIDE; }
"<"          	    { yylval.Token = new Token(Tokens.SMALLER, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.SMALLER); return (int) Tokens.SMALLER; }
">"          	    { yylval.Token = new Token(Tokens.GREATER, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.GREATER); return (int) Tokens.GREATER; }
"<="        	    { yylval.Token = new Token(Tokens.SMEQ, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.SMEQ); return (int) Tokens.SMEQ; }
">="        	    { yylval.Token = new Token(Tokens.GTEQ, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.GTEQ); return (int) Tokens.GTEQ; }
"="               	{ yylval.Token = new Token(Tokens.EQ, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.EQ); return (int) Tokens.EQ; }
"!="               	{ yylval.Token = new Token(Tokens.NEQ, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.NEQ); return (int) Tokens.NEQ; }
"<>"               	{ yylval.Token = new Token(Tokens.NEQ, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.NEQ); return (int) Tokens.NEQ; }
":"                	{ yylval.Token = new Token(Tokens.ASSIGN, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ASSIGN); return (int) Tokens.ASSIGN; }
"!"                	{ yylval.Token = new Token(Tokens.NOT, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.NOT); return (int) Tokens.NOT; }
"%"	                { yylval.Token = new Token(Tokens.MOD, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.MOD); return (int) Tokens.MOD; }
"("                	{ yylval.Token = new Token(Tokens.LPAREN, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LPAREN); return (int) Tokens.LPAREN; }
")"                	{ yylval.Token = new Token(Tokens.RPAREN, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.RPAREN); return (int) Tokens.RPAREN; }
"{"                	{ yylval.Token = new Token(Tokens.LBRACE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LBRACE); return (int) Tokens.LBRACE; }
"}"                	{ yylval.Token = new Token(Tokens.RBRACE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.RBRACE); return (int) Tokens.RBRACE; }
"["                	{ yylval.Token = new Token(Tokens.LBRACKET, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LBRACKET); return (int) Tokens.LBRACKET; }
"]"                	{ yylval.Token = new Token(Tokens.RBRACKET, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.RBRACKET); return (int) Tokens.RBRACKET; }
"[]"				{ yylval.Token = new Token(Tokens.PBRACKET, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PBRACKET); return (int) Tokens.PBRACKET; }
"++"				{ yylval.Token = new Token(Tokens.INCREMENT, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.INCREMENT); return (int) Tokens.INCREMENT; }
"--"				{ yylval.Token = new Token(Tokens.DECREMENT, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.DECREMENT); return (int) Tokens.DECREMENT; }
"^"					{ yylval.Token = new Token(Tokens.EXP, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.EXP); return (int) Tokens.EXP; }
"**"				{ yylval.Token = new Token(Tokens.EXP, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.EXP); return (int) Tokens.EXP; }
"."					{ yylval.Token = new Token(Tokens.DOT, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.DOT); return (int) Tokens.DOT; }
","					{ yylval.Token = new Token(Tokens.COMMA, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.COMMA); return (int) Tokens.COMMA; }
"::"                { yylval.Token = new Token(Tokens.CONS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.CONS); return (int) Tokens.CONS; }
"@+"				{ yylval.Token = new Token(Tokens.ATPLUS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATPLUS); return (int) Tokens.ATPLUS; }
"@-"				{ yylval.Token = new Token(Tokens.ATMINUS, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATMINUS); return (int) Tokens.ATMINUS; }
"@*"				{ yylval.Token = new Token(Tokens.ATTIMES, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATTIMES); return (int) Tokens.ATTIMES; }
"@/"				{ yylval.Token = new Token(Tokens.ATDIV, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATDIV); return (int) Tokens.ATDIV; }
"@^"				{ yylval.Token = new Token(Tokens.ATEXP, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATEXP); return (int) Tokens.ATEXP; }
"@**"				{ yylval.Token = new Token(Tokens.ATEXP, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATEXP); return (int) Tokens.ATEXP; }
"@%"				{ yylval.Token = new Token(Tokens.ATMOD, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.ATMOD); return (int) Tokens.ATMOD; }
"|"					{ yylval.Token = new Token(Tokens.PIPE, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.PIPE); return (int) Tokens.PIPE; }


{Integer}	 		{ yylval.Token = new Token(Tokens.LITERAL_INT, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LITERAL_INT); return (int) Tokens.LITERAL_INT; }
{Real}	 			{ yylval.Token = new Token(Tokens.LITERAL_REAL, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LITERAL_REAL); return (int) Tokens.LITERAL_REAL; }
{String}			{ yylval.Token = new Token(Tokens.LITERAL_STRING, yytext, yyline, yycol); if(isDebug) Console.WriteLine(Tokens.LITERAL_STRING); return (int) Tokens.LITERAL_STRING; }
{Identifier}		{ yylval.Token = new Token(Tokens.IDENTIFIER, yytext); if(isDebug) Console.WriteLine(Tokens.IDENTIFIER); return (int) Tokens.IDENTIFIER; }
{NewLine}			{ /* NewLine - Do Nothing */ }
{WhiteSpace}		{ /* Whitespace - Do Nothing */ }
{Comment}			{ /* Comment - Do Nothing */ }

	yylloc = new LexLocation(tokLin,tokCol,tokELin,tokECol);

%%

public bool isDebug = true;

public override void yyerror(string format, params object[] args)
{
	Console.Write("Line: {0} ", yyline);
	Console.WriteLine(format, args);
}
