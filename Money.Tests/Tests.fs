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
//Equals() -> done
//HashCode() -> done
//Equal null -> done
//Equal object -> done


[<Fact>]
let ``Test multiplication`` () =
    let fiveDollars = Dollar(5)
    let tenDollars = fiveDollars.Times(2)
    tenDollars.Amount |> should equal 10
    
    let fifteenDollars = fiveDollars.Times(3)
    fifteenDollars.Amount |> should equal 15
    
[<Fact>]
let ``Test equality`` () =
    let fiveDollars = Dollar(5)
    Dollar(5) |> should equal fiveDollars
    Dollar(6) |> should not' (equal fiveDollars)
    
    