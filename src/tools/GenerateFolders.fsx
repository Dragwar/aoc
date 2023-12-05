#load "../years/Lib.fsx"

open Lib

open System
open System.IO


let () =
    let combinePath x y = Path.Combine(x, y)

    let dir =
        DirectoryInfo(combinePath __SOURCE_DIRECTORY__ "../years")

    let defaultFiles = [ ".gitkeep"; "input.txt" ]

    let makeDayFolderPath (parentDirPath: string) =
        sprintf "d%i" >> combinePath parentDirPath

    [ 2015..2023 ]
    |> List.map (string >> dir.CreateSubdirectory)
    |> List.map (fun x ->
        Int32.TryParse x.Name
        |> Option.ofTryParse
        |> Option.map (fun year ->
            [ 1 .. DateTime.DaysInMonth(year, 12) ]
            |> List.map (makeDayFolderPath x.FullName)))
    |> List.filter Option.isSome
    |> List.collect Option.get
    |> List.collect (fun x -> defaultFiles |> List.map (combinePath x))
    |> List.map (FileInfo)
    |> List.filter (_.Exists >> not)
    |> List.map (fun x ->
        x.Directory.Parent.Create()
        x.Directory.Create()
        x.Create().Dispose()
        x)
    |> List.map (
        _.FullName
        >> fun x -> Path.GetRelativePath(Directory.GetCurrentDirectory(), x)
    )
    |> function
        | [] -> printfn "Files already exist"
        | x -> x |> List.iter (printfn "%s")
