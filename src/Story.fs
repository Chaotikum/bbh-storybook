module Story

type QuestionType =
    | Love
    | Money

type Question =
    { Type: QuestionType
      Text: string
      LeftAnswer: Answer
      RightAnswer: Answer }

and Answer =
    { Title: string
      ResultText: string
      Next: Next }

and Next =
    | End
    | NextQuestion of Question

// € 4
let private papierTheaterQ =
    { Type = Money
      Text =
        "Tony, frohe Weihnachten! Dein Neffe Hanno wünscht sich so sehr ein
        Papiertheater. Erlaubst du, dass er es von seiner Großmutter zu
        Weihnachten bekommt und riskierst damit, dass er sich mehr für das
        Theater interessiert als für den kaufmännischen Beruf?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Hanno freut sich sehrt über das Geschenk, trotz der Mahnung
                seines Onkels Christian: „Hör’ mal, Kind, laß dir raten, hänge
                deine Gedanken nur nicht zu sehr an solche Sachen... Theater...
                und sowas... Das taugt nichts, glaube deinem Onkel. Ich habe
                mich auch immer viel zu sehr für diese Dinge interessiert, und
                darum ist auch nicht viel aus mir geworden. Ich habe große
                Fehler begangen, mußt du wissen…“ Sieht ganz so aus, als würde
                die Familie Buddenbrook Hanno als Erben für die Firma
                verlieren…"
            Next = End }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du bleibst streng, denn es geht immerhin um das Wohl der Firma
                Buddenbrook! Allerdings ist das auch ziemlich egoistisch von
                dir. Durch die Entscheidung, Hanno seinen größten Wunsch nicht
                zu erfüllen, verärgerst du ihn so sehr, dass er sich schwört,
                niemals Kaufmann zu werden."
            Next = End } }

// € 3
let private hannoKaufmannQ =
    { Type = Money
      Text =
        "Thomas, willst du deinen Sohn Hanno streng zum Kaufmann erziehen oder
        lässt du ihn frei entscheiden, was er später beruflich machen möchte?"
      LeftAnswer =
          { Title = "Zum Kaufmann erziehen"
            ResultText =
                "Du handelst pflichtbewusst und im Interesse der Firma, wie du
                es gewohnt bist. Hanno soll ja dein Nachfolger werden!
                Allerdings interessiert er sich viel mehr für das Theater und
                die Musik…"
            Next = End }
      RightAnswer =
          { Title = "Frei entscheiden lassen"
            ResultText =
                "Du handelst sehr fortschrittlich für deine Zeit: Dein Kind
                darf seinen Beruf frei wählen. Dafür wirst du aber vermutlich
                den einzigen Erben für deine Firma verlieren…"
            Next = End } }

// € 2
let private hausBauenQ =
    { Type = Money
      Text =
        "Thomas, baust du für dich und deine Familie ein neues Haus, um dich
        von deinen beruflichen Problemen abzulenken und riskierst damit
        finanzielle Verluste?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Du hast sehr egoistisch gehandelt und einen weiteren
                Grundstein für den Ruin deiner Firma gelegt."
            Next = NextQuestion hannoKaufmannQ }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du hast sehr vorbildlich gehandelt und deine eigenen
                Bedürfnisse zurückgestellt. Dadurch verliest du kein Kapital
                und kannst die Firma vielleicht doch wieder auf den grünen
                Zweig bringen?"
            Next = End } }

// € 1b
let private getreideTonyQ =
    { Type = Money
      Text =
        "Du hast nun nicht mehr viel eigenes Geld und
        deine Eltern sind immer noch böse auf dich wegen der
        Verweigerung der Heirat. Du musst dich jetzt dafür
        einsetzen, dass es der Firma unter Thomas gut geht, dann
        ist er dir vielleicht nicht mehr böse? Überredest du ihn zu
        einem ziemlich riskanten Getreidegeschäft, das einen hohen
        Gewinn, aber auch einen hohen Verlust verspricht?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Pech gehabt! Thomas verliert 40.000 Mark
                Courant, da ein Unwetter die Ernte zerstört.
                Damit macht die Firma einen großen Verlust."
            Next = NextQuestion hausBauenQ }
      RightAnswer =
          { Title = "Nein"
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
            Next = NextQuestion hausBauenQ } }

// € 1a
let private getreideThomasQ =
    { Type = Money
      Text =
        "Greifst du Tonys Idee zu einem ziemlich riskanten Getreidegeschäft
        auf, das einen hohen Gewinn, aber auch einen hohen Verlust verspricht?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Pech gehabt! Du verlierst 40.000 Mark Courant, da ein Unwetter
                die Ernte zerstört. Damit macht die Firma einen großen Verlust."
            Next = NextQuestion hausBauenQ }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du lehnst das riskante Geschäft ab, weil du dem Rat folgst,
                der in der Familienchronik der Buddenbrooks steht: „Mein Sohn,
                sey mit Lust bey den Geschäften am Tage, aber mache nur solche,
                daß wir bey Nacht ruhig schlafen können.“ Glück gehabt! Du
                hättest deine Investitionen aufgrund eines Unwetters verloren."
            Next = NextQuestion hausBauenQ } }

// ♥ 6
let private alinePsychQ =
    { Type = Love
      Text =
        "Nach Thomas‘ Tod kann Christian dann endlich Aline Puvogel heiraten.
        Jetzt bist du Aline. Das Geld Deines Ehemanns Christian Buddenbrook
        kannst du für dich und deine Kinder gut gebrauchen. Allerdings benimmt
        er sich immer seltsamer, er scheint wahnsinnig zu werden. Lässt du ihn
        in eine psychiatrische Anstalt einweisen?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Du lässt Christian in die Anstalt einweisen und überlässt ihm
                so seinem Schicksal. Gesellschaftlich schadet dir diese
                Entscheidung: Wie kannst du nur so kaltherzig sein? Viele
                wenden sich von dir ab. Dafür kannst du dir mit deinen Kindern
                und Christians Geld ein entspanntes Leben machen."
            Next = End }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du behältst deinen psychisch kranken Mann zu Hause. So leidet
                euer Ruf nicht und auf sein Geld hast du trotzdem Zugriff. Aber
                er geht dir zu Hause ziemlich auf die Nerven."
            Next = End } }

// ♥ 5
let private christianEntmuendigenQ =
    { Type = Love
      Text =
        "Dein Bruder Christian möchte eine nicht standesgemäße Ehe mit Aline
        Puvogel eingehen, von der man sagt, Christian sei nicht ihr einziger
        Liebhaber. Ein Skandal! Reagierst du mit der Drohung, deinen eigenen
        Bruder zu entmündigen?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Du handelst rational und hart. Christian darf Aline nicht
                heiraten, solange du am Leben bist."
            Next = NextQuestion alinePsychQ }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du handelst wie ein richtiger Bruder und lässt ihn selbst
                entscheiden. Aber was denken die Leute!"
            Next = NextQuestion hausBauenQ } }

// ♥ 4
let private permanderHeiratenQ =
    { Type = Love
      Text =
        "Deine erste Ehe mit Bendix Grünlich wurde geschieden. Über eine
        Freundin lernst du den bayrischen Hopfenhändler Alois Permaneder
        kennen. Ihr versteht euch gut, aber es ist etwas anders als die
        Lübecker Kaufmänner. Eben ein bisschen gemütlicher. Wenn du ihn
        heiratest, ist das deine Chance, doch noch eine gute Partie zu machen.
        Aber was ist, wenn er sich mit der Mitgift zur Ruhe setzt?"
      LeftAnswer =
          { Title = "Heirat"
            ResultText =
                "Du heiratest Permaneder und ziehst zu ihm nach München. Die
                Eingewöhnung in der neuen Stadt fällt dir nicht leicht. Nach
                drei Wochen liegt auf deinem Bett ein Zettel mit der
                Aufschrift: „Danke für die Mitgift. Schönes Leben noch“.
                Permaneder setzt sich mit deinem Geld zur Ruhe und auch sonst
                läuft es in der Ehe schlecht – bis du zurück nach Lübeck gehst
                und auch deine zweite Ehe geschieden wird. Aber eins muss man
                Permaneder lassen: Er zahlt nach der Scheidung die Mitgift an
                die Firma Buddenbrook zurück. Immerhin das!"
            Next = NextQuestion papierTheaterQ }
      RightAnswer =
          { Title = "Keine Heirat"
            ResultText =
                "Du bist zwar unglücklich, weil die Hochzeit auf die letzte
                Minute geplatzt ist. Aber du hast dich vor einem riesen Fehler
                bewahrt. Permaneder hätte sich mit der Mitgift zur Ruhe gesetzt
                und eure Ehe wäre alles andere als rosig verlaufen."
            Next = NextQuestion papierTheaterQ } }

// ♥ 3
let private gerdaAnnaQ =
    { Type = Love
      Text =
        "Heiratest du die reiche und Geige spielende Gerda oder das arme
        Blumenmädchen Anna, die du manchmal heimlich besuchst…?"
      LeftAnswer =
          { Title = "Gerda"
            ResultText =
                "Du erntest von der Gesellschaft viel Anerkennung. Wenn ihr als
                Paar in der Öffentlichkeit auftretet, seid ihr ein echter
                Hingucker! Aber Anna kannst du dein Leben lang nicht
                vergessen…"
            Next = NextQuestion christianEntmuendigenQ }
      RightAnswer =
          { Title = "Anna"
            ResultText =
                "Von deinen Nachbarn und Freunden erntest du viele abfällige
                Blicke. An der Börse spricht man nicht mehr mit dir. Ein großer
                Schaden für deinen Ruf und die Firma! Aber mit Anna bist du
                sehr glücklich."
            Next = NextQuestion getreideThomasQ } }

// ♥ 2
let private gruenlichSchwarzkopfQ =
    { Type = Love
      Text =
        "Jetzt musst du dich endgültig entscheiden: Wirst du Bendix Grünlich
        heiraten, weil deine Familie es so wünscht und er eine „gute Partie“
        ist? Oder lieber bei deiner Jugendliebe Morten Schwarzkopf bleiben, der
        allerdings aus einfachem Hause kommt…"
      LeftAnswer =
          { Title = "Grünlich"
            ResultText =
                "Deine Eltern haben dich sehr lieb, weil die Heirat einen
                wirtschaftlichen Vorteil zu versprechen scheint. Dafür siehst
                du jeden Tag den verhassten Grünlich. Und leider endet die Ehe
                mit Grünlich nicht besonders gut für dich und deine Familie:
                    Grünlich macht bankrott und es stellt sich heraus, dass er
                    dich nur wegen deiner hohen Mitgift geheiratet hat. Die Ehe
                    wird geschieden."
            Next = NextQuestion permanderHeiratenQ }
      RightAnswer =
          { Title = "Schwarzkopf"
            ResultText =
                "Du handelst nach deinem Herzen. Zwar bekommst du böse Blicke
                deiner Eltern und des gesamten Lübecker Bürgertums, aber du
                bist dein Leben lang glücklich. Deine Eltern reden nicht mehr
                mit dir, und wer weiß, ob sie dir eines Tages dein Erbe
                auszahlen…"
            Next = NextQuestion getreideTonyQ } }

// ♥ 1
let private gruenlichVerlobenQ =
    { Type = Love
      Text =
        "Würdest du dich mit dem Hamburger Kaufmann Bendix Grünlich verloben,
        weil deine Eltern es so wollen, obwohl du ihn hasst?"
      LeftAnswer =
          { Title = "Ja"
            ResultText =
                "Du handelst genau wie Tony aus Pflichtgefühl. Deine Eltern
                sind sehr stolz auf dich!"
            Next = NextQuestion gruenlichSchwarzkopfQ }
      RightAnswer =
          { Title = "Nein"
            ResultText =
                "Du handelst egoistisch, aber für viele nachvollziehbar – außer
                für deine Eltern, weswegen sie dich vielleicht sogar enterben
                werden!"
            Next = NextQuestion getreideTonyQ } }

let tony = gruenlichVerlobenQ
let thomas = gerdaAnnaQ
