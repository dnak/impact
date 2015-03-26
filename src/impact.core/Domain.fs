namespace impact.core

[<AutoOpen>]
module Domain = 
    open Neo4jClient
    open System

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
        let client = new GraphClient(new Uri("http://localhost:7474/db/data"))
        client.Connect()
        printfn "%s solves %s" solution.Title impact.Title
        client.Cypher
            .Match("(i:Impact)", "(s:Solution)")
            .Where(fun (s:Solution) -> s.Title = solution.Title)
            .AndWhere(fun (i:Impact) -> i.Title = i.Title)
            .CreateUnique("s-[:solves]->i")
            .ExecuteWithoutResults()

