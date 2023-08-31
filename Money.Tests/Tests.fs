module ``Money tests``

open Money
open Xunit
open FsUnit.Xunit

//Ideas
//$5 + 10 CHF = $10 if rate is 2:1
//$5 * 2 = $10 -> done
//Make "amount" private
//Dollar side-effects -> done
//Money rounding?


[<Fact>]
let ``Test multiplication`` () =
    let fiveDollar = Dollar(5)
    let tenDollars = fiveDollar.Times(2)
    tenDollars.Amount |> should equal 10
    
    let fifteenDollars = fiveDollar.Times(3)
    fifteenDollars.Amount |> should equal 15
    
    