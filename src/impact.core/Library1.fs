namespace impact.core

module Domain = 
    // structural equality allows me to require that Goals are 
    // unique on Title, which happens to be very human-friendly
    // CLIMutable allows the record to be serialized over the wire 
    // (which is required to use Neo4j's REST API), but still be immutable in F#
    [<CLIMutable>]
    type Goal = { Title:string; Description:string } 

    [<CLIMutable>]
    type Actor = { Name:string; Role:string }

    [<CLIMutable>]
    type Impact = { Title:string; Priority:int; SubImpacts:Impact list}

    [<CLIMutable>]
    type Solution = { Title:string; Impacts:Impact list}

    let solves (impact:Impact) (solution:Solution) = 
        printfn "%s solves %s" solution.Title impact.Title

    let solution = { Title="great idea"; Impacts=[]}
    let impact = { Title="big problem"; Priority=0; SubImpacts=[]}

    solution |> solves impact
