module ``Money tests``

open Money
open Xunit
open FsUnit.Xunit

//Ideas
//$5 + 10 CHF = $10 if rate is 2:1
//$5 * 2 = $10 -> done
//Make "amount" private -> in progress
//Dollar side-effects -> done
//Money rounding?
//Equals() -> done
//HashCode() -> done
//Equal null -> done
//Equal object -> done


[<Fact>]
let ``Test multiplication`` () =
    let fiveDollars = Dollar(5)
    let tenDollars = fiveDollars.Times(2)
    let expectedTenDollars = Dollar(10)
    tenDollars |> should equal expectedTenDollars
    
    let fifteenDollars = fiveDollars.Times(3)
    let expectedFifteenDollars = Dollar(15)
    fifteenDollars |> should equal expectedFifteenDollars
    
[<Fact>]
let ``Test equality`` () =
    let fiveDollars = Dollar(5)
    Dollar(5) |> should equal fiveDollars
    Dollar(6) |> should not' (equal fiveDollars)
    
    