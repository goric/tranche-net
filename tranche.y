%namespace SyntaxAnalysis
%visibility internal

%using LexicalAnalysis;
%using AbstractSyntaxTree;
%using tc;

%start program
%YYSTYPE SemanticValue

/* Terminals */
%token<Token> RPAREN LBRACE RBRACE RBRACKET PBRACKET DOT DOTDOT COMMA IN TINT TREAL
%token<Token> TSTRING TBOOL TVOID TCHAR TFILE TSET TLIST FOR IF ELSE CLASS
%token<Token> NEW RETURN TRUE FALSE LITERAL_INT LITERAL_REAL LITERAL_STRING LITERAL_CHAR IDENTIFIER

/* Precedence rules */
%right ASSIGN
%left OR AND INCREMENT DECREMENT EXP
%left SMALLER GREATER SMEQ GTEQ EQ NEQ
%left PLUS MINUS TIMES DIVIDE MOD
%left UMINUS LPAREN NOT LBRACKET
%nonassoc ELSE

/* Non-terminal types */
program			: typeList		{ SyntaxTreeRoot = $$; $$.Location = CurrentLocationSpan; }
				;

typeDecList		:						{ $$ = new ASTStatementList(); $$.Location = CurrentLocationSpan; }
				| typeDec typeDecList	{ $$ = new ASTStatementList($1, $2); $$.Location = CurrentLocationSpan; }
				;

