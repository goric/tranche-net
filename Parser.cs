// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, QUT 2005-2010
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.4.0
// Machine:  VOSTRO
// DateTime: 2/4/2012 3:44:32 PM
// UserName: Tim
// Input file <..\GeneratorInputFiles\tranche.y>

// options: no-lines gplex

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using LexicalAnalysis;
using AbstractSyntaxTree;
using AbstractSyntaxTree.InternalTypes;
using tc;

namespace SyntaxAnalysis
{
internal enum Tokens {
    error=1,EOF=2,SETTINGS=3,DEAL=4,COLLATERAL=5,COLLATERALITEM=6,
    SECURITIES=7,BOND=8,CREDITPAYMENTRULES=9,SIMULATION=10,RULES=11,RPAREN=12,
    LBRACE=13,RBRACE=14,RBRACKET=15,PBRACKET=16,DOT=17,DOTDOT=18,
    COMMA=19,IN=20,TINT=21,TREAL=22,SEMI=23,INTEREST=24,
    TSTRING=25,TBOOL=26,TVOID=27,TCHAR=28,TFILE=29,TSET=30,
    TLIST=31,FOR=32,IF=33,ELSE=34,CLASS=35,AUTOPROP=36,
    PRINCIPAL=37,RULE=38,NEW=39,RETURN=40,TRUE=41,FALSE=42,
    LITERAL_INT=43,LITERAL_REAL=44,LITERAL_STRING=45,LITERAL_CHAR=46,IDENTIFIER=47,ASSIGN=48,
    OR=49,AND=50,INCREMENT=51,DECREMENT=52,EXP=53,SMALLER=54,
    GREATER=55,SMEQ=56,GTEQ=57,EQ=58,NEQ=59,PLUS=60,
    MINUS=61,TIMES=62,DIVIDE=63,MOD=64,UMINUS=65,LPAREN=66,
    NOT=67,LBRACKET=68};

// Abstract base class for GPLEX scanners
internal abstract class ScanBase : AbstractScanner<SemanticValue,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

internal class Parser: ShiftReduceParser<SemanticValue, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliasses;
#pragma warning restore 649
  private static Rule[] rules = new Rule[49];
  private static State[] states = new State[107];
  private static string[] nonTerms = new string[] {
      "program", "statementList", "statement", "expression", "literal", "compExpression", 
      "onePlusActuals", "actuals", "boolListOpt", "settingsOpt", "dealOpt", "collateralSection", 
      "collListOpt", "securitySection", "simulationSection", "rulesListOpt", 
      "rulesList", "secListOpt", "creditRulesOpt", "interestRules", "principalRules", 
      "$accept", };

  static Parser() {
    states[0] = new State(new int[]{3,103,4,-3,5,-3},new int[]{-1,1,-10,3});
    states[1] = new State(new int[]{2,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{4,99,5,-5},new int[]{-11,4});
    states[4] = new State(new int[]{5,90},new int[]{-12,5});
    states[5] = new State(new int[]{7,66},new int[]{-14,6});
    states[6] = new State(new int[]{9,53,10,-16},new int[]{-19,7});
    states[7] = new State(new int[]{10,9},new int[]{-15,8});
    states[8] = new State(-2);
    states[9] = new State(new int[]{13,10});
    states[10] = new State(new int[]{11,13},new int[]{-17,11});
    states[11] = new State(new int[]{14,12});
    states[12] = new State(-25);
    states[13] = new State(new int[]{13,14});
    states[14] = new State(new int[]{38,17,14,-20},new int[]{-16,15});
    states[15] = new State(new int[]{14,16});
    states[16] = new State(-30);
    states[17] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-6,18,-4,27,-5,46});
    states[18] = new State(new int[]{38,17,49,21,50,24,58,-34,59,-34,56,-34,57,-34,55,-34,54,-34,14,-20},new int[]{-9,19,-16,20});
    states[19] = new State(-21);
    states[20] = new State(-22);
    states[21] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-6,22,-4,27,-5,46});
    states[22] = new State(new int[]{38,17,49,21,50,24,58,-34,59,-34,56,-34,57,-34,55,-34,54,-34,14,-20},new int[]{-9,23,-16,20});
    states[23] = new State(-23);
    states[24] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-6,25,-4,27,-5,46});
    states[25] = new State(new int[]{38,17,49,21,50,24,58,-34,59,-34,56,-34,57,-34,55,-34,54,-34,14,-20},new int[]{-9,26,-16,20});
    states[26] = new State(-24);
    states[27] = new State(new int[]{58,28,59,30,56,32,57,34,55,36,54,38});
    states[28] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,29,-5,46,-6,52});
    states[29] = new State(-35);
    states[30] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,31,-5,46,-6,52});
    states[31] = new State(-36);
    states[32] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,33,-5,46,-6,52});
    states[33] = new State(-37);
    states[34] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,35,-5,46,-6,52});
    states[35] = new State(-38);
    states[36] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,37,-5,46,-6,52});
    states[37] = new State(-39);
    states[38] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,39,-5,46,-6,52});
    states[39] = new State(-40);
    states[40] = new State(new int[]{66,41,58,-31,59,-31,56,-31,57,-31,55,-31,54,-31,38,-31,49,-31,50,-31,14,-31,12,-31,23,-31,47,-31,13,-31,33,-31,43,-31,44,-31,45,-31,41,-31,42,-31});
    states[41] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51,12,-41},new int[]{-8,42,-7,44,-4,45,-5,46,-6,52});
    states[42] = new State(new int[]{12,43});
    states[43] = new State(-32);
    states[44] = new State(-42);
    states[45] = new State(new int[]{58,28,59,30,56,32,57,34,55,36,54,38,12,-43});
    states[46] = new State(-33);
    states[47] = new State(-44);
    states[48] = new State(-45);
    states[49] = new State(-46);
    states[50] = new State(-47);
    states[51] = new State(-48);
    states[52] = new State(-34);
    states[53] = new State(new int[]{13,54});
    states[54] = new State(new int[]{24,62},new int[]{-20,55});
    states[55] = new State(new int[]{37,58},new int[]{-21,56});
    states[56] = new State(new int[]{14,57});
    states[57] = new State(-17);
    states[58] = new State(new int[]{13,59});
    states[59] = new State(new int[]{38,17,14,-20},new int[]{-16,60});
    states[60] = new State(new int[]{14,61});
    states[61] = new State(-19);
    states[62] = new State(new int[]{13,63});
    states[63] = new State(new int[]{38,17,14,-20},new int[]{-16,64});
    states[64] = new State(new int[]{14,65});
    states[65] = new State(-18);
    states[66] = new State(new int[]{13,67});
    states[67] = new State(new int[]{8,70,14,-28},new int[]{-18,68});
    states[68] = new State(new int[]{14,69});
    states[69] = new State(-15);
    states[70] = new State(new int[]{13,71});
    states[71] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,72,-3,75,-4,89,-5,46,-6,52});
    states[72] = new State(new int[]{14,73});
    states[73] = new State(new int[]{8,70,14,-28},new int[]{-18,74});
    states[74] = new State(-29);
    states[75] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,76,-3,75,-4,89,-5,46,-6,52});
    states[76] = new State(-8);
    states[77] = new State(-9);
    states[78] = new State(new int[]{48,79,66,41,58,-31,59,-31,56,-31,57,-31,55,-31,54,-31,23,-31,47,-31,13,-31,33,-31,43,-31,44,-31,45,-31,41,-31,42,-31,14,-31});
    states[79] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,80,-5,46,-6,52});
    states[80] = new State(new int[]{58,28,59,30,56,32,57,34,55,36,54,38,23,-10,47,-10,13,-10,33,-10,43,-10,44,-10,45,-10,41,-10,42,-10,14,-10});
    states[81] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,82,-3,75,-4,89,-5,46,-6,52});
    states[82] = new State(new int[]{14,83});
    states[83] = new State(-11);
    states[84] = new State(new int[]{66,85});
    states[85] = new State(new int[]{47,40,43,47,44,48,45,49,41,50,42,51},new int[]{-4,86,-5,46,-6,52});
    states[86] = new State(new int[]{12,87,58,28,59,30,56,32,57,34,55,36,54,38});
    states[87] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51},new int[]{-3,88,-4,89,-5,46,-6,52});
    states[88] = new State(-12);
    states[89] = new State(new int[]{58,28,59,30,56,32,57,34,55,36,54,38,23,-13,47,-13,13,-13,33,-13,43,-13,44,-13,45,-13,41,-13,42,-13,14,-13});
    states[90] = new State(new int[]{13,91});
    states[91] = new State(new int[]{6,94,14,-26},new int[]{-13,92});
    states[92] = new State(new int[]{14,93});
    states[93] = new State(-14);
    states[94] = new State(new int[]{13,95});
    states[95] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,96,-3,75,-4,89,-5,46,-6,52});
    states[96] = new State(new int[]{14,97});
    states[97] = new State(new int[]{6,94,14,-26},new int[]{-13,98});
    states[98] = new State(-27);
    states[99] = new State(new int[]{13,100});
    states[100] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,101,-3,75,-4,89,-5,46,-6,52});
    states[101] = new State(new int[]{14,102});
    states[102] = new State(-6);
    states[103] = new State(new int[]{13,104});
    states[104] = new State(new int[]{23,77,47,78,13,81,33,84,43,47,44,48,45,49,41,50,42,51,14,-7},new int[]{-2,105,-3,75,-4,89,-5,46,-6,52});
    states[105] = new State(new int[]{14,106});
    states[106] = new State(-4);

    rules[1] = new Rule(-22, new int[]{-1,2});
    rules[2] = new Rule(-1, new int[]{-10,-11,-12,-14,-19,-15});
    rules[3] = new Rule(-10, new int[]{});
    rules[4] = new Rule(-10, new int[]{3,13,-2,14});
    rules[5] = new Rule(-11, new int[]{});
    rules[6] = new Rule(-11, new int[]{4,13,-2,14});
    rules[7] = new Rule(-2, new int[]{});
    rules[8] = new Rule(-2, new int[]{-3,-2});
    rules[9] = new Rule(-3, new int[]{23});
    rules[10] = new Rule(-3, new int[]{47,48,-4});
    rules[11] = new Rule(-3, new int[]{13,-2,14});
    rules[12] = new Rule(-3, new int[]{33,66,-4,12,-3});
    rules[13] = new Rule(-3, new int[]{-4});
    rules[14] = new Rule(-12, new int[]{5,13,-13,14});
    rules[15] = new Rule(-14, new int[]{7,13,-18,14});
    rules[16] = new Rule(-19, new int[]{});
    rules[17] = new Rule(-19, new int[]{9,13,-20,-21,14});
    rules[18] = new Rule(-20, new int[]{24,13,-16,14});
    rules[19] = new Rule(-21, new int[]{37,13,-16,14});
    rules[20] = new Rule(-16, new int[]{});
    rules[21] = new Rule(-16, new int[]{38,-6,-9});
    rules[22] = new Rule(-9, new int[]{-16});
    rules[23] = new Rule(-9, new int[]{49,-6,-9});
    rules[24] = new Rule(-9, new int[]{50,-6,-9});
    rules[25] = new Rule(-15, new int[]{10,13,-17,14});
    rules[26] = new Rule(-13, new int[]{});
    rules[27] = new Rule(-13, new int[]{6,13,-2,14,-13});
    rules[28] = new Rule(-18, new int[]{});
    rules[29] = new Rule(-18, new int[]{8,13,-2,14,-18});
    rules[30] = new Rule(-17, new int[]{11,13,-16,14});
    rules[31] = new Rule(-4, new int[]{47});
    rules[32] = new Rule(-4, new int[]{47,66,-8,12});
    rules[33] = new Rule(-4, new int[]{-5});
    rules[34] = new Rule(-4, new int[]{-6});
    rules[35] = new Rule(-6, new int[]{-4,58,-4});
    rules[36] = new Rule(-6, new int[]{-4,59,-4});
    rules[37] = new Rule(-6, new int[]{-4,56,-4});
    rules[38] = new Rule(-6, new int[]{-4,57,-4});
    rules[39] = new Rule(-6, new int[]{-4,55,-4});
    rules[40] = new Rule(-6, new int[]{-4,54,-4});
    rules[41] = new Rule(-8, new int[]{});
    rules[42] = new Rule(-8, new int[]{-7});
    rules[43] = new Rule(-7, new int[]{-4});
    rules[44] = new Rule(-5, new int[]{43});
    rules[45] = new Rule(-5, new int[]{44});
    rules[46] = new Rule(-5, new int[]{45});
    rules[47] = new Rule(-5, new int[]{41});
    rules[48] = new Rule(-5, new int[]{42});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
    switch (action)
    {
      case 2: // program -> settingsOpt, dealOpt, collateralSection, securitySection, 
              //            creditRulesOpt, simulationSection
{ CurrentSemanticValue.Prog = new Prog(ValueStack[ValueStack.Depth-6].Settings, ValueStack[ValueStack.Depth-5].Deal, ValueStack[ValueStack.Depth-4].Collateral, ValueStack[ValueStack.Depth-3].Securities, ValueStack[ValueStack.Depth-2].CreditPaymentRules, ValueStack[ValueStack.Depth-1].Simulation); SyntaxTreeRoot = CurrentSemanticValue.Prog; CurrentSemanticValue.Prog.Location = CurrentLocationSpan; }
        break;
      case 3: // settingsOpt -> /* empty */
{ CurrentSemanticValue.Settings = new Settings(); CurrentSemanticValue.Settings.Location = CurrentLocationSpan; }
        break;
      case 4: // settingsOpt -> SETTINGS, LBRACE, statementList, RBRACE
{ CurrentSemanticValue.Settings = new Settings(ValueStack[ValueStack.Depth-2].StatementList);  }
        break;
      case 5: // dealOpt -> /* empty */
{ CurrentSemanticValue.Deal = new Deal(); CurrentSemanticValue.Deal.Location = CurrentLocationSpan; }
        break;
      case 6: // dealOpt -> DEAL, LBRACE, statementList, RBRACE
{ CurrentSemanticValue.Deal = new Deal(ValueStack[ValueStack.Depth-2].StatementList); CurrentSemanticValue.Deal.Location = CurrentLocationSpan; }
        break;
      case 7: // statementList -> /* empty */
{ CurrentSemanticValue.StatementList = new StatementList(); CurrentSemanticValue.StatementList.Location = CurrentLocationSpan; }
        break;
      case 8: // statementList -> statement, statementList
{ CurrentSemanticValue.StatementList = new StatementList(ValueStack[ValueStack.Depth-2].Statement, ValueStack[ValueStack.Depth-1].StatementList); CurrentSemanticValue.StatementList.Location = CurrentLocationSpan; }
        break;
      case 10: // statement -> IDENTIFIER, ASSIGN, expression
{ CurrentSemanticValue.Statement = new StatementVariable(CurrentLocationSpan, ValueStack[ValueStack.Depth-3].Token.Value, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Statement.Location = CurrentLocationSpan; }
        break;
      case 11: // statement -> LBRACE, statementList, RBRACE
{ CurrentSemanticValue.Statement = new Block(ValueStack[ValueStack.Depth-2].StatementList); CurrentSemanticValue.Statement.Location = CurrentLocationSpan; }
        break;
      case 12: // statement -> IF, LPAREN, expression, RPAREN, statement
{ CurrentSemanticValue.Statement = new IfThen(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Statement); CurrentSemanticValue.Statement.Location = CurrentLocationSpan; }
        break;
      case 13: // statement -> expression
{ CurrentSemanticValue.Statement = new StatementExpression(ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Statement.Location = CurrentLocationSpan; }
        break;
      case 14: // collateralSection -> COLLATERAL, LBRACE, collListOpt, RBRACE
{ CurrentSemanticValue.Collateral = new Collateral(ValueStack[ValueStack.Depth-2].CollateralItem); CurrentSemanticValue.Collateral.Location = CurrentLocationSpan; }
        break;
      case 15: // securitySection -> SECURITIES, LBRACE, secListOpt, RBRACE
{ CurrentSemanticValue.Securities = new Securities(ValueStack[ValueStack.Depth-2].Bond); CurrentSemanticValue.Securities.Location = CurrentLocationSpan; }
        break;
      case 16: // creditRulesOpt -> /* empty */
{ CurrentSemanticValue.CreditPaymentRules = new CreditPaymentRules(); CurrentSemanticValue.CreditPaymentRules.Location = CurrentLocationSpan; }
        break;
      case 17: // creditRulesOpt -> CREDITPAYMENTRULES, LBRACE, interestRules, principalRules, 
               //                   RBRACE
{ CurrentSemanticValue.CreditPaymentRules = new CreditPaymentRules(ValueStack[ValueStack.Depth-3].InterestRules,ValueStack[ValueStack.Depth-2].PrincipalRules); CurrentSemanticValue.CreditPaymentRules.Location = CurrentLocationSpan; }
        break;
      case 18: // interestRules -> INTEREST, LBRACE, rulesListOpt, RBRACE
{ CurrentSemanticValue.InterestRules = new InterestRules(ValueStack[ValueStack.Depth-2].InternalRuleList); CurrentSemanticValue.InterestRules.Location = CurrentLocationSpan; }
        break;
      case 19: // principalRules -> PRINCIPAL, LBRACE, rulesListOpt, RBRACE
{ CurrentSemanticValue.PrincipalRules =  new PrincipalRules(ValueStack[ValueStack.Depth-2].InternalRuleList); CurrentSemanticValue.PrincipalRules.Location = CurrentLocationSpan; }
        break;
      case 20: // rulesListOpt -> /* empty */
{ CurrentSemanticValue.InternalRuleList = new InternalRuleList(); CurrentSemanticValue.InternalRuleList.Location = CurrentLocationSpan; }
        break;
      case 21: // rulesListOpt -> RULE, compExpression, boolListOpt
{ CurrentSemanticValue.InternalRuleList = new InternalRuleList(ValueStack[ValueStack.Depth-2].Expression, ValueStack[ValueStack.Depth-1].ExpressionList); CurrentSemanticValue.InternalRuleList.Location = CurrentLocationSpan; }
        break;
      case 22: // boolListOpt -> rulesListOpt
{ CurrentSemanticValue.ExpressionList = ValueStack[ValueStack.Depth-1].InternalRuleList; CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan; }
        break;
      case 23: // boolListOpt -> OR, compExpression, boolListOpt
{ CurrentSemanticValue.ExpressionList = new InternalRuleListOr(ValueStack[ValueStack.Depth-2].Expression, ValueStack[ValueStack.Depth-1].ExpressionList); CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan;  }
        break;
      case 24: // boolListOpt -> AND, compExpression, boolListOpt
{ CurrentSemanticValue.ExpressionList = new InternalRuleListAnd(ValueStack[ValueStack.Depth-2].Expression, ValueStack[ValueStack.Depth-1].ExpressionList); CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan; }
        break;
      case 25: // simulationSection -> SIMULATION, LBRACE, rulesList, RBRACE
{ CurrentSemanticValue.Simulation = new Simulation(ValueStack[ValueStack.Depth-2].Rules); CurrentSemanticValue.Simulation.Location = CurrentLocationSpan; }
        break;
      case 26: // collListOpt -> /* empty */
{ CurrentSemanticValue.CollateralItem = new CollateralItem(); CurrentSemanticValue.CollateralItem.Location = CurrentLocationSpan; }
        break;
      case 27: // collListOpt -> COLLATERALITEM, LBRACE, statementList, RBRACE, collListOpt
{ CurrentSemanticValue.CollateralItem = new CollateralItem(ValueStack[ValueStack.Depth-3].StatementList, ValueStack[ValueStack.Depth-1].CollateralItem); CurrentSemanticValue.CollateralItem.Location = CurrentLocationSpan; }
        break;
      case 28: // secListOpt -> /* empty */
{ CurrentSemanticValue.Bond = new Bond(); CurrentSemanticValue.Bond.Location = CurrentLocationSpan; }
        break;
      case 29: // secListOpt -> BOND, LBRACE, statementList, RBRACE, secListOpt
{ CurrentSemanticValue.Bond = new Bond(ValueStack[ValueStack.Depth-3].StatementList, ValueStack[ValueStack.Depth-1].Bond); CurrentSemanticValue.Bond.Location = CurrentLocationSpan; }
        break;
      case 30: // rulesList -> RULES, LBRACE, rulesListOpt, RBRACE
{ CurrentSemanticValue.Rules = new Rules(ValueStack[ValueStack.Depth-2].InternalRuleList); CurrentSemanticValue.Rules.Location = CurrentLocationSpan; }
        break;
      case 31: // expression -> IDENTIFIER
{ CurrentSemanticValue.Expression = new Identifier(CurrentLocationSpan, ValueStack[ValueStack.Depth-1].Token.Value); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 32: // expression -> IDENTIFIER, LPAREN, actuals, RPAREN
{ CurrentSemanticValue.Expression = new Invoke(ValueStack[ValueStack.Depth-4].Token.Value, ValueStack[ValueStack.Depth-2].ExpressionList); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 33: // expression -> literal
{ CurrentSemanticValue.Expression = ValueStack[ValueStack.Depth-1].Expression; CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 34: // expression -> compExpression
{ CurrentSemanticValue.Expression = ValueStack[ValueStack.Depth-1].Expression; CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 35: // compExpression -> expression, EQ, expression
{ CurrentSemanticValue.Expression = new Equal(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 36: // compExpression -> expression, NEQ, expression
{ CurrentSemanticValue.Expression = new NotEqual(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 37: // compExpression -> expression, SMEQ, expression
{ CurrentSemanticValue.Expression = new SmallerEqual(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 38: // compExpression -> expression, GTEQ, expression
{ CurrentSemanticValue.Expression = new GreaterEqual(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 39: // compExpression -> expression, GREATER, expression
{ CurrentSemanticValue.Expression = new Greater(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 40: // compExpression -> expression, SMALLER, expression
{ CurrentSemanticValue.Expression = new Smaller(ValueStack[ValueStack.Depth-3].Expression, ValueStack[ValueStack.Depth-1].Expression); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 41: // actuals -> /* empty */
{ CurrentSemanticValue.ExpressionList = new ExpressionList(); CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan; }
        break;
      case 42: // actuals -> onePlusActuals
{ CurrentSemanticValue.ExpressionList = ValueStack[ValueStack.Depth-1].ExpressionList; CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan; }
        break;
      case 43: // onePlusActuals -> expression
{ CurrentSemanticValue.ExpressionList = new ExpressionList(ValueStack[ValueStack.Depth-1].Expression, new ExpressionList()); CurrentSemanticValue.ExpressionList.Location = CurrentLocationSpan; }
        break;
      case 44: // literal -> LITERAL_INT
{ CurrentSemanticValue.Expression = new IntegerLiteral(Int32.Parse(ValueStack[ValueStack.Depth-1].Token.Value.ToString().Replace(",",""))); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 45: // literal -> LITERAL_REAL
{ CurrentSemanticValue.Expression = new RealLiteral(Double.Parse(ValueStack[ValueStack.Depth-1].Token.Value.ToString().Replace(",",""))); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 46: // literal -> LITERAL_STRING
{ CurrentSemanticValue.Expression = new StringLiteral(ValueStack[ValueStack.Depth-1].Token.Value); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 47: // literal -> TRUE
{ CurrentSemanticValue.Expression = new BooleanLiteral(true); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
      case 48: // literal -> FALSE
{ CurrentSemanticValue.Expression = new BooleanLiteral(false); CurrentSemanticValue.Expression.Location = CurrentLocationSpan; }
        break;
    }
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliasses != null && aliasses.ContainsKey(terminal))
        return aliasses[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }


public Prog SyntaxTreeRoot { get; set; }

public Parser(Scanner scan) : base(scan)
{
}
}
}
