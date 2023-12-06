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

let (|MergedSubsets|) =
    Array.reduce (
        Map.fold (fun acc key value ->
            match (Map.tryFind key acc) with
            | Some(v) -> Map.add key (max value v) acc
            | _ -> Map.add key value acc)
    )

let power: Map<string, int> -> int =
    Map.values >> Seq.reduce (*)

readInput __SOURCE_DIRECTORY__
|> Array.map (_.Split(":", StringSplitOptions.TrimEntries))
|> Array.map (function
    | [| Game(game); SubSets(subsets) |] -> (game, subsets)
    | x -> failwithf "this shouldn't happen %A" x)
|> Array.map (fun (game, MergedSubsets(mergedSubsets)) -> (game, mergedSubsets))
|> Array.map (snd)
|> Array.map (power)
|> Array.sum
|> logf "%A"
