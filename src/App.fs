module App.View

open System
open Elmish
open Fable.React
open Fable.React.Props
open Fulma
open Fable.FontAwesome

open Animation


type AnswerMotivation =
    | Love
    | Money

type Question =
    { Text: string
      LeftAnswer: Answer
      RightAnswer: Answer }

and Answer =
    { Motivation: AnswerMotivation
      Title: string
      ResultText: string
      Next: Next }

and Next =
    | End
    | NextQuestion of Question

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
    let tony =
        { Text =
            "Würdest du dich mit dem Hamburger Kaufmann Bendix Grünlich
            verloben, weil deine Eltern es so wollen, obwohl du ihn hasst?"
          LeftAnswer =
              { Motivation = Love
                Title = "Ja"
                ResultText =
                    "Du handelst genau wie Tony aus Pflichtgefühl. Deine Eltern
                    sind sehr stolz auf dich!"
                Next =
                    { Text =
                        "Jetzt musst du dich endgültig entscheiden: Wirst du
                        Bendix Grünlich heiraten, weil deine Familie es so
                        wünscht und er eine „gute Partie“ ist? Oder lieber bei
                        deiner Jugendliebe Morten Schwarzkopf bleiben, der
                        allerdings aus einfachem Hause kommt… "
                      LeftAnswer =
                          { Motivation = Love
                            Title = "Grünlich"
                            ResultText =
                                "Deine Eltern haben dich sehr lieb, weil die
                                Heirat einen wirtschaftlichen Vorteil zu
                                versprechen scheint. Dafür siehst du jeden Tag
                                den verhassten Grünlich.<br>Und leider endet die
                                Ehe mit Grünlich nicht besonders gut für dich
                                und deine Familie: Grünlich macht bankrott und
                                es stellt sich heraus, dass er dich nur wegen
                                deiner hohen Mitgift geheiratet hat. Die Ehe
                                wird geschieden."
                            Next = End }
                      RightAnswer =
                          { Motivation = Money
                            Title = "Scharzkopf"
                            ResultText =
                                "Du handelst nach deinem Herzen. Zwar bekommst
                                du böse Blicke deiner Eltern und des gesamten
                                Lübecker Bürgertums, aber du bist dein Leben
                                lang glücklich. Deine Eltern reden nicht mehr
                                mit dir, und wer weiß, ob sie dir eines Tages
                                dein Erbe auszahlen…"
                            Next = End } }
                    |> NextQuestion }
          RightAnswer =
              { Motivation = Money
                Title = "Nein"
                ResultText =
                    "Du handelst egoistisch, aber für viele nachvollziehbar –
                    außer für deine Eltern, weswegen sie dich vielleicht sogar
                    enterben werden!"
                Next =
                    { Text =
                        "Du hast nun nicht mehr viel eigenes Geld und
                        deine Eltern sind immer noch böse auf dich wegen der
                        Verweigerung der Heirat. Du musst dich jetzt dafür
                        einsetzen, dass es der Firma unter Thomas gut geht, dann
                        ist er dir vielleicht nicht mehr böse? Überredest du ihn zu
                        einem ziemlich riskanten Getreidegeschäft, das einen hohen
                        Gewinn, aber auch einen hohen Verlust verspricht?"
                      LeftAnswer =
                          { Motivation = Money
                            Title = "Ja"
                            ResultText =
                                "Pech gehabt! Thomas verliert 40.000 Mark
                                Courant, da ein Unwetter die Ernte zerstört.
                                Damit macht die Firma einen großen Verlust."
                            Next = End }
                      RightAnswer =
                          { Motivation = Money
                            Title = "Nein"
                            ResultText =
                                "Du erinnerst dich an den Rat, den du in der
                                Familienchronik der Buddenbrooks gelesen hast:
                                    „Mein Sohn, sey mit Lust bey den Geschäften
                                    am Tage, aber mache nur solche, daß wir bey
                                    Nacht ruhig schlafen können.“
                                Deshalb rätst
                                du Thomas von dem riskanten Geschäft ab.
                                Glück gehabt! Er hätte seine Investitionen
                                aufgrund eines Unwetters verloren."
                            Next = End } }
                    |> NextQuestion } }

    let start =
        { Tony = tony
          Thomas = tony }
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
          Animate.Prop.Duration (TimeSpan.FromSeconds 1.)
          ]
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




