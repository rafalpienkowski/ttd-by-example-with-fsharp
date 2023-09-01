module ``Money tests``

open Money
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
//Dollar/Franc duplication -> in progress
//Common equals
//Common times

[<Fact>]
let ``Test multiplication`` () =
    let fiveDollars = Dollar(5)
    
    fiveDollars.Times(2) |> should equal (Dollar(10))
    fiveDollars.Times(3) |> should equal (Dollar(15))

[<Fact>]
let ``Test Franc multiplication`` () =
    let fiveFrancs = Franc(5)
    
    fiveFrancs.Times(2) |> should equal (Franc(10))
    fiveFrancs.Times(3) |> should equal (Franc(15))
    
[<Fact>]
let ``Test equality`` () =
    Dollar(5) |> should equal (Dollar(5))
    Dollar(6) |> should not' (equal (Dollar(5)))
    
    Franc(5) |> should equal (Franc(5))
    Franc(6) |> should not' (equal (Franc(5)))
    
    