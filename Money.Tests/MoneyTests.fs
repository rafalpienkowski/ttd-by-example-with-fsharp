module ``Money should``

open System.Collections.Generic
open Money
open Xunit
open FsUnit.Xunit

[<Fact>]
let ``multiply dollars`` () =
    let fiveDollars: Money = Money.Dollar(5)

    fiveDollars.Times(2) |> should equal (Money.Dollar(10))
    fiveDollars.Times(3) |> should equal (Money.Dollar(15))

[<Fact>]
let ``multiply francs`` () =
    let fiveFrancs: Money = Money.Franc(5)

    fiveFrancs.Times(2) |> should equal (Money.Franc(10))
    fiveFrancs.Times(3) |> should equal (Money.Franc(15))

[<Fact>]
let ``be able to compare between`` () =
    Money.Dollar(5) |> should equal (Money.Dollar(5))
    Money.Dollar(6) |> should not' (equal (Money.Dollar(5)))
    Money.Franc(5) |> should not' (equal (Money.Dollar(5)))

[<Fact>]
let ``contain currency information`` () =
    Money.Dollar(1).Currency |> should equal "USD"
    Money.Franc(1).Currency |> should equal "CHF"
    
[<Fact>]
let ``support addition of money in the same currency`` () =
    let fiveDollars = Money.Dollar(5)
    let sum = fiveDollars.Plus(fiveDollars)
    let bank = Bank()
    let reduced = bank.Reduce(sum, "USD")
    Money.Dollar(10) |> should equal reduced
    
[<Fact>]
let ``support subtraction of money in the same currency`` () =
    let tenDollars = Money.Dollar(10)
    let fourDollars = Money.Dollar(4)
    let diff = tenDollars.Minus(fourDollars)
    let bank = Bank()
    let reduced = bank.Reduce(diff, "USD")
    Money.Dollar(6) |> should equal reduced
    
[<Fact>]
let ``return sum of addition operation`` () =
    let fiveDollars = Money.Dollar(5)
    let result = fiveDollars.Plus(fiveDollars)
    let sum = result :?> Sum
    fiveDollars |> should equal sum.Augend
    fiveDollars |> should equal sum.Addend
    
[<Fact>]
let ``reduce sum`` () =
    let sum = Sum(Money.Dollar(3), Money.Dollar(4))
    let bank = Bank()
    let result = bank.Reduce(sum, "USD")
    Money.Dollar(7) |> should equal result
    
[<Fact>]
let ``reduce money`` () =
    let bank = Bank()
    let result = bank.Reduce(Money.Dollar(1), "USD")
    Money.Dollar(1) |> should equal result

[<Fact>]    
let ``check identity rate`` () =
    Bank().Rate("USD", "USD") |> should equal 1
    
[<Fact>]
let ``reduce money in different currencies`` () =
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let result = bank.Reduce(Money.Franc(2), "USD")
    Money.Dollar(1) |> should equal result
    
[<Fact>]
let ``support addition of mixed currencies`` () =
    let fiveDollars = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let result = bank.Reduce(fiveDollars.Plus(tenFrancs), "USD")
    Money.Dollar(10) |> should equal result
 
[<Fact>]
let ``support subtraction of mixed currencies`` () =
    let tenDollars = Money.Dollar(10)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let result = bank.Reduce(tenDollars.Minus(tenFrancs), "USD")
    Money.Dollar(5) |> should equal result
    
[<Fact>]
let ``support sum with plus money operation`` () =
    let fiveDollars = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let sum = Sum(fiveDollars, tenFrancs).Plus(fiveDollars)
    let result = bank.Reduce(sum, "USD")
    Money.Dollar(15) |> should equal result

[<Fact>]
let ``support sum with times operation`` () =
    let fiveDollars = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let sum = Sum(fiveDollars, tenFrancs).Times(2)
    let result = bank.Reduce(sum, "USD")
    Money.Dollar(20) |> should equal result

[<Fact>]
let ``block reduce operation when bank doesn't know the rate`` () =
    let fiveDollars = Money.Dollar(5)
    let tenFrancs = Money.Franc(10)
    let bank = Bank()
    let sum = Sum(fiveDollars, tenFrancs)
    (fun() -> bank.Reduce(sum, "USD") |> ignore) |> should throw typeof<KeyNotFoundException>