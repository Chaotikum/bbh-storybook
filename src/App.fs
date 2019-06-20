module App

open System
open Elmish
open Fable.React
open Fable.React.Props
open Fulma
open Fable.FontAwesome

open Story
open Animation


type Story =
    { History: string list
      Next: Next }

type Choices =
    { Tony: Question 
      Thomas: Question }

type Model =
    | StoryPage of Story
    | Start of Choices

type Person =
    | Tony
    | Thomas

type Msg =
    | Choose of Person
    | AnswerLeft
    | AnswerRight
    | Restart

let init () =
    // todo: remove from state, it's static
    let start =
        { Tony = Story.tony
          Thomas = Story.thomas }
        |> Start

    start, Cmd.none

let choose model question =
    let newModel =
        { History = []
          Next = NextQuestion question }
        |> StoryPage 
    newModel, Cmd.none

let answer history question answer =
    { History =
        history
        @ [ question.Text
            answer.Title
            answer.ResultText ]
      Next = answer.Next }
    |> StoryPage
    , Cmd.none

let update msg model =
    match msg, model with
    | Choose Tony, Start choices ->
        choose model choices.Tony
    | Choose Thomas, Start choices ->
        choose model choices.Thomas

    | AnswerLeft, StoryPage { History = history; Next = NextQuestion question } ->
        answer history question question.LeftAnswer
    | AnswerRight, StoryPage { History = history; Next = NextQuestion question } ->
        answer history question question.RightAnswer

    | Restart, _ -> init()

    | _ -> model, Cmd.none


let button msg title dispatch =
      Button.button
          [ Button.Option.OnClick (fun _ -> dispatch msg)
            Button.Size IsLarge ]
          [ str title ]

let startView choices dispatch =
    div []
        [ button (Choose Tony) "Tony" dispatch
          button (Choose Thomas) "Thomas" dispatch ]

let questionView question dispatch =
    Animate.animate
        [ Animate.Prop.Play true
          Animate.Prop.Start [ Opacity 0. ]
          Animate.Prop.End [ Opacity 1. ]
          Animate.Prop.Duration (TimeSpan.FromSeconds 1.) ]
        [ div []
            [ p [] [ str question.Text ]
              button AnswerLeft question.LeftAnswer.Title dispatch
              button AnswerRight question.RightAnswer.Title dispatch ] ]

let storyView story dispatch =
    let history =
        story.History
        |> List.map (fun h -> p [] [ str h ])
        |> div []
    let next =
        match story.Next with
        | End -> p [] [str "The End <3"]
        | NextQuestion question -> questionView question dispatch
    div []
        [ history
          next
          button Restart "restart" dispatch ]

let view model dispatch =
    match model with
    | Start choices -> startView choices dispatch
    | StoryPage story -> storyView story dispatch
    |> List.singleton
    |> Container.container []

open Elmish.Debug
open Elmish.HMR

Program.mkProgram init update view
|> Program.withReactSynchronous "elmish-app"
#if DEBUG
|> Program.withDebugger
#endif
|> Program.run




