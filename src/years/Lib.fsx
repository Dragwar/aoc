#if !INTERACTIVE
module Lib
#endif

open System.IO

/// only meant to be called within ./src/years/{year}/d{n}/p{n}.fsx
let readInput directory =
    Path.Combine(directory, "input.txt") |> File.ReadAllLines

/// only meant to be called within ./src/years/{year}/d{n}/p{n}.fsx
let readInputAsync directory =
    task {
        return!
            Path.Combine(directory, "input.txt")
            |> File.ReadAllLinesAsync
    }

let inline logf fmt x =
    printfn fmt x
    x


module Option =
    let ofTryParse: bool * 'a -> 'a option =
        function
        | (false, _) -> None
        | (true, x) -> Some x

module ValueOption =
    let ofTryParse: bool * 'a -> 'a voption =
        function
        | (false, _) -> ValueNone
        | (true, x) -> ValueSome x
