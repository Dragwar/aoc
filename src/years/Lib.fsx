#if !INTERACTIVE
module Lib
#endif

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
