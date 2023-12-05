#if !INTERACTIVE
module Lib
#endif

open System.IO


type Path with

    static member combine (p1: string) (p2: string) : string =
        Path.Combine(p1, p2)
