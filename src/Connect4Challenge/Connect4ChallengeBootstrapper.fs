module Connect4Challenge.Bootstrapper

open System
open System.Reflection
open System.IO
open System.Security
open System.Security.Permissions
open System.Reflection
open System.Security.Policy
open Connect4Challenge.Interface
open System


// http://fsharpforfunandprofit.com/posts/cli-types/
let tryCast<'a> o = match box o with
                        | :? 'a as output -> Some output
                        | _ -> None

//NOT USED
let getInterfaceFromAssembly<'a> assemblyPath =
    let assembly = Assembly.LoadFrom(assemblyPath);
    let interfaceType = assembly.GetTypes()
                        |> Seq.tryFind (fun a -> a.GetInterface(typeof<'a>.Name) <> null)
    match interfaceType with
    | Some(ifType) -> match System.Activator.CreateInstance(ifType) |> tryCast<'a> with
                      | Some(instance) -> instance
                      | None -> Unchecked.defaultof<'a>
    | None -> Unchecked.defaultof<'a>
    

//TODO: refactor out the code duplicates in the invoke methods
type Sandboxer() =
    
    inherit System.MarshalByRefObject()

    member this.setup(pathToUntrusted) = 
        //Setting the AppDomainSetup. It is very important to set the ApplicationBase to a folder 
        //other than the one in which the sandboxer resides.
        let adSetup = new AppDomainSetup( ApplicationBase = Path.GetFullPath(pathToUntrusted) )

        //Setting the permissions for the AppDomain. We give the permission to execute and to 
        //read/discover the location where the untrusted code is loaded.
        let permSet = new PermissionSet(PermissionState.None)
        permSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution)) |> ignore

        //We want the sandboxer assembly's strong name, so that we can add it to the full trust list.
        let fullTrustAssembly = typeof<Sandboxer>.Assembly.Evidence.GetHostEvidence<StrongName>()

        //Now we have everything we need to create the AppDomain, so let's create it.
        let newDomain = AppDomain.CreateDomain("Sandbox", null, adSetup, permSet, fullTrustAssembly)
        //Use CreateInstanceFrom to load an instance of the Sandboxer class into the
        //new AppDomain. 
        let handle = Activator.CreateInstanceFrom(newDomain, typeof<Sandboxer>.Assembly.ManifestModule.FullyQualifiedName, typeof<Sandboxer>.FullName)
        
        //Unwrap the new domain instance into a reference in this domain and use it to execute the 
        //untrusted code.
        let newDomainInstance =  handle.Unwrap() :?> Sandboxer

        newDomainInstance


     member private this.getInstance<'a>(untrustedAssemblyName: string) =
        
        let assembly = Assembly.Load(untrustedAssemblyName)
        let ``type`` = typeof<'a>

        let instanceType = assembly.GetTypes()
                                |> Seq.tryFind (fun a -> a.IsSubclassOf(``type``))
        
        match instanceType with
                | Some(ifType) -> match System.Activator.CreateInstance(ifType) |> tryCast<'a> with
                                  | Some(instance) -> instance
                                  | None -> Unchecked.defaultof<'a>
                | None -> Unchecked.defaultof<'a>
    
    member private this.invoke<'a, 'ret>(untrustedAssemblyName, getMemberValue) =
        let t = this.getInstance<'a>(untrustedAssemblyName)
        match tryCast<'ret> (getMemberValue(t)) with
        | Some(T) -> T
        | None -> failwith "Bad return type"
        
    member this.invokeProperty<'a, 'ret>(untrustedAssemblyName, memberName) : 'ret =
        this.invoke(untrustedAssemblyName, fun t-> t.GetType().GetProperty(memberName).GetValue(t, null))

    member this.invokeMethod<'a, 'ret>(untrustedAssemblyName, memberName, [<ParamArray>] args: Object[]) : 'ret =
        this.invoke(untrustedAssemblyName, fun t -> t.GetType().GetMethod(memberName).Invoke(t, args))
        

