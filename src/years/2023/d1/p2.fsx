#load "../../Lib.fsx"

open Lib
open System

let numStrMap =
    Map [
        ("one", 1)
        ("two", 2)
        ("three", 3)
        ("four", 4)
        ("five", 5)
        ("six", 6)
        ("seven", 7)
        ("eight", 8)
        ("nine", 9)
    ]

let (|Number|_|) (s: string) : int option =
    match
        (Int32.TryParse s |> Option.ofTryParse, numStrMap |> Map.tryFind s)
    with
    | (Some(n), _) -> Some n
    | (_, Some(n)) -> Some n
    | _ -> None

let numStrings = [
    yield! numStrMap.Keys
    yield! numStrMap.Values |> Seq.map string
]

readInput __SOURCE_DIRECTORY__
|> Array.map (fun x ->
    numStrings
    |> Seq.allPairs (numStrings)
    |> Seq.map (fun (b, e) -> [
        (x.IndexOf(b), b)
        (x.LastIndexOf(e), e)
    ])
    |> Seq.map (List.filter (fst >> (<>) -1))
    |> Seq.filter (List.length >> (<>) 0)
    |> Seq.collect (id)
    |> Seq.toList
    |> Set
    |> Seq.sortBy (fst)
    |> fun z -> (Seq.head z, Seq.last z))
|> Array.map (fun (first, last) -> (snd first, snd last))
|> Array.map (function
    | (Number(s), Number(e)) -> (s, e)
    | x -> failwithf "This shouldn't happen - %A" x)
|> Array.map (fun (s, e) -> sprintf "%i%i" s e)
|> Array.map (int)
|> Array.sum
|> logf "%A"
