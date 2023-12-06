#load "../../Lib.fsx"

open Lib

type Shape =
    | Rock = 1
    | Paper = 2
    | Scissors = 3

module Shape =
    open System

    let weight (shape: Shape) = int shape
    let (|Weight|) shape = weight shape

    let tryDecrypt =
        function
        | 'A'
        | 'a'
        | 'X'
        | 'x' -> Some Shape.Rock
        | 'B'
        | 'b'
        | 'Y'
        | 'y' -> Some Shape.Paper
        | 'C'
        | 'c'
        | 'Z'
        | 'z' -> Some Shape.Scissors
        | _ -> None

    let decrypt =
        tryDecrypt
        >> Option.defaultWith (fun () ->
            raise (NotSupportedException("Invalid Shape")))

type Outcome =
    | Loss = 0
    | Draw = 3
    | Win = 6

module Outcome =
    open Shape
    open System.Diagnostics

    let weight (outcome: Outcome) = int outcome

    let fromRound (round: Shape * Shape) =
        let (opp, player) = round
        let playerWin = ((opp, Outcome.Loss), (player, Outcome.Win))
        let playerLoss = ((opp, Outcome.Win), (player, Outcome.Loss))
        let playerDraw = ((opp, Outcome.Draw), (player, Outcome.Draw))

        match round with
        | (Shape.Rock, Shape.Paper) -> playerWin
        | (Shape.Rock, Shape.Scissors) -> playerLoss
        | (Shape.Paper, Shape.Rock) -> playerLoss
        | (Shape.Paper, Shape.Scissors) -> playerWin
        | (Shape.Scissors, Shape.Rock) -> playerWin
        | (Shape.Scissors, Shape.Paper) -> playerLoss
        | (Weight(opp), Weight(player)) when opp = player -> playerDraw
        | _ -> raise (UnreachableException("This shouldn't happen."))

let decryptedRounds (input: string array) =
    input
    |> Array.map (_.Split(" "))
    |> Array.map (Array.map (char))
    |> Array.map (Array.map (Shape.decrypt))
    |> Array.map (fun x -> (x.[0], x.[1]))


// """
// A Y
// B X
// C Z
// """
readInput __SOURCE_DIRECTORY__
|> decryptedRounds
|> Array.map (Outcome.fromRound >> snd)
|> Array.map (fun (s, o) -> (Shape.weight s) + (Outcome.weight o))
|> Array.sum
|> logf "%A"
