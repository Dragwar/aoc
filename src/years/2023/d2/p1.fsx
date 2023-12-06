#load "../../Lib.fsx"

open Lib
open System

let (|Int|) = int
let (|Game|) (s: string) = s.Split(" ") |> Seq.last |> int

let (|SubSets|) (s: string) =
    s.Split(";")
    |> Array.map (_.Split(",", StringSplitOptions.TrimEntries))
    |> Array.map (Array.map (_.Split(" ")))
    |> Array.map (
        Array.fold
            (fun (z: Map<string, int>) x ->
                match x with
                | [| Int(amount); colour |] -> z |> Map.add colour amount
                | x -> failwithf "this shouldn't happen %A" x)
            Map.empty
    )

let limits =
    Map [
        ("red", 12)
        ("green", 13)
        ("blue", 14)
    ]

let withinLimits (subsets: Map<string, int> array) =
    subsets
    |> Array.forall (Map.forall (fun key value -> value <= limits.[key]))

readInput __SOURCE_DIRECTORY__
|> Array.map (_.Split(":", StringSplitOptions.TrimEntries))
|> Array.map (function
    | [| Game(game); SubSets(subsets) |] -> (game, subsets)
    | x -> failwithf "this shouldn't happen %A" x)
|> Array.filter (snd >> withinLimits)
|> Array.map (fst)
|> Array.sum
|> logf "%A"
