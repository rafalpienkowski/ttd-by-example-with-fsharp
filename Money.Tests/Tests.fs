module ``Money tests``

open Currency
open Xunit
open FsUnit.Xunit

//Ideas
//$5 + 10 CHF = $10 if rate is 2:1
//$5 * 2 = $10 -> done
//Make "amount" private -> done
//Dollar side-effects -> done
//Money rounding?
//Equals() -> done
//HashCode() -> done
//Equal null -> done
//Equal object -> done
//5 CSF * 2 = 20 CHF -> done
//Dollar/Franc duplication -> done
//Common equals -> done
//Common times
//Compare Francs with Dollars -> done

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

    Money.Franc(5) |> should equal (Money.Franc(5))
    Money.Franc(6) |> should not' (equal (Money.Franc(5)))

    Money.Franc(5) |> should not' (equal (Money.Dollar(5)))
