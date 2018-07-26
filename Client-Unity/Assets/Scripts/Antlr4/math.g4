grammar math;
prog:    (expr|equ);

//-------------------------Characters------------------------------//
WS    :    [ \t\r\n]+ -> channel(HIDDEN);    //Whitespace declaration
fragment LETTER    :    ('a'..'z' | 'A'..'Z');
fragment DIGIT    :    ('0'..'9');
ID    :    (LETTER)+;
NUM    :    (INT|DOUBLE);
INT    :    (DIGIT)+;
DOUBLE    :    INT '.'(DIGIT)*;


// no sirve la a


term
	:    NUM    //numbers
	|	 constant
//	|	 matrix
	|	 trig
	|	 logarithm
	|    ID    //variables
	|    '(' expr ')'    //parentheses
	;

//Implicit multiplication
factor
	:	term
	|	<assoc=right> term '^' factor
	|	term factor
	;

//Unary minus/plus
expr
	:	factor
	|	('+' | '-') expr    //unary plus/minus
	|	expr '/' expr    //division
	|	expr '*' expr    //explicit multiplication
	|	expr ('+' | '-') expr    //addition/subtraction
	;

//Ecuation
equ
    :    expr '=' expr
    ;

// ADDED between 10/11/17 and 12/11/17

//pi constant
pi
	:	'(pi)'
	;

//euler constant, using parentheses to diferintiate between sen 
e
	:	'(' 'e' ')'
	;

//constant
constant
	:	pi
	|	e
	;

/**
remember to uncomment line 17 : term matrix
//Matrix functions
matrix
	:	matrixdec
	|	trace
	|	determinant
	|	inverse
	|	evector
	|	evalues
	|	transpose
	|	rank
	|	nullity
	|	adjacent
	|	diagonal
	|	li
	;
	
//Matrix declaration
matrixdec
	: 	LETTERCAPS '[' INT 'x' INT ']'
	;

//Matrix trace
trace
	:	't' 'r' '[' matrix ']'
	;
	
//Matrix determinant
determinant
	:	'd' 'e' 't' '[' matrix ']'
	;

//Matrix inverse
inverse
	:	'I' 'n' 'v' '[' matrix ']'
	;

//Matrix eigenvector
evector
	:	'E' 'v' 'e' 'c' 't' 'o' 'r' '[' matrix ']'
	;
	
//Matrix eigenvalues
evalues
	:	'E' 'v' 'a' 'l' 'o' 'r' '[' matrix ']'
	;
	
//Matrix transpose
transpose
	:	'T' 'r' 'a' '[' matrix ']'
	;
	
//Matrix rank
rank
	:	'r' 'a' 'n' 'k' '[' matrix ']'
	;
	
//Matrix nullity
nullity
	:	'n' 'u' 'l' '[' matrix ']'
	;

//Adjacent matrix
adjacent
	:	'A' 'd' 'j' '[' matrix ']'
	;
	
//Diagonal Matrix
diagonal
	:	'D' '[' matrix ']'
	;
	
//Matrix Linear independence
li
	:	'L' 'I' '[' matrix ']'
	|	',' '[' matrix ']'
	|	',' '[' matrix ']'
	|	',' '[' matrix ']'
	;
*/
	
//logarithm functions
logarithm
	:	log
	|	ln
	;
	
//logarithm with base
log
	:	'log' INT '(' expr ')' //expressions?
	|	'log' '(' expr ')'
	|	'log' '(' 'e' ')'
	;

//Natural logarithm
ln
	:	'ln' '(' expr ')'
	;

//trigonometric functions
trig
	:	sin
	|	sinh
	|	arcsin
	|	arcsinh
	|	cos
	|	cosh
	|	arccos
	|	arccosh
	|	tan
	|	tanh
	|	arctan
	|	arctanh
	;
	
//sine
sin
	:	'sen' '(' expr ')'
	;

//cosine
cos
	:	'cos' '(' expr ')'
	;

//tangent
tan
	:	'tan' '(' expr ')'
	;

//hyperbolic sine
sinh
	:	'senh' '(' expr ')'
	;

//hyperbolic cosine
cosh
	:	'cosh' '(' expr ')'
	;
	
//hyperbolic tangent
tanh
	:	'tanh' '(' expr ')'
	;

//inverse sine
arcsin
	:	'arcsen' '(' expr ')'
	;

	
//inverse cosine
arccos
	:	'arccos' '(' expr ')'
	;
	
//inverse tangent
arctan
	:	'arctan' '(' expr ')'
	;

//hyperbolic inverse sine
arcsinh
	:	'arcsenh' '(' expr ')'
	;

//hyperbolic inverse cosine
arccosh
	:	'arccosh' '(' expr ')'
	;
	
//hyperbolic inverse tangent
arctanh
	:	'arctanh' '(' expr ')'
	;
