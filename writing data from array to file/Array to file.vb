Module Module1

    Sub Main()
        Dim NamesArray(0 To 2) As String
        Dim IDArray(0 To 2) As String
        Dim fileLine As String
        FileOpen(1, "ArrayToFile.txt", OpenMode.Output)
        For i = 1 To 3
            Console.WriteLine("Input name no. " & i & ":")
            NamesArray(i - 1) = Console.ReadLine
            Console.WriteLine("Input ID no. " & i & ":")
            IDArray(i - 1) = Console.ReadLine
        Next
        'combining name and ID into a single string and then storing it in the file

        For c = 0 To 2
            fileLine = NamesArray(c) & "*" & IDArray(c)
            PrintLine(1, fileLine)
        Next
        FileClose(1)
        Console.ReadKey()


    End Sub

End Module
