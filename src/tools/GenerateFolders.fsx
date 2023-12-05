#load "../years/Lib.fsx"

open Lib

open System
open System.IO


let () =
    let dir =
        DirectoryInfo(Path.combine __SOURCE_DIRECTORY__ "../years")

    let defaultFiles = [ ".gitkeep"; "input.txt" ]

    let makeDayFolderPath (parentDirPath: string) =
        sprintf "d%i" >> Path.combine parentDirPath


    [ 2015..2023 ]
    |> List.map (string >> dir.CreateSubdirectory)
    |> List.collect (fun x ->
        let year = int x.Name

        [ 1 .. DateTime.DaysInMonth(year, 12) ]
        |> List.map (makeDayFolderPath x.FullName))
    |> List.collect (fun x -> defaultFiles |> List.map (Path.combine x))
    |> List.map (FileInfo)
    |> List.filter (_.Exists >> not)
    |> List.map (fun x ->
        x.Directory.Parent.Create()
        x.Directory.Create()
        x.Create().Dispose()
        x)
    |> function
        | [] -> printfn "Files already exist"
        | x -> printfn "Created:\n%A" x
