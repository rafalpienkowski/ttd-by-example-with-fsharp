module ``Money tests``

open Currency
open Xunit
open FsUnit.Xunit

[<Fact>]
let ``Test multiplication`` () =
    let fiveDollars: Money = Money.Dollar(5)

    fiveDollars.Times(2) |> should equal (Money.Dollar(10))
    fiveDollars.Times(3) |> should equal (Money.Dollar(15))

[<Fact>]
let ``Test Franc multiplication`` () =
    let fiveFrancs: Money = Money.Franc(5)

    fiveFrancs.Times(2) |> should equal (Money.Franc(10))
    fiveFrancs.Times(3) |> should equal (Money.Franc(15))

[<Fact>]
let ``Test equality`` () =
    Money.Dollar(5) |> should equal (Money.Dollar(5))
    Money.Dollar(6) |> should not' (equal (Money.Dollar(5)))
    Money.Franc(5) |> should not' (equal (Money.Dollar(5)))

[<Fact>]
let ``Test currency`` () =
    Money.Dollar(1).Currency |> should equal "USD"
    Money.Franc(1).Currency |> should equal "CHF"
    
[<Fact>]
let ``Simple addition`` () =
    let fiveDollars = Money.Dollar(5)
    let sum = fiveDollars.Plus(fiveDollars)
    let bank = Bank()
    let reduced = bank.Reduce(sum, "USD")
    Money.Dollar(10) |> should equal reduced
    
[<Fact>]
let ``Plus returns sum`` () =
    let fiveDollars = Money.Dollar(5)
    let result = fiveDollars.Plus(fiveDollars)
    let sum = result :?> Sum
    fiveDollars |> should equal sum.Augend
    fiveDollars |> should equal sum.Added
    
[<Fact>]
let ``Reduce sum`` () =
    let sum = Sum(Money.Dollar(3), Money.Dollar(4))
    let bank = Bank()
    let result = bank.Reduce(sum, "USD")
    Money.Dollar(7) |> should equal result
    
[<Fact>]
let ``Reduce money`` () =
    let bank = Bank()
    let result = bank.Reduce(Money.Dollar(1), "USD")
    Money.Dollar(1) |> should equal result

[<Fact>]    
let ``Check identity rate`` () =
    Bank().Rate("USD", "USD") |> should equal 1
    
[<Fact>]
let ``Reduce money in different currencies`` () =
    let bank = Bank()
    bank.AddRate("CHF", "USD", 2)
    let result = bank.Reduce(Money.Franc(2), "USD")
    Money.Dollar(1) |> should equal result