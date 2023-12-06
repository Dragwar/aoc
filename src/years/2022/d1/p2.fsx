#load "../../Lib.fsx"

open Lib
open System

type State = { Current: int; Elfs: int list }

readInput __SOURCE_DIRECTORY__
|> Seq.toList
|> List.mapFold
    (fun state line ->
        match Int32.TryParse line |> Option.ofTryParse with
        | Some(n) ->
            (line,
             {
                 state with
                     Elfs =
                         (List.updateAt
                             state.Current
                             (n + state.Elfs.[state.Current])
                             state.Elfs)
             })
        | None ->
            (line,
             {
                 Current = state.Current + 1
                 Elfs = state.Elfs @ [ 0 ]
             }))
    ({ Current = 0; Elfs = [ 0 ] })
|> snd
|> _.Elfs
|> List.sortDescending
|> List.take 3
|> List.sum
|> logf "%A"
