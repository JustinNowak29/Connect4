Imports System
Imports System.ComponentModel.DataAnnotations.Schema

Module Program

    Dim Board(5, 6) As String
    Dim P1Name As String = ""
    Dim P2Name As String = ""
    Dim P1Symbol As String = ""
    Dim P2Symbol As String = ""

    Sub Main()

        Console.Title = "Connect 4"
        Console.CursorVisible = False
        SetUpBoard()
        Welcome()

        Dim Running As Boolean = True
        While Running

            Console.Clear()
            DisplayMenu()

            Select Case Console.ReadKey().KeyChar
                Case "1" 'Play Game Selected
                    PlayGame()

                    'Play Computer Selected
                Case "2"

                    'Exit Game
                Case "3"

                    'Invalid Input
                Case Else
                    Console.WriteLine()
                    InvalidInput()

            End Select




        End While



    End Sub

    Sub Welcome()

        'Welcoming Players and getting their names
        Console.Write("Enter the name of Player 1: " & Environment.NewLine & "> ")
        P1Name = Console.ReadLine()
        Console.Write("Enter the name of Player 2: " & Environment.NewLine & "> ")
        P2Name = Console.ReadLine()

        Console.WriteLine($"{P1Name} and {P2Name}, Welcome to Connect 4!")
        Threading.Thread.Sleep(1000)
        Console.Clear()
    End Sub

    Sub InvalidInput()

        'Handles invalid input
        Console.WriteLine(Environment.NewLine & "Invalid Input. Try again!")
        Threading.Thread.Sleep(1000)
        Console.Clear()
    End Sub

    Sub DisplayMenu()

        'Displays Main Menu
        Console.Write("Options:         " & Environment.NewLine &
                      "-----------------" & Environment.NewLine &
                      "[1] Play Game    " & Environment.NewLine &
                      "[2] Play Computer" & Environment.NewLine &
                      "[3] Exit Game    " & Environment.NewLine &
                      "Enter your option: " & Environment.NewLine & "> ")

    End Sub

    Sub SetUpBoard()

        'Initializes the board
        For x As Integer = 0 To 5
            For y As Integer = 0 To 6
                Board(x, y) = "."
            Next
        Next
    End Sub

    Sub DisplayBoard()

        'Displays the board
        Console.WriteLine(" 1 2 3 4 5 6 7")
        For x As Integer = 0 To 5
            'Console.Write(x & "|") TEMPORARY ROW NUMBERING
            Console.Write("|")
            For y As Integer = 0 To 6

                'Coloring the symbols
                If Board(x, y) = "X" Then
                    Console.ForegroundColor = ConsoleColor.Red
                ElseIf Board(x, y) = "O" Then
                    Console.ForegroundColor = ConsoleColor.Cyan
                End If

                'Once colour is determined, print each symbol
                Console.Write(Board(x, y))
                Console.ForegroundColor = ConsoleColor.White 'Must reset colour after each symbol before printing border
                Console.Write("|")

            Next
            Console.WriteLine() 'Makes new line after each row
        Next
    End Sub

    Sub SelectSymbols()
        Dim Choice As String

        'Validation for correct symbol
        While Not (P1Symbol = "O" Or P1Symbol = "X")

            Console.WriteLine("Enter either O or X for the symbol!")
            Console.Write($"Enter the symbol you want to assign for Player 1 ({P1Name}): " & Environment.NewLine & "> ")
            Choice = Console.ReadKey().KeyChar.ToString.ToUpper()

            'Assigning symbols based on choice
            Select Case Choice
                Case "O"
                    P1Symbol = Choice
                    P2Symbol = "X"
                Case "X"
                    P1Symbol = Choice
                    P2Symbol = "O"
                Case Else
                    InvalidInput()
            End Select
        End While

        'Displays which player has what symbol
        Console.Clear()
        Console.WriteLine($"Player 1 ({P1Name}) symbol: {P1Symbol}")
        Console.WriteLine($"Player 2 ({P2Name}) symbol: {P2Symbol}")

        Console.Write("Enter any key to continue: ")
        Console.ReadKey()

    End Sub

    Sub PlayGame()
        Dim Turns As Integer = 0
        Dim GameEnd As Boolean = False

        'Main game runthrough loop
        Console.Clear()
        SelectSymbols()

        'Displays Board, waits for user to begin
        Console.Clear()
        DisplayBoard()

        Console.Write("Ready to play? (Press any key to continue) : ")
        Console.ReadKey()

        'Loop for each turn. Continues until someone wins
        While GameEnd = False

            If Turns Mod 2 = 0 Then
                'Player 1 Turn
                PlaceSymbol(P1Symbol)
                CheckWin(P1Symbol, GameEnd)
            Else
                'Player 2 Turn
                PlaceSymbol(P2Symbol)
                CheckWin(P2Symbol, GameEnd)
            End If

            Turns += 1 'Increment turn counter

        End While

    End Sub

    Sub PlaceSymbol(ByVal Symbol As String)
        Dim Column As Integer

        'Input Loop with Validation
        While True
            Console.Clear()
            DisplayBoard()
            Console.WriteLine($"{If(Symbol = P1Symbol, P1Name, P2Name)}'s Turn ({Symbol})") 'result = If(condition, valueIfTrue, valueIfFalse)
            Console.Write("Enter the Column Number:" & Environment.NewLine & "> ")

            Dim keyChar As Char = Console.ReadKey().KeyChar

            ' Try to convert key to a string, then to a number
            If Integer.TryParse(keyChar.ToString(), Column) Then
                Column -= 1 'converts to 0-based index
            End If

            If Column < 0 Or Column > 6 Then 'If out of bounds
                Console.WriteLine(Environment.NewLine & "Out of Bounds! Try again")
                Threading.Thread.Sleep(1000)
            ElseIf Board(0, Column) <> "." Then 'If column is full
                Console.WriteLine(Environment.NewLine & "Coordinate taken! Try again")
                Threading.Thread.Sleep(1000)
            Else 'Input is valid
                Exit While
            End If

        End While

        'After input passes valdation, place on board
        Board(0, Column) = Symbol
        Console.Clear()

        'Once placed, animate the symbol falling
        SymbolAnimation(Symbol, Column)

        DisplayBoard()
        Threading.Thread.Sleep(200)

    End Sub

    Sub SymbolAnimation(ByVal Symbol As String, ByVal Column As Integer)

        'Animates the symbol falling
        For x As Integer = 0 To 4 Step 1

            DisplayBoard() 'Display current board state
            Threading.Thread.Sleep(75)

            If Board(x + 1, Column) = "." Then
                Board(x + 1, Column) = Symbol 'Update position into board
                Console.ForegroundColor = ConsoleColor.White 'Console color reset
                Board(x, Column) = "."
            Else
                Console.Clear()
                Exit For
            End If

            Console.Clear()

        Next

    End Sub

    Sub CheckWin(ByVal Symbol As String, ByRef GameEnd As Boolean)

        Dim GameWin As Boolean = False
        Dim SpacesLeft As Integer = 0

        'Check 4 Continuous Vertical Lines
        For x As Integer = 0 To 2
            For y As Integer = 0 To 6
                If Board(x, y) = Symbol And Board(x + 1, y) = Symbol And Board(x + 2, y) = Symbol And Board(x + 3, y) = Symbol Then
                    GameWin = True
                End If
            Next
        Next
        'Check 4 Continuous Horizontal Lines
        For x As Integer = 0 To 5
            For y As Integer = 0 To 2
                If Board(x, y) = Symbol And Board(x, y + 1) = Symbol And Board(x, y + 2) = Symbol And Board(x, y + 3) = Symbol Then
                    GameWin = True
                End If
            Next
        Next
        'Check 4 Leading Diagonal Lines (Top Left to Bottom Right)
        For x As Integer = 0 To 2
            For y As Integer = 0 To 3
                If Board(x, y) = Symbol And Board(x + 1, y + 1) = Symbol And Board(x + 2, y + 2) = Symbol And Board(x + 3, y + 3) = Symbol Then
                    GameWin = True
                End If
            Next
        Next
        'Check 4 Following Diagonal Lines (Top Right to Bottom Left)
        For x As Integer = 3 To 5
            For y As Integer = 0 To 3
                If Board(x, y) = Symbol And Board(x - 1, y + 1) = Symbol And Board(x - 2, y + 2) = Symbol And Board(x - 3, y + 3) = Symbol Then
                    GameWin = True
                End If
            Next
        Next

        'Check Draw
        For x As Integer = 0 To 5
            For y As Integer = 0 To 6
                If Board(x, y) = "." Then
                    SpacesLeft += 1
                End If
            Next
        Next

        'Message Display for Win or Draw
        If GameWin = True Then
            Console.WriteLine(Environment.NewLine & $"{If(Symbol = P1Symbol, P1Name, P2Name)} Wins! Congratulations!")
            GameEnd = True
            Threading.Thread.Sleep(3000)
            Console.Clear()
        ElseIf SpacesLeft = 0 Then
            Console.WriteLine(Environment.NewLine & "It's a Draw! Well played both!")
            GameEnd = True
            Threading.Thread.Sleep(3000)
            Console.Clear()
        End If

    End Sub

End Module
