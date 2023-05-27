Module Module1
    Structure Item
        Dim ItemID As Integer
        <VBFixedString(20)> Dim Name As String
        Dim Price As Integer
    End Structure
    Sub Main()
        Dim choice As Integer
        Dim temp As Integer
        Dim tempflag As Boolean
        Dim newrec As Item
        Dim searchID As Integer
        Console.WriteLine("1 to Add")
        Console.WriteLine("2 to print all")
        Console.WriteLine("3 to Search by Name")
        Console.WriteLine("4 to search by ID")
        Console.WriteLine("5 to delete")
        Console.WriteLine("6 to print No of items")
        Console.WriteLine("7 to modify")
        Console.WriteLine("8 to quit")
        Do
            Console.WriteLine("enter choice")
            choice = Console.ReadLine
            Select Case choice
                Case Is = 1
                    Console.WriteLine("Enter item ID:")
                    newrec.ItemID = Console.ReadLine
                    Console.WriteLine("Enter item name:")
                    newrec.Name = Console.ReadLine
                    Console.WriteLine("Enter item price:")
                    newrec.Price = Console.ReadLine
                    Call AddRecord(newrec)
                Case Is = 2
                    Call PrintAll()
                Case Is = 3
                    Call SearchByName()
                Case Is = 4
                    Console.WriteLine("enter ID to search")
                    SearchID = Console.ReadLine
                    tempflag = SearchByID(searchID)
                    If tempflag Then
                        Console.WriteLine("item found")
                    Else
                        Console.WriteLine("item not found")
                    End If
                Case Is = 5
                    Call Delete()
                Case Is = 6
                    temp = PrintItemsNumber()
                    Console.WriteLine("Number of items are : " & temp)
                Case Is = 7
                    Call Modify()
            End Select
        Loop Until choice = 8
    End Sub
    Sub Modify()
        Dim modifyID As Integer
        Dim tempflag As Boolean
        Dim temprec As Item
        Dim NewName As String
        Dim NewPrice As Integer
        
        Console.WriteLine("enter item ID to modify")
        modifyID = Console.ReadLine
        tempflag = SearchByID(modifyID)
        
        If Not tempflag Then
            Console.WriteLine("ID not found")
        Else
            FileOpen(1, "ItemFile", OpenMode.Binary)
            FileOpen(2, "TempFile", OpenMode.Binary)
            While Not EOF(1)
                FileGet(1, temprec)
                If LOF(2) > 0 Then
                    Seek(2, LOF(2) + 1)
                End If
                NewName = temprec.Name
                NewPrice = temprec.Price
                If temprec.ItemID = modifyID Then
                    Console.WriteLine("enter new name:")
                    NewName = Console.ReadLine
                    Console.WriteLine("enter new price:")
                    NewPrice = Console.ReadLine
                End If
                temprec.Name = NewName
                temprec.Price = NewPrice
                FilePut(2, temprec)
            End While
            FileClose(1)
            Kill("ItemFile")
            FileClose(2)
            Rename("TempFile", "ItemFile")
        End If
    End Sub
    Sub Delete()
        Dim tempflag As Boolean
        Dim tempRec As Item
        Dim deleteID As Integer
        Console.WriteLine("enter Item ID you want to delete")
        deleteID = Console.ReadLine
        tempflag = SearchByID(deleteID)
        
        If tempflag = False Then
            Console.WriteLine("Error! ID not found")
        Else
            FileOpen(1, "ItemFile", OpenMode.Binary)
            FileOpen(2, "TempFile", OpenMode.Binary)
            While Not EOF(1)
                FileGet(1, tempRec)
                If LOF(2) > 0 Then
                    Seek(2, LOF(2) + 1)
                End If
                If tempRec.ItemID <> deleteID Then
                    FilePut(2, tempRec)
                End If
            End While
            FileClose(1)
            Kill("ItemFile")                                            'deletes file
            FileClose(2)
            Rename("TempFile", "ItemFile")                              'renames file
        End If

    End Sub
    Function PrintItemsNumber() As Integer
        Dim total As Integer
        Dim temprec As Item
        FileOpen(1, "ItemFile", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, temprec)
            total = total + 1
        End While
        FileClose(1)
        Return (total)
    End Function
    Sub SearchByName()
        Dim TempRec As Item
        Dim NameToSearch As String
        Dim isFound As Boolean = False
        FileOpen(1, "ItemFile", OpenMode.Binary)
        Console.WriteLine("enter name to search:")
        NameToSearch = Console.ReadLine
        While Not EOF(1)
            FileGet(1, TempRec)
            TempRec.Name = RTrim(TempRec.Name)      '*removes all the spaces on the right
            If NameToSearch = TempRec.Name Then
                isFound = True
                Console.WriteLine(TempRec.ItemID & " " & TempRec.Price)
            End If
        End While
        FileClose(1)
        If Not isFound Then
            Console.WriteLine("Name not found")
        End If
    End Sub
    Function SearchByID(ByVal SearchID As Integer) As Boolean
        Dim temprec As Item
        Dim returnval As Boolean = False

        FileOpen(1, "ItemFile", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, temprec)
            If temprec.ItemID = SearchID Then
                Console.WriteLine(temprec.Name & " " & temprec.Price)
                returnval = True
            End If
        End While
        FileClose(1)
        Return returnval
    End Function
    Sub PrintAll()
        Dim TempRec As Item
        FileOpen(1, "ItemFile", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, TempRec)                   'picks up data from file and puts in a temporary place and moves pointer forward
            Console.WriteLine(TempRec.ItemID & " " & TempRec.Name & " " & TempRec.Price)
        End While
        FileClose(1)
    End Sub
    Sub AddRecord(ByVal i As Item)
        FileOpen(1, "ItemFile", OpenMode.Binary)
        If LOF(1) > 0 Then
            Seek(1, LOF(1) + 1)                   'points to next available space in file
        End If
        FilePut(1, i)                             'puts i in file
        FileClose(1)
    End Sub
End Module
