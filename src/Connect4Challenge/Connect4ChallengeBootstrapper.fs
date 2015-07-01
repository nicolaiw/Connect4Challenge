module Connect4Challenge.Bootstrapper

open System.Reflection
open Connect4Challenge.Interface

// http://fsharpforfunandprofit.com/posts/cli-types/
let tryCast<'a> o = match box o with
                        | :? 'a as output -> Some output
                        | _ -> None

//TODO: Test it
let getInterfaceFromAssembly<'a> assemblyPath =
    let instance = Assembly.LoadFrom(assemblyPath).GetTypes()
                    |> Seq.map (fun a -> a.GetInterface(typeof<'a>.Name))
                    |> Seq.find (fun a -> a.GetConstructor(System.Type.EmptyTypes) <> null) // The class has to have an empty ctor so CreateInstance will not fail
    match System.Activator.CreateInstance(instance) |> tryCast<'a> with
    | Some(instance) -> instance
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

