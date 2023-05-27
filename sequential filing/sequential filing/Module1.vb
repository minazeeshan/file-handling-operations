Module Module1
    Structure Item
        Dim ItemCode As Integer
        Dim ItemName As String
        Dim UnitPrice As Decimal
        Dim QuantityInStock As Integer
    End Structure
    Sub Main()
        Dim choice As Integer
        Dim tempFlag As Boolean
        Dim tempInt As Integer
        Console.WriteLine("1 to print all items")
        Console.WriteLine("2 to add an item")
        Console.WriteLine("3 to conduct a sale")
        Console.WriteLine("4 to search by item code")
        Console.WriteLine("5 to delete an item")
        Console.WriteLine("6 to incoming stock")
        Console.WriteLine("7 to quit")
        Do
            Console.WriteLine("enter choice")
            choice = Console.ReadLine
            Select Case choice
                Case Is = 1
                    Call PrintAll()
                Case Is = 2
                    Call AddItem()
                Case Is = 3
                    Call ConductSale()
                Case Is = 4
                    Console.WriteLine("enter item code to search")
                    tempInt = Console.ReadLine
                    tempFlag = SearchByCode(tempInt)
                    If Not tempFlag Then
                        Console.WriteLine("item not found")
                    End If
                Case Is = 5
                    Console.WriteLine("enter code of the item you wish to delete:")
                    tempInt = Console.ReadLine
                    Call DeleteItem(tempInt)
                Case Is = 6
                    Call incomingStock()
            End Select
        Loop Until choice = 7
        Console.ReadKey()
    End Sub
    Sub AddItem()
        Dim itemrec As Item
        Dim temprec As Item
        Dim inserted As Boolean = False
        Console.WriteLine("enter code:")
        itemrec.ItemCode = Console.ReadLine
        Console.WriteLine("enter name:")
        itemrec.ItemName = Console.ReadLine
        Console.WriteLine("enter quantity left in stock")
        itemrec.QuantityInStock = Console.ReadLine
        Console.WriteLine("enter unit price:")
        itemrec.UnitPrice = Console.ReadLine

        FileOpen(1, "Items", OpenMode.Binary)
        FileOpen(2, "tempfile", OpenMode.Binary)


        If LOF(1) = 0 Then
            FilePut(2, itemrec)
        Else
            While Not EOF(1)
                FileGet(1, temprec)
                If temprec.ItemCode < itemrec.ItemCode Then
                    FilePut(2, temprec)
                Else
                    If Not inserted Then
                        FilePut(2, itemrec)
                        FilePut(2, temprec)
                        inserted = True
                    Else
                        FilePut(2, temprec)
                    End If
                End If
            End While
        End If
        If Not inserted Then
            FilePut(2, itemrec)
        End If

        FileClose(1)
        Kill("Items")
        FileClose(2)
        Rename("tempfile", "Items")
    End Sub
    Sub incomingStock()
        Dim temprec As Item
        Dim code As Integer
        Dim qty As Integer
        Dim isfound As Boolean = False
        Console.WriteLine("enter item that has arrived : ")
        code = Console.ReadLine
        Console.WriteLine("how much stock has arrived?")
        qty = Console.ReadLine
        FileOpen(1, "Items", OpenMode.Binary)
        FileOpen(2, "tempfile", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, temprec)
            If temprec.ItemCode = code Then
                isfound = True
                temprec.QuantityInStock = temprec.QuantityInStock + qty
            End If
            FilePut(2, temprec)
        End While
        If Not isfound Then
            Console.WriteLine("item not found")
        End If
        FileClose(1)
        Kill("Items")
        FileClose(2)
        Rename("tempfile", "Items")
    End Sub
    Sub ConductSale()
        Dim code As Integer
        Dim total As Decimal
        Dim qty As Integer
        Dim temprec As Item
        Dim thisRecord As Item
        Dim tempflag As Boolean = False
        FileOpen(1, "Items", OpenMode.Binary)

        Console.WriteLine("enter item code to purchase:")
        code = Console.ReadLine
        While Not EOF(1) AndAlso Not tempflag
            FileGet(1, temprec)
            If temprec.ItemCode = code Then
                Console.WriteLine(temprec.ItemName & " " & temprec.QuantityInStock & " " & temprec.UnitPrice)
                thisRecord = temprec
                tempflag = True
            End If
        End While

        If Not tempflag Then
            Console.WriteLine("item not found")
        Else
            Console.WriteLine("enter quantity to purchase:")
            qty = Console.ReadLine
            While qty > thisRecord.QuantityInStock
                Console.WriteLine("not enough in stock, quantity in stock is : " & thisRecord.QuantityInStock & " , so please re-enter quantity to purchase:")
                qty = Console.ReadLine
            End While
            total = qty * thisRecord.UnitPrice
            Console.WriteLine("your total is : " & total)

        End If
        FileClose(1)
        Call UpdateDailyStock(code, qty)
    End Sub
    Sub UpdateDailyStock(ByVal code As Integer, ByVal quantityPurchased As Integer)
        Dim temprec As Item
        FileOpen(1, "Items", OpenMode.Binary)
        FileOpen(2, "tempfile", OpenMode.Binary)

        While Not EOF(1)
            FileGet(1, temprec)
            If temprec.ItemCode = code Then
                temprec.QuantityInStock = temprec.QuantityInStock - quantityPurchased
            End If
            FilePut(2, temprec)
        End While

        FileClose(1)
        Kill("Items")
        FileClose(2)
        Rename("tempfile", "Items")
    End Sub
    Sub DeleteItem(ByVal code As Integer)
        Dim temprec As Item
        Dim tempflag As Boolean

        tempflag = SearchByCode(code)

        If Not tempflag Then
            Console.WriteLine("item not found")
        Else
            FileOpen(1, "Items", OpenMode.Binary)
            FileOpen(2, "tempfile", OpenMode.Binary)
            While Not EOF(1)
                FileGet(1, temprec)
                If temprec.ItemCode <> code Then
                    FilePut(2, temprec)
                End If
            End While
            FileClose(1)
            Kill("Items")
            FileClose(2)
            Rename("tempfile", "Items")
        End If
    End Sub
    Function SearchByCode(ByVal code As Integer) As Boolean
        Dim returnval As Boolean = False
        Dim temprec As Item
        FileOpen(1, "Items", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, temprec)
            If temprec.ItemCode = code Then
                Console.WriteLine(temprec.ItemName & " " & temprec.QuantityInStock & " " & temprec.UnitPrice)
                returnval = True
            End If
        End While
        FileClose(1)
        Return returnval
    End Function
    Sub PrintAll()
        Dim temprec As Item
        FileOpen(1, "Items", OpenMode.Binary)
        While Not EOF(1)
            FileGet(1, temprec)
            Call PrintRecord(temprec)
        End While
        FileClose(1)
    End Sub
    Sub PrintRecord(ByVal tempRecord As Item)
        Console.WriteLine(tempRecord.ItemCode & " " & tempRecord.ItemName & " " & tempRecord.QuantityInStock & " " & tempRecord.UnitPrice)
    End Sub
End Module
