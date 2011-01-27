﻿namespace HelloAspNet.App

open System
open System.Web
open System.Web.Routing
open Frack
open Frack.Middleware
open Frack.Hosting.AspNet

type Global() =
  inherit System.Web.HttpApplication() 

  static member RegisterRoutes(routes:RouteCollection) =
    // Echo the request body contents back to the sender. 
    // Use Fiddler to post a message and see it return.
    let cts = new System.Threading.CancellationTokenSource()
    let app = Owin.create (fun request -> async {
      let greeting = "Howdy!\r\n"B
      return ("200 OK", (dict [("Content-Type", "text/html")]), seq { yield greeting :> obj }) }) cts.Token
    // Uses the head middleware.
    // Try using Fiddler and perform a HEAD request.
    routes.MapFrackRoute("{*path}", app)

  member x.Start() =
    Global.RegisterRoutes(RouteTable.Routes)

