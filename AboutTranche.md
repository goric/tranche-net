# Getting Started With Tranche #

Tranche is a DSL for modeling structured finance products. It is not Turing complete, and may initially seem restrictive to programmers familiar with general purpose languages.

## Hello World ##
In keping with programming language tradition, here is the smallest valid Tranche program which prints `"Hello, World!"`:
```
    .Collateral {}
    .Securities {}
    .CreditPaymentRules{}
    .Simulation
    {
        print("Hello, World!");
    }
```

## Program Sections ##
A tranche program is split into various sections, which act as logical separations of concerns. Most sections provide data storage only, and do not allow for arbitrary code execution. Sections are denoted by a period followed by the section name, with the section's contents wrapped in a set of curly braces. Every valid Tranche program must contain the following sections:
  * `.Collateral`
  * `.Securities`
  * `.CreditPaymentRules`
  * `.Simulation`

Optional top-level sections include:
  * `.Settings`
  * `.Deal`

In addition to top-level sections, there are available sub-sections as well. These are:
  * `.CollateralItem`, available inside `.Collateral`
  * `.Bond`, available inside `.Securities`
  * `.Interest` and `.Principal`, available inside `.CreditPaymentRules`

The only sections of these which allow for statements other than simple assignments are `.CreditPaymentRules` and its sub-sections, which allow for logical statements involving `Rule`s and `.Simulation`, which allows for any arbitrary code.

## Primitive Types ##
Tranche is a statically-typed language, but the majority of the type system normally goes unseen in the code, relying on compiler inference instead. The primitive types in Tranche are `real`, `integer`, `string`, `Boolean`, and `date`, which should all be self-explanatory. The compiler will determine what values should be what type, unless a type qualifier is used.

## Assignments ##
Any primitive or complex type can be assigned to a variable with the assignment operator `:`. Note that there is no type specification needed.
```
    pi: 3.14159    /* real */
    value: 12      /* integer */
    isOpen: true   /* Boolean */
    description: "This is Tranche..." /* string */
```

Explicit type qualification is allowed, especially for use with the `date` type. There is no syntax for declaring a date literal other than placing a type qualifier on a string:
```
    today: (date)"4/14/2012"
```

## Arithmetic ##
Tranche supports the expected arithmetic operations +, -, `*` ,/ ,% (modulo), and ^ (exponentiation) between numeric types (`real` and `integer` only; `Boolean` is non-numeric). The type of the result of a cross-type arithmetic operation will be the higher of the two types (_e.g._ `real` and `integer` yields `real`). The operator `*``*` is an alias for exponentiation.
When a `+` is used on two strings or a string and a numeric type, the result will be a string concatenation, not an arithmetic operation, even if the string contains a numeric value.
```
    a: (4 + 8) * 3     /* 36 */
    b: a / 6           /* 6 */
    c: a % 11          /* 3 */
    d: c ** 3          /* 27 */
```

## Comparisons ##
Typical logical comparisons are implemented and return a `Boolean` type. The operators available are <, <=, =, !=, >, and >=. The equality operator (=) should not be confused with the assignment operator (:).

## Complex Types ##
There are complex types available as well. The `Set` is similar to ordered lists or arrays in other languages. It offers concatenation and removal operations, as well as set-wise (vector) arithmetic operations:
```
    reals: [1.2 3.45 6.789] /* [1.2 3.45 6.789] */
    appended: reals :: 5.3  /* yields [1.2 3.45 6.789 5.2] */
    removed: appended - 2   /* remove at index 2 (0-based), yields [1.2 3.45 5.3] */
    double: removed @* 2    /* vector operation, yields [2.4 6.9 10.6] */
```

The `TimeSeries` type is based on `Set`, namely a grouping of two `Sets`. There are two representations, one being combination of a set of `dates` and a set of numeric types and the other being two sets of numeric types. The former is the traditional intuitive definition of a time series. When used in the latter context, it is assumed that the sets are two related time series of the same periodicity, but the actual start and end times are not important.
```
    dates: [(date)"1/1/2008" (date)"1/1/2009" (date)"1/1/2010" (date)"1/1/2011" (date)"1/1/2012"]
    payments: [300 300 300 300 300]
    interest: [1.5 1.5 1.5 1.5 1.5]
    seriesOne: <dates, payments>
    seriesTwo: <payments, interest>
```

The `Rule` is a hybrid of a `Boolean` statement and an action to be performed if that statement evaluates to true given some input. This type was introduced for use with the `filter` operation for manipulating `Set`s, described in more detail below. This type has no valid meaning when not acting on some input parameter.
```
    rule: |x > 15, x: x+5|
    rule2: |y = "test", y: "passed"|
```

There are no user-defined types that can be explicitly created. All created types are specializations of built-in types. The way to create a new type would be by creating a set from a loop, and returning custom properties for that set. In the second looping example below, `y` is a custom type, specifically a set of types with one attribute, an integer named `value`. Created types should be seen as similar to a `tuple` or `struct` in other languages. No methods can be defined for custom types, although all methods that can be used with a `Set` are available.

## Looping ##
There are no unbounded loops available in tranche. The only type of loop which can be written is a for loop where both upper and lower bounds are given. Loops can traverse forwards (increment) or backwards (decrement) using the `upto` and `downto` keywords. Loops can be freestanding, or can return values to be assigned to a set. If the `with` statement is omitted, the default incrementer or decrementer for the type of the looping variable will be used.
```
    /* print 0 to 9 */
    [x:0 upto 10](
        println(x)
    )

    /* assign a set [10 8 6 4 2] */
    y : [x:10 downto 0 with x: x-2](
        {
            value: x
        })

    /* dates can be looped as well using the special increment/decrement built-in functions */
    first: (date)"1/1/2012"
    last: (date)"1/31/2012"
    [y:first upto last with y +Day(1)](
        println(y)
    )
```

## Set Manipulation ##
There are two built in operations available for `Set`s; filtering and aggregation. The `aggregate` function takes as input the `Set` on which to act and an aggregation function to apply. The built in aggregation functions are `min`, `max`, `product`, `average`, and `sum`. Each of these takes as a parameter the name of the attribute on the type of the `Set` which should be aggregated unless it is of a primitive type, in which case the parameter should be omitted. Alternatively, another function may be supplied in place of these aggregation functions, provided it takes a set as its first parameter and returns a single value.
```
    odds: [1 3 5 7 9 11]
    minimum: aggregate odds min   /* 1 */
    maximum: aggregate odds max   /* 11 */
    summation: aggregate odds sum /* 36 */
    prod: aggregate odds product  /* 10395 */
    avg: aggregate odds average   /* 6 */
```

The `filter` function returns a possibly empty subset of the input `Set` based on some criteria. It can be used in two different contexts. When the members of the set being filtered can be evaluated to a Boolean type (as is the case with the Rule type), the keywords first and last are available. These must be used in conjunction with another input operator, which acts as the parameter on which the rules will evaluate. These predefined operators will return the first or last element of the set which evaluates to true, respectively. When the members of the set are anything else, the second parameter to filter is a function which takes as input an element of the set and returns a Boolean. In this case, the function will return a set containing all of the elements in the original set for which the input function returns true.
```
    rules: [|y>25,print(“y>25”)| |y<9,print(“y<9”)| |y>=13,print(“y>=13”)| |y!=12,print(“y!=12”)|]
    x: 17
    match: filter rules x first  /* returns third item in the set */
    match()                      /* prints y>=13 */

    numbers: [1 2 3 4 5 6 7 8 9]
    odds: filter numbers n n%2 != 0  /* [1 3 5 7 9] */
    evens: filter numbers n n%2 = 0  /* [2 4 6 8] */
```

## Simulation Functions ##

Several functions are provided for ease when performing very simple simulations of deals.
The first is `SimulateCollateral()`. In order to use this function,
each CollateralItem must have defined a date attribute named MaturityDate, a real attribute named Coupon, and an integer attribute named PaymentFrequency. If these are not defined, the compiler will throw an error. This also assumes all of the collateral items are fixed rate agreements, so if any of them rely on floating interest rates, they will need to be considered separately. When this function is called, it will determine an amortization schedule for each
element present in the Collateral section. It will aggregate all of them together into a single schedule representing the entire cash flow into the deal. It is possible to provide a parameter to this function in the form of a Rule statement or a logical concatenation of Rules. In this case, any CollateralItem matching the criteria of the rules will have an amortization schedule computed, and the others will be ignored.

The second is `SimulateBonds()`. This function must be given a parameter of an amortization schedule representing the cash flows into
the deal. It will then apply the rules given in the Interest and Principal sections within the CreditPaymentRules section in order to determine a cash flow schedule for each bond in the Securities section (assuming there is information in the CreditPaymentRules section on how to allocate payments to it).

There are also two more generic financial calculation functions. The `payment` function for determining the value of a periodic payment for a loan, and the `discount` for determining the present value of a stream of cash flows. `Payment` takes as parameters the current balance of the loan, its periodic interest rate, and the number of periods which are left until the loan is paid off, and returns the total amount of the payment which is to be made at each period. `Discount` takes a `TimeSeries` of payments and a discount rate and returns a present value.

## A Full Basic Example ##

```
.Deal
{
    Name: "BACM 2005-3"
    CutoffDate: (date)"7/1/2005"
}

.Collateral
{
    .CollateralItem
    {
        ID: 0
        PropertyName: "Woolworth Building"
        OriginalBalance: 200,000,000
        CurrentBalance: 200,000,000 
        InterestRate: 0.05191
        Maturity: (date)"6/1/2015"
        PropertyType: "Office"
        City: "New York"
        State: "NY" 
    }
    .CollateralItem
    {
        ID: 1
        PropertyName: "Ridgedale Center"
        OriginalBalance: 168,632,029 
        CurrentBalance: 168,632,029 
        InterestRate: 0.04861
        Maturity: (date)"9/30/2016"
        PropertyType: "Retail"
        City: "Minnetonka"
        State: "MN"
    }
    .CollateralItem
    {
        ID: 2
        PropertyName: "Marley Station"
        OriginalBalance: 114,400,000 
        CurrentBalance: 100,000,000 
        InterestRate: 0.04891
        Maturity: (date)"7/1/12"
        PropertyType: "Retail"
        City: "Glen Burnie"
        State: "MD"
    }
    .CollateralItem
    {
        ID: 3
        PropertyName: "Fiesta Mall"
        OriginalBalance: 84,000,000
        CurrentBalance: 78,000,000
        InterestRate: 0.04875
        Maturity: (date)"1/1/2015"
        PropertyType: "Retail"
        City: "Mesa"
        State: "AZ"
    }
}

.Securities
{
    .Bond
    {
        ID: 0
        CUSIP: "05947UR42"
        Class: "A-2"
        OriginalBalance: 505,650,000 
        CurrentBalance: 125,483,169 
    }
    .Bond
    {
        ID: 1
        CUSIP: "05947UR59"
        Class: "A-3"
        OriginalBalance: 279,216,000 
        CurrentBalance: 279,216,000 
    }
    .Bond
    {
        ID: 2
        CUSIP: "05947US41"
        Class: "X"
        OriginalBalance: 24,312,000
        CurrentBalance: 24,312,000
    }
}

.CreditPaymentRules
{
    .Interest
    {
        -> Class="A-2" or Class="A-3"
        -> Class="X"
    }
    .Principal
    {
        -> CUSIP="05947UR42"
        -> Class="A-3"
    }
}

.Simulation
{
    collateralCashflows: SimulateCollateral()
    bondCashflows: SimulateBonds(collateralCashflows)

    bondA2: filter bondCashflows x x.Class="A-2"
    price: aggregate bondA2 discount(12)
    println("Bond A-2 has price " + price)
}

```