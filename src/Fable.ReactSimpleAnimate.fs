// https://react-simple-animate.now.sh
module Fable.ReactSimpleAnimate

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
        | EaseType of string // todo: type
        | Render of (unit -> ReactElement)
        | SequenceIndex of int
        | SequenceId of string
        | Overlay of TimeSpan
        | Key of string

    type private InternalProp =
        | Play of bool
        | End of obj
        | Start of obj
        | Complete of obj
        | Duration of float
        | Delay of float
        | OnComplete of (unit -> unit)
        | EaseType of string // todo: type
        | Render of (unit -> ReactElement)
        | SequenceIndex of int
        | SequenceId of string
        | Overlay of float
        | Key of string

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
        | Prop.Render v         -> Render v
        | Prop.SequenceIndex v  -> SequenceIndex v
        | Prop.SequenceId v     -> SequenceId v
        | Prop.Overlay v        -> Overlay v.TotalSeconds
        | Prop.Key v            -> Key v

    let animate props children =
        let internalProps =
            props
            |> List.map toInternalProp
            |> keyValueList CaseRules.LowerFirst
        ofImport "Animate" "react-simple-animate" internalProps children
