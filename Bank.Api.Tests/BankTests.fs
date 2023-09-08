module ``Money should``

open System.Text.Json
open Xunit
open System.Net
open System.Net.Http
open FsUnit.Xunit
open Money.Api.Tests.TestFunctions

type MoneyDto = { currency: string; amount: decimal }

type SumMoneyRequest =
    { augend: MoneyDto
      addend: MoneyDto
      toCurrency: string }

let getMoneyDtoFromHttpResponse (httpResponse: HttpResponseMessage) =
    let content = httpResponse.Content.ReadAsStringAsync().Result
    JsonSerializer.Deserialize<MoneyDto> content

let twoDollars = { currency = "USD"; amount = 2m }
let threeDollars = { currency = "USD"; amount = 3m }
let tenFrancs = { currency = "CHF"; amount = 10m }

let sumMoney request =
    createPostRequest "/banks/nbp/sum" request

[<Fact>]
let ``accept request`` () =
    let request =
        { toCurrency = "USD"
          addend = twoDollars
          augend = twoDollars }

    let response = sumMoney request |> sendRequest
    response.StatusCode |> should equal HttpStatusCode.OK

[<Fact>]
let ``calculate sum for money in the same currency`` () =
    let request =
        { toCurrency = "USD"
          addend = twoDollars
          augend = threeDollars }
    sumMoney request
    |> sendRequest
    |> getMoneyDtoFromHttpResponse
    |> should equal { currency = "USD"; amount = 5m }

[<Fact>]
let ``calculate sum of 2USD and 10 CHF in USD`` () =
    let request =
        { toCurrency = "USD"
          addend = twoDollars
          augend = tenFrancs }

    sumMoney request
    |> sendRequest
    |> getMoneyDtoFromHttpResponse
    |> should equal { currency = "USD"; amount = 7m }
