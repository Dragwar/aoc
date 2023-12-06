#load "../../Lib.fsx"

open Lib
open System

readInput __SOURCE_DIRECTORY__
|> Array.map (fun x ->
    x
    |> Seq.skipWhile (Char.IsDigit >> not)
    |> Seq.rev
    |> Seq.skipWhile (Char.IsDigit >> not)
    |> Seq.rev
    |> Seq.toArray)
|> Array.map (fun x -> (x.[0], x.[x.Length - 1]))
|> Array.map (fun (x, y) -> sprintf "%c%c" x y)
|> Array.map (int)
|> Array.sum
|> logf "%A"
