New to tranche? See the [Getting Started](AboutTranche.md) wiki.

![https://tranche-net.googlecode.com/svn/trunk/Docs/Images/tranche-med.png](https://tranche-net.googlecode.com/svn/trunk/Docs/Images/tranche-med.png)


---


The tranche programming language is a domain-specific language developed to aid in the modeling of structured financial products and the forecasting of their cash flows. It is notably (and by design) non-Turing complete, and is named after the eponymous pieces of structured finance transactions.

tranche.net is the initial compiler built to implement of the tranche language. It it written in C# 4.0 using the [gplex](http://gplex.codeplex.com/) and [gppg](http://gppg.codeplex.com/) lexer- and parser-generator tools, and compiles to the Common Intermediate Language (CIL) for use on any implementation of the Common Language Runtime (CLR).

## Motivation ##
Given the impact this one class of investments at least allegedly had on the world economy in the financial downturn of 2007-2009, we naturally seek to determine how we got into such a situation in the first place, and how it can be avoided in the future. One general (though over-simplified) explanation is that there was a lack of understanding of these investments by those trading them. Such lack of understanding on a global scale would seem to indicate some inherent complexity in the transactions. We are then led to ask how complex computational models for these products actually are, and seek to develop a domain-specific language focused on these models, with the goals of easing implementation and maintenance while reducing code complexity and decreasing possibility for error.