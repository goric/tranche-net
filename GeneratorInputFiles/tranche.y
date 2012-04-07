%namespace SyntaxAnalysis
%visibility internal

%using LexicalAnalysis;
%using AbstractSyntaxTree;
%using AbstractSyntaxTree.InternalTypes;
%using tc;

%start program
%YYSTYPE SemanticValue

/* Terminals */
%token<Token> SETTINGS DEAL COLLATERAL COLLATERALITEM SECURITIES BOND CREDITPAYMENTRULES INTEREST PRINCIPAL SIMULATION RULES
%token<Token> UPTO DOWNTO TRUE FALSE AND OR WHERE AGGREGATE FILTER FIRST LAST WITH
%token<Token> RULE PLUS MINUS TIMES DIVIDE SMALLER GREATER SMEQ GTEQ EQ NEQ ASSIGN NOT MOD 
%token<Token> LPAREN RPAREN LBRACE RBRACE LBRACKET RBRACKET PBRACKET INCREMENT DECREMENT EXP DOT COMMA CONS 
%token<Token> ATPLUS ATMINUS ATTIMES ATDIV ATMOD ATEXP PIPE LITERAL_INT LITERAL_REAL LITERAL_STRING IDENTIFIER
%token<Token> PLUSDAY MINUSDAY PLUSMONTH MINUSMONTH PLUSYEAR MINUSYEAR CONCAT

/* Precedence rules */
%right ASSIGN
%left OR AND INCREMENT DECREMENT EXP
%left SMALLER GREATER SMEQ GTEQ EQ NEQ
%left PLUS MINUS TIMES DIVIDE MOD
%left ATPLUS ATMINUS ATTIMES ATDIV ATMOD ATEXP
%left LPAREN NOT LBRACKET

/* Non-terminal types, generic */
%type<Prog> program
%type<StatementList> statementList 
%type<Statement> statement loop instantiation
%type<Expression> expression literal compExpression lvalue expression arithmetic concat
%type<ExpressionList> onePlusActuals actuals boolListOpt
%type<SpecialFunction> specialFunction

/* Non-terminal types, tranche-specific */
%type<DeclarationClass> settingsOpt dealOpt collatSection securitySection simSection secListOpt creditRulesOpt
%type<InternalRuleList> rulesListOpt 
%type<CollateralItem> collListOpt
%type<Bond> secListOpt
%type<InterestRules> interestRules
%type<PrincipalRules> principalRules

%%

/* Non-terminal productions */
program			: settingsOpt dealOpt collatSection securitySection	creditRulesOpt simSection { $$ = new Prog($1, $2, $3, $4, $5, $6); SyntaxTreeRoot = $$; $$.Location = CurrentLocationSpan; }
				;

settingsOpt		:										{ $$ = new Settings(); $$.Location = CurrentLocationSpan; }
				| SETTINGS LBRACE statementList RBRACE	{ $$ = new Settings($3);  }
				;

dealOpt			:										{ $$ = new Deal(); $$.Location = CurrentLocationSpan; }
				| DEAL LBRACE statementList RBRACE		{ $$ = new Deal($3); $$.Location = CurrentLocationSpan; }
				;

collatSection	: COLLATERAL LBRACE collListOpt RBRACE	{ $$ = new Collateral($3); $$.Location = CurrentLocationSpan; }
				;

securitySection : SECURITIES LBRACE secListOpt RBRACE	{ $$ = new Securities($3); $$.Location = CurrentLocationSpan; }
				;

creditRulesOpt	:																{ $$ = new CreditPaymentRules(); $$.Location = CurrentLocationSpan; }
				| CREDITPAYMENTRULES LBRACE interestRules principalRules RBRACE	{ $$ = new CreditPaymentRules(new StatementList($3,$4)); $$.Location = CurrentLocationSpan; }
				;

simSection		: SIMULATION LBRACE statementList RBRACE	{ $$ = new Simulation($3); $$.Location = CurrentLocationSpan; }
				;

interestRules	:										{ $$ = new InterestRules(); $$.Location = CurrentLocationSpan; }
				| INTEREST LBRACE rulesListOpt RBRACE	{ $$ = new InterestRules($3); $$.Location = CurrentLocationSpan; }
				;

principalRules	:										{ $$ =  new PrincipalRules(); $$.Location = CurrentLocationSpan; }
				| PRINCIPAL LBRACE rulesListOpt RBRACE	{ $$ =  new PrincipalRules($3); $$.Location = CurrentLocationSpan; }
				;

rulesListOpt	:										{ $$ = new InternalRuleList(); $$.Location = CurrentLocationSpan; }
				| RULE compExpression boolListOpt		{ $$ = new InternalRuleList($2, $3); $$.Location = CurrentLocationSpan; }
				;

boolListOpt		: rulesListOpt							{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| OR compExpression boolListOpt			{ $$ = new InternalRuleListOr($2, $3); $$.Location = CurrentLocationSpan;  }
				| AND compExpression boolListOpt		{ $$ = new InternalRuleListAnd($2, $3); $$.Location = CurrentLocationSpan; }
				;

statementList	:										{ $$ = new StatementList(); $$.Location = CurrentLocationSpan; }
				| statement statementList				{ $$ = new StatementList($1, $2); $$.Location = CurrentLocationSpan; }
				;

statement		: IDENTIFIER ASSIGN expression			{ $$ = new Assign(new Identifier(CurrentLocationSpan, $1.Value), $3); $$.Location = CurrentLocationSpan; }
				| IDENTIFIER ASSIGN instantiation		{ $$ = new Assign(new Identifier(CurrentLocationSpan, $1.Value), $3); $$.Location = CurrentLocationSpan; }
				| IDENTIFIER ASSIGN LPAREN IDENTIFIER RPAREN expression { $$ = new Assign(new Identifier(CurrentLocationSpan, $1.Value), new Qualifier($4.Value, $6)); $$.Location = CurrentLocationSpan; }
				| LBRACE statementList RBRACE			{ $$ = new Block($2); $$.Location = CurrentLocationSpan; }
				| loop									{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| expression							{ $$ = new StatementExpression($1); $$.Location = CurrentLocationSpan; }
				| IDENTIFIER CONS literal				{ $$ = new Cons(new Identifier(CurrentLocationSpan, $1.Value), $3); $$.Location = CurrentLocationSpan; }
				| IDENTIFIER CONS lvalue				{ $$ = new Cons(new Identifier(CurrentLocationSpan, $1.Value), $3); $$.Location = CurrentLocationSpan; }
				;

collListOpt		:															{ $$ = new CollateralItem(); $$.Location = CurrentLocationSpan; }
				| COLLATERALITEM LBRACE statementList RBRACE collListOpt	{ $$ = new CollateralItem($3, $5); $$.Location = CurrentLocationSpan; }
				;

secListOpt		:												{ $$ = new Bond(); $$.Location = CurrentLocationSpan; }
				| BOND LBRACE statementList RBRACE secListOpt	{ $$ = new Bond($3, $5); $$.Location = CurrentLocationSpan; }
				;

expression		: IDENTIFIER LPAREN actuals RPAREN		{ $$ = new Invoke($1.Value, $3); $$.Location = CurrentLocationSpan; } 
				| lvalue DOT IDENTIFIER LPAREN actuals RPAREN		{ $$ = new Invoke($1, $3.Value, $5); $$.Location = CurrentLocationSpan; } 
				| literal								{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| compExpression						{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| lvalue								{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| arithmetic							{ $$ = $1; $$.Location = CurrentLocationSpan; }
				| lvalue specialFunction				{ $$ = new Invoke($1, $2); $$.Location = CurrentLocationSpan; }
				| concat								{ $$ = $1; $$.Location = CurrentLocationSpan; }
				;

instantiation	: SMALLER IDENTIFIER COMMA IDENTIFIER GREATER	{ $$ = new TimeSeries(new Identifier(CurrentLocationSpan, $2.Value), new Identifier(CurrentLocationSpan, $4.Value)); $$.Location = CurrentLocationSpan; }
				| LBRACKET statementList RBRACKET				{ $$ = new Set($2); $$.Location =  CurrentLocationSpan; }
				| FILTER IDENTIFIER IDENTIFIER FIRST			{ $$ = new Filter(new Identifier(CurrentLocationSpan, $2.Value), new Identifier(CurrentLocationSpan, $3.Value), "first"); $$.Location = CurrentLocationSpan; }
				| FILTER IDENTIFIER IDENTIFIER LAST				{ $$ = new Filter(new Identifier(CurrentLocationSpan, $2.Value), new Identifier(CurrentLocationSpan, $3.Value), "last"); $$.Location = CurrentLocationSpan; }
				| FILTER IDENTIFIER IDENTIFIER expression		{ $$ = new Filter(new Identifier(CurrentLocationSpan, $2.Value), new Identifier(CurrentLocationSpan, $3.Value), $4); $$.Location = CurrentLocationSpan; }
				| AGGREGATE IDENTIFIER expression				{ $$ = new Aggregate(new Identifier(CurrentLocationSpan, $2.Value), $3); $$.Location = CurrentLocationSpan; }
				| PIPE expression PIPE							{ $$ = new RuleType($2); $$.Location = CurrentLocationSpan; }
				| loop											{ $$ = $1; $$.Location = CurrentLocationSpan; }
				;

concat			: IDENTIFIER CONCAT IDENTIFIER			{ $$ = new Concat(new Identifier(CurrentLocationSpan, $1.Value), new Identifier(CurrentLocationSpan, $3.Value)); $$.Location = CurrentLocationSpan; }
				| IDENTIFIER CONCAT literal				{ $$ = new Concat(new Identifier(CurrentLocationSpan, $1.Value), $3); $$.Location = CurrentLocationSpan; }
				| literal CONCAT IDENTIFIER				{ $$ = new Concat($1, new Identifier(CurrentLocationSpan, $3.Value)); $$.Location = CurrentLocationSpan; }
				| literal CONCAT literal				{ $$ = new Concat($1, $3); $$.Location = CurrentLocationSpan; }
				;

loop			: LBRACKET IDENTIFIER ASSIGN expression UPTO expression RBRACKET LPAREN statementList RPAREN					{ $$ = new Loop($2.Value, $4, "upto", $6, $9); $$.Location = CurrentLocationSpan; }
				| LBRACKET IDENTIFIER ASSIGN expression DOWNTO expression RBRACKET LPAREN statementList RPAREN					{ $$ = new Loop($2.Value, $4, "downto", $6, $9); $$.Location = CurrentLocationSpan; }
				| LBRACKET IDENTIFIER ASSIGN expression UPTO expression WITH expression RBRACKET LPAREN statementList RPAREN	{ $$ = new Loop($2.Value, $4, "upto", $6, $8, $11); $$.Location = CurrentLocationSpan; }
				| LBRACKET IDENTIFIER ASSIGN expression DOWNTO expression WITH expression RBRACKET LPAREN statementList RPAREN  { $$ = new Loop($2.Value, $4, "downto", $6, $8, $11); $$.Location = CurrentLocationSpan; }
				;

lvalue			: IDENTIFIER							{ $$ = new Identifier(CurrentLocationSpan, $1.Value); $$.Location = CurrentLocationSpan; }
				| lvalue DOT IDENTIFIER					{ $$ = new DereferenceField($1, $3.Value); $$.Location = CurrentLocationSpan; }
				;

compExpression	: expression EQ expression		{ $$ = new Equal($1, $3); $$.Location = CurrentLocationSpan; }
				| expression NEQ expression		{ $$ = new NotEqual($1, $3); $$.Location = CurrentLocationSpan; }
				| expression SMEQ expression	{ $$ = new SmallerEqual($1, $3); $$.Location = CurrentLocationSpan; }
				| expression GTEQ expression	{ $$ = new GreaterEqual($1, $3); $$.Location = CurrentLocationSpan; }
				| expression GREATER expression	{ $$ = new Greater($1, $3); $$.Location = CurrentLocationSpan; }
				| expression SMALLER expression	{ $$ = new Smaller($1, $3); $$.Location = CurrentLocationSpan; }
				;

arithmetic		: expression PLUS expression	{ $$ = new Plus($1, $3); $$.Location = CurrentLocationSpan; }
				| expression MINUS expression	{ $$ = new Minus($1, $3); $$.Location = CurrentLocationSpan; }
				| expression TIMES expression	{ $$ = new Times($1, $3); $$.Location = CurrentLocationSpan; }
				| expression DIVIDE expression	{ $$ = new Divide($1, $3); $$.Location = CurrentLocationSpan; }
				| expression MOD expression		{ $$ = new Mod($1, $3); $$.Location = CurrentLocationSpan; }
				| expression EXP expression		{ $$ = new Exp($1, $3); $$.Location = CurrentLocationSpan; }
				| expression ATPLUS expression	{ $$ = new Plus($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression ATMINUS expression	{ $$ = new Minus($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression ATTIMES expression	{ $$ = new Times($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression ATDIV expression	{ $$ = new Divide($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression ATMOD expression	{ $$ = new Mod($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression ATEXP expression	{ $$ = new Exp($1, true, $3); $$.Location = CurrentLocationSpan; }
				| expression INCREMENT			{ $$ = new Increment($1); $$.Location = CurrentLocationSpan; }
				| expression DECREMENT			{ $$ = new Decrement($1); $$.Location = CurrentLocationSpan; }
				;

specialFunction : PLUSDAY LPAREN expression RPAREN		{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				| MINUSDAY LPAREN expression RPAREN		{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				| PLUSMONTH LPAREN expression RPAREN	{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				| MINUSMONTH LPAREN expression RPAREN	{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				| PLUSYEAR LPAREN expression RPAREN		{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				| MINUSYEAR LPAREN expression RPAREN	{ $$ = new SpecialFunction($1.Value, $3); $$.Location = CurrentLocationSpan; }
				;

actuals			:					{ $$ = new ExpressionList(); $$.Location = CurrentLocationSpan; }
				| onePlusActuals	{ $$ = $1; $$.Location = CurrentLocationSpan; }
				;

onePlusActuals	: expression				{ $$ = new ExpressionList($1, new ExpressionList()); $$.Location = CurrentLocationSpan; }
				| expression COMMA actuals	{ $$ = new ExpressionList($1, $3); $$.Location = CurrentLocationSpan; }
				;

literal			: LITERAL_INT		{ $$ = new IntegerLiteral(Int32.Parse($1.Value.ToString().Replace(",",""))); $$.Location = CurrentLocationSpan; } 
				| LITERAL_REAL		{ $$ = new RealLiteral(Double.Parse($1.Value.ToString().Replace(",",""))); $$.Location = CurrentLocationSpan; }
				| LITERAL_STRING	{ $$ = new StringLiteral($1.Value); $$.Location = CurrentLocationSpan; }
				| TRUE				{ $$ = new BooleanLiteral(true); $$.Location = CurrentLocationSpan; }
				| FALSE				{ $$ = new BooleanLiteral(false); $$.Location = CurrentLocationSpan; }
				;

%%

public Prog SyntaxTreeRoot { get; set; }

public Parser(Scanner scan) : base(scan)
{
}
