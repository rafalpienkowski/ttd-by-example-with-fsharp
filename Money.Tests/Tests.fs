module ``Money tests``

open Money
open Xunit
open FsUnit.Xunit

//Ideas
//$5 + 10 CHF = $10 if rate is 2:1
//$5 * 2 = $10 -> done
//Make "amount" private
//Dollar side-effects?
//Money rounding?


[<Fact>]
let ``Test multiplication`` () =
    let fiveDollar = Dollar(5)
    fiveDollar.Times(2)
    fiveDollar.Amount |> should equal 10