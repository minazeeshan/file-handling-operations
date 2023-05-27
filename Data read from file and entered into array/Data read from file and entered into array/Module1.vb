Module Module1

    Sub Main()
        Dim fileLine(0 To 2) As String
        Dim hashPos As Integer
        Dim index As Integer
        Dim NamesArray(0 To 2) As String
        Dim IDArray(0 To 2) As String
        hashPos = 0
        index = 0
        FileOpen(1, "ArrayToFile.txt", OpenMode.Input)
        While Not EOF(1) Or index <= 2
            fileLine(index) = LineInput(1)
            index = index + 1
        End While
        For i = 0 To 2
            hashPos = InStr(fileLine(i), "*")
            NamesArray(i) = Left(fileLine(i), hashPos - 1)
            IDArray(i) = Right(fileLine(i), Len(fileLine(i)) - hashPos)
        Next
        FileClose(1)
        For c = 0 To 2
            Console.WriteLine(NamesArray(c))
            Console.WriteLine(IDArray(c))
        Next
        Console.ReadKey()
    End Sub

End Module
