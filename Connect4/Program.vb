Imports System
Imports System.ComponentModel.DataAnnotations.Schema

Module Program

    Dim Board(5, 6) As String
    Dim P1Name As String = ""
    Dim P2Name As String = ""
    Dim P1Symbol As String = ""
    Dim P2Symbol As String = ""
    Dim GameWon As Boolean = False

    Sub Main()

        SetUpBoard()
        Welcome()

        Dim Running As Boolean = True
        While Running

            DisplayMenu()

            Select Case Console.ReadKey().KeyChar
                'Play Game Selected
                Case "1"
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

        Console.Write("Enter the name of Player 1: ")
        P1Name = Console.ReadLine()
        Console.Write("Enter the name of Player 2: ")
        P2Name = Console.ReadLine()

        Console.WriteLine($"{P1Name} and {P2Name}, Welcome to Connect 4!")
        Threading.Thread.Sleep(1000)

        Console.Clear()

    End Sub

    Sub InvalidInput()
        Console.WriteLine(Environment.NewLine & "Invalid Input. Try again!")
        Threading.Thread.Sleep(1000)
        Console.Clear()
    End Sub

    Sub DisplayMenu()

        Console.Write("Options:         " & Environment.NewLine &
                      "-----------------" & Environment.NewLine &
                      "[1] Play Game    " & Environment.NewLine &
                      "[2] Play Computer" & Environment.NewLine &
                      "[3] Exit Game    " & Environment.NewLine &
                      "Enter your option: ")

    End Sub
    Sub SetUpBoard()

        For x As Integer = 0 To 5
            For y As Integer = 0 To 6
                Board(x, y) = "."
            Next
        Next

    End Sub

    Sub DisplayBoard()

        Console.WriteLine("  1 2 3 4 5 6 7")
        For x As Integer = 0 To 5
            Console.Write(x & "|")
            For y As Integer = 0 To 6
                Console.Write(Board(x, y) & "|")
            Next
            Console.WriteLine()
        Next

    End Sub

    Sub SelectSymbols()
        Dim Choice As String

        'Validation for correct symbol.
        While Not (P1Symbol = "O" Or P1Symbol = "X")
            Console.WriteLine("Enter either O or X for the symbol!")

            Console.Write($"Enter the symbol you want to assign for Player 1 ({P1Name}): ")
            Choice = Console.ReadKey().KeyChar.ToString.ToUpper()

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
        Console.WriteLine(Environment.NewLine & $"Player 1 ({P1Name}) symbol: {P1Symbol}")
        Console.WriteLine($"Player 2 ({P2Name}) symbol: {P2Symbol}")

        Console.Write("Enter any key to continue: ")
        Console.ReadKey()
        Console.Clear()

    End Sub


    Sub PlayGame()

        Console.Clear()
        SelectSymbols()

        'Displays Board, waits for user to begin
        Console.Clear()
        DisplayBoard()
        Console.Write("Ready to play? (Press any key to continue) : ")
        Console.ReadKey()

        'While GameWon = False


        'Test
        PlaceSymbol(P1Symbol)
        DisplayBoard()




    End Sub

    Sub PlaceSymbol(ByVal Symbol As String)
        Dim Column As Integer

        While True

            Console.Clear()
            Console.Write("Enter the Column Number:")
            Column = Console.ReadLine() - 1

            If Board(0, Column) <> "." Then
                Console.WriteLine("Coordinate taken! Try again")
                Threading.Thread.Sleep(1000)
                Console.ReadLine()
            ElseIf Column < 1 Or Column > 7 Then
                Console.WriteLine("Out of Bounds! Try again")
                Threading.Thread.Sleep(1000)
                Console.ReadLine()
            Else
                Exit While
            End If

        End While

        'After input passes valdation, place on board
        Board(0, Column) = Symbol

        'If Board(0, Column) Then

    End Sub


End Module
