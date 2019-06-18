module App.Animation

open System
open Fable.Core
open Fable.Core.JsInterop
open Fable.React
open Fable.React.Props


module Animate =

    type Prop =
        | Play of bool
        | End of CSSProp list
        | Start of CSSProp list
        | Complete of CSSProp list
        | Duration of TimeSpan
        | Delay of TimeSpan
        | OnComplete of (unit -> unit)
        // todo: type
        | EaseType of string
        // TODO
        //| Render of
        //| SequenceIndex
        //| SequenceId
        //| Overlay

    type private InternalProp =
        | Play of bool
        | End of obj
        | Start of obj
        | Complete of obj
        | Duration of double
        | Delay of double
        | OnComplete of (unit -> unit)
        // todo: type
        | EaseType of string
        // TODO
        //| Render of
        //| SequenceIndex
        //| SequenceId
        //| Overlay

    let private toInternalProp =
        function
        | Prop.Play v           -> Play v
        | Prop.End cssList      -> keyValueList CaseRules.LowerFirst cssList |> End
        | Prop.Start cssList    -> keyValueList CaseRules.LowerFirst cssList |> Start
        | Prop.Complete cssList -> keyValueList CaseRules.LowerFirst cssList |> Complete
        | Prop.Duration v       -> Duration v.TotalSeconds
        | Prop.Delay v          -> Delay v.TotalSeconds
        | Prop.OnComplete v     -> OnComplete v
        | Prop.EaseType v       -> EaseType v

    let animate props children =
        let internalProps = props |> List.map toInternalProp
        ofImport<InternalProp list> "Animate" "react-simple-animate" internalProps children
