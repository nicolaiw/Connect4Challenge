module Connect4Challenge.Bootstrapper

open System.Reflection
open Connect4Challenge.Interface

// http://fsharpforfunandprofit.com/posts/cli-types/
let tryCast<'a> o = match box o with
                        | :? 'a as output -> Some output
                        | _ -> None

//TODO: load assembly in an AppDomain (AddIn Framework)
let getInterfaceFromAssembly<'a> assemblyPath =
    let assembly = Assembly.LoadFrom(assemblyPath);
    let interfaceType = assembly.GetTypes()
                        |> Seq.tryFind (fun a -> a.GetInterface(typeof<'a>.Name) <> null)
    match interfaceType with
    | Some(ifType) -> match System.Activator.CreateInstance(ifType) |> tryCast<'a> with
                      | Some(instance) -> instance
                      | None -> Unchecked.defaultof<'a>
    | None -> Unchecked.defaultof<'a>
    

   
    
        



//    var instances = from t in Assembly.GetExecutingAssembly().GetTypes()
//                where t.GetInterfaces().Contains(typeof(ISomething))
//                         && t.GetConstructor(Type.EmptyTypes) != null
//                select Activator.CreateInstance(t) as ISomething;
//
//foreach (var instance in instances)
//{
//    instance.Foo(); // where Foo is a method of ISomething
//}

