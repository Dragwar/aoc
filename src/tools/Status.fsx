#load "../years/Lib.fsx"

open Lib
open System.IO
open System

let () =
    let dir =
        Path.Combine(__SOURCE_DIRECTORY__, "../years")
        |> DirectoryInfo

    let isNumber (s: string) =
        s |> Int32.TryParse |> Option.ofTryParse |> Option.isSome

    dir.EnumerateDirectories("*")
    |> Seq.filter (_.Name >> isNumber)
    |> Seq.collect (
        _.EnumerateDirectories("d*")
        >> Seq.filter (_.Name >> _.Substring(1) >> isNumber)
    )
    |> Seq.sortBy (fun x -> (int (x.Parent.Name), int (x.Name.Substring(1))))
    |> Seq.collect (_.EnumerateFiles("p*.fsx"))
    |> Seq.filter (fun x ->
        x.Name.[1 .. x.Name.LastIndexOf('.') - 1] |> isNumber)
    |> Seq.map (
        _.FullName
        >> fun x -> Path.GetRelativePath(Directory.GetCurrentDirectory(), x)
    )
    |> Seq.toList
    |> function
        | [] -> printfn "No solutions found"
        | x -> x |> List.iter (printfn "%s")
