cd ..\Dependencies

gplex /out:..\Scanner.cs ..\GeneratorInputFiles\tranche.lex

gppg /gplex /no-lines ..\GeneratorInputFiles\tranche.y > ..\Parser.cs

cd ..\GeneratorInputFiles

pause