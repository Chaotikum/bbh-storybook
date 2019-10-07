module App

open System
open Elmish
open Fable.React
open Fable.React.Props
open Fulma
open Fable.FontAwesome

open Story
open Fable.ReactSimpleAnimate


type Story =
    { History: string list
      Next: Next }

type Person =
    | Tony
    | Thomas

type Choices = (Person * Question) list

type Model =
    | StoryPage of Story
    | Start of Choices

type AnswerSide =
    | Left
    | Right

type Msg =
    | Choose of Question
    | Answer of AnswerSide
    | Restart

let init () =
    Start [ Tony, Story.tony; Thomas, Story.thomas ]
    , Cmd.none

let choose question =
    let newModel =
        { History = []
          Next = NextQuestion question }
        |> StoryPage
    newModel, Cmd.none

let update msg model =
    match msg, model with
    | Choose question, Start _ ->
        choose question

    | Answer side, StoryPage { History = history; Next = NextQuestion question } ->
        let answer =
            match side with | Left -> question.LeftAnswer | Right -> question.RightAnswer
        { History =
            history
            @ [ question.Text
                // answer.Title
                answer.ResultText ]
          Next = answer.Next }
        |> StoryPage
        , Cmd.none

    | Restart, _ -> init()

    | _ -> model, Cmd.none


let button msg title dispatch =
      Button.button
          [ Button.Option.OnClick (fun _ -> dispatch msg)
            Button.Size IsLarge ]
          [ str title ]

let startView choices dispatch =
    choices
    |> List.map (fun (person, question) ->
        button (Choose question) (string person) dispatch)
    |> div []

let fadeIn text =
    Animate.animate
        [ Animate.Play true
          Animate.Start [ Opacity 0. ]
          Animate.End [ Opacity 1. ]
          Animate.Duration (TimeSpan.FromSeconds 1.)
          Animate.Key text ]
        [ p [] [ str text ] ]

let questionView question dispatch =
    [ fadeIn question.Text
      button (Answer Left) question.LeftAnswer.Title dispatch
      button (Answer Right) question.RightAnswer.Title dispatch ]
   |> div []

let storyView story dispatch =
    let history =
        story.History
        |> List.map (fun h -> p [] [ str h ])
        |> div [ Class "history" ]
    let next =
        match story.Next with
        | End -> p [] [ str "Ende" ]
        | NextQuestion question -> questionView question dispatch
    div []
        [ history
          next
          button Restart "Nochmal" dispatch ]

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
